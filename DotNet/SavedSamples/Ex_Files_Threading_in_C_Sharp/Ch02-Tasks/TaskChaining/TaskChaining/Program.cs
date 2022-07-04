using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskChaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App starts..");
            Task<string> antecedent = Task.Run(() =>
            {
                // Task.Delay(5000);
                // Thread.Sleep(3000);
                return DateTime.Today.ToShortDateString();
            });
            Task<string> continuation = antecedent.ContinueWith(x => "Today is " + antecedent.Result);

            Console.WriteLine("This will display before the result");
            Console.WriteLine(continuation.Result);

            Console.ReadKey();
        }
    }
}
