using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StreamSeeking.Tests.MockClasses;
using System;
using System.IO;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class ReadPropertyTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\JsonSeeker_TestData\\ReadProperty.json";

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
            var propVal = JsonSeeker.ReadProperty(TEST_FILE, "stringProp");
            Assert.AreEqual(propVal, MockData.TEXTS[0]);
        }

        [TestMethod]
        public void Read_Number()
        {
            var propVal = JsonSeeker.ReadProperty(TEST_FILE, "numberProp");
            Assert.AreEqual(propVal, MockData.NUMBERS[0].ToString());
        }

        [TestMethod]
        public void Read_Complex()
        {
            var propVal = JsonSeeker.ReadProperty(TEST_FILE, "complexProp");
            var expected = ToJString(TestJsonModel2.GetDefault());

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_StringArray()
        {
            var propVal = JsonSeeker.ReadProperty(TEST_FILE, "stringArrayProp");
            var expected = ToJString(MockData.STRING_ARRAY1);

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_IntArray()
        {
            var propVal = JsonSeeker.ReadProperty(TEST_FILE, "numberArrayProp");
            var expected = ToJString(MockData.NUMBERS_ARRAY1);

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_ComplexArray()
        {
            var propVal = JsonSeeker.ReadProperty(TEST_FILE, "complexArrayProp");
            var expected = ToJString(MockData.COMPLEX_ARRAY);

            Assert.AreEqual(expected, propVal);
        }
    }
}
