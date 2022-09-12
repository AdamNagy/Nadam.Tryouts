using MyNetworkService.EventInfrastructure.Contracts;
using System.Collections.Concurrent;
using System.Text;

namespace MyNetworkService
{
    public class MessageBroker
    {
        private readonly ConcurrentDictionary<string, SocketClient> _clients;
        private readonly object _locker = new object();
        private readonly IEventBus _eventBus;

        public MessageBroker(IEventBus eventBus)
        {
            _clients = new ConcurrentDictionary<string, SocketClient>();
            _eventBus = eventBus;

            _eventBus.Subscribe<ClientConnectedEvent>((payload) =>
            {
                lock (_locker)
                {
                    _clients.TryAdd(payload.Client.ClientId, payload.Client);
                }
            });
        }

        public void SendMessage(string message)
        {
            try
            {
                lock( _locker)
                {
                    foreach (var client in _clients.Values)
                    {
                        byte[] byteData = Encoding.UTF8.GetBytes(message);
                        client.SendMessage(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendMessage(string clientId, string message)
        {
            if (!_clients.ContainsKey(clientId))
                return;

            _clients[clientId].SendMessage(message);
        }
    }
}
