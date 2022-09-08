using System.Net.Sockets;

namespace MyNetworkService
{
    public class TcpConnectedClient
    {
        public string ClientId { get; set; }
        public Socket Handler { get; set; }
        public int BufferSize { get; set; } = 1024;
        public byte[] Buffer { get; set; }
        public int Offset { get; set; }
        public string Data { get; set; }

        public static TcpConnectedClient Default()
        {
            var cl = new TcpConnectedClient();
            cl.BufferSize = 1024;
            cl.Offset = 0;
            cl.Buffer = new byte[cl.BufferSize];
            cl.Data = "";

            return cl;
        }
    }
}
