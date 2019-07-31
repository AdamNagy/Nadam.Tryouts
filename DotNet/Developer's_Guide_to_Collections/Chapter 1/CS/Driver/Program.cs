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

        static void Lesson1A()
        {
            Random rnd = new Random();
            ArrayEx<int> array = new ArrayEx<int>();

            for (int i = 0; i < 20; ++i)
            {
                array.Add(rnd.Next(100));
            }

            Console.WriteLine("Sorting the following list");
            Console.WriteLine(ArrayToString(array.ToArray()));

            for (int i = 0; i < array.Count; ++i)
            {
                for (int j = i + 1; j < array.Count; ++j)
                {
                    if (array[i] > array[j])
                    {
                        int tmp = array[j];
                        array[j] = array[i];
                        array[i] = tmp;
                    }
                }
            }

            Console.WriteLine("The sorted list is");
            Console.WriteLine(ArrayToString(array.ToArray()));
        }

        static void Lesson2A()
        {
            Random rnd = new Random();
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();

            Console.WriteLine("Adding to the list...");

            for (int i = 0; i < 10; ++i)
            {
                // Get the value to add
                int nextValue = rnd.Next(100);

                Console.Write("{0} ", nextValue);

                bool added = false;

                // Traverse the list until you find the item greater than nextValue.
                for (DoubleLinkedListNode<int> curr = list.Head; curr != null; curr = curr.Next)
                {


                    // If the item is less than the current value you need to insert item before the
                    // current node.
                    if (nextValue < curr.Data)
                    {
                        list.AddBefore(curr, nextValue);

                        // Mark the item as added
                        added = true;

                        // Exit the loop
                        break;
                    }
                }

                // If the item has not been added to the list the item is either greater than 
                // all items in the list or the list is empty. In either case the item should be
                // added to the end of the list.
                if (!added)
                {
                    list.AddToEnd(nextValue);
                }
            }

            Console.WriteLine();

            Console.WriteLine("The sorted list is");
            Console.WriteLine(ArrayToString(list.ToArray()));
        }

        #endregion

        static void PrintOptions()
        {
            Console.WriteLine("1. Lesson 1A");
            Console.WriteLine("2. Lesson 2A");
            Console.WriteLine("3. Test Collections");
            Console.WriteLine("4. Exit");
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
                    Console.WriteLine("***Lesson 1A***");
                    Lesson1A();
                }
                else if (option == "2")
                {
                    Console.WriteLine("***Lesson 2A***");
                    Lesson2A();
                }
                else if (option == "3")
                {
                    Console.WriteLine("***Testing Collections***");
                    UnitTests.RunTests();
                    Console.WriteLine("Completed");
                }
                else if (option == "4")
                {
                    break;
                }
                Console.WriteLine();
            }

        }
    }
}
