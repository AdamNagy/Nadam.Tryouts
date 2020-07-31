using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StreamSeeking.Tests.MockClasses;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class GetValuePositionTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\ReadValueT_Mock.json";
        private static string TEST_FILE_CONTENT;

        [TestInitialize]
        public void BeforeAll()
        {
            TEST_FILE_CONTENT = ToJString(TestJsonModel.GetDefault());
            File.WriteAllText(TEST_FILE, TEST_FILE_CONTENT);
        }

        public static string ToJString(Object subject)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None
            };

            return JsonConvert.SerializeObject(subject, jsonSerializerSettings);
        }

        [TestMethod]
        public void Read_NumberProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("numberProp", fs);
                var expected = MockData.MOCK_NUMBERS[0].ToString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Read_StringProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("stringProp", fs);
                var expected = MockData.MOCK_TEXT[0].ToString();

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual($"\"{expected}\"", result);
            }
        }

        [TestMethod]
        public void Read_ComplexProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("complexProp", fs);
                var expected = ToJString(ComplexJsonType.GetDefault());

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }

        [TestMethod]
        public void Read_ComplexArrayProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("complexArrayProp", fs);
                var expected = ToJString(MockData.MOCK_COMPLEX_ARRAY);

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }

        [TestMethod]
        public void Read_NumberArrayProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("numberArrayProp", fs);
                var expected = ToJString(MockData.MOCK_NUMBERS_ARRAY1);

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }

        [TestMethod]
        public void Read_StringArrayProp()
        {
            using (FileStream fs = File.OpenRead(TEST_FILE))
            {
                var valPos = JsonSeeker.GetValuePosition("stringArrayProp", fs);
                var expected = ToJString(MockData.MOCK_STRING_ARRAY1);

                var result = TEST_FILE_CONTENT.Substring(valPos.startPos, valPos.endPos).Trim();

                Assert.AreEqual($"{expected}", result);
            }
        }
    }
}
