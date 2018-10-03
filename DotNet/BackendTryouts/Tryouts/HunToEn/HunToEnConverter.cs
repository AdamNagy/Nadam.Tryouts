using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyTryouts.HunToEn
{
    class HunToEnConverter
    {
        public static void ModuleMain()
        {
            var path = @"C:\Documents\GitRepositories\Nadam.Dcms\MigrationPickup\shops.txt";
            string text = System.IO.File.ReadAllText(path, Encoding.GetEncoding("iso-8859-15"));
            var lines = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("iso-8859-15"));
            Console.WriteLine(text);
        }
    }
}
