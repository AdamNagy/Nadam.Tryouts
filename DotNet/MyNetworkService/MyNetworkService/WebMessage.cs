using Newtonsoft.Json;

namespace MyNetworkService
{
    public class WebMessage
    {
        public MessageType Type { get; set; }
        public IEnumerable<string> Recipiants { get; set; }
        public string Data { get; set; }

        public WebMessage()
        {

        }

        public WebMessage(MessageType type, string data = "")
        {
            Type = type;
            Data = data;
        }

        public override string ToString()
        {
            var message = JsonConvert.SerializeObject(this);
            return message;
        }
    }

    public enum MessageType
    {
        checkin, checkout, message, confirm
    }
}
