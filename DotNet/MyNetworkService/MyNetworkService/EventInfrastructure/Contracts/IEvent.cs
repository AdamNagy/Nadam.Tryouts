namespace MyNetworkService.EventInfrastructure.Contracts
{
    public interface IEvent
    {
        public string Id { get; }
    }

    public interface IEvent<TPayload> : IEvent
    {
        TPayload Payload { get; }
    }
}
