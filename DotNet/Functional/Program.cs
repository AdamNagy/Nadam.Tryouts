using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    class Program
    {
        static void Main(string[] args)
        {
            var siteCrackers = new SiteCrackerContainer();

            // ReadInt("Give me a number");
            // var greet = CreateGreetings("Csá");
            // Console.WriteLine(greet("Adam"));

            // Console.WriteLine(Get_ReadString("csumika")());

            Console.ReadKey();
        }

        public static string ReadString(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public static Func<string> Get_ReadString(string text)
        {
            return () =>
            {
                Console.WriteLine(text);
                return Console.ReadLine();
            };
        }

        public static string ReadString(string text, Func<string, bool> pred)
        {
            Console.WriteLine(text);
            var value = Console.ReadLine();

            while (!pred(value))
            {
                value = Console.ReadLine();
            }

            return value;
        }

        public static int ReadInt(string text)
        {
            Console.WriteLine(text);
            int value;
            while (!Int32.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine(text);
            }

            return value;
        }

        public static bool ReadBoolean(string text)
        {
            Console.WriteLine(text);
            bool value;
            while (!ParseBool(Console.ReadLine(), out value))
            {
                Console.WriteLine(text);
            }

            return value;
        }

        public static bool ParseBool(string text, out bool result)
        {
            switch (text.ToLower())
            {
                case "y":
                case "yes":
                case "igen":
                case "true":
                    result = true;
                    return true;

                case "n":
                case "no":
                case "nem":
                case "false":
                    result = false;
                    return true;

                default:
                    result = false;
                    return false;
            }
        }

        public static Func<string, string> CreateGreetings(string greeting)
            => (name) => $"{greeting} {name}";
        
    }
}
