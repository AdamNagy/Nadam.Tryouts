namespace MyNetworkService
{
    public class SocketMessage
    {
        public int BufferSize { get; private set; }
        public byte[] Buffer { get; private set; }
        public int Offset { get; set; }
        public string Data { get; set; }

        public SocketMessage(int buffer, int offset = 0)
        {
            BufferSize = buffer;
            Buffer = new byte[BufferSize];
            Offset = offset;
        }
    }
}
