// See https://aka.ms/new-console-template for more information
using SocketProgramming;

Console.WriteLine("Tcp Socket Server\nStarting server");

var tcpServer = new TcpSocketServer();
tcpServer.StartServer();

