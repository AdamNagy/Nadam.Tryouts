using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class GetValuePositionTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\ReadValueT_Mock.json";

        private static string TEST_CONTENT =
            "{\"prop1\": { \"ip\": \"8.8.8.8\" }, \"textProp\": \"basic text\", \"prop2\": { \"Accept-Language\": \"en-US,en;q=0.8\", \"Host\": \"headers.jsontest.com\",\"Accept-Charset\": \"ISO-8859-1,utf-8;q=0.7,*;q=0.3\",\"Accept\": \"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\"},\"prop3\": {\"one\": \"two\",\"key\": \"value\",\"nested\": {\"object_or_array\": \"object\",\"empty\": false,\"parse_time_nanoseconds\": 19608,\"validate\": true,\"size\": 1}}}";

        [TestInitialize]
        public void BeforeAll()
        {

        }

        [TestMethod]
        public void Read_Prop1()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("prop1", fs);
                var expected = JsonSeeker.NormalizeJsonString("{ \"ip\": \"8.8.8.8\" }");
                var result = TEST_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Read_AcceptLanguage()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("Accept-Language", fs);
                var result = TEST_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual("\"en-US,en;q=0.8\"", result);
            }
        }

        [TestMethod]
        public void Read_textProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("textProp", fs);
                var result = TEST_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual("\"basic text\"", result);
            }
        }
    }
}
