// See https://aka.ms/new-console-template for more information
using MyMessageQueue;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            .Skip(200)
            .Take(10)
            .ToList();

    var sw = new Stopwatch();
    sw.Start();
    DownloadBatch(client, uris);
    sw.Stop();
    Console.WriteLine($"DownloadBatch: {sw.ElapsedMilliseconds}");

    sw.Reset();
    sw.Start();
    DownloadSemaphore(client, uris);
    sw.Stop();
    Console.WriteLine($"DownloadSemaphore: {sw.ElapsedMilliseconds}");
}

async void DownloadBatch(HttpClient client, IEnumerable<string> uris)
{
    var path = @"C:\Users\Adam_Nagy1\Documents\test-urls-to-download\";
    var downloader = new BulkDownloader(client);

    foreach (var uriBatch in uris.Chunk(10))
    {
        var downloadsBatch = await downloader.DownloadBatch(uriBatch);

        foreach (var regex in HtmlRegex.Regexes)
        {
            var attributes = downloadsBatch
                .Select(p => p.Item2)
                .Select(q => regex.Value.Matches(q))
                .SelectMany(r => r)
                .SelectMany(r => r.Groups.Values.Where(s => s.Name.ToLower() == regex.Key))
                .Select(t => t.Value)
                .ToList();
        }

        foreach (var result in downloadsBatch)
        {
            var uri = new Uri(result.Item1);
        }
    }
}

async void DownloadSemaphore(HttpClient client, IEnumerable<string> uris)
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

void TestUriTree()
{
    var uris = File.ReadAllLines(@"C:\Users\Adam_Nagy1\Documents\test-urls-to-download.txt")
        .Where(p => !string.IsNullOrEmpty(p))
        .ToList();

    var domainTree = new DomainTree();
    foreach (var item in uris)
    {
        domainTree.Add(item);
    }

    var toSearch = new string[]
    {
        "https://ipon.hu/shop/csoport/szamitogep-periferia-gamer/ajandekutalvany/18476",
        "https://facebook.com/iPon.hu/",
        "https://www.bauhaus.hu/informaciok/adatvedelmi-nyilatkozat/",
        "https://cdn.euromart.com/subcategories/belts/58",
        "https://www.ecipo.hu/noi/gyartok:tamaris.html",
        "https://www.fassag.com/szopsz"
    };

    var sw = Stopwatch.StartNew();
    foreach (var item in toSearch)
    {
        uris.Contains(item);
    }
    sw.Stop();
    Console.WriteLine($"Linear search: {sw.ElapsedMilliseconds}");

    sw.Reset();
    sw.Start();
    foreach (var item in toSearch)
    {
        domainTree.Contains(item);
    }
    sw.Stop();
    Console.WriteLine($"Tree search: {sw.ElapsedMilliseconds}");
}

public class HtmlRegex
{
    public static readonly Dictionary<string, Regex> Regexes = new Dictionary<string, Regex>();

    static HtmlRegex()
    {
        Regexes.Add(
            "href",
            new Regex("href=\"(?<href>.+?)\"", RegexOptions.Compiled | RegexOptions.Multiline));

        //Regexes.Add(
        //    "src",
        //    new Regex("src=\"(?<src>.+?)\"", RegexOptions.Compiled | RegexOptions.Multiline));

        Regexes.Add(
            "img-src",
            new Regex("img src=\"(?<src>.+?)\"", RegexOptions.Compiled | RegexOptions.Multiline));
    }

    public Regex this[string i] => Regexes[i];
    
    public bool ContainsKey(string attr)
        => Regexes.ContainsKey(attr);
}