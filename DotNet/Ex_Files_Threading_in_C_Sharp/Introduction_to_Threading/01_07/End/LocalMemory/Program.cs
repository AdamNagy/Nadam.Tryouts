using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            //Worker Thread
            new Thread(PrintOneToThirty).Start();

            //Main Thread
            PrintOneToThirty();
        }

        private static void PrintOneToThirty()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.Write(i + 1 + " ");
            }
        }
    }
}
