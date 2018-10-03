using System;
using System.Collections.Generic;

namespace TechnologyTryouts
{
    class ForeachShit
    {
        public static void ModuleMain()
        {
            var arr = new List<int> { 1, 2, 3, 4, 5, 6 };

            Func<int, int> dublicate = szor2;
            Action<int> dublicate2 = szor2void;


            arr.ForEach(p => Console.Write($"{p} "));
            arr.ForEach(p => 
                {
                    p *= 2;
                }
            );
            Console.WriteLine();
            arr.ForEach(p => Console.Write($"{p} "));

            arr.ForEach(p => szor2(p));
            Console.WriteLine();
            arr.ForEach(p => Console.Write($"{p} "));

            arr.ForEach(dublicate2);
            Console.WriteLine();
            arr.ForEach(p => Console.Write($"{p} "));
        }

        private static int szor2(int i)
        {
            return i * 2;
        }

        private static void szor2void(int i)
        {
            i *= 2;
        }
    }
}
