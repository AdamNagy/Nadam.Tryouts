using MyNetworkService.EventInfrastructure.Contracts;

namespace MyNetworkService.EventInfrastructure
{
    public class Subscription
    {
        public Action<IEvent> Handler { get; set; }

        public Subscription(Action<IEvent> handler)
        {
            Handler = handler;
        }
    }
}
