using MyNetworkService.EventInfrastructure.Contracts;

namespace MyNetworkService
{
    public class ClientConnectedEvent : IEvent
    {
        public string ClientId { get; set; }
        public string Id { get => "Client-Connected"; }

        public ClientConnectedEvent(string clientId)
        {
            ClientId = clientId;
        }
    }

    public class MessageArrivedEvent : IEvent
    {
        public string Id => "Message-Arrived";
        public string ClientId { get; private set; }
        public string Message { get; private set; }

        public MessageArrivedEvent(string clientId, string message)
        {
            ClientId = clientId;
            Message = message;
        }
    }
}
