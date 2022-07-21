/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            var random = new Random();

            var taskA = new Task<IEnumerable<int>>(() => Enumerable.Range(0, 10).Select((p) => random.Next(10)));
            // taskA.Start();

            var taskB = taskA.ContinueWith((randoms) => randoms.Result.Select(p => p * random.Next(10)));
            var taskC = taskB.ContinueWith((randoms) => { randoms.Result.ToList().Sort(); return randoms.Result; });
            var taskD = taskC.ContinueWith((sortedRandoms) => sortedRandoms.Result.ToList().Average());

            taskA.Start(); // if task is is started here, program does not work
            var result = taskD.Result;
            

            Console.WriteLine($"Resut is {result}");
            Console.ReadLine();
        }
    }
}
