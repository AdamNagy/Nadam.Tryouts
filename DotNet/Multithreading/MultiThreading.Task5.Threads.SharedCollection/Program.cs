/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static ConcurrentList<string> ConcurrentList = new ConcurrentList<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            for (int i = 0; i < 2; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientRobot));
            }

            Process();

            var customMessage = Console.ReadLine();
            while(customMessage.ToLower() != "exit")
            {
                ConcurrentList.Add($"Custom message: {customMessage}");
                customMessage = Console.ReadLine();
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void ClientRobot(object obj)
        {
            var rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                var next = rnd.Next(1, 10);
                Console.WriteLine($"Thred {Thread.CurrentThread.ManagedThreadId} added {next}");
                ConcurrentList.Add($"id: {next}");
                Thread.Sleep(next * 1000);  // simulate http request incoming
            }
        }

        private async static void Process()
        {
            await foreach (var item in ConcurrentList.Subscribe())
            {
                Console.WriteLine($"Message retrieved: {item}");
            }
        }
    }

    public class ConcurrentList<T>
    {
        private readonly object _locker = new object();
        private TaskCompletionSource<T> next;

        public ConcurrentList()
        {
            next = new TaskCompletionSource<T>();
        }

        public void Add(T addedItem)
        {
            lock (_locker)
            {
                next.SetResult(addedItem);
                next = new TaskCompletionSource<T>();
            }
        }

        public async IAsyncEnumerable<T> Subscribe()
        {
            while(true)
            {
                var nextItem = await next.Task;
                yield return nextItem;
            }
        }
    }

    public class Subscribtion<T>
    {
        public bool HasMore { get; private set; }
        public TaskCompletionSource<T> Next { get; private set; }

        public async void ReadNext()
        {
        }

        public T Current { get; private set; }
    }

    public class Observable<T>
    {
        private readonly Queue<Task<T>> _tasks
            = new Queue<Task<T>>();

        private readonly List<Action<T>> _actions
            = new List<Action<T>>();

        public void Next(TaskCompletionSource<T> next)
        {
            next.Task.ContinueWith(p =>
            {
                foreach (var action in _actions)
                {
                    var endInvokeCallback = new AsyncCallback(ar => (ar.AsyncState as Action).EndInvoke(ar));
                    action.BeginInvoke(p.Result, endInvokeCallback, null);
                }
            });
        }

        public void Next(T next)
        {
            foreach (var action in _actions)
            {
                var endInvokeCallback = new AsyncCallback(ar => (ar.AsyncState as Action).EndInvoke(ar));
                action.BeginInvoke(next, endInvokeCallback, null);
            }
        }

        public void Subscribe(Action<T> callback)
        {
            _actions.Add(callback);
        }

        public static void HowToUse(Observable<string> obs)
        {
            obs.Subscribe(Console.WriteLine);
        }
    }

    class ItemAddedEventArgs<T> : EventArgs
    {
        public T Item { get; set; }

        public ItemAddedEventArgs(T i)
        {
            Item = i;
        }
    }
}
