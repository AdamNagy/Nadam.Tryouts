/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static SemaphoreSlim semaphore;

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            semaphore = new SemaphoreSlim(1);
            var payload = new Payload()
            {
                Amount = 100  
            };
            // feel free to add your code
            // A
            var rootThread = ThreadPool.QueueUserWorkItem(new WaitCallback(DecrementWithThreadPool), payload);

            // B
            var root = new Thread(new ParameterizedThreadStart(DecrementerWithThread));
            root.Start(payload);
            root.Join();

            Console.WriteLine("Main Done");
            Console.ReadKey();
        }

        #region A
        private static void DecrementerWithThread(object payloadObj)
        {
            var payload = payloadObj as Payload;
            Console.WriteLine($"amount: {payload.Amount}");
            --payload.Amount;

            if (payload.Amount == 0)
                return;

            var child = new Thread(new ParameterizedThreadStart(DecrementerWithThread));
            child.Start(payload);

            child.Join();
        }
        #endregion

        #region B
        public static void DecrementWithThreadPool(object payloadObj)
        {
            semaphore.Wait();
            var payload = payloadObj as Payload;

            Console.WriteLine($"amount: {payload.Amount}");

            --payload.Amount;

            if (payload.Amount == 0)
            {
                semaphore.Release();
                return;
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(DecrementWithThreadPool), payload);

            semaphore.Release();
        }
        #endregion
    }

    class Payload
    {
        public int Amount { get; set; }
    }
}
