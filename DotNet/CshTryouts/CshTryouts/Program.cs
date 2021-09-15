using System;
using System.Collections.Generic;
using System.Linq;
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
            var i = 0;
            foreach (var item in LazyQueryable4().Skip(2).Take(3))
            {
                ++i;
            }

            Console.WriteLine($"\n{i} run happend");
            Console.ReadKey();
        }

        #region Lazy collection and queryable
        static IEnumerable<int> LazyReading()
        {
            Console.WriteLine("LazyReading");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i} ");
                yield return i;
            }

            yield break;
        }

        static IQueryable<int> LazyQueryable()
            => LazyReading().AsQueryable();

        static IQueryable<int> LazyQueryable2()
        {
            var list = LazyReading();

            return list.Take(5).AsQueryable();
        }


        static IQueryable<int> LazyQueryable3()
        {
            var list = new List<int>();

            foreach (var item in LazyReading())
            {
                list.Add(item * 2);
            }

            return list.AsQueryable();
        }

        static IQueryable<int> LazyQueryable4()
        {
            var list = new List<int>();

            var query = LazyReading().Select(p => 2 * p);

            return query.AsQueryable();
        }
        #endregion

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
