using MyNetworkService;

Console.WriteLine("Hello, World!");

var eventBus = new EventBus();

eventBus.Subscribe<ClientConnectedEvent>((payload) =>
{
    Console.WriteLine($"{payload.ClientId} has connected");
});

eventBus.Subscribe<MessageArrivedEvent>((payload) =>
{
    Console.WriteLine($"{payload.ClientId} has sent message: {payload.Message}");
});

var tcpServer = new TcpServer(eventBus);
tcpServer.Start();

var command = Console.ReadLine();
while(command != "exit")
{
    command = Console.ReadLine();
}