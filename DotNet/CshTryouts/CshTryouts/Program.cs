using System;
using CshTryouts.EnumerablePattern;

namespace CshTryouts
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var fibn in YieldUseage.Fibonacci(100))
            {
                Console.Write($"{fibn} ");
            }

            Console.ReadKey();
        }


    }
}
