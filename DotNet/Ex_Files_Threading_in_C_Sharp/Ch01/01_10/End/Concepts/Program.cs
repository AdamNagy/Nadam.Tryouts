using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Concepts
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(PrintHelloWorld);
            thread.Start();
            thread.IsBackground = true;
            thread.Join();
            Console.WriteLine("Hello World printed");
        }

        private static void PrintHelloWorld()
        {
            Console.WriteLine("Hello World");
            Thread.Sleep(5000);
        }
    }
}
