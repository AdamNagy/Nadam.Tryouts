using Newtonsoft.Json;
using SocketServer;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class TcpSocketClient
    {
        private readonly IPHostEntry _ipHostInfo;
        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _sender;

        public TcpSocketClient()
        {

            _ipHostInfo = Dns.GetHostEntry("localhost");
            _ipAddress = _ipHostInfo.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, 11000);

            _sender = new Socket(_ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartClient()
        {
            var bytes = new byte[1024];

            try
            {
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    _sender.Connect(_localEndPoint);

                    Console.WriteLine("Socket connected to {0}", _sender.RemoteEndPoint.ToString());
                    Console.WriteLine("Checking in...");

                    var checkinMessage = new WebMessage();
                    checkinMessage.Type = MessageType.checkin;
                    var messageBytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(checkinMessage));
                    _sender.Send(messageBytes);

                    var bytesRec = _sender.Receive(bytes);
                    Console.WriteLine("Your id: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;
                    Task.Run(() =>
                    {
                        while(true)
                        {
                            var receiveBuffer = new byte[1024];

                            var numOfReceived = _sender.Receive(receiveBuffer);

                            Console.WriteLine("Echoed test = {0}",
                                Encoding.ASCII.GetString(receiveBuffer, 0, numOfReceived));
                        }
                    }, token);

                    Console.WriteLine($"> Type message:");
                    var message = Console.ReadLine();
                    while(message.ToLower() != "exit")
                    {
                        var messageObj = new WebMessage();
                        messageObj.Type = MessageType.message;
                        messageObj.Text = message;
                        messageObj.Recipiants = new List<string>() { "*" };
                        var messageBytesToSend = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(messageObj));

                        // Send the data through the socket.
                        var bytesSent = _sender.Send(messageBytesToSend);
                        message = Console.ReadLine();
                    }

                    tokenSource.Cancel();
                    // send checkout
                    var checkoutMessage = new WebMessage();
                    checkoutMessage.Type = MessageType.checkout;
                    var checkoutMessageBytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(checkoutMessage));
                    _sender.Send(checkoutMessageBytes);

                    // Release the socket.
                    _sender.Shutdown(SocketShutdown.Both);
                    _sender.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
