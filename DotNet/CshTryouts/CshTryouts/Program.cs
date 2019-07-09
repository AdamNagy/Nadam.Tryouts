using System;
using CshTryouts.EnumerablePattern;

namespace CshTryouts
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var fibn in YieldUseage.Fibonacci(10))
            {
                Console.Write($"{fibn} ");
            }

            Console.ReadKey();
        }
    }
}
