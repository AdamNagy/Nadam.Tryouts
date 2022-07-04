using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsyncSocketServer
{
    public class AsynchronousSocketServer : IDisposable
    {
        // Thread signal.  
        public ManualResetEvent allDone = new ManualResetEvent(false);
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);

        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _server;

        public AsynchronousSocketServer()
        {
            // Establish the local endpoint for the socket.
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".
            _ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 81);

            // Create a TCP/IP socket.  
            _server = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListening()
        {
            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                _server.Bind(_localEndPoint);
                _server.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();
                    //connectDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    _server.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        _server);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            // Socket server = (Socket)ar.AsyncState;
            Socket handler = _server.EndAccept(ar);
            // connectDone.Set();

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;

            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            // Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = _server.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    // Echo the data back to the client.  
                    Send(state.workSocket, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    _server.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Dispose()
        {

            _server.Shutdown(SocketShutdown.Both);
            _server.Close();
        }
    }
}
