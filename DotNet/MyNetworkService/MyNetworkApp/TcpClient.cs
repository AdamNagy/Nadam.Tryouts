using System.Net;
using System.Net.Sockets;
using System.Text;
using MyNetworkService;
using MyNetworkService.EventInfrastructure.Contracts;

namespace MyNetworkApp
{
    internal class TcpClient
    {
        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _clientSocket;
        private readonly IEventBus _eventBus;

        public TcpClient(IEventBus eventBus)
        {

            _ipHostInfo = Dns.GetHostEntry("localhost");
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 11000);

            _clientSocket = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            _eventBus = eventBus;
        }

        public void Connect()
        {
            try
            {
                _clientSocket.Connect(_localEndPoint);
                Listen();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Disconnect()
        {
            try
            {
                _clientSocket.Disconnect(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(message);

                _clientSocket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), _clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Listen()
        {
            try
            {
                var clientMessage = new SocketMessage(128);

                _clientSocket.BeginReceive(clientMessage.Buffer, clientMessage.Offset, clientMessage.BufferSize, 0,
                     new AsyncCallback(ReceiveCallback), clientMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var clientMessage = (SocketMessage)ar.AsyncState;
                int bytesRead = _clientSocket.EndReceive(ar);
                Listen();

                clientMessage.Data = Encoding.UTF8.GetString(clientMessage.Buffer, 0, bytesRead);

                if (bytesRead < _clientSocket.Available)
                {
                    var leftBuffer = new byte[_clientSocket.Available - bytesRead];
                    var leftBufferRed = _clientSocket.Receive(leftBuffer, bytesRead, leftBuffer.Length, SocketFlags.None);

                    var leftMessage = Encoding.UTF8.GetString(leftBuffer, 0, leftBufferRed);
                    clientMessage.Data = clientMessage.Data + leftMessage;
                }

                _eventBus.Publish<MessageArrivedEvent>(new MessageArrivedEvent("", clientMessage.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
