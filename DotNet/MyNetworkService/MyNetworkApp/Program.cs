using MyNetworkApp;
using MyNetworkService.EventInfrastructure;
using MyNetworkService.EventInfrastructure.Contracts;

Console.WriteLine("Starting Client");

IEventBus eventBus = new EventBus();
var tcpClient = new MyNetworkApp.TcpClient(eventBus);
var messageClient = new MessageClient(tcpClient, eventBus);
messageClient.Start();

Console.WriteLine("Please give a temporary name:");

var name = Console.ReadLine();
messageClient.Checkin(name);

var command = Console.ReadLine();

while(command != "exit")
{
    messageClient.SendMessage(command);
    command = Console.ReadLine();
}

messageClient.Checkout();

return;