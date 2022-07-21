// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using MyPizza_ParallelProg;

#region Parallel.For
Console.WriteLine("Parallell.For");

var orderService = new OrderService();
var numOfSuccess = 0;

//Parallel.For(0, 5000, () => 0, (idx, loopState, localSum) =>
//{
//    try
//    {
//        var order = new Order();
//        if (idx % 2 == 0)
//        {
//            orderService.Order(order);
//            localSum++;
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"{idx}:{ex.Message}");
//        loopState.Break();  // stop()
//    }

//    return localSum;
//}, (result) => Interlocked.Add(ref numOfSuccess, result));

Console.WriteLine($"Successfull orders: {numOfSuccess}");
#endregion

#region Parallel.Forech
//Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Parallell.ForEach");
//CompareRuns(500000);
//CompareRuns(5000000);
//CompareRuns(50000000);

static void CompareRuns(int numOfOrders)
{
    Console.WriteLine($"numOfOrders {numOfOrders}");
    var orders = Enumerable.Range(0, numOfOrders)
            .Select(i => i % 2 == 0
                ? new Order()
                : new Order(State.failed));
            //.ToConcurrentQueue();

    var failedOrders = new ConcurrentBag<Order>();


    var failed = 0;
    var syncRunTime = Benchmark.Run(() =>
    {
        foreach (var item in orders)
        {
            if (item.State == State.failed)
                failed++;
        }

    });
    Console.WriteLine($"Sync: {syncRunTime} {failed}{Environment.NewLine}");
    //Console.WriteLine($"Syncron run time: {syncRunTime}");

    failed = 0;
    failedOrders = new ConcurrentBag<Order>();
    var time = Benchmark.Run(() =>
    {
        Parallel.ForEach(orders, i =>
        {
            if (i.State == State.failed)
            {
                failedOrders.Add(i);
                // Interlocked.Increment(ref failed);
                failed++;
            }
        });
    });
    Console.WriteLine($"Parallel.ForEach: {time}, {failed}{Environment.NewLine}");

    failed = 0;
    failedOrders = new ConcurrentBag<Order>();
    var parallelRunTime = Benchmark.Run(() =>
    {
        failed = orders.AsParallel().Count(p => p.State == State.failed);
    });
    Console.WriteLine($"PLINQ: {parallelRunTime} {failed}{Environment.NewLine}");
    //Console.WriteLine($"PLINQ run time: {parallelRunTime}{Environment.NewLine}");
}
#endregion

#region Parallel.Invoke
Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Parallel.invoke");
//var actions = 
//    Enumerable.Range(0, 50000)
//    .Select<int, Action>( 
//        (int i) => 
//        {   
//            if( i == 10)
//                return () => Benchmark.Run(() => { Thread.Sleep(400); Enumerable.Range(0, 1000000).Select(i => { SumNumbers(); return 0; }); });

//            return () => Benchmark.Run(() => Enumerable.Range(0, 1000000).Select(i => { SumNumbers(); return 0; }));
//        })
//    .ToArray();

var actions2 =
    Enumerable.Range(0, 100)
    .Select<int, Action>(
        (int i) =>
        {
            if (i == 10)
                return () => { 
                    Enumerable.Range(0, 1_000)
                        .Select(i => { 
                            SumNumbers(); 
                            return 0; 
                        }).ToList(); 
                };

            return () => Enumerable.Range(0, 1_000).Select(i => { SumNumbers(); return 0; }).ToList();
        })
    .ToArray();

Console.WriteLine("Parallel.Invoke");
Benchmark.Run(() => Parallel.Invoke(actions2));
Benchmark.Run(() => {
    foreach (var item in actions2)
    {
        item.Invoke();
    }
});

static long SumNumbers()
{
    long sum = 0;
    for (int i = 0; i < 1_000; i++)
    {
        sum += i;
    }

    return sum;
}
#endregion