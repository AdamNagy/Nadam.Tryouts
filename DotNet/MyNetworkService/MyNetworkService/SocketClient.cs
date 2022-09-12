using MyNetworkService.EventInfrastructure.Contracts;
using System.Net.Sockets;
using System.Text;

namespace MyNetworkService
{
    public class SocketClient
    {
        private readonly IEventBus _eventBus;

        public string ClientId { get; set; }
        public Socket Client { get; set; }
        public IList<SocketMessage> Messages { get; private set; }
        public SocketMessage CurrentMessage { get => Messages.Last(); }

        public SocketClient(Socket client, string clientId, IEventBus eventBus)
        {
            ClientId = clientId;
            Client = client;
            Messages = new List<SocketMessage>();
            _eventBus = eventBus;

            BeginReceive();
        }

        public void BeginReceive()
        {
            var newMessage = new SocketMessage(32);
            Messages.Add(newMessage);
            Client.BeginReceive(newMessage.Buffer, newMessage.Offset, newMessage.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), null);
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = Client.EndReceive(ar);

                CurrentMessage.Data = Encoding.UTF8.GetString(CurrentMessage.Buffer, 0, bytesRead);

                if (Client.Available > 0)
                {
                    var leftBuffer = new byte[Client.Available];
                    var leftBufferRed = Client.Receive(leftBuffer, 0, leftBuffer.Length, SocketFlags.None);

                    var leftMessage = Encoding.UTF8.GetString(leftBuffer, 0, leftBufferRed);
                    CurrentMessage.Data = CurrentMessage.Data + leftMessage;
                }
                _eventBus.Publish(new MessageArrivedEvent(ClientId, CurrentMessage.Data));

                BeginReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(message);

                Client.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                int bytesSent = Client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
