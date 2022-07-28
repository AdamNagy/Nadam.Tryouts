using System.Collections.Concurrent;

namespace MyMessageQueue
{
    internal class EventBus : IEventBus
    {
        private readonly ConcurrentDictionary<string, Dictionary<string, Action<IAppEvent>>> _subscriptions;

        public EventBus()
        {
            _subscriptions = new ConcurrentDictionary<string, Dictionary<string, Action<IAppEvent>>>();
        }

        public void Publish(IAppEvent raisedEvent)
        {
            if (!_subscriptions.ContainsKey(raisedEvent.Name))
                return;

            Dictionary<string, Action<IAppEvent>> subscriptions;
            ushort trings = 0;
            while(_subscriptions.TryGetValue(raisedEvent.Name, out subscriptions) && trings < 5)
            {
                _subscriptions.TryGetValue(raisedEvent.Name, out subscriptions);
                ++trings;
            }
            trings = 0;

            if (subscriptions == null)
                throw new Exception("Something went wrong with event bus");

            Parallel.ForEach(subscriptions.Values, (subscription) => subscription(raisedEvent));
        }

        public AppEventSubscription Subscribe(string eventname, Action<IAppEvent> handler)
        {
            var subscriptionId = Guid.NewGuid().ToString();
            if(!_subscriptions.ContainsKey(eventname))
                _subscriptions.TryAdd(eventname, new Dictionary<string, Action<IAppEvent>>());

            _subscriptions[eventname].Add(subscriptionId, handler);
            return new AppEventSubscription(subscriptionId, this);
        }

        public AppEventSubscription Subscribe<TEvent>(string eventname, Action<TEvent> handler) where TEvent : IAppEvent
        {
            Action<IAppEvent> xevent = handler as Action<IAppEvent>;
            return Subscribe(eventname, handler);
        }

        public void UnSubscribe(string id)
        {
            foreach (var item in _subscriptions)
            {
                if(item.Value.ContainsKey(id))
                {
                    item.Value.Remove(id);
                    break;
                }
            }
        }
    }

    internal interface IAppEvent
    {
        string Name { get; }
    }

    internal class TextEvent : IAppEvent
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

    internal class AppEventSubscription
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

    internal interface IEventBus
    {
        void Publish(IAppEvent raisedEvent);
        AppEventSubscription Subscribe(string eventname, Action<IAppEvent> handler);
        public void UnSubscribe(string id);
    }
}
