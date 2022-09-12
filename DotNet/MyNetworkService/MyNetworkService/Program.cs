using MyNetworkService;
using MyNetworkService.EventInfrastructure;
using MyNetworkService.EventInfrastructure.Contracts;

Console.WriteLine("Starting Server");

IEventBus eventBus = new EventBus();
var tcpServer = new SocketServer(eventBus);
tcpServer.Start();
var broker = new MessageBroker(eventBus);

var messageServer = new MessageServer(eventBus, broker);
messageServer.Strart();

var command = Console.ReadLine();
while(command != "exit")
{
    command = Console.ReadLine();
}
