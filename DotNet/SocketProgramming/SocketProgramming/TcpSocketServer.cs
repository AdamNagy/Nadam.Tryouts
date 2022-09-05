using Newtonsoft.Json;
using SocketServer;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming
{
    internal class TcpSocketServer
    {
        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _server;

        private readonly ConcurrentDictionary<string, Socket> _clients;
        private readonly object _locker = new object();

        public TcpSocketServer()
        {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses

            _ipHostInfo = Dns.GetHostEntry("localhost");
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 11000);

            _server = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            _clients = new ConcurrentDictionary<string, Socket>();
        }

        public void StartServer()
        {
            try
            {
                _server.Bind(_localEndPoint);
                _server.Listen(100);

                Console.WriteLine("Waiting for a connection...");

                // miért tud csatlakozni a 4. kliens is, de nem jelenik meg az üzenet?
                // connection vs listening
                for (int i = 0; i < 3; i++)
                {
                    Task.Run(() => Robot());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        private void Robot()
        {
            var handler = _server.Accept();
            var clientId = Guid.NewGuid().ToString();

            lock (_locker)
            {
                _clients.TryAdd(clientId, handler);
            }

            while (true)
            {
                var buffer = new byte[1024];
                var bytesRec = handler.Receive(buffer);

                var data = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                var message = JsonConvert.DeserializeObject<WebMessage>(data);
                
                switch(message.Type)
                {
                    case MessageType.checkin:
                        handler.Send(Encoding.ASCII.GetBytes(clientId));
                        break;
                    case MessageType.checkout:
                        {
                            lock(_locker)
                            {
                                _clients.Remove(clientId, out _);                                
                            }
                            break;
                        }
                    case MessageType.message:
                        {
                            Broadcast(message.Recipiants, message.Text);
                            break;
                        }
                }

                if (data.Contains("exit"))
                    break;
            }

            // var msg = Encoding.ASCII.GetBytes(data);
            // handler.Send(msg);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        private void Broadcast(IEnumerable<string> clients, string message)
        {
            if(clients.First() == "*")
            {
                lock (_locker)
                {
                    foreach (var client in _clients)
                    {
                        client.Value.Send(Encoding.ASCII.GetBytes(message));
                    }
                }
            }
            else
            {
                foreach (var clientId in clients)
                {
                    if(!_clients.ContainsKey(clientId))
                        continue;

                    var client = _clients[clientId];
                    client.Send(Encoding.ASCII.GetBytes(message));
                }
            }

        }
    }
}
