using System.Linq;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests
{
    [TestClass]
    public class ImageList_Tests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\TitleStructuredList.txt";

        [TestMethod]
        public void KeysShouldBePresentInTheDict()
        {
            var sut = new TitleStructuredList();
            var actual = sut.ProcessFile(TEST_FILE);

            Assert.IsTrue(actual.Count() == 8);

            Assert.IsTrue(actual.ContainsKey("titlestructuredlist"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title-h2_title"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title-h2_title-h3_title"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title-h2_title-h3_title_2"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title-h2_title_other"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title-h2_title_other_2"));
            Assert.IsTrue(actual.ContainsKey("titlestructuredlist-h1_title_2"));
        }

        [TestMethod]
        public void DictShouldContainTheCorrectNumberOfItems()
        {
            var sut = new TitleStructuredList();
            var actual = sut.ProcessFile(TEST_FILE);

            Assert.AreEqual(2, actual["titlestructuredlist"].Count());
            Assert.AreEqual(5, actual["titlestructuredlist-h1_title-h2_title-h3_title"].Count());
        }
    }
}
