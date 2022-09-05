using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsyncSocketClient
{
    public class AsynchronousClient : IDisposable
    {
        // The port number for the remote device.  
        private const int port = 81;

        // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);

        private ManualResetEvent sendDone =
            new ManualResetEvent(false);

        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private String response = String.Empty;

        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _remoteEP;
        private readonly Socket _client;

        public AsynchronousClient()
        {
            // Establish the remote endpoint for the socket.  
            // The name of the
            // remote device is "host.contoso.com".  
            _ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            _ipAddress = _ipHostInfo.AddressList[0];
            _remoteEP = new IPEndPoint(_ipAddress, port);

            // Create a TCP/IP socket.  
            _client = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Connect to the remote endpoint.  
                _client.BeginConnect(_remoteEP,
                    new AsyncCallback(ConnectCallback),
                    _client);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                Send("This is a test<EOF>");
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(_client);
                receiveDone.WaitOne();

                // Write the response to the console.  
                Console.WriteLine("Response received : {0}", response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        public void Send(String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            _client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), _client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();

                Receive(_client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Dispose()
        {
            // Release the socket.  
            _client.Shutdown(SocketShutdown.Both);
            _client.Close();
        }
    }
}
