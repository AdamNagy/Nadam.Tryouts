namespace MyPizza_ParallelProg
{
    internal class Order
    {

        public string Id { get; private set; }
        public State State { get; set; }

        public Order()
        {
            Id = Guid.NewGuid().ToString();
            State = State.pending;
        }

        public Order(State state)
        {
            Id = Guid.NewGuid().ToString();
            State = state;
        }
    }

    internal enum State
    {
        pending, done, failed
    }
        
}
