using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollection;

namespace Collections.Tests
{
    [TestClass]
    public class IndexListTests
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

            CollectionAssert.AreEqual(new int[3]{0, 1, 2}, indexList);
        }

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
}
