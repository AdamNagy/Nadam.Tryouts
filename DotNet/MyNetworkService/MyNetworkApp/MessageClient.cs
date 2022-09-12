using MyNetworkService;
using MyNetworkService.EventInfrastructure.Contracts;
using Newtonsoft.Json;

namespace MyNetworkApp
{
    internal class MessageClient
    {
        private readonly TcpClient _client;
        private readonly IEventBus _eventBus;
        public string Name { get; set; }

        public MessageClient(TcpClient client, IEventBus eventBus)
        {
            _client = client;
            _eventBus = eventBus;
        }

        public void Start()
        {
            try
            {
                _eventBus.Subscribe<MessageArrivedEvent>((payload) =>
                {
                    Console.WriteLine($"Message Arrived: {payload.Message}");
                    var message = JsonConvert.DeserializeObject<WebMessage>(payload.Message);
                    Console.WriteLine(message.Data);
                });

                _client.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Checkin(string name)
        {
            try
            {
                var checkinMessage = new WebMessage();
                checkinMessage.Type = MessageType.checkin;
                checkinMessage.Data = name;
                Name = name;

                var message = JsonConvert.SerializeObject(checkinMessage);
                _client.SendMessage(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Checkout()
        {
            try
            {
                var checkinMessage = new WebMessage();
                checkinMessage.Type = MessageType.checkout;

                var message = JsonConvert.SerializeObject(checkinMessage);
                _client.SendMessage(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    
        public void SendMessage(string message)
        {
            var messageObj = new WebMessage();
            messageObj.Type = MessageType.message;
            messageObj.Data = $"{Name}:{message}";

            var messageText = JsonConvert.SerializeObject(messageObj);

            _client.SendMessage(messageText);
        }
    }
}
