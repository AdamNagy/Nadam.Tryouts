namespace MyPizza_ParallelProg
{
    internal interface IOrderService
    {
        void Order(Order order);
    }

    internal class OrderService : IOrderService
    {
        public int MaxOrders { get; set; }
        public int NumOfOders { get; private set; }
        public object Lock { get; set; } = new object();

        public OrderService()
        {
            MaxOrders = 500;
        }

        public void Order(Order order)
        {
            lock (Lock)
            {
                if(NumOfOders >= MaxOrders)            
                    throw new Exception("Cannot process any more orders today");
            
                Console.WriteLine($"Pizza has been ordered. ({NumOfOders}:{order.Id})");
                NumOfOders++;
            }
        }
    }
}
