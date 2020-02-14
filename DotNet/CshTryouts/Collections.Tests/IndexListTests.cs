using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyCollection.Tests.IndexList
{
    public class IndexListTests
    {
        [TestClass]
        public class AddTests
        {
            [TestMethod]
            public void AddSingle()
            {
                var list = new IndexList<int>();
                var idx = list.Add(1);

                Assert.AreEqual(0, idx);
            }

            [TestMethod]
            public void AddMultiple()
            {
                var list = new IndexList<int>();

                var indexList = new int[3];
                indexList[0] = list.Add(1);
                indexList[1] = list.Add(2);
                indexList[2] = list.Add(3);

                CollectionAssert.AreEqual(new int[3] {0, 1, 2}, indexList);
            }
        }

        [TestClass]
        public class RemoveAndAddTests
        {
            [TestMethod]
            public void RemoveFirstAndAddnew()
            {
                var list = new IndexList<int>();
                list.Add(1);

                list.Remove(1);
                var idx = list.Add(10);

                Assert.AreEqual(0, idx);
            }

            [TestMethod]
            public void RemoveMidleAndAddnew()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);

                list.Remove(2);
                var idx = list.Add(20);

                Assert.AreEqual(1, idx);
            }

            [TestMethod]
            public void RemoveLastAndAddnew()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);

                list.Remove(3);
                var idx = list.Add(30);

                Assert.AreEqual(2, idx);
            }

            [TestMethod]
            public void Remove2AndAddnew()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);

                list.Remove(2);
                list.Remove(3);
                var idx = list.Add(20);

                Assert.AreEqual(1, idx);
            }

            [TestMethod]
            public void Remove2AndAddnew_2()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);

                // this is swapped
                list.Remove(3);
                list.Remove(2);
                var idx = list.Add(20);

                Assert.AreEqual(1, idx);
            }
        }

        [TestClass]
        public class CountTests
        {
            [TestMethod]
            public void CountZero()
            {
                var list = new IndexList<int>();

                Assert.AreEqual(0, list.Count);
            }

            [TestMethod]
            public void CountOne()
            {
                var list = new IndexList<int>();
                list.Add(1);

                Assert.AreEqual(1, list.Count);
            }

            [TestMethod]
            public void CountMore()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);

                Assert.AreEqual(3, list.Count);
            }

            [TestMethod]
            public void CountMoreAfterRemoveSome()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                list.Add(5);

                list.Remove(3);
                list.Remove(4);

                Assert.AreEqual(3, list.Count);
            }

            [TestMethod]
            public void CountMoreAfterRemoveSomeThenReadding()
            {
                var list = new IndexList<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                list.Add(5);

                list.Remove(3);
                list.Remove(4);

                list.Add(3);
                list.Add(4);

                Assert.AreEqual(5, list.Count);
            }
        }

        [TestClass]
        public class IntIndexerTests
        {
            [TestMethod]
            public void IntIndexer_shouldthrowError()
            {
                var list = new IndexList<int>();
                Assert.ThrowsException<IndexOutOfRangeException>(() => list[3]);
            }

            [TestMethod]
            public void IntIndexer_valid()
            {
                var list = new IndexList<string>();
                list.Add("Adam");

                Assert.AreEqual("Adam", list[0]);
            }

            [TestMethod]
            public void IntIndexer_middle()
            {
                var list = new IndexList<string>();
                list.Add("Adam");
                list.Add("Dani");
                list.Add("Peti");

                Assert.AreEqual("Peti", list[2]);
            }


        }

        [TestClass]
        public class TypedIndexerTests
        {
            [TestMethod]
            public void Indexer_shouldthrowError()
            {
                var list = new IndexList<string>();
                Assert.ThrowsException<IndexOutOfRangeException>(() => list["qwe"]);
            }

            [TestMethod]
            public void IntIndexer_valid()
            {
                var list = new IndexList<string>();
                list.Add("Adam");

                Assert.AreEqual(0, list["Adam"]);
            }

            [TestMethod]
            public void IntIndexer_middle()
            {
                var list = new IndexList<string>();
                list.Add("Adam");
                list.Add("Dani");
                list.Add("Peti");

                Assert.AreEqual(1, list["Dani"]);
            }


        }

        [TestClass]
        public class TypeContainsTests
        {
            [TestMethod]
            public void NotContains()
            {
                var list = new IndexList<string>();
                var containsRes = list.Contains("qwe");

                Assert.AreEqual(-1, containsRes);
            }
        }

        [TestClass]
        public class IteratorTests
        {
            [TestMethod]
            public void IterateEmpty()
            {
                var list = new IndexList<int>();
                var counter = 0;

                foreach (var i in list)
                {
                    counter++;
                }

                Assert.AreEqual(0, counter);
            }

            [TestMethod]
            public void IterateSingle()
            {
                var list = new IndexList<int>();
                list.Add(1);
                var counter = 0;

                foreach (var i in list)
                {
                    counter++;
                }

                Assert.AreEqual(1, counter);
            }

            [TestMethod]
            public void IterateMultiple()
            {
                var list = new IndexList<int>();
                list.Add(1); list.Add(1); list.Add(1); list.Add(1);
                var counter = 0;

                foreach (var i in list)
                {
                    counter++;
                }

                Assert.AreEqual(4, counter);
            }

            [TestMethod]
            public void IterateMultipleAfterRemoval()
            {
                var list = new IndexList<int>();
                list.Add(1); list.Add(2); list.Add(3); list.Add(4);
                list.Remove(3);
                var counter = 0;

                foreach (var i in list)
                    counter++;

                Assert.AreEqual(3, counter);
            }

            [TestMethod]
            public void ExceedCapacity()
            {
                var list = new IndexList<int>(3);
                list.Add(1); list.Add(2); list.Add(3); list.Add(4);
                
                Assert.AreEqual(4, list.Count);
            }

            [TestMethod]
            public void ExceedCapacityTwice()
            {
                var list = new IndexList<int>(2);
                list.Add(1); list.Add(2); list.Add(3); list.Add(4); list.Add(5); list.Add(6);

                CollectionAssert.AreEqual(new int[] {1, 2, 3, 4, 5, 6}, list.ToArray());
            }

            [TestMethod]
            public void ExceedCapacity_CheckingIndex()
            {
                var list = new IndexList<string>(3);
                list.Add("1"); list.Add("2"); list.Add("3");

                var idxOf3 = list["3"];
                list.Add("4"); list.Add("5");
                var newIdxOf3 = list["3"];

                Assert.AreEqual(idxOf3, newIdxOf3);
            }
        }
    }
}
