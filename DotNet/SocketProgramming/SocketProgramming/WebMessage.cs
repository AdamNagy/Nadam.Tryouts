namespace SocketServer
{
    [Serializable]
    public class WebMessage
    {
        public MessageType Type { get; set; }
        public IEnumerable<string> Recipiants { get; set; }
        public string Text { get; set; }
    }

    public enum MessageType
    {
        checkin, checkout, message
    }
}
