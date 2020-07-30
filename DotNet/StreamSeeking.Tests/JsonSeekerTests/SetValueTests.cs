using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class SetValueTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\SetValue_Mock.json";

        private static string TEST_CONTENT =
            "{\"prop1\": { \"ip\": \"8.8.8.8\" }, \"textProp\":\"basic text\", \"prop2\": { \"Accept-Language\": \"en-US,en;q=0.8\", \"Host\": \"headers.jsontest.com\",\"Accept-Charset\": \"ISO-8859-1,utf-8;q=0.7,*;q=0.3\",\"Accept\": \"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\"},\"prop3\": {\"one\": \"two\",\"key\": \"value\",\"nested\": {\"object_or_array\": \"object\",\"empty\": false,\"parse_time_nanoseconds\": 19608,\"validate\": true,\"size\": 1}}}";

        [TestInitialize]
        public void BeforeAll()
        {
            File.WriteAllText(TEST_FILE, TEST_CONTENT);
        }

        [TestMethod]
        public void Write_Complex_ToMiddle_Longer()
        {
            JsonSeeker.SetValue("prop1", TEST_FILE, "{ \"changedProp\": 123456789 }");
            var result = JsonSeeker.ReadValue("prop1", TEST_FILE);

            Assert.AreEqual("{ \"changedProp\": 123456789 }", result);
        }

        [TestMethod]
        public void Write_Text_ToMiddle_Longer()
        {
            JsonSeeker.SetValue("textProp", TEST_FILE, "some longer text to check");
            var result = JsonSeeker.ReadValue("prop1", TEST_FILE);

            Assert.AreEqual("some longer text to check", result);
        }
    }
}
