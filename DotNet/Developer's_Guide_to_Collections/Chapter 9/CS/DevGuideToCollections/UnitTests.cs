using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


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

        static bool Compare(IEnumerable left, IEnumerable right)
        {
            bool retval = true;

            IEnumerator leftEnumerator = left.GetEnumerator();
            IEnumerator rightEnumerator = right.GetEnumerator();

            leftEnumerator.Reset();
            rightEnumerator.Reset();

            bool bLeft = leftEnumerator.MoveNext();
            bool bRight = rightEnumerator.MoveNext();
            System.Diagnostics.Debug.Assert(bLeft == bRight);

            while (bLeft && retval)
            {
                object oLeft = leftEnumerator.Current;
                object oRight = rightEnumerator.Current;

                if (oLeft != null || oRight != null)
                {
                    System.Diagnostics.Debug.Assert(oLeft != null);
                    System.Diagnostics.Debug.Assert(oRight != null);

                    IComparable comparer = oLeft as IComparable;

                    retval &= (comparer.CompareTo(oRight) == 0);

                    System.Diagnostics.Debug.Assert(retval);
                }

                bLeft = leftEnumerator.MoveNext();
                bRight = rightEnumerator.MoveNext();
                System.Diagnostics.Debug.Assert(bLeft == bRight);
            }

            return retval;
        }

        static bool TestSerialize(IEnumerable original)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, original);

                stream.Position = 0;

                IEnumerable clone = (IEnumerable)formatter.Deserialize(stream);

                return Compare(original, clone);
            }
        }

        static void TestArrayEx()
        {
            ArrayEx<int> original = new ArrayEx<int>();

            for (int i = 0; i < 16; ++i)
            {
                original.Add(i);
            }


            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.RemoveAt(original.Count - 1);
            original.RemoveAt(original.Count - 1);

            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.Clear();
            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.Add(12);
            original.Add(13);
            System.Diagnostics.Debug.Assert(TestSerialize(original));
        }

        static void TestSingleLinkedList()
        {
            SingleLinkedList<int> original = new SingleLinkedList<int>();

            for (int i = 0; i < 16; ++i)
            {
                original.AddToEnd(i);
            }


            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.Remove(original.Tail);
            original.Remove(original.Tail);
            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.Clear();
            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.AddToEnd(12);
            original.AddToEnd(13);
            System.Diagnostics.Debug.Assert(TestSerialize(original));
        }

        static void TestDoubleLinkedList()
        {
            DoubleLinkedList<int> original = new DoubleLinkedList<int>();

            for (int i = 0; i < 16; ++i)
            {
                original.AddToEnd(i);
            }


            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.Remove(original.Tail);
            original.Remove(original.Tail);

            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.Clear();
            System.Diagnostics.Debug.Assert(TestSerialize(original));

            original.AddToEnd(12);
            original.AddToEnd(13);
            System.Diagnostics.Debug.Assert(TestSerialize(original));
        }
    }
}
