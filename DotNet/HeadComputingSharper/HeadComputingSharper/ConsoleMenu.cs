using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadComputingSharper
{
    public class ConsoleMenu
    {
        public List<string> MenuItems { get; set; }
        public char Input { get; set; }

        public void PrintMenu()
        {
            Console.WriteLine("Main menu");
            MenuItems.ForEach(Console.WriteLine);
        }

    }
}
