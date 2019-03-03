using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {


        static void Main(string[] args)
        {
            var obj = new MyClass()
            {
                Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                Name = "Adam"
            };

            var s = Func(obj);

            var name = "adam";
            Func2(ref name);

            Console.ReadKey();
        }

        private static MyClass Func(MyClass arg1)
        {
            arg1.Name = "Bela";
            arg1.Numbers[3] = 0;
            return arg1;
        }

        private static void Func2(ref string str)
        {
            str = "Hello world";
        }
    }

    class MyClass
    {
        public List<int> Numbers { get; set; }
        public string Name { get; set; }
    }
}
