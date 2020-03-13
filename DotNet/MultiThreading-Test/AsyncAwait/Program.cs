using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new Program();
            Console.WriteLine("Before async method");
            controller.DoSomethingAsync();
            Console.WriteLine("After async method");
            Console.ReadKey();
        }

        public async Task DoSomethingAsync()
        {
            Console.WriteLine("In async method");
            await Wait();
        }

        public async Task Wait()
        {
            Console.WriteLine("In wait");
            await Task.Delay(5000);
        }
    }
}
