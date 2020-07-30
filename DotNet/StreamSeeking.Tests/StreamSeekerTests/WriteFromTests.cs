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
    public class WriteFromTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\StreamSeeker_Mock2.txt";

        [TestInitialize]
        public void BeforeAll()
        {
            if( File.Exists(TEST_FILE) )
                File.WriteAllText(TEST_FILE, "12345abc");
        }

        [TestMethod]
        public void WriteToMidle_Longer()
        {
            var result = "";
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                StreamSeeker.WriteFrom(5, fileStream, "6789");
            }

            result = File.ReadAllText(TEST_FILE);
            Assert.AreEqual("123456789", result);
        }

        [TestMethod]
        public void WriteToMidle_Shorter()
        {
            var result = "";
            using (FileStream fileStream = File.Open(TEST_FILE, FileMode.Open))
            {
                StreamSeeker.WriteFrom(5, fileStream, "xy");
            }

            result = File.ReadAllText(TEST_FILE);
            Assert.AreEqual("12345xyc", result);
        }
    }
}
