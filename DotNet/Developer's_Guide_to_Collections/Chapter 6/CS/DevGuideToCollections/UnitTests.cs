using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public static class UnitTests
    {
        public static void RunTests()
        {
            TestArrayEx();
            TestAssociativeArrayAL();
            TestAssociativeArrayHT();
            TestQueuedArray();
            TestQueuedLinkedList();
            TestSingleLinkedList();
            TestDoubleLinkedList();
            TestCircularBuffer();
            TestStackedArray();
            TestStackedLinkedList();
        }

        static void TestAssociativeArrayAL()
        {
            AssociativeArrayAL<string, int> aa = new AssociativeArrayAL<string, int>();

            ArrayEx<KeyValuePair<string, int>> kvps = new ArrayEx<KeyValuePair<string, int>>(new KeyValuePair<string, int>[] 
                {
                    new KeyValuePair<string, int>("uno",1),
                    new KeyValuePair<string, int>("dos",2),
                    new KeyValuePair<string, int>("one",1),
                    new KeyValuePair<string, int>("ichi",1),
                    new KeyValuePair<string, int>("ni",2),
                    new KeyValuePair<string, int>("two",2),
                    new KeyValuePair<string, int>("three",3),
                    new KeyValuePair<string, int>("four",4)
                });

            foreach (KeyValuePair<string, int> kvp in kvps)
            {
                aa[kvp.Key] = kvp.Value;
            }
            System.Diagnostics.Debug.Assert(aa.Count == kvps.Count);

            // Make sure all values could be enumerated
            int count = 0;
            foreach (KeyValuePair<string, int> kvp in aa)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == kvp.Key && test.Value == kvp.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key value pair");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(count == kvps.Count);

            // Recheck the list after removing one
            KeyValuePair<string, int> removed = kvps[3];
            kvps.RemoveAt(3);
            aa.Remove(removed.Key);
            count = 0;
            foreach (KeyValuePair<string, int> kvp in aa)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == kvp.Key && test.Value == kvp.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key value pair");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(count == kvps.Count);

            // Recheck the list after adding one
            kvps.Add(new KeyValuePair<string, int>("five", 5));
            aa.Add("five", 5);
            count = 0;
            foreach (KeyValuePair<string, int> kvp in aa)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == kvp.Key && test.Value == kvp.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key value pair");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(count == kvps.Count);

            // Check the keys
            foreach (string key in aa.Keys)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == key)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(aa.Keys.Count == kvps.Count);

            // Check the values
            foreach (int value in aa.Values)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Value == value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find value");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(aa.Values.Count == kvps.Count);

            // Modify collection while traversing
            try
            {
                foreach (KeyValuePair<string, int> test in aa)
                {
                    aa.Remove(test.Key);
                }
                System.Diagnostics.Debug.Assert(false, "Didn't warn user that the collection changed");
            }
            catch (InvalidOperationException)
            {
            }

            // Modify collection while traversing keys
            try
            {
                foreach (string key in aa.Keys)
                {
                    aa.Remove(key);
                }
                System.Diagnostics.Debug.Assert(false, "Didn't warn user that the collection changed");
            }
            catch (InvalidOperationException)
            {
            }

            // Modify collection while traversing values
            try
            {
                foreach (int value in aa.Values)
                {
                    aa.RemoveValue(value);
                }
                System.Diagnostics.Debug.Assert(false, "Didn't warn user that the collection changed");
            }
            catch (InvalidOperationException)
            {
            }
        }

        static void TestAssociativeArrayHT()
        {
            AssociativeArrayHT<string, int> aa = new AssociativeArrayHT<string, int>();

            ArrayEx<KeyValuePair<string, int>> kvps = new ArrayEx<KeyValuePair<string, int>>(new KeyValuePair<string, int>[] 
                {
                    new KeyValuePair<string, int>("uno",1),
                    new KeyValuePair<string, int>("dos",2),
                    new KeyValuePair<string, int>("one",1),
                    new KeyValuePair<string, int>("ichi",1),
                    new KeyValuePair<string, int>("ni",2),
                    new KeyValuePair<string, int>("two",2),
                    new KeyValuePair<string, int>("three",3),
                    new KeyValuePair<string, int>("four",4)
                });

            foreach (KeyValuePair<string, int> kvp in kvps)
            {
                aa[kvp.Key] = kvp.Value;
            }
            System.Diagnostics.Debug.Assert(aa.Count == kvps.Count);

            // Make sure all values could be enumerated
            int count = 0;
            foreach (KeyValuePair<string, int> kvp in aa)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == kvp.Key && test.Value == kvp.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key value pair");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(count == kvps.Count);

            // Recheck the list after removing one
            KeyValuePair<string, int> removed = kvps[3];
            kvps.RemoveAt(3);
            aa.Remove(removed.Key);
            count = 0;
            foreach (KeyValuePair<string, int> kvp in aa)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == kvp.Key && test.Value == kvp.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key value pair");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(count == kvps.Count);

            // Recheck the list after adding one
            kvps.Add(new KeyValuePair<string, int>("five", 5));
            aa.Add("five", 5);
            count = 0;
            foreach (KeyValuePair<string, int> kvp in aa)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == kvp.Key && test.Value == kvp.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key value pair");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(count == kvps.Count);

            // Check the keys
            foreach (string key in aa.Keys)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Key == key)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find key");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(aa.Keys.Count == kvps.Count);

            // Check the values
            foreach (int value in aa.Values)
            {
                bool found = false;

                foreach (KeyValuePair<string, int> test in kvps)
                {
                    if (test.Value == value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Diagnostics.Debug.Assert(false, "Couldn't find value");
                }
                ++count;
            }
            System.Diagnostics.Debug.Assert(aa.Values.Count == kvps.Count);

            // Modify collection while traversing
            try
            {
                foreach (KeyValuePair<string, int> test in aa)
                {
                    aa.Remove(test.Key);
                }
                System.Diagnostics.Debug.Assert(false, "Didn't warn user that the collection changed");
            }
            catch (InvalidOperationException)
            {
            }

            // Modify collection while traversing keys
            try
            {
                foreach (string key in aa.Keys)
                {
                    aa.Remove(key);
                }
                System.Diagnostics.Debug.Assert(false, "Didn't warn user that the collection changed");
            }
            catch (InvalidOperationException)
            {
            }

            // Modify collection while traversing values
            try
            {
                foreach (int value in aa.Values)
                {
                    aa.RemoveValue(value);
                }
                System.Diagnostics.Debug.Assert(false, "Didn't warn user that the collection changed");
            }
            catch (InvalidOperationException)
            {
            }
        }

        static void TestCircularBuffer()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            CircularBuffer<int> list = new CircularBuffer<int>(10, values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[i] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // Check the copyto method

            int[] buffer = new int[20];
            try
            {
                ((System.Collections.ICollection)list).CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(new int[10, 21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            ((System.Collections.ICollection)list).CopyTo(buffer, 0);
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            i = 10;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }

            // Try the traverser in the middle of the list
            list.Push(2);
            for (int j = 0; j < values.Length; ++j)
            {
                list.Pop();
            }
            for (int j = 1; j < values.Length; ++j)
            {
                list.Push(values[j]);
            }

            // Check the data that was copied
            ((System.Collections.ICollection)list).CopyTo(buffer, 2);
            i = 2;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }
        }

        static void TestArrayEx()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            ArrayEx<int> array = new ArrayEx<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in array)
            {
                System.Diagnostics.Debug.Assert(values[i] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(array.Count == values.Length);

            // Check the copyto method

            int[] buffer = new int[20];
            try
            {
                array.CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                array.CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)array).CopyTo(new int[10,21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            array.CopyTo(buffer, 0);
            array.CopyTo(buffer, 10);
            try
            {
                array.CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            array.CopyTo(buffer, 10);
            i = 10;
            foreach (int value in array)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }

            // Check Casting
            ((System.Collections.IList)array).Add((object)1);

            try
            {
                ((System.Collections.IList)array).Add((object)1.1);
                System.Diagnostics.Debug.Assert(false, "ArgumentException not thrown");
            }
            catch (ArgumentException)
            {
            }
        
            try
            {
                ((System.Collections.IList)array).Add((object)new Exception());
                System.Diagnostics.Debug.Assert(false, "ArgumentException not thrown");
            }
            catch (ArgumentException)
            {
            }

            List<Exception> exceptions = new List<Exception>();
            ((System.Collections.IList)exceptions).Add(new Exception());
            ((System.Collections.IList)exceptions).Add(new ArgumentException());

        }

        static void TestQueuedLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            QueuedLinkedList<int> list = new QueuedLinkedList<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[i] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // The copyto uses the linked list so there isn't a need to test it
        }

        static void TestQueuedArray()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            QueuedArray<int> list = new QueuedArray<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[i] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // Check the copyto method

            int[] buffer = new int[20];
            try
            {
                ((System.Collections.ICollection)list).CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(new int[10, 21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            ((System.Collections.ICollection)list).CopyTo(buffer, 0);
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            i = 10;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }

            // Try the traverser in the middle of the list
            list.Push(2);
            for (int j = 0; j < values.Length; ++j)
            {
                list.Pop();
            }
            for (int j = 1; j < values.Length; ++j)
            {
                list.Push(values[j]);
            }

            // Check the data that was copied
            ((System.Collections.ICollection)list).CopyTo(buffer, 2);
            i = 2;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }
        }

        static void TestStackedArray()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            StackedArray<int> list = new StackedArray<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[values.Length - i - 1] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // Check the copyto method
            int[] buffer = new int[20];
            try
            {
                ((System.Collections.ICollection)list).CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(new int[10, 21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            ((System.Collections.ICollection)list).CopyTo(buffer, 0);
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            i = 10;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }
        }

        static void TestStackedLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            StackedLinkedList<int> list = new StackedLinkedList<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[values.Length - i - 1] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // Check the copyto method
            int[] buffer = new int[20];
            try
            {
                ((System.Collections.ICollection)list).CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(new int[10, 21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            ((System.Collections.ICollection)list).CopyTo(buffer, 0);
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            try
            {
                ((System.Collections.ICollection)list).CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            ((System.Collections.ICollection)list).CopyTo(buffer, 10);
            i = 10;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }
        }

        static void TestSingleLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            SingleLinkedList<int> list = new SingleLinkedList<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[i] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // Check the copyto method

            int[] buffer = new int[20];
            try
            {
                list.CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                list.CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(new int[10, 21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            list.CopyTo(buffer, 0);
            list.CopyTo(buffer, 10);
            try
            {
                list.CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            list.CopyTo(buffer, 10);
            i = 10;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }
        }

        static void TestDoubleLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            DoubleLinkedList<int> list = new DoubleLinkedList<int>(values);

            // Check the enumerator
            int i = 0;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(values[i] == value);
                ++i;
            }
            System.Diagnostics.Debug.Assert(list.Count == values.Length);

            // Check the copyto method

            int[] buffer = new int[20];
            try
            {
                list.CopyTo(null, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentNullException not thrown");
            }
            catch (ArgumentNullException)
            { }

            try
            {
                list.CopyTo(buffer, -1);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException)
            { }

            try
            {
                ((System.Collections.ICollection)list).CopyTo(new int[10, 21], 0);
                System.Diagnostics.Debug.Assert(false, "RankException not thrown");
            }
            catch (RankException)
            { }

            list.CopyTo(buffer, 0);
            list.CopyTo(buffer, 10);
            try
            {
                list.CopyTo(buffer, 15);
                System.Diagnostics.Debug.Assert(false, "ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentException)
            { }

            // Check the data that was copied
            list.CopyTo(buffer, 10);
            i = 10;
            foreach (int value in list)
            {
                System.Diagnostics.Debug.Assert(buffer[i] == value);
                ++i;
            }
        }
    }
}
