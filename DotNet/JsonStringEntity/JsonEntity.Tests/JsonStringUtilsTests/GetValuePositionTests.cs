using System.IO;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

namespace JsonStringUtilsTests
{
    [TestClass]
    public class GetValuePosition
    {
        private static string TEST_FILE_PATH = "..\\..\\App_Data\\ReadValueT_Mock.json";
        private static string TEST_FILE_CONTENT;
        private static TestJsonModel TEST_MODEL = TestJsonModel.GetDefault();

        [TestInitialize]
        public void BeforeAll()
        {
            TEST_FILE_CONTENT = TEST_MODEL.ToJsonString();
            File.WriteAllText(TEST_FILE_PATH, TEST_FILE_CONTENT);
        }

        [TestMethod]
        public void Read_NumberProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE_PATH))
            {
                var valPos = JsonStringUtils.GetValuePosition("numberProp", fs);
                var expected = TEST_MODEL.NumberProp.ToString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.length).Trim();

                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Read_StringProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE_PATH))
            {
                var valPos = JsonStringUtils.GetValuePosition("stringProp", fs);
                var expected = TEST_MODEL.StringProp;

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.length).Trim();

                Assert.AreEqual($"\"{expected}\"", result);
            }
        }

        [TestMethod]
        public void Read_ComplexProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE_PATH))
            {
                var valPos = JsonStringUtils.GetValuePosition("complexProp", fs);
                var expected = TEST_MODEL.ComplexProp.ToJsonString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.length).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }

        [TestMethod]
        public void Read_ComplexArrayProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE_PATH))
            {
                var valPos = JsonStringUtils.GetValuePosition("complexArrayProp", fs);
                var expected = TEST_MODEL.ComplexArrayProp.ToJsonString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.length).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }

        [TestMethod]
        public void Read_NumberArrayProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE_PATH))
            {
                var valPos = JsonStringUtils.GetValuePosition("numberArrayProp", fs);
                var expected = TEST_MODEL.NumberArrayProp.ToJsonString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.length).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }

        [TestMethod]
        public void Read_StringArrayProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE_PATH))
            {
                var valPos = JsonStringUtils.GetValuePosition("stringArrayProp", fs);
                var expected = TEST_MODEL.StringArrayProp.ToJsonString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.length).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }
    }
}
