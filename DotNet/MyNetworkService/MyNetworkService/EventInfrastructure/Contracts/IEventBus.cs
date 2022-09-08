namespace MyNetworkService.EventInfrastructure.Contracts
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> action) where TEvent : IEvent;
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;

        void Publish<TEvent>(TEvent payload) where TEvent : IEvent;
    }
}
