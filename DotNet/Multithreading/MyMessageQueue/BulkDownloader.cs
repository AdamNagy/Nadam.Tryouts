namespace MyMessageQueue
{
    internal class BulkDownloader
    {
        private readonly HttpClient _client;
        private readonly int _parallelism;
        private readonly IEventBus _eventBus;

        public BulkDownloader(HttpClient client, int parallelism = 10, IEventBus eventBus = default(IEventBus))
        {
            _client = client;
            _parallelism = parallelism;
            _eventBus = eventBus;
        }

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

        #region semaphore
        public async Task<IList<(string, string)>> DownloadSemaphore(IEnumerable<string> uris)
        {
            var uriQueue = new Queue<string>(uris.Count());
            foreach (var item in uris)
                uriQueue.Enqueue(item);

            var downloadTasks = new List<Task<(string, string)>>();
            var semaphore = new SemaphoreSlim(_parallelism);

            foreach (var uri in uris)
            {
                var next = uriQueue.Dequeue();
                downloadTasks.Add(Download(next, semaphore));
            }

            await Task.WhenAny(downloadTasks);
            return downloadTasks.Select(p => p.Result).ToList();
        }

        private async Task<(string, string)> Download(string uri, SemaphoreSlim semaphore)
        {
            semaphore.Wait();
            var response = await _client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            semaphore.Release();
            return (uri, content);
        }
        #endregion
    }
}
