using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadComputingSharper
{
    class Program
    {
        static void Main(string[] args)
        {
            char input;
            input = Console.ReadKey().KeyChar;

            int a, b, sum, quess;
            var randomGen = new Random();

            while( input != 'q' )
            {
                a = randomGen.Next(1, 20);
                b = randomGen.Next(1, 20);
                sum = a + b;
                Console.WriteLine($"{a} + {b} = ");
                try
                {
                    quess = Convert.ToInt32(Console.ReadLine());
                    while (quess != sum)
                    {
                        Console.WriteLine("Try again");
                        quess = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("Fucken good");
                }
                catch
                {
                    Console.WriteLine("Write a number asshole or press 'q' to exit");
                }
            }
        }
    }
}
