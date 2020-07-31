using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class AddValueToArrayTest
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
            JsonSeeker.AddValueToArray(TEST_FILE, "stringArrayProp", "barack");
            var result = JsonSeeker.ReadValue(TEST_FILE, "stringArrayProp");

            Assert.AreEqual("[\"barack\",\"alma\",\"korte\",\"szilva\"]", result);
        }

        [TestMethod]
        public void Append_End_Simple()
        {
            JsonSeeker.AddValueToArray(TEST_FILE, "stringArrayProp", "barack", AppendPosition.end);
            var result = JsonSeeker.ReadValue(TEST_FILE, "stringArrayProp");

            Assert.AreEqual("[\"alma\",\"korte\",\"szilva\",\"barack\"]", result);
        }
    }
}
