namespace MyMessageQueue
{
    internal class BulkDownloader
    {
        private readonly HttpClient _client;
        private readonly int _parallelism;

        public BulkDownloader(HttpClient client, int parallelism = 3)
        {
            _client = client;
            _parallelism = parallelism;
        }

        #region general batch processor
        public async Task<IEnumerable<TOut>> BatchProcess<TOut>(IList<Func<Task<TOut>>> tasks)
        {
            var taskQueue = new Queue<Func<Task<TOut>>>(tasks);

            var ongoingTasks = new List<Task<TOut>>();

            for (int i = 0; i < _parallelism; i++)
            {
                var next = taskQueue.Dequeue();
                ongoingTasks.Add(next());
            }

            while (taskQueue.Any())
            {
                await Task.WhenAny(ongoingTasks);

                var next = taskQueue.Dequeue();
                ongoingTasks.Add(next());
            }

            return ongoingTasks.Select(p => p.Result).ToList();
        }

        public IEnumerable<Func<Task<(string uri, string content)>>> GenerateHttpGetFunc(IEnumerable<string> uris)
            => CaptureInputParam(uris, HttpGet);
        
        public Task<(string uri, string content)> HttpGet(string uri)
             => Task.Run(async () =>
                {
                    var response = await _client.GetAsync(uri);
                    var content = await response.Content.ReadAsStringAsync();
                    return (uri, content);
                });

        private IEnumerable<Func<Task<TOut>>> CaptureInputParam<TIn, TOut>(IEnumerable<TIn> input, Func<TIn, Task<TOut>> hotTaskGenerator)
        {
            foreach (var item in input)
                yield return () => hotTaskGenerator(item);
        }
        #endregion

        #region throttle
        public async Task<IList<(string, string)>> DownloadBatch(IEnumerable<string> uris)
        {
            var uriQueue = new Queue<string>(uris.Count());
            foreach (var item in uris)
                uriQueue.Enqueue(item);

            var downloadTasks = new List<Task<(string, string)>>();

            for (int i = 0; i < _parallelism; i++)
            {
                var next = uriQueue.Dequeue();
                downloadTasks.Add(Download(next));
            }

            while (uriQueue.Any())
            {
                await Task.WhenAny(downloadTasks);

                var next = uriQueue.Dequeue();
                downloadTasks.Add(Download(next));
            }

            return downloadTasks.Select(p => p.Result).ToList();
        }

        private async Task<(string, string)> Download(string uri)
        {
            var response = await _client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            return (uri, content);
        }
        #endregion

        #region semaphore
        public async Task<IList<(string, string)>> DownloadSemaphore(IEnumerable<string> uris)
        {
            var uriQueue = new Queue<string>(uris);

            var downloadTasks = new List<Task<(string, string)>>();
            var semaphore = new SemaphoreSlim(_parallelism);

            foreach (var uri in uris)
            {
                var next = uriQueue.Dequeue();
                downloadTasks.Add(Download(next, semaphore));
            }

            await Task.WhenAll(downloadTasks);
            return downloadTasks.Select(p => p.Result).ToList();
        }

        private async Task<(string, string)> Download(string uri, SemaphoreSlim semaphore)
        {
            var runing = Task.Run(() =>
            {
                Console.WriteLine($"Before wait: {Thread.CurrentThread.ManagedThreadId} {semaphore.CurrentCount}");
                semaphore.Wait();
                Console.WriteLine($"After wait: {Thread.CurrentThread.ManagedThreadId} {semaphore.CurrentCount}");
                var response = _client.GetAsync(uri).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                // Console.WriteLine($"Run: {Thread.CurrentThread.ManagedThreadId} {semaphore.CurrentCount}");
                semaphore.Release();
                Console.WriteLine($"After release: {Thread.CurrentThread.ManagedThreadId} {semaphore.CurrentCount}");
                return (uri, content);
            });

            //await runing.ContinueWith(t => {
            //    Console.WriteLine($"ContinueWith: {Thread.CurrentThread.ManagedThreadId} {semaphore.CurrentCount}");
            //});

            return await runing;
        }
        #endregion
    }
}
