using Nadam.Infrastructure.GenericGetterSetter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static void Test2()
        {
            var obj = new TestObject();
            obj.TestProperty = 17;
            obj.TestVariable = 71;
            var cache = TypeCache.Get(obj.GetType());

            var s = cache.Get<int>(obj, "TestVariable");
            cache.Set<int>(obj, "TestVariable", 42);
            
            Console.ReadKey();
        }
    }
}
