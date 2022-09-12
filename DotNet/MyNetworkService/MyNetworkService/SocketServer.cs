using System.Net;
using System.Net.Sockets;
using MyNetworkService.EventInfrastructure.Contracts;

namespace MyNetworkService
{
    public class SocketServer
    {
        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _server;
        private readonly IEventBus _eventBus;

        public SocketServer(IEventBus eventBus)
        {
            _ipHostInfo = Dns.GetHostEntry("localhost");
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 11000);

            _server = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

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
                Accept();

                var clientId = Guid.NewGuid().ToString();
                var socketClient = new SocketClient(handler, clientId, _eventBus);
                
                _eventBus.Publish(new ClientConnectedEvent(socketClient));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
