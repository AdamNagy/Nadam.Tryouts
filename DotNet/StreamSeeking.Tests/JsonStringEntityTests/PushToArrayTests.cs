using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class PushToArrayTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\JsonSeeker_TestData\\AddValueToArray_complex.txt";

        [TestInitialize]
        public void BeforeAll()
        {
            File.WriteAllText(TEST_FILE, "{\"someProp\":123,\"stringArrayProp\":[\"alma\",\"korte\",\"szilva\"]}");
        }

        [TestMethod]
        public void Append_Begining_Simple()
        {
            // JsonSeeker.PushToArray(TEST_FILE, "stringArrayProp", "\"barack\"", AppendPosition.begining);
            JsonSeeker.PushToArray(TEST_FILE, "\"barack\"", "stringArrayProp", AppendPosition.begining);
            var result = JsonSeeker.ReadProperty(TEST_FILE, "stringArrayProp");

            Assert.AreEqual("[\"barack\",\"alma\",\"korte\",\"szilva\"]", result);
        }

        [TestMethod]
        public void Append_End_Simple()
        {
            JsonSeeker.PushToArray(TEST_FILE, "\"barack\"", "stringArrayProp", AppendPosition.end);
            var result = JsonSeeker.ReadProperty(TEST_FILE, "stringArrayProp");

            Assert.AreEqual("[\"alma\",\"korte\",\"szilva\",\"barack\"]", result);
        }
    }
}
