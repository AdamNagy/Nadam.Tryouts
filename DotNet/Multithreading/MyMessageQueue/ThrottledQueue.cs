using System.Collections.Concurrent;

namespace MyMessageQueue
{
    internal class ThrottledQueue
    {
        private readonly ConcurrentQueue<QueueJobItem> _queue = new ConcurrentQueue<QueueJobItem>();
        private readonly int _maxQueueSize = 0;
        private readonly int _batchSize = 0;

        private bool running;
        private int delay = 1000;
        public ThrottledQueue()
        {

        }

        public ThrottledQueue(int queueSize, int batchSize)
        {
            _maxQueueSize = queueSize;
            _batchSize = batchSize;
        }

        public ThrottledQueue(IEnumerable<QueueJob> jobs, int batchSize)
        {
            _batchSize= batchSize;

            foreach (var job in jobs)
            {
                if( job is QueueAction)
                    Enque(job as QueueAction);
                else if( job is QueueFunction)
                    Enque(job as QueueFunction);
            }
        }

        public Task Enque(QueueAction action)
        {
            var tcs = new TaskCompletionSource<object>();
            if (_maxQueueSize > 0)
            {
                if (_queue.Count < _maxQueueSize)
                {
                    _queue.Enqueue(new QueueJobItem(action, tcs));
                }
                else
                {
                    Console.WriteLine("No space for new messaga. Please wait.");
                }
            }
            else
            {
                _queue.Enqueue(new QueueJobItem(action, tcs));
            }

            return tcs.Task;
        }

        public Task<object> Enque(QueueFunction action)
        {
            var tcs = new TaskCompletionSource<object>();
            if (_maxQueueSize > 0)
            {
                if (_queue.Count < _maxQueueSize)
                {
                    _queue.Enqueue(new QueueJobItem(action, tcs));
                    return tcs.Task;
                }

                throw new Exception("Queue is full");
            }
            else
            {
                _queue.Enqueue(new QueueJobItem(action, tcs));
                return tcs.Task;
            }
        }

        public async void Start()
        {
            running = true;
            while (running)
            {
                while (!_queue.Any() || (_batchSize > 0 && _queue.Count < _batchSize))
                    await Task.Delay(delay);

                var currentBatchSize = _queue.Count;
                if ( _batchSize > 0 )
                {
                    currentBatchSize = Math.Min(_queue.Count, _batchSize);
                }
    
                Parallel.For(0, currentBatchSize, (idx) => {
                    var dequeued = _queue.TryDequeue(out var queueJobItem);

                    if (queueJobItem.Job is QueueAction)
                    {
                        (queueJobItem.Job as QueueAction).Handler(queueJobItem.Job.Payload);
                        queueJobItem.Result.SetResult(null);
                    }
                    else if(queueJobItem.Job is QueueFunction)
                    {
                        var res = (queueJobItem.Job as QueueFunction).Handler(queueJobItem.Job.Payload);
                        queueJobItem.Result.SetResult(res);
                    }
                });
            }
        }

        public void Pause()
        {
            running = false;            
        }
    }

    internal abstract class QueueJob
    {
        public object Payload { get; private set; }

        public QueueJob(object payload)
        {
            Payload = payload;
        }
    }

    internal class QueueFunction : QueueJob
    {
        public Func<object, object> Handler { get; set; }

        public QueueFunction(object payload, Func<object, object> handler) : base(payload)
        {
            Handler = handler;
        }

        public QueueFunction(object payload, Func<object, Task<object>> handler) : base(payload)
        {
            Handler = handler;
        }
    }

    internal class QueueAction : QueueJob
    {
        public Action<object> Handler { get; set; }

        public QueueAction(object payload, Action<object> handler) : base(payload)
        {
            Handler = handler;
        }
    }

    internal class QueueJobItem
    {
        public QueueJob Job { get; private set; }
        public TaskCompletionSource<object> Result { get; set; }
        public bool Done { get; set; } = false;

        public QueueJobItem(QueueJob job, TaskCompletionSource<object> result)
        {
            Job = job;
            Result = result;
        }
    }
}
