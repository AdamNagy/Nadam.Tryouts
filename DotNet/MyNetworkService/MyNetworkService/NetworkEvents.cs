namespace MyNetworkService
{
    public class ClientConnectedEvent : IAppEvent
    {
        public string Name => "Client-Connected";
        public string ClientId { get; set; }

        public ClientConnectedEvent(string clientId)
        {
            ClientId = clientId;
        }
    }

    public class MessageArrivedEvent : IAppEvent
    {
        public string Name => "Message-Arrived";
        public string ClientId { get; private set; }
        public string Message { get; private set; }

        public MessageArrivedEvent(string clientId, string message)
        {
            ClientId = clientId;
            Message = message;
        }
    }
}
