using System.Collections.Concurrent;

namespace MyMessageQueue
{
    internal class EventBus
    {
        private readonly ConcurrentDictionary<string, Dictionary<string, Action<object>>> _subscriptions;

        public EventBus()
        {
            _subscriptions = new ConcurrentDictionary<string, Dictionary<string, Action<object>>>();
        }

        public void Publish(AppEvent raisedEvent)
        {
            if (!_subscriptions.ContainsKey(raisedEvent.Name))
                return;

            Dictionary<string, Action<object>> subscriptions;
            ushort trings = 0;
            while(_subscriptions.TryGetValue(raisedEvent.Name, out subscriptions) && trings < 5)
            {
                _subscriptions.TryGetValue(raisedEvent.Name, out subscriptions);
                ++trings;
            }

            if (subscriptions == null)
                throw new Exception("Something went wrong with event bus");

            Parallel.ForEach(subscriptions.Values, (subscription) => subscription(raisedEvent));
        }

        public AppEventSubscription Subscribe(string eventname, Action<object> handler)
        {
            var subscriptionId = Guid.NewGuid().ToString();
            if(!_subscriptions.ContainsKey(eventname))
                _subscriptions.TryAdd(eventname, new Dictionary<string, Action<object>>());

            _subscriptions[eventname].Add(subscriptionId, handler);
            return new AppEventSubscription(subscriptionId, this);
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

    internal class AppEvent
    {
        public string Name { get; set; }
        public object Payload { get; set; }
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
}
