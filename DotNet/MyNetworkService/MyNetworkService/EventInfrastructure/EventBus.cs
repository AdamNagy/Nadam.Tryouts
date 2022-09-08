using MyNetworkService.EventInfrastructure.Contracts;

namespace MyNetworkService.EventInfrastructure
{
    public class EventBus : IEventBus
    {
        public Dictionary<string, List<Subscription>> Actions { get; set; }

        public EventBus()
        {
            Actions = new Dictionary<string, List<Subscription>>();
        }

        public void Publish<TEvent>(TEvent payload) where TEvent : IEvent
        {
            var type = typeof(TEvent).Name;

            if (!Actions.ContainsKey(type))
                return;

            foreach (var subscription in Actions[type])
            {
                subscription.Handler(payload);
            }
        }

        public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            var type = typeof(TEvent).Name;

            if(!Actions.ContainsKey(type))
                Actions.Add(type, new List<Subscription>());

            Actions[type].Add(new Subscription((e) => handler.Handle((TEvent)e)));
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var type = typeof(TEvent).Name;

            if (!Actions.ContainsKey(type))
                Actions.Add(type, new List<Subscription>());

            Actions[type].Add(new Subscription((e) => handler((TEvent)e)));
        }
    }
}
