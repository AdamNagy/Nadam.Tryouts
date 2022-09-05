// See https://aka.ms/new-console-template for more information
using SocketClient;

Console.WriteLine("Tcp Socket Client\nStarting client app");

var client = new TcpSocketClient();
client.StartClient();