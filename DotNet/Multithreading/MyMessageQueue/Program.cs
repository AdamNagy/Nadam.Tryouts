// See https://aka.ms/new-console-template for more information
using MyMessageQueue;

Console.WriteLine("Hello, World!");

// TestThrottledQueue();
TestThrottledJobQueue();

void Print(string message)
{
    Console.Write($"{message}\t");
}

bool RunCommand()
{
    var command = Console.ReadLine();
    if ( command == "exit")
        return false;

    //switch (command.ToLower())
    //{
    //    case "continue":
    //    case "start":
    //        queue.Start();
    //        break;

    //    case "pause":
    //    case "stop":
    //    case "break":
    //        queue.Pause();
    //        break;
    //}

    return true;
}

void TestThrottledQueue()
{
    var queue = new ThrottledQueue(50, 25);
    queue.Start();

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken cancellationToken = cancellationTokenSource.Token;

    foreach (var item in Enumerable.Range(0, 5))
    {
        Task.Factory.StartNew(async (state) =>
        {
            var rnd = new Random();
            while (true)
            {
                var next = rnd.Next(100, 1000);
                await Task.Delay(next);
                var action = new QueueAction(
                        "Some.Event.Has.Raised",
                        $"Event {next}",
                        (payload) => Print(payload as string));
                queue.Enque(action);
            }
        }, cancellationToken, TaskCreationOptions.LongRunning);
    }

    var input = Console.ReadLine();
    while (RunCommand()) ;

    cancellationTokenSource.Cancel();
    queue.Pause();
}

void TestThrottledJobQueue()
{
    var queue = new ThrottledJobQueue();
    queue.Start();

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken cancellationToken = cancellationTokenSource.Token;

    foreach (var item in Enumerable.Range(0, 5))
    {
        Task.Factory.StartNew(async (state) =>
        {
            var rnd = new Random();
            while (true)
            {
                var next = rnd.Next(100, 1000);
                await Task.Delay(next);
                var action = new QueueJob(
                        "Some.Event.Has.Raised",
                        $"Event {next}",
                        (payload) => payload.ToString().ToUpper());

                // in asp.net action the await approach would be used
                //var result = await queue.Enque(action);
                //Print(result as string);

                // for testing ContinueWith can be used
                queue.Enque(action).ContinueWith((res) => Print((res.Result) as string));
            }
        }, cancellationToken, TaskCreationOptions.LongRunning);
    }

    var input = Console.ReadLine();
    while (RunCommand()) ;

    cancellationTokenSource.Cancel();
    queue.Pause();
}