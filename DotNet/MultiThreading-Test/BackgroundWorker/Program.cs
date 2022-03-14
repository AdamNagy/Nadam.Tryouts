using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    class Program
    {
        public static MessageQueue<string> jobQueue;
        static void Main(string[] args)
        {
            jobQueue = new MessageQueue<string>();
            ThreadPool.QueueUserWorkItem(new WaitCallback(jobQueue.Init));

            App().Wait();
            Console.WriteLine("exiting");
        }

        public async static Task App()
        {
            var command = "";
            Console.WriteLine("Write message");
            command = Console.ReadLine().ToLower();

            while (command != "exit")
            {
                if (Int32.TryParse(command, out var theNumber))
                {
                    jobQueue.Push<int>((x) => x.ToString(), theNumber)
                        .ContinueWith((job) => Console.WriteLine($"the result is: {job.Result}"));

                    command = Console.ReadLine().ToLower();
                }
                else
                {
                    Console.WriteLine("Please give a number");
                    command = Console.ReadLine().ToLower();
                }
            }
        }
    }
}
