using MyNetworkService.EventInfrastructure.Contracts;

namespace MyNetworkService
{
    internal class MessageServer
    {
        private readonly TcpServer _server;
        private readonly IEventBus _eventBus;

        public MessageServer(TcpServer server, IEventBus eventBus)
        {
            _server = server;
            _eventBus = eventBus;
        }

        public void Strart()
        {
            try
            {
                _eventBus.Subscribe<ClientConnectedEvent>((payload) =>
                {
                    Console.WriteLine($"{payload.ClientId} has connected");
                    SendConfirmationMessage(payload.ClientId);
                });

                _eventBus.Subscribe<MessageArrivedEvent>((payload) =>
                {
                    Console.WriteLine($"{payload.ClientId} has sent message: {payload.Message}");
                });

                _server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendConfirmationMessage(string clientId)
        {
            var message = new WebMessage(MessageType.confirm, $"{{'id' = '{clientId}'}}");
            _server.SendMessage(clientId, message.ToString());
        }
    }
}
