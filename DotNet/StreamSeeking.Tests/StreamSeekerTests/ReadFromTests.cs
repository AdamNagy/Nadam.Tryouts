using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.StreamSeekerTests
{
    [TestClass]
    public class ReadFromTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\StreamSeeker_Mock.txt";

        [TestMethod]
        public void ReadFromRandomPlace()
        {
            var result = "";
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                result = StreamSeeker.ReadFrom(5, fileStream);
            }

            Assert.AreEqual("abc", result);
        }

        [TestMethod]
        public void ReadFromTheZero()
        {
            var result = "";
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                result = StreamSeeker.ReadFrom(0, fileStream);
            }

            Assert.AreEqual("12345abc", result);
        }

        [TestMethod]
        public void ReadFromTheOne()
        {
            var result = "";
            using (FileStream fileStream = File.OpenRead(TEST_FILE))
            {
                result = StreamSeeker.ReadFrom(1, fileStream);
            }

            Assert.AreEqual("2345abc", result);
        }
    }
}
