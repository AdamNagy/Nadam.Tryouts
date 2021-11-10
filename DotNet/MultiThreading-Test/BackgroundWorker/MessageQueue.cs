using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public class MessageQueue<TJobResult>
    {
        private ConcurrentQueue<Task<TJobResult>> _jobQueue;
        private bool _isRunning = false;

        public MessageQueue()
        {
            _jobQueue = new ConcurrentQueue<Task<TJobResult>>();
        }

        public Task<TJobResult> Push<TIn>(Func<TIn, TJobResult> job, TIn arg)
        {
            var jobTask = new Task<TJobResult>(() => job(arg));
            _jobQueue.Enqueue(jobTask);
            return jobTask;
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.Init));
        }

        public void Stop()
        {

        }

        public void Init(object v)
        {
            if (_isRunning)
                return;

            _isRunning = true;
            while (true)
            {
                Process();
                Thread.Sleep(3000);
            }
        }

        public void Process()
        {
            Console.WriteLine($"Have {_jobQueue.Count()} in the queue");
            var haveMoreJob = _jobQueue.TryDequeue(out var toProcess);
            while(haveMoreJob)
            {
                toProcess.Start();
                haveMoreJob = _jobQueue.TryDequeue(out toProcess);
            }
        }
    }
}
