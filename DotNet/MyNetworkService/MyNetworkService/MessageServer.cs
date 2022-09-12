using MyNetworkService.EventInfrastructure.Contracts;
using Newtonsoft.Json;

namespace MyNetworkService
{
    internal class MessageServer
    {
        private readonly IEventBus _eventBus;
        private readonly MessageBroker _broker;

        public MessageServer(IEventBus eventBus, MessageBroker broker)
        {
            _eventBus = eventBus;
            _broker = broker;
        }

        public void Strart()
        {
            try
            {
                _eventBus.Subscribe<ClientConnectedEvent>((payload) =>
                {
                    Console.WriteLine($"{payload.Client.ClientId} has connected");
                    SendConfirmationMessage(payload.Client.ClientId);
                });

                _eventBus.Subscribe<MessageArrivedEvent>((payload) =>
                {
                    Console.WriteLine($"{payload.ClientId} has sent message: {payload.Message}");
                    var message = JsonConvert.DeserializeObject<WebMessage>(payload.Message);

                    switch(message.Type)
                    {
                        case MessageType.checkin:
                            _broker.SendMessage(CreateMessage($"{message.Data} has checked in"));
                            break;
                        case MessageType.checkout:
                            _broker.SendMessage(CreateMessage($"{message.Data} has left"));
                            break;
                        case MessageType.message:
                            _broker.SendMessage(CreateMessage(message.Data));
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendConfirmationMessage(string clientId)
        {
            var message = new WebMessage(MessageType.confirm, $"{{'id' = '{clientId}'}}");
            _broker.SendMessage(clientId, message.ToString());
        }

        private string CreateMessage(string message, MessageType type = MessageType.message)
        {
            var messageObj = new WebMessage(type, message);
            return messageObj.ToString();
        }        
    }
}
