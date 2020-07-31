using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.StreamSeekerTests
{
    [TestClass]
    public class SeekWordTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\StreamSeeker_TestData\\SeekWord.txt";
        private static string TEST_FILE_CONTENT = "abcdef prop1: sdfd  \"prop2\": sdfsdf";

        [TestMethod]
        public void Seek_InMiddle_1()
        {
            var result = StreamSeeker.SeekWord("prop1:", TEST_FILE);

            Assert.AreEqual(7, result);
            Assert.AreEqual("prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
        }

        [TestMethod]
        public void Seek_InMiddle_2()
        {

            var result = StreamSeeker.SeekWord("\"prop2\"", TEST_FILE);

            Assert.AreEqual(20, result);
            Assert.AreEqual("\"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
        }

        [TestMethod]
        public void Seek_WithStream_1()
        {
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                var result = StreamSeeker.SeekWord("prop1:", fileStream);

                Assert.AreEqual(7, result);
                Assert.AreEqual("prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }

        }

        [TestMethod]
        public void Seek_WithStream_2()
        {
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                var result = StreamSeeker.SeekWord("\"prop2\"", fileStream);

                Assert.AreEqual(20, result);
                Assert.AreEqual("\"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }
        }

        [TestMethod]
        public void Seek_AtIndex0()
        {
            var result = StreamSeeker.SeekWord("abcdef", TEST_FILE);

            Assert.AreEqual(0, result);
            Assert.AreEqual("abcdef prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
        }

        [TestMethod]
        public void Seek_AtIndex1()
        {
            var result = StreamSeeker.SeekWord("bcdef", TEST_FILE);

            Assert.AreEqual(1, result);
            Assert.AreEqual("bcdef prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
        }

        [TestMethod]
        public void Seek_AtTheEnd()
        {

            var result = StreamSeeker.SeekWord("sdfsdf", TEST_FILE);

            Assert.AreEqual(29, result);
            Assert.AreEqual("sdfsdf", TEST_FILE_CONTENT.Substring(result));
        }
    }
}
