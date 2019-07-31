using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuideToCollections;

namespace Driver
{
    class Program
    {

        #region Helpers

        static string ArrayToString(Array array)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            if (array.Length > 0)
            {
                sb.Append(array.GetValue(0));
            }
            for (int i = 1; i < array.Length; ++i)
            {
                sb.AppendFormat(",{0}", array.GetValue(i));
            }
            sb.Append("]");

            return sb.ToString();
        }
        
        #endregion

        #region Lessons

        #region Lesson3A Helpers

        static void AddNumber(AssociativeArrayHT<string, string> phoneBook)
        {
            Console.WriteLine("Enter the name to add");
            Console.Write("> ");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            Console.WriteLine("Enter their phone number");
            Console.Write("> ");
            string number = Console.ReadLine();

            if (string.IsNullOrEmpty(number))
            {
                return;
            }

            phoneBook[name] = number;

            Console.WriteLine("'{0}' phone number is {1}", name, phoneBook[name]);
        }
        
        static void Show(AssociativeArrayHT<string, string> phoneBook)
        {
            string[] names = phoneBook.Keys;

            for (int i = 0; i < names.Length; ++i)
            {
                Console.WriteLine("'{0}' phone number is {1}", names[i], phoneBook[names[i]]);
            }
        }

        static void LookupNumber(AssociativeArrayHT<string, string> phoneBook)
        {
            Console.WriteLine("Enter name to lookup");
            Console.Write("> ");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (phoneBook.ContainsKey(name))
            {
                Console.WriteLine("'{0}' phone number is {1}", name, phoneBook[name]);
            }
            else
            {
                Console.WriteLine("Couldn't find '{0}'", name);
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("1. Add Name");
            Console.WriteLine("2. Lookup Number");
            Console.WriteLine("3. Show Phonebook");
            Console.WriteLine("4. Exit");
            Console.Write("> ");
        }
        
        #endregion

        static void Lesson3A()
        {
            AssociativeArrayHT<string, string> phoneBook = new AssociativeArrayHT<string, string>(StringComparer.CurrentCultureIgnoreCase);
            string line;
            for (; ; )
            {
                PrintMenu();

                line = Console.ReadLine();
                line = line.Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                switch (line[0])
                {
                    case '1':
                        AddNumber(phoneBook);
                        break;
                    case '2':
                        LookupNumber(phoneBook);
                        break;
                    case '3':
                        Show(phoneBook);
                        break;
                    case '4':
                        return;
                }

            }
        }

        #endregion
        
        static void PrintOptions()
        {
            Console.WriteLine("1. Lesson 3A");
            Console.WriteLine("2. Test Collections");
            Console.WriteLine("3. Exit");
            Console.Write("> ");
        }

        static void Main(string[] args)
        {
            string option = "";

            for (; ; )
            {
                PrintOptions();

                option = Console.ReadLine();

                Console.WriteLine();
                if (option == "1")
                {
                    Console.WriteLine("***Lesson 3A***");
                    Lesson3A();
                }
                else if (option == "2")
                {
                    Console.WriteLine("***Testing Collections***");
                    UnitTests.RunTests();
                    Console.WriteLine("Completed");
                }
                else if (option == "3")
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
