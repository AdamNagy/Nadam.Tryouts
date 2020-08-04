using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StreamSeeking.Tests.MockClasses;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class SetPropertyTests
    {
        private static string TEST_FILE = "..\\..\\App_Data\\JsonSeeker_TestData\\SetProperty.json";

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
        public void Write_String_Begining()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.SetProperty("stringProp", "\"Hello change!!\"");
            var result = sut.Read("stringProp");

            Assert.AreEqual("Hello change!!", result);
        }

        [TestMethod]
        public void Write_Number_Middle()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.SetProperty("numberProp", "666");
            var result = sut.Read("numberProp");

            Assert.AreEqual("666", result);
        }

        [TestMethod]
        public void Write_Complex_Middle()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.SetProperty("complexProp", "{\"newProp1\": 1234}");
            var result = sut.Read("complexProp");

            Assert.AreEqual("{\"newProp1\": 1234}", result);
        }

        [TestMethod]
        public void Write_NumberArray_Middle()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.SetProperty("numberArrayProp", "[1,2,3,4,5]");
            var result = sut.Read("numberArrayProp");

            Assert.AreEqual("[1,2,3,4,5]", result);
        }

        [TestMethod]
        public void Write_StringArray_Middle()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.SetProperty("stringArrayProp", "[\"Adam\",\"Janos\", \"Diablo\"]");
            var result = sut.Read("stringArrayProp");

            Assert.AreEqual("[\"Adam\",\"Janos\", \"Diablo\"]", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_Exception()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            sut.SetProperty("textProp", "\"some longer text to check\"");
        }

        [TestMethod]
        public void Should_Remain_Valid_Json()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.SetProperty("stringProp", "\"Hello change2!!\"");
            sut.SetProperty("numberProp", "666");
            sut.SetProperty("complexProp", "{\"newProp1\": 1234}");
            sut.SetProperty("numberArrayProp", "[1,2,3,4,5]");
            sut.SetProperty("stringArrayProp", "[\"Adam\",\"Janos\", \"Diablo\"]");

            var json = JsonConvert.DeserializeObject<TestJsonModel>(sut.Read());

            Assert.AreEqual(json.NumberProp, 666);
            Assert.AreEqual(json.StringProp, "Hello change2!!");
            CollectionAssert.AreEqual(json.StringArrayProp.ToArray(), new string[] {"Adam", "Janos", "Diablo"});
            CollectionAssert.AreEqual(json.NumberArrayProp.ToArray(), new int[] { 1, 2, 3, 4, 5 });
        }
    }
}
