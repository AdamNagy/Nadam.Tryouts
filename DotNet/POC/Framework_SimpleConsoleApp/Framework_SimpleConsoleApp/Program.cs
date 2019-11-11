using System;
using B_Project;

namespace Framework_SimpleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new CRepository().Get();
            Console.ReadKey();
        }
    }
}
