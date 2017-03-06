using static System.Console;

namespace Nadam.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.PrintMenu();
            char input = ReadKey().KeyChar;
            WriteLine();
            while (input != 'q')
            {
                switch (input)
                {
                    case 'a':
                        JsonDbTestConsole.TestRunner();
                        break;
                    case 'b':
                        GraphTestConsole.TestRunner();
                        break;
                    case 'c':
                        MivDbTestConsole.TestRunner();
                        break;
                }
                program.MenuItemEnd();
                Clear();
                program.PrintMenu();
                input = ReadKey().KeyChar;
            }
        }

        private void PrintMenu()
        {
            WriteLine("Main menu");
            WriteLine("a: JsonDbTestConsole");
            WriteLine("b: GraphTestConsole");
            WriteLine("c: Miv Extension Db Test");
            WriteLine("q: Quit");

            Write("Selected: ");
        }

        private void MenuItemEnd()
        {
            WriteLine("\nProgram endied..\n press any key to continue");
        }
    }
}
