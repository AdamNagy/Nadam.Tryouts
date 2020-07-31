using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StreamSeeking.Tests.MockClasses;
using System;
using System.IO;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class ReadValueTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\ReadValueT_Mock.json";

        [TestInitialize]
        public void BeforeAll()
        {
            File.WriteAllText(TEST_FILE, ToJString(TestJsonModel.GetDefault()));
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
        public void Read_String()
        {
            var propVal = JsonSeeker.ReadValue("stringProp", TEST_FILE);
            Assert.AreEqual(propVal, MockData.MOCK_TEXT[0]);
        }

        [TestMethod]
        public void Read_Number()
        {
            var propVal = JsonSeeker.ReadValue("numberProp", TEST_FILE);
            Assert.AreEqual(propVal, MockData.MOCK_NUMBERS[0].ToString());
        }

        [TestMethod]
        public void Read_Complex()
        {
            var propVal = JsonSeeker.ReadValue("complexProp", TEST_FILE);
            var expected = ToJString(ComplexJsonType.GetDefault());

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_StringArray()
        {
            var propVal = JsonSeeker.ReadValue("stringArrayProp", TEST_FILE);
            var expected = ToJString(MockData.MOCK_STRING_ARRAY1);

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_IntArray()
        {
            var propVal = JsonSeeker.ReadValue("numberArrayProp", TEST_FILE);
            var expected = ToJString(MockData.MOCK_NUMBERS_ARRAY1);

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_ComplexArray()
        {
            var propVal = JsonSeeker.ReadValue("complexArrayProp", TEST_FILE);
            var expected = ToJString(MockData.MOCK_COMPLEX_ARRAY);

            Assert.AreEqual(expected, propVal);
        }
    }
}
