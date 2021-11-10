using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public static class MessageQueue
    {
        private static ConcurrentQueue<string> _messages;
        private static ConcurrentQueue<Task<dynamic>> _dynamicMessages;

        public static Task<TOut> Push<TOut, TIn>(Func<TIn, TOut> job, TIn arg)
        {
            var jobTask = new Task<TOut>(() => job(arg));
            _dynamicMessages.Enqueue(jobTask);
            return jobTask;
        }

        public static void Push(string newMessage)
        {
            _messages.Enqueue(newMessage);
        }

        public static void Init(object v)
        {
            _messages = new ConcurrentQueue<string>();
            while (true)
            {
                Process();
                Thread.Sleep(5000);
            }
        }

        public static void Process()
        {
            Console.WriteLine($"Processing {_messages.Count()}");
            while(_messages.Any())
            {
                _messages.TryDequeue(out var toProcess);
                Console.WriteLine(toProcess);
            }
        }
    }
}
