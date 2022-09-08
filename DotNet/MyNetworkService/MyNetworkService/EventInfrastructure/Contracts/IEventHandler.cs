namespace MyNetworkService.EventInfrastructure.Contracts
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent e);
    }
}
