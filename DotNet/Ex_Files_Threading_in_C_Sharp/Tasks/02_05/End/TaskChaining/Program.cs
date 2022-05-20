using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskChaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> antecedent = Task.Run(() =>
            {
                Task.Delay(2000);
                return DateTime.Today.ToShortDateString();
            });
            Task<string> continuation = antecedent.ContinueWith(x =>
            {
                return "Today is " + antecedent.Result;
            });
            Console.WriteLine("This will display before the result");
            Console.WriteLine(continuation.Result);
        }
    }
}
