using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StreamSeeking.Tests.MockClasses;
using System;
using System.IO;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class ReadTests
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
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.Read("stringProp");
            Assert.AreEqual(propVal, MockData.TEXTS[0]);
        }

        [TestMethod]
        public void Read_Number()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.Read("numberProp");
            Assert.AreEqual(propVal, MockData.NUMBERS[0].ToString());
        }

        [TestMethod]
        public void Read_Complex()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.Read("complexProp");
            var expected = ToJString(TestJsonModel2.GetDefault());

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_StringArray()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.Read("stringArrayProp");
            var expected = ToJString(MockData.STRING_ARRAY1);

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_IntArray()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.Read("numberArrayProp");
            var expected = ToJString(MockData.NUMBERS_ARRAY1);

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_ComplexArray()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.Read("complexArrayProp");
            var expected = ToJString(MockData.COMPLEX_ARRAY);

            Assert.AreEqual(expected, propVal);
        }
    }
}
