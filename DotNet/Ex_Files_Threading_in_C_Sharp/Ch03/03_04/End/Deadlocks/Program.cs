using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlocks
{
    class Program
    {
        
        static void Main(string[] args)
        {
            object caztonLock = new object();
            object chanderLock = new object();
            new Thread(() =>
            {
                lock (caztonLock)
                {
                    Console.WriteLine("Cazton Lock obtained");
                    Thread.Sleep(2000);
                    lock (chanderLock)
                    {
                        Console.WriteLine("Chander Lock obtained");
                    }
                }
            }).Start();
            lock (chanderLock)
            {
                Console.WriteLine("Main Thread obtained Chander Lock");
                Thread.Sleep(1000);
                lock (caztonLock)
                {
                    Console.WriteLine("Main Thread obtained Chander Lock");
                }
            }
        }
    }
}
