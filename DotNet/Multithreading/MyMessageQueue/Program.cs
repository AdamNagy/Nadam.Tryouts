// See https://aka.ms/new-console-template for more information
using MyMessageQueue;
using System.Diagnostics;
using System.Text.RegularExpressions;

// TestThrottledQueue();
TestBulkDownload();

Console.WriteLine("Done");
Console.ReadKey();

/****************************/

void TestThrottledQueue()
{
    // var queue = new ThrottledQueue(50, 25);
    var queue = new ThrottledQueue(5, 0);
    queue.Start();

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken cancellationToken = cancellationTokenSource.Token;

    foreach (var item in Enumerable.Range(0, 10))
    {
        Task.Factory.StartNew(async (state) =>
        {
            var rnd = new Random();
            while (true)
            {
                var next = rnd.Next(10);
                // await Task.Delay(next);

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

                    var queuedJob = queue.Enque(action);
                    if(queuedJob != null)
                        queuedJob.ContinueWith((p) => Print(p.Result as string));
                    
                }
            }
        }, cancellationToken, TaskCreationOptions.LongRunning);
    }

    var input = Console.ReadLine();
    while (RunCommand()) ;

    cancellationTokenSource.Cancel();
    queue.Pause();
}

void TestBulkDownload()
{
    HttpClient client = new HttpClient();
    var uris = File.ReadAllLines(@"C:\Users\Adam_Nagy1\Documents\test-urls-to-download.txt")
            .Where(p => !string.IsNullOrEmpty(p))
            .Take(20)
            .ToList();

    var sw = new Stopwatch();
    //sw.Start();
    //DownloadBatch(client, uris);
    //sw.Stop();
    //Console.WriteLine($"DownloadBatch: {sw.ElapsedMilliseconds}");

    sw.Reset();
    sw.Start();
    DownloadSemaphore(client, uris);
    sw.Stop();
    Console.WriteLine($"DownloadSemaphore: {sw.ElapsedMilliseconds}");
}

void DownloadBatch(HttpClient client, IEnumerable<string> uris)
{
    var path = @"C:\Users\Adam_Nagy1\Documents\test-urls-to-download\batched";
    var downloader = new BulkDownloader(client);

    var downloadsBatch = downloader.DownloadBatch(uris).Result;
    foreach (var item in downloadsBatch)
    {
        File.WriteAllText(Path.Combine(path, $"{Guid.NewGuid().ToString()}.html"), item.Item2);
    }
}

void DownloadSemaphore(HttpClient client, IEnumerable<string> uris)
{
    var path = @"C:\Users\Adam_Nagy1\Documents\test-urls-to-download\semaphored\";
    var downloader = new BulkDownloader(client);

    var downloadsBatch = downloader.DownloadSemaphore(uris).Result;
    foreach (var item in downloadsBatch)
    {
        File.WriteAllText(Path.Combine(path, $"{Guid.NewGuid().ToString()}.html"), item.Item2);
    }
}

void Print(string message)
{
    Console.WriteLine(message);
}

bool RunCommand()
{
    var command = Console.ReadLine();
    if (command == "exit")
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
