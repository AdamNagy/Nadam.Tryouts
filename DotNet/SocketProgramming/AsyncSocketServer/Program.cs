using AsyncSocketServer;

Console.WriteLine("This is the Server.");

var server = new AsynchronousSocketServer();
Task.Run(() => server.StartListening());

var input = Console.ReadLine();
while(input.ToLower() != "exit")
{
    input = Console.ReadLine();
}

return 0;
