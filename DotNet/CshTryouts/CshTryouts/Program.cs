using System;
using System.Reflection;
using CshTryouts.EnumerablePattern;

namespace CshTryouts
{
    public class Class1
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            Class1 c1 = new Class1();
            //  Show the current module.
            Module m = c1.GetType().Module;
            Console.WriteLine("The current module is {0}.", m.Name);

            //  List all modules in the assembly.
            Assembly curAssembly = typeof(Program).Assembly;
            Console.WriteLine("The current executing assembly is {0}.", curAssembly);

            Module[] mods = curAssembly.GetModules();
            foreach (Module md in mods)
            {
                Console.WriteLine("This assembly contains the {0} module", md.Name);
            }
            Console.ReadLine();

            //foreach (var fibn in YieldUseage.Fibonacci(10))
            //    Console.Write($"{fibn} ");

            //ValueAnRefTypeDemo();

            //var x = new StringNum("123");

            // Console.ReadKey();
        }

        static void ValueAnRefTypeDemo()
        {
            var myNum = 5;
            ValueTypeAsRef_ChangeValue(ref myNum);

            var adam = new Person(){Name = "Adam"};
            RefTypeWithoutRef_ChangingSomeProp(adam);

            RefTypeWithRef_ChangingSomeProp(ref adam);
            RefTypeWithRef_ChangingEntireObj(ref adam);
            RefTypeWithoutRef_ChangingEntireObj(adam);
        }

        static void ValueTypeAsRef_ChangeValue(ref int value)
        {
            value++;
        }

        static void RefTypeWithoutRef_ChangingSomeProp(Person myObj)
        {
            myObj.Name = "Adam2";
        }

        static void RefTypeWithRef_ChangingSomeProp(ref Person myObj)
        {
            myObj.Name = "Adam3";
        }

        static void RefTypeWithRef_ChangingEntireObj(ref Person myObj)
        {
            myObj = new Person(){ Name = "Johy" };
        }

        static void RefTypeWithoutRef_ChangingEntireObj(Person myObj)
        {
            myObj = new Person() { Name = "Wick" };
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}
