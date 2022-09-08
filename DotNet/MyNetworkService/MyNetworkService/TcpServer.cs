using MyNetworkService.EventInfrastructure.Contracts;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyNetworkService
{
    public class TcpServer
    {
        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _server;

        private readonly ConcurrentDictionary<string, TcpConnectedClient> _clients;

        private readonly object _locker = new object();
        private readonly IEventBus _eventBus;

        public TcpServer(IEventBus eventBus)
        {
            _ipHostInfo = Dns.GetHostEntry("localhost");
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 11000);

            _server = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            _clients = new ConcurrentDictionary<string, TcpConnectedClient>();
            _eventBus = eventBus;
        }

        public void Start()
        {
            _server.Bind(_localEndPoint);
            _server.Listen(100);

            Accept();
        }

        private void Accept()
        {
            try
            {
                _server.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    _server);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = _server.EndAccept(ar);
                var clientId = Guid.NewGuid().ToString();

                lock(_locker)
                {

                    var tcpCLienthandler = new TcpConnectedClient();
                    tcpCLienthandler.ClientId = clientId;
                    tcpCLienthandler.Buffer = new byte[1024];
                    tcpCLienthandler.Offset = 0;
                    tcpCLienthandler.Handler = handler;

                    _clients.TryAdd(clientId, tcpCLienthandler);
                    handler.BeginReceive(tcpCLienthandler.Buffer, tcpCLienthandler.Offset, tcpCLienthandler.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), tcpCLienthandler);

                    Accept();
                    _eventBus.Publish(new ClientConnectedEvent(clientId));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (TcpConnectedClient)ar.AsyncState;

                int bytesRead = _server.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.Data = state.Data + Encoding.ASCII.GetString(
                        state.Buffer, 0, bytesRead);

                    _eventBus.Publish(new MessageArrivedEvent(state.ClientId, state.Data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
        public void SendMessage(string clientId, string message)
        {
            try
            {
                lock(_locker)
                {
                    if (!_clients.TryGetValue(clientId, out var client))
                        return;

                    byte[] byteData = Encoding.UTF8.GetBytes(message);

                    client.Handler.BeginSend(byteData, 0, byteData.Length, 0,
                        new AsyncCallback(SendCallback), client);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendMessage(IEnumerable<string> clientIds, string message)
        {
            try
            {
                lock (_locker)
                {
                    foreach (var clientId in clientIds)
                    {
                        if (!_clients.TryGetValue(clientId, out var client))
                            continue;

                        byte[] byteData = Encoding.UTF8.GetBytes(message);

                        client.Handler.BeginSend(byteData, 0, byteData.Length, 0,
                            new AsyncCallback(SendCallback), client);
                    }
                }
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
                lock (_locker)
                {
                    foreach (var client in _clients.Values)
                    {
                        byte[] byteData = Encoding.UTF8.GetBytes(message);

                        client.Handler.BeginSend(byteData, 0, byteData.Length, 0,
                            new AsyncCallback(SendCallback), client);
                    }
                }
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
                var client = (TcpConnectedClient)ar.AsyncState;

                int bytesSent = client.Handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
