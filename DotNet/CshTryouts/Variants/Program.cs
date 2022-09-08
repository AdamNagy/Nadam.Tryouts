// See https://aka.ms/new-console-template for more information
using Variants;

Console.WriteLine("Hello, World!");

var list = new List<IEvent>();
var actions = new List<Action<IEvent>>();

IEventBus messanger = new EventBus();

messanger.Subscribe(new TextEventhandler((msg) => Console.WriteLine(msg.Payload)));
messanger.Subscribe<TextEvent>((payload) => Console.WriteLine(payload.Payload.ToUpper()));

messanger.Publish(new TextEvent("Halika"));
messanger.Publish(new TextEvent("Hi"));
messanger.Publish(new TextEvent("Hello"));

interface IEventBus
{
    void Subscribe<TEvent>(IEventHandler<TEvent> action) where TEvent : IEvent;
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    void Publish<TEvent>(TEvent payload) where TEvent : IEvent;
}

class EventBus : IEventBus
{
    public List<Subscription> Actions { get; set; }

    public EventBus()
    {
        Actions = new List<Subscription>();
    }

    public void Publish<TEvent>(TEvent payload) where TEvent : IEvent
    {
        foreach (var item in Actions)
        {
            item.Handler(payload);
        }
    }

    public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        Actions.Add(new Subscription((e) => handler.Handle((TEvent)e)));
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        Actions.Add(new Subscription((e) => handler((TEvent)e)));
    }
}

interface IEventHandler<TEvent> where TEvent : IEvent
{
    void Handle(TEvent e);
}

class TextEventhandler : IEventHandler<TextEvent>
{
    private readonly Action<TextEvent> _handler;

    public void Handle(TextEvent e)
    {
        _handler(e);
    }

    public TextEventhandler(Action<TextEvent> handler)
    {
        _handler = handler;
    }
}

class Subscription
{
    public Action<IEvent> Handler { get; set; }

    public Subscription(Action<IEvent> handler)
    {
        Handler = handler;
    }
}