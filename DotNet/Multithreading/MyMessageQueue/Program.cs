// See https://aka.ms/new-console-template for more information
using MyMessageQueue;
using System.Diagnostics;

// TestThrottledQueue();
TestBulkDownload();

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
            .Where(p => p.ToLower().Contains("gate"))
            .Take(100)
            .ToList();

    var sw = new Stopwatch();
    sw.Start();
    var res2 = DownloadBatch(client, uris).Result;
    sw.Stop();
    Console.WriteLine($"DownloadBatch: {sw.ElapsedMilliseconds}\nCount: {res2.Count()}");

    sw.Reset();
    sw.Start();
    var res3 = DownloadSemaphore(client, uris).Result;
    sw.Stop();
    Console.WriteLine($"DownloadSemaphore: {sw.ElapsedMilliseconds}\nCount: {res3.Count()}");
}

//async Task<IEnumerable<object>> DownloadParallel(HttpClient client, IEnumerable<string> uris)
//{
//    var queueJobs = uris
//        .Select(p => new QueueFunction(p, (uri) => {
//            var response = client.GetAsync(uri as string).Result;
//            return response.Content.ReadAsStringAsync().Result;
//        }))
//        .ToList();

//    var downloadJobs = new ThrottledList(queueJobs, 10);
//    return downloadJobs.Start();
//}

async Task<IList<(string, string)>> DownloadBatch(HttpClient client, IEnumerable<string> uris)
{
    var path = @"C:\Users\Adam_Nagy1\Documents\test-urls-to-download\batched\";
    var downloader = new BulkDownloader(client);
    var downloads = new List<(string, string)>();

    foreach (var uriBatch in uris.Chunk(10))
    {
        var downloadsBatch = await downloader.DownloadBatch(uriBatch);
        downloads.AddRange(downloadsBatch);

        Parallel.ForEach(downloadsBatch.Select(p => p.Item2),
            webPage => File.WriteAllText($"{path}{Guid.NewGuid()}.html", webPage));
    }

    return downloads;
}

async Task<IList<(string, string)>> DownloadSemaphore(HttpClient client, IEnumerable<string> uris)
{
    var path = @"C:\Users\Adam_Nagy1\Documents\test-urls-to-download\semaphored\";
    var downloader = new BulkDownloader(client);
    var downloads = new List<(string, string)>();

    foreach (var uriBatch in uris.Chunk(10))
    {
        var downloadsBatch = await downloader.DownloadSemaphore(uriBatch);
        downloads.AddRange(downloadsBatch);

        Parallel.ForEach(downloadsBatch.Select(p => p.Item2),
            webPage => File.WriteAllText($"{path}{Guid.NewGuid()}.html", webPage));
    }

    return downloads;
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
