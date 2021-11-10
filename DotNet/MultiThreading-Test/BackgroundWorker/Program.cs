using System;
using System.Threading;

namespace BackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(MessageQueue.Init));

            var command = "";
            Console.WriteLine("Write message");
            command = Console.ReadLine().ToLower();

            while(command != "exit")
            {
                MessageQueue.Push(command);
                Console.WriteLine("Write message");
                command = Console.ReadLine().ToLower();
            }
        }
    }
}
