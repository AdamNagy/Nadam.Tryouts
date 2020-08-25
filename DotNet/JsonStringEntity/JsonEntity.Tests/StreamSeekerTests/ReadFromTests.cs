using System.IO;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeekerTests
{
    [TestClass]
    public class ReadFrom
    {
        private static string TEST_FILE = "..\\..\\App_Data\\StreamSeeker_TestData\\ReadFrom.txt";
        private static string TEST_FILE_CONTENT = "0123456789abcdefghijklmnopqrstvwxyz";

        [TestMethod]
        public void ReadFrom_TheBegining()
        {
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
               var result = StreamSeeker.ReadFrom(0, fileStream);
                Assert.AreEqual(TEST_FILE_CONTENT.Substring(0), result);
            }
        }

        [TestMethod]
        public void ReadFrom_TheIndex1()
        {
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                var result = StreamSeeker.ReadFrom(1, fileStream);
                Assert.AreEqual(TEST_FILE_CONTENT.Substring(1), result);
            }
        }

        [TestMethod]
        public void ReadFrom_Middle()
        {
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                var result = StreamSeeker.ReadFrom(5, fileStream);
                Assert.AreEqual(TEST_FILE_CONTENT.Substring(5), result);
            }
        }

        [TestMethod]
        public void ReadFrom_End()
        {
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                var result = StreamSeeker.ReadFrom(TEST_FILE_CONTENT.Length - 1, fileStream);
                Assert.AreEqual(TEST_FILE_CONTENT.Substring(TEST_FILE_CONTENT.Length - 1), result);
            }
        }
    }
}
