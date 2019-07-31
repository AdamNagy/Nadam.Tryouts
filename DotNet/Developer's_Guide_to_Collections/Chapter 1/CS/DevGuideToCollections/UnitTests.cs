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
            TestSingleLinkedList();
            TestDoubleLinkedList();
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
