// See https://aka.ms/new-console-template for more information
using MyMessageQueue;
using System.Diagnostics;

HttpClient client = new HttpClient();
var uris = File.ReadAllLines(@"C:\Users\Adam_Nagy1\Documents\test-urls-to-download.txt")
        .Where(p => !string.IsNullOrEmpty(p))
        .Where(p => p.ToLower().Contains("gate"))
        .Take(100)
        .ToList();

var sw = new Stopwatch();
sw.Start();
var res = DownloadParallel(client, uris).Result;
sw.Stop();
Console.WriteLine($"DownloadParallel: {sw.ElapsedMilliseconds}\nCount: {res.Count()}");

sw.Reset();
sw.Start();
var res2 = DownloadBatch(client, uris).Result;
sw.Stop();
Console.WriteLine($"DownloadBatch: {sw.ElapsedMilliseconds}\nCount: {res2.Count()}");

sw.Reset();
sw.Start();
var res3 = DownloadSemaphore(client, uris).Result;
sw.Stop();
Console.WriteLine($"DownloadSemaphore: {sw.ElapsedMilliseconds}\nCount: {res3.Count()}");

Console.ReadKey();

void Print(string message)
{
    Console.WriteLine(message);
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
    // var queue = new ThrottledQueue(50, 25);
    var queue = new ThrottledQueue(0, 20);
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
                var next = rnd.Next(600, 3000);
                await Task.Delay(next);

                if( next % 2 == 0)
                {
                    var action = new QueueAction(
                            $"{next}",
                            (payload) => Print($"Action: ${payload as string}"));
                        queue.Enque(action);
                }
                else
                {
                    var action = new QueueFunction(
                            $"{next}",
                            (payload) =>
                            {
                                return $"Function: {payload as string}";
                            });
                    queue.Enque(action).ContinueWith((p) => Print(p.Result as string));
                    
                }
            }
        }, cancellationToken, TaskCreationOptions.LongRunning);
    }

    var input = Console.ReadLine();
    while (RunCommand()) ;

    cancellationTokenSource.Cancel();
    queue.Pause();
}

async Task<IEnumerable<object>> DownloadParallel(HttpClient client, IEnumerable<string> uris)
{
    var progressEventBus = new EventBus();
    var eventBus = new EventBus();
    var processQueue = new ThrottledQueue();

    var queueJobs = uris
        .Select(p => new QueueFunction(p, (uri) => {
            var response = client.GetAsync(uri as string).Result;
            return response.Content.ReadAsStringAsync().Result;
        }))
        .ToList();
        
    eventBus.Subscribe<ProgressEvent>(ThrottledList.PROGRESS_EVENT_NAME, (payload) => Console.WriteLine(payload));

    var downloadJobs = new ThrottledList(queueJobs, 10, eventBus);
    return downloadJobs.Start();
}

async Task<IList<(string, string)>> DownloadBatch(HttpClient client, IEnumerable<string> uris)
{
    var progressEventBus = new EventBus();
    var eventBus = new EventBus();
    var processQueue = new ThrottledQueue();

    eventBus.Subscribe(ThrottledList.PROGRESS_EVENT_NAME, (payload) => Console.WriteLine(payload));

    var downloadJobs = new BulkDownloader(client);
    return await downloadJobs.DownloadBatch(uris);
}

async Task<IList<(string, string)>> DownloadSemaphore(HttpClient client, IEnumerable<string> uris)
{
    var progressEventBus = new EventBus();
    var eventBus = new EventBus();
    var processQueue = new ThrottledQueue();

    eventBus.Subscribe(ThrottledList.PROGRESS_EVENT_NAME, (payload) => Console.WriteLine(payload));

    var downloadJobs = new BulkDownloader(client);
    return await downloadJobs.DownloadSemaphore(uris);
}

string ToUpper(string value)
{
    return value.ToUpper();
}

Task<string> ToUpperAsync(string value)
{
    var tcs = new TaskCompletionSource<string>();

    Task.Run(async () =>
    {
        var result = ToUpper(value);
        tcs.SetResult(result);
    });

    return tcs.Task;
}