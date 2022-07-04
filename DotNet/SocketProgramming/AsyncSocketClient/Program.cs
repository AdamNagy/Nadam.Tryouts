// See https://aka.ms/new-console-template for more information
using AsyncSocketClient;

Console.WriteLine("This is the client.\n Press any key to start client");

var tcpClient = new AsynchronousClient();
Console.ReadKey();
Task.Run(() => tcpClient.StartClient());

var input = Console.ReadLine();
while(input.ToLower() != "exit")
{
    tcpClient.Send(input);
    input = Console.ReadLine();
}

return 0;
