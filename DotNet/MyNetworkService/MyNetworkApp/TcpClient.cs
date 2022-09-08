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
        private readonly Socket _client;
        private readonly IEventBus _eventBus;

        public TcpClient(IEventBus eventBus)
        {

            _ipHostInfo = Dns.GetHostEntry("localhost");
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 11000);

            _client = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            _eventBus = eventBus;
        }

        public void Connect()
        {
            try
            {
                _client.Connect(_localEndPoint);
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
                _client.Disconnect(false);
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

                _client.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), _client);
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

        public void Listen()
        {
            try
            {
                var tcpCLienthandler = TcpConnectedClient.Default();
                _client.BeginReceive(tcpCLienthandler.Buffer, tcpCLienthandler.Offset, tcpCLienthandler.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), tcpCLienthandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (TcpConnectedClient)ar.AsyncState;

                int bytesRead = _client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.Data = state.Data + Encoding.ASCII.GetString(
                        state.Buffer, 0, bytesRead);

                    _eventBus.Publish(new MessageArrivedEvent(state.ClientId, state.Data));
                    Listen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
