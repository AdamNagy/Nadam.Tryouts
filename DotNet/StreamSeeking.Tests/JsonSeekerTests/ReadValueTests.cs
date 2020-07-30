using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class ReadValueTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\ReadValueT_Mock.json";

        [TestMethod]
        public void ReadComplex_1()
        {
            var propVal = JsonSeeker.ReadValue("prop1", TEST_FILE);
            var expected = JsonSeeker.NormalizeJsonString("{ \"ip\": \"8.8.8.8\" }");

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void ReadComplex_2()
        {
            var propVal = JsonSeeker.NormalizeJsonString(JsonSeeker.ReadValue("prop2", TEST_FILE));
            var expected = JsonSeeker.NormalizeJsonString("{ \"Accept-Language\": \"en-US,en;q=0.8\", \"Host\": \"headers.jsontest.com\", \"Accept-Charset\": \"ISO-8859-1,utf-8;q=0.7,*;q=0.3\",\"Accept\": \"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\"}");

            Assert.AreEqual(expected, propVal);
        }
    }
}
