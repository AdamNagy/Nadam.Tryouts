using System.IO;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeekerTests
{
    [TestClass]
    public class SeekWord
    {
        private static string TEST_FILE = "..\\..\\App_Data\\StreamSeeker_TestData\\SeekWord.txt";
        private static string TEST_FILE_CONTENT = "abcdef prop1: sdfd  \"prop2\": sdfsdf";

        [TestMethod]
        public void Seek_InMiddle_1()
        {
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                var result = StreamSeeker.SeekWord("prop1:", fileStream);

                Assert.AreEqual(7, result);
                Assert.AreEqual("prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }
        }

        [TestMethod]
        public void Seek_InMiddle_2()
        {
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                var result = StreamSeeker.SeekWord("\"prop2\"", fileStream);

                Assert.AreEqual(20, result);
                Assert.AreEqual("\"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }
        }

        [TestMethod]
        public void Seek_AtIndex0()
        {
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                var result = StreamSeeker.SeekWord("abcdef", fileStream);

                Assert.AreEqual(0, result);
                Assert.AreEqual("abcdef prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }
        }

        [TestMethod]
        public void Seek_AtIndex1()
        {
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                var result = StreamSeeker.SeekWord("bcdef", fileStream);

                Assert.AreEqual(1, result);
                Assert.AreEqual("bcdef prop1: sdfd  \"prop2\": sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }
        }

        [TestMethod]
        public void Seek_AtTheEnd()
        {
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                var result = StreamSeeker.SeekWord("sdfsdf", fileStream);

                Assert.AreEqual(29, result);
                Assert.AreEqual("sdfsdf", TEST_FILE_CONTENT.Substring(result));
            }
        }
    }
}
