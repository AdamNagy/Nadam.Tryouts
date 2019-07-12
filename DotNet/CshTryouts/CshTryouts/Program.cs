using System;
using CshTryouts.EnumerablePattern;

namespace CshTryouts
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var fibn in YieldUseage.Fibonacci(10))
                Console.Write($"{fibn} ");

            ValueAnRefTypeDemo();

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
