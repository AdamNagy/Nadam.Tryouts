using System.Net.Sockets;

namespace MyNetworkService
{
    public class TcpClient
    {
        public string ClientId { get; set; }
        public Socket Handler { get; set; }
        public int BufferSize { get; set; } = 1024;
        public byte[] Buffer { get; set; }
        public int Offset { get; set; }
        public string Data { get; set; }
    }
}
