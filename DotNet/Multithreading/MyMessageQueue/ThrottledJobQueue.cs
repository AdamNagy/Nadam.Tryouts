using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageQueue
{
    internal class ThrottledJobQueue
    {
        private readonly ConcurrentQueue<QueueJobItem> _queue;
        private int delay = 1000;
        private readonly int _maxQueueSize;
        private readonly int _batchSize;
        private bool running;

        public ThrottledJobQueue()
        {
            _queue = new ConcurrentQueue<QueueJobItem>();

            _maxQueueSize = 0;
            _batchSize = 0;
        }

        public ThrottledJobQueue(int queueSize, int batchSize)
        {
            _queue = new ConcurrentQueue<QueueJobItem>();

            _maxQueueSize = queueSize;
            _batchSize = batchSize;
        }

        public Task<object> Enque(QueueJob action)
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
                Console.WriteLine($"Have {_queue.Count} in the queue");

                var currentBatchSize = _queue.Count;
                if (_batchSize > 0)
                {
                    currentBatchSize = Math.Min(_queue.Count, _batchSize);
                }

                Parallel.For(0, currentBatchSize, (idx) => {
                    var dequeued = _queue.TryDequeue(out var job);
                    var result = job.Job.Handler(job.Job.Payload);
                    job.Result.SetResult(result);
                });

                Console.WriteLine($"{Environment.NewLine}Left {_queue.Count} in the queue");
                
                if(_queue.Count == 0)
                    await Task.Delay(delay);
            }
        }

        public void Pause()
        {
            running = false;
        }
    }

    internal class QueueJob
    {
        public string Message { get; private set; }
        public object Payload { get; private set; }
        public Func<object, object> Handler { get; set; }

        public QueueJob(string msg, object payload, Func<object, object> handler)
        {
            Message = msg;
            Payload = payload;
            Handler = handler;
        }
    }

    internal class QueueJobItem
    {
        public QueueJob Job { get; private set; }
        public TaskCompletionSource<object> Result { get; set; }

        public QueueJobItem(QueueJob job, TaskCompletionSource<object> result)
        {
            Job = job;
            Result = result;
        }
    }
}
