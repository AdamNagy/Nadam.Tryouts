﻿using MyNetworkService;
using MyNetworkService.EventInfrastructure;
using MyNetworkService.EventInfrastructure.Contracts;

Console.WriteLine("Starting Server");

IEventBus eventBus = new EventBus();
TcpServer tcpServer = new TcpServer(eventBus); ;

var messageServer = new MessageServer(tcpServer, eventBus);
messageServer.Strart();

var command = Console.ReadLine();
while(command != "exit")
{
    command = Console.ReadLine();
}