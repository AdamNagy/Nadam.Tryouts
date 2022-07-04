using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContextSwitching
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(WriteUsingNewThread);
            thread.Name = "Cazton Worker";
            //Worker Thread
            thread.Start();
            //Main Thread
            Thread.CurrentThread.Name = "Cazton main";
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(" A" + i + " ");
            }
        }

        private static void WriteUsingNewThread()
        {
            for(int i=0; i<1000; i++)
            {
                Console.Write(" Z" + i + " ");
            }
        }
    }
}
