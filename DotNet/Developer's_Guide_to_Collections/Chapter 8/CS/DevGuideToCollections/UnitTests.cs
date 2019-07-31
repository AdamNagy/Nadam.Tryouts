using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
            TestSynchronization();
            TestSynchronizedListWrapper();
        }

        static void TestSynchronizedListWrapper()
        {
            ArrayEx<int> list = new ArrayEx<int>() { 55, 8 };

            SynchronizedListWrapper<int> slist = new SynchronizedListWrapper<int>(list);

            slist.Add(12);


            lock (((ICollection)list).SyncRoot)
            {
                list.Add(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.Add(12);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }

        static void TestSynchronization()
        {
            ArrayEx<int> list = new ArrayEx<int>() { 55, 8 };

            SynchronizedListWrapper<int> slist = new SynchronizedListWrapper<int>(list);
            System.Diagnostics.Debug.Assert(slist.Count == 2);

            lock(((System.Collections.ICollection)slist).SyncRoot)
            {
                slist.Add(12);
            }

            slist.Clear();
            System.Diagnostics.Debug.Assert(slist.Count == 0);

            object syncroot = new object();
            slist = new SynchronizedListWrapper<int>(list, syncroot);
            System.Diagnostics.Debug.Assert(((System.Collections.ICollection)slist).SyncRoot == syncroot);
        }


        static void TestAssociativeArrayAL()
        {
            AssociativeArrayAL<string, int> aa = new AssociativeArrayAL<string, int>();

            lock (((ICollection)aa).SyncRoot)
            {
                aa.Add("twelve", 12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)aa).SyncRoot))
            {
                try
                {
                    aa.Add("two", 2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)aa).SyncRoot);
                }
            }
        }

        static void TestAssociativeArrayHT()
        {
            AssociativeArrayHT<string, int> aa = new AssociativeArrayHT<string, int>();

            lock (((ICollection)aa).SyncRoot)
            {
                aa.Add("twelve", 12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)aa).SyncRoot))
            {
                try
                {
                    aa.Add("two", 2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)aa).SyncRoot);
                }
            }
        }

        static void TestCircularBuffer()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            CircularBuffer<int> cb = new CircularBuffer<int>(10, values);

            lock (((ICollection)cb).SyncRoot)
            {
                cb.Push(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)cb).SyncRoot))
            {
                try
                {
                    cb.Push(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)cb).SyncRoot);
                }
            }
        }

        static void TestArrayEx()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            ArrayEx<int> array = new ArrayEx<int>(values);

            lock (((ICollection)array).SyncRoot)
            {
                array.Add(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)array).SyncRoot))
            {
                try
                {
                    array.Add(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)array).SyncRoot);
                }
            }
        }

        static void TestQueuedLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            QueuedLinkedList<int> list = new QueuedLinkedList<int>(values);

            lock (((ICollection)list).SyncRoot)
            {
                list.Push(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.Push(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }

        static void TestQueuedArray()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            QueuedArray<int> list = new QueuedArray<int>(values);

            lock (((ICollection)list).SyncRoot)
            {
                list.Push(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.Push(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }

        static void TestStackedArray()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            StackedArray<int> list = new StackedArray<int>(values);

            lock (((ICollection)list).SyncRoot)
            {
                list.Push(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.Push(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }

        static void TestStackedLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            StackedLinkedList<int> list = new StackedLinkedList<int>(values);

            lock (((ICollection)list).SyncRoot)
            {
                list.Push(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.Push(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }

        static void TestSingleLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            SingleLinkedList<int> list = new SingleLinkedList<int>(values);

            lock (((ICollection)list).SyncRoot)
            {
                list.AddToEnd(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.AddToEnd(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }

        static void TestDoubleLinkedList()
        {
            int[] values = new int[] { 2, 7, 10, 7, 5, 3 };
            DoubleLinkedList<int> list = new DoubleLinkedList<int>(values);

            lock (((ICollection)list).SyncRoot)
            {
                list.AddToEnd(12);
            }

            if (System.Threading.Monitor.TryEnter(((ICollection)list).SyncRoot))
            {
                try
                {
                    list.AddToEnd(2);
                }
                finally
                {
                    System.Threading.Monitor.Exit(((ICollection)list).SyncRoot);
                }
            }
        }
    }
}
