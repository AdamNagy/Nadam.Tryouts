using MyNetworkApp;
using MyNetworkService.EventInfrastructure;
using MyNetworkService.EventInfrastructure.Contracts;

Console.WriteLine("Starting Client");

IEventBus eventBus = new EventBus();
var tcpClient = new MyNetworkApp.TcpClient(eventBus);
var messageClient = new MessageClient(tcpClient, eventBus);
messageClient.Start();

var command = Console.ReadLine();

while(command != "exit")
{
    command = Console.ReadLine();
}

return;