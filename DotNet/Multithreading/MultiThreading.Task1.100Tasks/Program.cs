/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 5000000;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            // feel free to add your code here
            var tasks = Enumerable.Range(1, TaskAmount)
                .Select(p => Task.Factory.StartNew(() => Job(p)))
                .ToArray();

            Debug.WriteLine("Wait all");
            Task.WaitAll(tasks);
        }

        static void Job(int jobId)
        {
            Debug.WriteLine($"{jobId} start");
            // Thread.Sleep(2000);
            // Task.Delay(20000).Wait();
            Thread.Sleep(20000);
            foreach (var iteration in Enumerable.Range(1, MaxIterationsCount));
            //    Output(jobId, iteration);
            Debug.WriteLine($"{jobId} ended");
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
