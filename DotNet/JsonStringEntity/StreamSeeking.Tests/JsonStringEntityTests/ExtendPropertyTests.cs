using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.JsonStringEntityTests
{
    [TestClass]
    public class ExtendPropertyTests
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
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("\"barack\"", "stringArrayProp", AppendPosition.begining);
            var result = sut.Read("stringArrayProp");

            Assert.AreEqual("[\"barack\",\"alma\",\"korte\",\"szilva\"]", result);
        }

        [TestMethod]
        public void Append_End_Simple()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("\"barack\"", "stringArrayProp", AppendPosition.end);
            var result = sut.Read("stringArrayProp");

            Assert.AreEqual("[\"alma\",\"korte\",\"szilva\",\"barack\"]", result);
        }
    }
}
