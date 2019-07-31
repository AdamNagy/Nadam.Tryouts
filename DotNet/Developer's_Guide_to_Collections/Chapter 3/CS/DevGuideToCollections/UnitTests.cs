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
            TestAssociativeArray();
            TestArrayEx();
            TestSingleLinkedList();
            TestDoubleLinkedList();
            TestQueuedArray();
            TestQueuedLinkedList();
            TestStackedLinkedList();
            TestStackedArray();
            TestCircularBuffer();
        }

        static void TestAssociativeArray()
        {
            TestAssociativeArrayHT();
            TestAssociativeArrayAL();
        }

        static void TestAssociativeArrayAL()
        {
            AssociativeArrayAL<string, int> ht = new AssociativeArrayAL<string, int>();

            ht["one"] = 1;
            ht["two"] = 2;
            ht["three"] = 3;
            ht["four"] = 4;
            ht.Add("five", 5);
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("one"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("two"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("three"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("four"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("five"));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(1));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(2));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(3));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(4));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(5));


            try
            {
                ht.Add("five", 8);
                System.Diagnostics.Debug.Assert(false, "The collection is allowing duplicate keys");
            }
            catch (InvalidOperationException)
            {
            }

            // Make sure the count is still 5
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            // Add a key that has the same value as another key
            ht["uno"] = 30;
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);

            // Check all keys that were added
            System.Diagnostics.Debug.Assert(ht["one"] == 1);
            System.Diagnostics.Debug.Assert(ht["uno"] == 30);
            System.Diagnostics.Debug.Assert(ht["two"] == 2);
            System.Diagnostics.Debug.Assert(ht["three"] == 3);
            System.Diagnostics.Debug.Assert(ht["four"] == 4);
            System.Diagnostics.Debug.Assert(ht["five"] == 5);

            // Check the containskey method
            System.Diagnostics.Debug.Assert(ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));

            // Check the containsvalue method
            System.Diagnostics.Debug.Assert(ht.ContainsValue(1));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(2));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(3));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(30));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(4));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(5));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(44));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(0));

            // Try removing something
            ht.Remove("uno");
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            // Check the containskey method again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("one"));
            System.Diagnostics.Debug.Assert(!ht.Keys.Contains("uno"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("two"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("three"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("four"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("five"));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(1));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(2));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(3));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(4));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(5));

            // Add the correct value for uno
            ht["uno"] = 11;
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);

            // Check the value
            System.Diagnostics.Debug.Assert(ht["uno"] == 11);

            // Check the contains again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(11));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(30));

            // Now really add the correct value
            ht["uno"] = 1;
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);

            // Check the value
            System.Diagnostics.Debug.Assert(ht["uno"] == 1);

            ht.Remove("one");
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            ht.Remove("two");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(2));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("two"));
            ht["dos"] = 2;
            System.Diagnostics.Debug.Assert(ht.ContainsValue(2));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("dos"));

            ht.Remove("three");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(3));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("three"));
            ht["tres"] = 3;
            System.Diagnostics.Debug.Assert(ht.ContainsValue(3));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("tres"));

            ht.Remove("four");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(4));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("four"));
            ht["quatro"] = 4;
            System.Diagnostics.Debug.Assert(ht.ContainsValue(4));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("quatro"));

            ht.Remove("five");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(5));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("five"));


            // Check the contains again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(1));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(11));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(30));

            // Try clearing the list
            ht.Clear();
            System.Diagnostics.Debug.Assert(ht.Count == 0);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 0);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 0);
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("dos"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("tres"));

            // Add the values again
            ht["one"] = 1;
            ht["two"] = 2;
            ht["three"] = 3;
            ht["four"] = 4;
            ht.Add("five", 5);
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            // Check the DebugView method
            AssociativeArrayDebugView view = new AssociativeArrayDebugView(ht);
            AssociativeArrayDebugView.KeyValuePair[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == ht.Count);
            for (int i = 0; i < view.Items.Length; ++i)
            {
                AssociativeArrayDebugView.KeyValuePair kvp = values[i];
                System.Diagnostics.Debug.Assert((int)ht[(string)kvp.Key] == (int)kvp.Value);
            }


            // Check the containskey method again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("dos"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("tres"));

            // Add two of some values
            ht["uno"] = 1;
            ht["quatro"] = 4;
            System.Diagnostics.Debug.Assert(ht.Count == 7);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 7);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 7);

            // Remove four
            ht.RemoveValue(4);
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);
            System.Diagnostics.Debug.Assert(ht.ContainsValue(4));

            // Remove all ones
            ht.RemoveValue(1, true);
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(1));
        }

        static void TestAssociativeArrayHT()
        {
            AssociativeArrayHT<string, int> ht = new AssociativeArrayHT<string, int>();

            ht["one"] = 1;
            ht["two"] = 2;
            ht["three"] = 3;
            ht["four"] = 4;
            ht.Add("five", 5);
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("one"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("two"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("three"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("four"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("five"));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(1));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(2));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(3));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(4));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(5));


            try
            {
                ht.Add("five", 8);
                System.Diagnostics.Debug.Assert(false, "The collection is allowing duplicate keys");
            }
            catch (InvalidOperationException)
            {
            }

            // Make sure the count is still 5
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            // Add a key that has the same value as another key
            ht["uno"] = 30;
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);

            // Check all keys that were added
            System.Diagnostics.Debug.Assert(ht["one"] == 1);
            System.Diagnostics.Debug.Assert(ht["uno"] == 30);
            System.Diagnostics.Debug.Assert(ht["two"] == 2);
            System.Diagnostics.Debug.Assert(ht["three"] == 3);
            System.Diagnostics.Debug.Assert(ht["four"] == 4);
            System.Diagnostics.Debug.Assert(ht["five"] == 5);

            // Check the containskey method
            System.Diagnostics.Debug.Assert(ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));

            // Check the containsvalue method
            System.Diagnostics.Debug.Assert(ht.ContainsValue(1));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(2));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(3));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(30));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(4));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(5));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(44));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(0));

            // Try removing something
            ht.Remove("uno");
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            // Check the containskey method again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("one"));
            System.Diagnostics.Debug.Assert(!ht.Keys.Contains("uno"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("two"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("three"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("four"));
            System.Diagnostics.Debug.Assert(ht.Keys.Contains("five"));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(1));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(2));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(3));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(4));
            System.Diagnostics.Debug.Assert(ht.Values.Contains(5));

            // Add the correct value for uno
            ht["uno"] = 11;
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);

            // Check the value
            System.Diagnostics.Debug.Assert(ht["uno"] == 11);
            
            // Check the contains again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(11));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(30));

            // Now really add the correct value
            ht["uno"] = 1;
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);

            // Check the value
            System.Diagnostics.Debug.Assert(ht["uno"] == 1);

            ht.Remove("one");
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            ht.Remove("two");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(2));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("two"));
            ht["dos"] = 2;
            System.Diagnostics.Debug.Assert(ht.ContainsValue(2));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("dos"));

            ht.Remove("three");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(3));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("three"));
            ht["tres"] = 3;
            System.Diagnostics.Debug.Assert(ht.ContainsValue(3));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("tres"));

            ht.Remove("four");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(4));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("four"));
            ht["quatro"] = 4;
            System.Diagnostics.Debug.Assert(ht.ContainsValue(4));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("quatro"));

            ht.Remove("five");
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(5));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("five"));


            // Check the contains again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsValue(1));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(11));
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(30));

            // Try clearing the list
            ht.Clear();
            System.Diagnostics.Debug.Assert(ht.Count == 0);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 0);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 0);
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("dos"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("tres"));

            // Add the values again
            ht["one"] = 1;
            ht["two"] = 2;
            ht["three"] = 3;
            ht["four"] = 4;
            ht.Add("five", 5);
            System.Diagnostics.Debug.Assert(ht.Count == 5);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 5);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 5);

            // Check the DebugView method
            AssociativeArrayDebugView view = new AssociativeArrayDebugView(ht);
            AssociativeArrayDebugView.KeyValuePair[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == ht.Count);
            for (int i = 0; i < view.Items.Length; ++i)
            {
                AssociativeArrayDebugView.KeyValuePair kvp = values[i];
                System.Diagnostics.Debug.Assert((int)ht[(string)kvp.Key] == (int)kvp.Value);
            }

            // Check the containskey method again
            System.Diagnostics.Debug.Assert(ht.ContainsKey("one"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("uno"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("two"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("three"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("four"));
            System.Diagnostics.Debug.Assert(ht.ContainsKey("five"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("test"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("dos"));
            System.Diagnostics.Debug.Assert(!ht.ContainsKey("tres"));

            // Add two of some values
            ht["uno"] = 1;
            ht["quatro"] = 4;
            System.Diagnostics.Debug.Assert(ht.Count == 7);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 7);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 7);

            // Remove four
            ht.RemoveValue(4);
            System.Diagnostics.Debug.Assert(ht.Count == 6);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 6);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 6);
            System.Diagnostics.Debug.Assert(ht.ContainsValue(4));

            // Remove all ones
            ht.RemoveValue(1, true);
            System.Diagnostics.Debug.Assert(ht.Count == 4);
            System.Diagnostics.Debug.Assert(ht.Values.Length == 4);
            System.Diagnostics.Debug.Assert(ht.Keys.Length == 4);
            System.Diagnostics.Debug.Assert(!ht.ContainsValue(1));
        }

        static void TestCircularBuffer()
        {
            CircularBuffer<int> list = new CircularBuffer<int>(8);

            int count = 0;

            int next = 0;
            for (int i = 0; i < list.Capacity - 1; ++i)
            {
                list.Push(next++);
                ++count;
            }

            System.Diagnostics.Debug.Assert(list.Count == count);

            // Testing popping
            System.Diagnostics.Debug.Assert(list.Pop() == 0);
            --count;
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Testing pushing
            list.Push(next++);
            ++count;
            list.Push(next++);
            ++count;
            System.Diagnostics.Debug.Assert(list.Peek() == 1);
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Testing pushing to a full list with pop enabled.
            list.FullOperation = FullOperations.Pop;
            System.Diagnostics.Debug.Assert(list.Peek() == 1);
            System.Diagnostics.Debug.Assert(list.Count == count);
            list.Push(next++);
            System.Diagnostics.Debug.Assert(list.Count == count);
            System.Diagnostics.Debug.Assert(list.Peek() == 2);
            list.Push(next++);
            System.Diagnostics.Debug.Assert(list.Count == count);
            System.Diagnostics.Debug.Assert(list.Peek() == 3);

            try
            {
                // Testing pushing to a full list with error enabled.
                list.FullOperation = FullOperations.Error;
                list.Push(next);
                System.Diagnostics.Debug.Assert(false);
            }
            catch (InvalidOperationException)
            {
            }
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Testing pushing to a full list with ignore enabled.
            list.FullOperation = FullOperations.Ignore;
            System.Diagnostics.Debug.Assert(list.Peek() == 3);
            list.Push(next);
            System.Diagnostics.Debug.Assert(list.Peek() == 3);
            System.Diagnostics.Debug.Assert(list.Count == count);

            list.FullOperation = FullOperations.Pop;

            // Testing popping
            System.Diagnostics.Debug.Assert(list.Pop() == 3);
            --count;
            System.Diagnostics.Debug.Assert(list.Pop() == 4);
            --count;
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Popping and pushing
            System.Diagnostics.Debug.Assert(list.Pop() == 5);
            --count;
            list.Push(next++);
            ++count;
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Pushing 2 items
            list.Push(next++);
            ++count;
            list.Push(next++);
            ++count;

            // Increasing capacity of a full list and pointers in the middle
            list.Capacity = 10;
            System.Diagnostics.Debug.Assert(list.Peek() == 6);
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Pushing 2 items
            list.Push(next++);
            ++count;
            list.Push(next++);
            ++count;
            System.Diagnostics.Debug.Assert(list.Peek() == 6);
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Increasing capacity of a full list and pointers at the beggining of the list");
            list.Capacity = 12;
            System.Diagnostics.Debug.Assert(list.Peek() == 6);
            System.Diagnostics.Debug.Assert(list.Count == count);

            //Console.WriteLine("Pushing to list");
            list.Push(next++);
            ++count;
            list.Push(next++);
            ++count;
            list.Push(next++);
            // ++count; truncated to 12
            list.Push(next++);
            // ++count; truncated to 12
            System.Diagnostics.Debug.Assert(list.Peek() == 8);
            System.Diagnostics.Debug.Assert(list.Count == count);


            System.Diagnostics.Debug.Assert(list.Pop() == 8);
            --count;
            System.Diagnostics.Debug.Assert(list.Pop() == 9);
            --count;
            System.Diagnostics.Debug.Assert(list.Peek() == 10);
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Increasing capacity of buffer with end before start
            list.Capacity = 14;
            System.Diagnostics.Debug.Assert(list.Peek() == 10);
            System.Diagnostics.Debug.Assert(list.Count == count);

            System.Diagnostics.Debug.Assert(list.Pop() == 10);
            --count;
            System.Diagnostics.Debug.Assert(list.Pop() == 11);
            --count;
            System.Diagnostics.Debug.Assert(list.Peek() == 12);
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Decreasing capacity of buffer with start before end"
            list.Capacity = 11;
            System.Diagnostics.Debug.Assert(list.Peek() == 12);
            System.Diagnostics.Debug.Assert(list.Count == count);

            // Pushing to list
            list.Push(next++);
            ++count;
            list.Push(next++);
            ++count;
            list.Push(next++);
            ++count;
            list.Push(next++);
            // ++count; truncated to 11
            System.Diagnostics.Debug.Assert(list.Peek() == 13);
            System.Diagnostics.Debug.Assert(list.Count == count);

            System.Diagnostics.Debug.Assert(list.Pop() == 13);
            --count;
            System.Diagnostics.Debug.Assert(list.Pop() == 14);
            --count;
            System.Diagnostics.Debug.Assert(list.Peek() == 15);
            System.Diagnostics.Debug.Assert(list.Count == count);

            System.Diagnostics.Debug.Assert(list.Contains(18));
            System.Diagnostics.Debug.Assert(!list.Contains(88));

            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(list);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == list.Count);
            for (int i = 0; i < view.Items.Length; ++i)
            {
                System.Diagnostics.Debug.Assert((int)values[i] == list.Pop());
            }

            list.Push(++next);
            list.Push(++next);
            list.Push(++next);
            list.Push(++next);
            list.Push(++next);
            list.Push(++next);
            list.Push(++next);
            list.Push(++next);
            list.Push(++next);

            // Test clearing
            list.Clear();
            System.Diagnostics.Debug.Assert(list.Count == 0);

            list.Push(12);
            list.Push(33);
            System.Diagnostics.Debug.Assert(list.Pop() == 12);
            System.Diagnostics.Debug.Assert(list.Count == 1);
            list.Push(41);
            System.Diagnostics.Debug.Assert(list.Pop() == 33);
            System.Diagnostics.Debug.Assert(list.Count == 1);
           
        }

        static void TestStackedArray()
        {
            StackedArray<int> stack = new StackedArray<int>();

            stack.Push(1);
            stack.Push(3);
            stack.Push(4);
            System.Diagnostics.Debug.Assert(stack.Count == 3);
            System.Diagnostics.Debug.Assert(stack.Peek() == 4);

            System.Diagnostics.Debug.Assert(stack.Pop() == 4);
            System.Diagnostics.Debug.Assert(stack.Count == 2);

            stack.Push(99);
            System.Diagnostics.Debug.Assert(stack.Count == 3);

            System.Diagnostics.Debug.Assert(stack.Pop() == 99);
            System.Diagnostics.Debug.Assert(stack.Count == 2);

            System.Diagnostics.Debug.Assert(stack.Pop() == 3);
            System.Diagnostics.Debug.Assert(stack.Count == 1);

            System.Diagnostics.Debug.Assert(stack.Pop() == 1);
            System.Diagnostics.Debug.Assert(stack.Count == 0);

            stack.Push(12);
            stack.Push(33);
            stack.Push(41);
            System.Diagnostics.Debug.Assert(stack.Count == 3);
            System.Diagnostics.Debug.Assert(stack.Peek() == 41);

            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(stack);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == stack.Count);
            System.Diagnostics.Debug.Assert((int)values[2] == 12);
            System.Diagnostics.Debug.Assert((int)values[1] == 33);
            System.Diagnostics.Debug.Assert((int)values[0] == 41);

            stack.Clear();
            System.Diagnostics.Debug.Assert(stack.Count == 0);

            stack.Push(124);
            stack.Push(335);
            stack.Push(416);
            System.Diagnostics.Debug.Assert(stack.Count == 3);
            System.Diagnostics.Debug.Assert(stack.Contains(335));
            System.Diagnostics.Debug.Assert(!stack.Contains(1));
        }

        static void TestStackedLinkedList()
        {
            StackedLinkedList<int> stack = new StackedLinkedList<int>();

            stack.Push(1);
            stack.Push(3);
            stack.Push(4);
            System.Diagnostics.Debug.Assert(stack.Count == 3);
            System.Diagnostics.Debug.Assert(stack.Peek() == 4);

            System.Diagnostics.Debug.Assert(stack.Pop() == 4);
            System.Diagnostics.Debug.Assert(stack.Count == 2);

            stack.Push(99);
            System.Diagnostics.Debug.Assert(stack.Count == 3);

            System.Diagnostics.Debug.Assert(stack.Pop() == 99);
            System.Diagnostics.Debug.Assert(stack.Count == 2);

            System.Diagnostics.Debug.Assert(stack.Pop() == 3);
            System.Diagnostics.Debug.Assert(stack.Count == 1);

            System.Diagnostics.Debug.Assert(stack.Pop() == 1);
            System.Diagnostics.Debug.Assert(stack.Count == 0);

            stack.Push(12);
            stack.Push(33);
            stack.Push(41);
            System.Diagnostics.Debug.Assert(stack.Count == 3);
            System.Diagnostics.Debug.Assert(stack.Peek() == 41);

            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(stack);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == stack.Count);
            System.Diagnostics.Debug.Assert((int)values[2] == 12);
            System.Diagnostics.Debug.Assert((int)values[1] == 33);
            System.Diagnostics.Debug.Assert((int)values[0] == 41);

            stack.Clear();
            System.Diagnostics.Debug.Assert(stack.Count == 0);

            stack.Push(124);
            stack.Push(335);
            stack.Push(416);
            System.Diagnostics.Debug.Assert(stack.Count == 3);
            System.Diagnostics.Debug.Assert(stack.Contains(335));
            System.Diagnostics.Debug.Assert(!stack.Contains(1));
        }

        static void TestQueuedLinkedList()
        {
            QueuedLinkedList<int> queue = new QueuedLinkedList<int>();

            queue.Push(1);
            queue.Push(3);
            queue.Push(4);
            System.Diagnostics.Debug.Assert(queue.Count == 3);
            System.Diagnostics.Debug.Assert(queue.Peek() == 1);

            System.Diagnostics.Debug.Assert(queue.Pop() == 1);
            System.Diagnostics.Debug.Assert(queue.Count == 2);

            queue.Push(99);
            System.Diagnostics.Debug.Assert(queue.Count == 3);

            System.Diagnostics.Debug.Assert(queue.Pop() == 3);
            System.Diagnostics.Debug.Assert(queue.Count == 2);

            System.Diagnostics.Debug.Assert(queue.Pop() == 4);
            System.Diagnostics.Debug.Assert(queue.Count == 1);

            System.Diagnostics.Debug.Assert(queue.Pop() == 99);
            System.Diagnostics.Debug.Assert(queue.Count == 0);

            queue.Push(12);
            queue.Push(33);
            queue.Push(41);
            System.Diagnostics.Debug.Assert(queue.Count == 3);
            System.Diagnostics.Debug.Assert(queue.Peek() == 12);


            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(queue);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == queue.Count);
            System.Diagnostics.Debug.Assert((int)values[0] == 12);
            System.Diagnostics.Debug.Assert((int)values[1] == 33);
            System.Diagnostics.Debug.Assert((int)values[2] == 41);


            queue.Clear();
            System.Diagnostics.Debug.Assert(queue.Count == 0);

            queue.Push(124);
            queue.Push(335);
            queue.Push(416);
            System.Diagnostics.Debug.Assert(queue.Count == 3);
            System.Diagnostics.Debug.Assert(queue.Contains(335));
            System.Diagnostics.Debug.Assert(!queue.Contains(1));
        }

        static void TestQueuedArray()
        {
            QueuedArray<int> queue = new QueuedArray<int>();

            queue.Push(1);
            queue.Push(3);
            queue.Push(4);
            System.Diagnostics.Debug.Assert(queue.Count == 3);
            System.Diagnostics.Debug.Assert(queue.Peek() == 1);

            System.Diagnostics.Debug.Assert(queue.Pop() == 1);
            System.Diagnostics.Debug.Assert(queue.Count == 2);

            queue.Push(99);
            System.Diagnostics.Debug.Assert(queue.Count == 3);

            System.Diagnostics.Debug.Assert(queue.Pop() == 3);
            System.Diagnostics.Debug.Assert(queue.Count == 2);

            System.Diagnostics.Debug.Assert(queue.Pop() == 4);
            System.Diagnostics.Debug.Assert(queue.Count == 1);

            System.Diagnostics.Debug.Assert(queue.Pop() == 99);
            System.Diagnostics.Debug.Assert(queue.Count == 0);

            queue.Push(12);
            queue.Push(33);
            queue.Push(41);
            System.Diagnostics.Debug.Assert(queue.Count == 3);
            System.Diagnostics.Debug.Assert(queue.Peek() == 12);


            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(queue);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == queue.Count);
            System.Diagnostics.Debug.Assert(values.Length == queue.Count);
            System.Diagnostics.Debug.Assert((int)values[0] == 12);
            System.Diagnostics.Debug.Assert((int)values[1] == 33);
            System.Diagnostics.Debug.Assert((int)values[2] == 41);


            queue.Clear();
            System.Diagnostics.Debug.Assert(queue.Count == 0);

            queue.Push(124);
            queue.Push(335);
            queue.Push(416);
            System.Diagnostics.Debug.Assert(queue.Count == 3);
            System.Diagnostics.Debug.Assert(queue.Contains(335));
            System.Diagnostics.Debug.Assert(!queue.Contains(1));
        }

        static void TestSingleLinkedList()
        {
            SingleLinkedList<int> list = new SingleLinkedList<int>();

            //Testing add
            list.AddToEnd(6);
            list.AddToEnd(9);
            SingleLinkedListNode<int> nodeAddAfter = list.AddToEnd(5);
            System.Diagnostics.Debug.Assert(list.Count == 3);

            SingleLinkedListNode<int> nodeAddBefore1 = list.AddToBeginning(4);
            list.AddToBeginning(1);
            list.AddBefore(nodeAddBefore1, 3);
            System.Diagnostics.Debug.Assert(list.Count == 6);

            SingleLinkedListNode<int> nodeAddBefore2 = list.AddToEnd(7);
            list.AddAfter(nodeAddAfter, 6);
            list.AddBefore(nodeAddBefore2, 9);
            list.AddBefore(nodeAddBefore2, 9);
            System.Diagnostics.Debug.Assert(list.Count == 10);

            // Check the links
            SingleLinkedListNode<int> node = list.Head;
            System.Diagnostics.Debug.Assert(node.Data == 1);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 3);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 4);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 6);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 5);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 6);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 7);

            // Deleting the first 6 from the list
            list.Remove(6, false);
            System.Diagnostics.Debug.Assert(list.Contains(6));
            //System.Diagnostics.Debug.Assert(list[5] == 6);
            System.Diagnostics.Debug.Assert(list.Count == 9);

            // Deleting all 9s from the list
            list.Remove(9, true);
            System.Diagnostics.Debug.Assert(!list.Contains(9));
            System.Diagnostics.Debug.Assert(list.Count == 6);

            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(list);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == list.Count);
            int i = 0;
            for (SingleLinkedListNode<int> tmpNode = list.Head; tmpNode != null; tmpNode = tmpNode.Next )
            {
                System.Diagnostics.Debug.Assert((int)values[i++] == tmpNode.Data);
            }

            // Testing clear
            list.Clear();
            System.Diagnostics.Debug.Assert(list.Count == 0);
            list.AddToEnd(99);
            list.AddToBeginning(66);
            list.AddToEnd(199);
            System.Diagnostics.Debug.Assert(list.Head.Data == 66);
            System.Diagnostics.Debug.Assert(list.Head.Next.Data == 99);
            System.Diagnostics.Debug.Assert(list.Tail.Data == 199);

            // Test removing
            System.Diagnostics.Debug.Assert(list.Remove(66));
            System.Diagnostics.Debug.Assert(!list.Remove(68));
        }

        static void TestDoubleLinkedList()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();

            //Testing add
            list.AddToEnd(6);
            list.AddToEnd(9);
            DoubleLinkedListNode<int> nodeAddAfter = list.AddToEnd(5);
            System.Diagnostics.Debug.Assert(list.Count == 3);

            DoubleLinkedListNode<int> nodeAddBefore1 = list.AddToBeginning(4);
            list.AddToBeginning(1);
            list.AddBefore(nodeAddBefore1, 3);
            System.Diagnostics.Debug.Assert(list.Count == 6);

            DoubleLinkedListNode<int> nodeAddBefore2 = list.AddToEnd(7);
            list.AddAfter(nodeAddAfter, 6);
            list.AddBefore(nodeAddBefore2, 9);
            list.AddBefore(nodeAddBefore2, 9);
            System.Diagnostics.Debug.Assert(list.Count == 10);

            // Check the next links
            DoubleLinkedListNode<int> node = list.Head;
            System.Diagnostics.Debug.Assert(node.Data == 1);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 3);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 4);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 6);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 5);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 6);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Next;
            System.Diagnostics.Debug.Assert(node.Data == 7);

            // Check the previous links
            node = list.Tail;
            System.Diagnostics.Debug.Assert(node.Data == 7);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 6);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 5);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 9);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 6);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 4);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 3);
            node = node.Previous;
            System.Diagnostics.Debug.Assert(node.Data == 1);

            // Deleting the first 6 from the list
            list.Remove(6, false);
            System.Diagnostics.Debug.Assert(list.Contains(6));
            //System.Diagnostics.Debug.Assert(list[5] == 6);
            System.Diagnostics.Debug.Assert(list.Count == 9);

            // Deleting all 9s from the list
            list.Remove(9, true);
            System.Diagnostics.Debug.Assert(!list.Contains(9));
            System.Diagnostics.Debug.Assert(list.Count == 6);

            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(list);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == list.Count);
            int i = 0;
            for (DoubleLinkedListNode<int> tmpNode = list.Head; tmpNode != null; tmpNode = tmpNode.Next)
            {
                System.Diagnostics.Debug.Assert((int)values[i++] == tmpNode.Data);
            }

            // Testing clear
            list.Clear();
            System.Diagnostics.Debug.Assert(list.Count == 0);
            list.AddToEnd(99);
            list.AddToBeginning(66);
            list.AddToEnd(199);
            System.Diagnostics.Debug.Assert(list.Head.Data == 66);
            System.Diagnostics.Debug.Assert(list.Head.Next.Data == 99);
            System.Diagnostics.Debug.Assert(list.Tail.Previous.Data == 99);
            System.Diagnostics.Debug.Assert(list.Tail.Data == 199);

            // Test removing
            System.Diagnostics.Debug.Assert(list.Remove(66));
            System.Diagnostics.Debug.Assert(!list.Remove(68));
        }

        public static void TestArrayEx()
        {
            // Check null indexing
            ArrayEx<ArrayEx<int>> nullableList = new ArrayEx<ArrayEx<int>>();
            nullableList = new ArrayEx<ArrayEx<int>>();
            ArrayEx<int> tmpList = new ArrayEx<int>();
            nullableList.Add(new ArrayEx<int>());
            nullableList.Add(null);
            nullableList.Add(new ArrayEx<int>());
            nullableList.Add(null);
            nullableList.Add(tmpList);
            nullableList.Add(null);
            System.Diagnostics.Debug.Assert(nullableList.Contains(null));
            System.Diagnostics.Debug.Assert(nullableList.Contains(tmpList));
            System.Diagnostics.Debug.Assert(!nullableList.Contains(new ArrayEx<int>()));

            ArrayEx<int> list = new ArrayEx<int>();

            // Testing the add
            list.Add(1);
            list.Add(3);
            list.Add(4);
            list.Add(6);
            list.Add(9);
            list.Add(5);
            list.Add(6);
            list.Add(9);
            list.Add(9);
            list.Add(7);

            List<int> tt = new List<int>(new int[] {1, 3, 4}) ;
            System.Diagnostics.Debug.Assert(list.Count == 10);

            // Testing the grow by
            list.Add(14);
            list.Add(19);
            System.Diagnostics.Debug.Assert(list.Count == 12);

            // Deleting the first 6 from the list
            list.Remove(6, false);
            System.Diagnostics.Debug.Assert(list.Contains(6));
            System.Diagnostics.Debug.Assert(list.IndexOf(6) == 5);
            System.Diagnostics.Debug.Assert(list.Count == 11);

            // Deleting all 9s from the list
            list.Remove(9, true);
            System.Diagnostics.Debug.Assert(!list.Contains(9));
            System.Diagnostics.Debug.Assert(list.Count == 8);

            // Inserting a two at the 2nd position
            list.Insert(1, 2);
            System.Diagnostics.Debug.Assert(list[1] == 2);
            System.Diagnostics.Debug.Assert(list[2] == 3);
            System.Diagnostics.Debug.Assert(list.Count == 9);

            // Check the DebugView method
            ArrayDebugView view = new ArrayDebugView(list);
            object[] values = view.Items;
            System.Diagnostics.Debug.Assert(values.Length == list.Count);
            for (int i = 0; i < list.Count; ++i)
            {
                System.Diagnostics.Debug.Assert((int)values[i] == list[i]);
            }

            // Testing clear
            list.Clear();
            System.Diagnostics.Debug.Assert(list.Count == 0);
            list.Add(66);
            list.Add(99);
            System.Diagnostics.Debug.Assert(list[0] == 66);
            System.Diagnostics.Debug.Assert(list[1] == 99);

            // Test removing
            System.Diagnostics.Debug.Assert(list.Remove(66));
            System.Diagnostics.Debug.Assert(!list.Remove(68));

            // Prepare for RemoveAt test
            list.Clear();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            System.Diagnostics.Debug.Assert(list.Count == 8);

            // Test RemoveAt the end
            list.RemoveAt(7);
            System.Diagnostics.Debug.Assert(list.Count == 7);
            System.Diagnostics.Debug.Assert(list.Contains(6));
            System.Diagnostics.Debug.Assert(!list.Contains(7));

            // Test RemoveAt the middle
            list.RemoveAt(4);
            System.Diagnostics.Debug.Assert(list.Count == 6);
            System.Diagnostics.Debug.Assert(list.Contains(3));
            System.Diagnostics.Debug.Assert(list.Contains(5));
            System.Diagnostics.Debug.Assert(!list.Contains(4));

            // Test RemoveAt the front
            list.RemoveAt(0);
            System.Diagnostics.Debug.Assert(list.Count == 5);
            System.Diagnostics.Debug.Assert(list.Contains(1));
            System.Diagnostics.Debug.Assert(!list.Contains(0));
        }
    }
}
