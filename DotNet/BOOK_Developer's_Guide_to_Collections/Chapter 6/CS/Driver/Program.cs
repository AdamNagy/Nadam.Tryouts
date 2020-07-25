using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevGuideToCollections;

namespace Driver
{
    class Program
    {
        static void ShowEnumerator()
        {
            QueuedLinkedList<int> ints = new QueuedLinkedList<int>();

            Random rnd = new Random();

            for (int i = 0; i < 20; ++i)
            {
                ints.Push(rnd.Next(50));
            }

            var tmp = from s in ints where (s > 10) orderby s select s;

            Range range = new Range(10);

            Console.WriteLine("starting");

            Queue<int> tt = new Queue<int>();


            foreach (int i in range)
            {
                Console.WriteLine("\t\ti={0}", i);
            }

            if (range is IEnumerable<int>)
            {
                IEnumerator<int> enumerator = ((IEnumerable<int>)range).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    int i = enumerator.Current;
                    Console.WriteLine("\t\ti={0}", i);
                }
            }

            Console.WriteLine("finished");
        }

        static void Main(string[] args)
        {
            UnitTests.RunTests();
            ShowEnumerator();
        }
    }
}
