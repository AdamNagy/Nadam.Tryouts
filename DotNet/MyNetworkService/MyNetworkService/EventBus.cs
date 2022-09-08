using System.Collections.Concurrent;

namespace MyNetworkService
{
    public interface IEventBus
    {
        void Publish(IAppEvent raisedEvent);
        AppEventSubscription Subscribe(string eventname, Action<IAppEvent> handler);
        public void UnSubscribe(string id);
    }

    public class EventBus : IEventBus
    {
        private readonly ConcurrentDictionary<string, Dictionary<string, Action<IAppEvent>>> _subscriptions;

        public EventBus()
        {
            _subscriptions = new ConcurrentDictionary<string, Dictionary<string, Action<IAppEvent>>>();
        }

        public void Publish(IAppEvent raisedEvent)
        {
            if (!_subscriptions.TryGetValue(raisedEvent.Name, out var subscriptions) 
                && !_subscriptions.TryGetValue(raisedEvent.GetType().Name, out subscriptions))
            {
                return;
            }

            foreach (var subscription in subscriptions.Values)
                subscription(raisedEvent);
        }

        public AppEventSubscription Subscribe(string eventname, Action<IAppEvent> handler)
        {
            var subscriptionId = Guid.NewGuid().ToString();
            if (!_subscriptions.ContainsKey(eventname))
                _subscriptions.TryAdd(eventname, new Dictionary<string, Action<IAppEvent>>());

            _subscriptions[eventname].Add(subscriptionId, handler);
            return new AppEventSubscription(subscriptionId, this);
        }

        public AppEventSubscription Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IAppEvent
        {
            Action<IAppEvent> xhandler = handler as Action<IAppEvent>;
            var eventType = typeof(TEvent).Name;

            return Subscribe(eventType, xhandler);
        }

        public void UnSubscribe(string id)
        {
            foreach (var item in _subscriptions)
            {
                if (item.Value.ContainsKey(id))
                {
                    item.Value.Remove(id);
                    break;
                }
            }
        }
    }

    public interface IAppEvent
    {
        string Name { get; }
    }

    public class TextEvent : IAppEvent
    {
        public string Name { get; private set; }
        public string Text { get; private set; }

        public TextEvent(string name, string payload)
        {
            Name = name;
            Text = payload;
        }

        public override string ToString()
        {
            return $"{Name}: {Text}";
        }
    }

    public class AppEventSubscription
    {
        private readonly EventBus _eventBus;
        private readonly string _id;

        public AppEventSubscription(string id, EventBus bus)
        {
            _id = id;
            _eventBus = bus;
        }

        public void UnSubscribe()
        {
            _eventBus.UnSubscribe(_id);
        }
    }
}
