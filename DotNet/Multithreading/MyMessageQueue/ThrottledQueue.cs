using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageQueue
{
    internal class ThrottledQueue
    {
        private readonly ConcurrentQueue<QueueAction> _queue;
        private int delay = 5000;
        private readonly int _maxQueueSize;
        private readonly int _batchSize;
        private bool running;

        public ThrottledQueue()
        {
            _queue = new ConcurrentQueue<QueueAction>();

            _maxQueueSize = 0;
            _batchSize = 0;
        }

        public ThrottledQueue(int queueSize, int batchSize)
        {
            _queue = new ConcurrentQueue<QueueAction>();

            _maxQueueSize = queueSize;
            _batchSize = batchSize;
        }

        public void Enque(QueueAction action)
        {
            if (_maxQueueSize > 0)
            {
                if (_queue.Count < _maxQueueSize)
                {
                    _queue.Enqueue(action);
                }
                else
                {
                    Console.WriteLine("No space for new messaga. Please wait.");
                }
            }
            else
            {
                _queue.Enqueue(action);
            }
        }

        public async void Start()
        {
            running = true;
            while (running)
            { 
                Console.WriteLine($"Have {_queue.Count} in the queue");

                var currentBatchSize = _queue.Count;
                if ( _batchSize > 0 )
                {
                    currentBatchSize = Math.Min(_queue.Count, _batchSize);
                }
    
                Parallel.For(0, currentBatchSize, (idx) => {
                    var nextMessage = _queue.TryDequeue(out var message);
                    message.Handler(message.Payload);
                });                

                Console.WriteLine($"{Environment.NewLine}Left {_queue.Count} in the queue");
                await Task.Delay(delay);
            }
        }

        public void Pause()
        {
            running = false;            
        }
    }

    internal class QueueAction
    {
        public string Message { get; private set; }
        public object Payload { get; private set; }
        public Action<object> Handler { get; set; }

        public QueueAction(string msg, object payload, Action<object> handler)
        {
            Message = msg;
            Payload = payload;
            Handler = handler;
        }
    }
}
