using System.Collections.Concurrent;

namespace MyMessageQueue
{
    internal class ThrottledList
    {
        private readonly ConcurrentBag<QueueJobItem> _queue = new ConcurrentBag<QueueJobItem>();
        private readonly int _batchSize;
        private readonly EventBus _eventBus;
        private readonly int _count;
        private readonly ConcurrentBag<Task<object>> _results = new ConcurrentBag<Task<object>>();
        private readonly object _lock = new object();

        public const string PROGRESS_EVENT_NAME = "making-progress";

        public ThrottledList(IEnumerable<QueueJob> jobs, int batchSize, EventBus eventBus)
        {
            _batchSize = batchSize;
            _eventBus = eventBus;
            _count = jobs.Count();

            foreach (var job in jobs)
            {
                if (job is QueueAction)
                    Add(job as QueueAction);
                else if (job is QueueFunction)
                    _results.Add(Add(job as QueueFunction));
            }
        }

        private Task Add(QueueAction action)
        {
            var tcs = new TaskCompletionSource<object>();
            _queue.Add(new QueueJobItem(action, tcs));
            return tcs.Task;
        }

        private Task<object> Add(QueueFunction action)
        {
            var tcs = new TaskCompletionSource<object>();
            _queue.Add(new QueueJobItem(action, tcs));
            return tcs.Task;
        }

        public IEnumerable<object> Start()
        {
            var result = new List<object>();
            int done = 0;
            while (_queue.Any(p => !p.Done))
            {
                Parallel.ForEach(_queue, () => new List<object>(), (idx, loopState, threadLocalResult) => {
                    var dequeued = _queue.TryTake(out var queueJobItem);

                    if (queueJobItem.Job is QueueAction)
                    {
                        (queueJobItem.Job as QueueAction).Handler(queueJobItem.Job.Payload);
                    }
                    else if (queueJobItem.Job is QueueFunction)
                    {
                        var res = (queueJobItem.Job as QueueFunction).Handler(queueJobItem.Job.Payload);
                        threadLocalResult.Add(res);
                    }

                    queueJobItem.Done = true;
                    return threadLocalResult;
                }, async (threadResult) => {
                    lock (_lock)
                    {
                        result.AddRange(threadResult);
                    }
                });

                done += _batchSize;
                _eventBus.Publish(CreateProgressEvent(done, _count));
            }

            return result;
        }

        private ProgressEvent CreateProgressEvent(int done, int from)
            => new ProgressEvent(PROGRESS_EVENT_NAME, done, from);
    }

    internal class ProgressEvent : IAppEvent
    {
        public string Name { get; private set; }

        public double Percentage => Done / Count;
        public int Done { get; private set; }
        public int Count { get; private set; }

        public ProgressEvent(string name, int done, int count)
        {
            Name = name;
            Done = done;
            Count = count;
        }

        public override string ToString()
            => $"{Done}/({Count})";
    }
}
