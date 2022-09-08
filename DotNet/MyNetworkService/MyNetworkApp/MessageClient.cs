using MyNetworkService;
using MyNetworkService.EventInfrastructure.Contracts;
using Newtonsoft.Json;

namespace MyNetworkApp
{
    internal class MessageClient
    {
        private readonly TcpClient _client;
        private readonly IEventBus _eventBus;

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
                });

                _client.Connect();
                _client.Listen();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Checkin()
        {
            try
            {
                var checkinMessage = new WebMessage();
                checkinMessage.Type = MessageType.checkin;

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
    }
}
