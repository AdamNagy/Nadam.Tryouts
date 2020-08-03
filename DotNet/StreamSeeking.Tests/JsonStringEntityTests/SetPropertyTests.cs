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
            JsonSeeker.SetProperty(TEST_FILE, "stringProp", "\"Hello change!!\"");
            var result = JsonSeeker.ReadProperty(TEST_FILE, "stringProp");

            Assert.AreEqual("Hello change!!", result);
        }

        [TestMethod]
        public void Write_Number_Middle()
        {
            JsonSeeker.SetProperty(TEST_FILE, "numberProp", "666");
            var result = JsonSeeker.ReadProperty(TEST_FILE, "numberProp");

            Assert.AreEqual("666", result);
        }

        [TestMethod]
        public void Write_Complex_Middle()
        {
            JsonSeeker.SetProperty(TEST_FILE, "complexProp", "{\"newProp1\": 1234}");
            var result = JsonSeeker.ReadProperty(TEST_FILE, "complexProp");

            Assert.AreEqual("{\"newProp1\": 1234}", result);
        }

        [TestMethod]
        public void Write_NumberArray_Middle()
        {
            JsonSeeker.SetProperty(TEST_FILE, "numberArrayProp", "[1,2,3,4,5]");
            var result = JsonSeeker.ReadProperty(TEST_FILE, "numberArrayProp");

            Assert.AreEqual("[1,2,3,4,5]", result);
        }

        [TestMethod]
        public void Write_StringArray_Middle()
        {
            JsonSeeker.SetProperty(TEST_FILE, "stringArrayProp", "[\"Adam\",\"Janos\", \"Diablo\"]");
            var result = JsonSeeker.ReadProperty(TEST_FILE, "stringArrayProp");

            Assert.AreEqual("[\"Adam\",\"Janos\", \"Diablo\"]", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_Exception()
        {
            JsonSeeker.SetProperty(TEST_FILE, "textProp", "\"some longer text to check\"");
        }

        [TestMethod]
        public void Should_Remain_Valid_Json()
        {
            JsonSeeker.SetProperty(TEST_FILE, "stringProp", "\"Hello change2!!\"");
            JsonSeeker.SetProperty(TEST_FILE, "numberProp", "666");
            JsonSeeker.SetProperty(TEST_FILE, "complexProp", "{\"newProp1\": 1234}");
            JsonSeeker.SetProperty(TEST_FILE, "numberArrayProp", "[1,2,3,4,5]");
            JsonSeeker.SetProperty(TEST_FILE, "stringArrayProp", "[\"Adam\",\"Janos\", \"Diablo\"]");

            var json = JsonConvert.DeserializeObject<TestJsonModel>(JsonSeeker.ReadProperty(TEST_FILE));

            Assert.AreEqual(json.NumberProp, 666);
            Assert.AreEqual(json.StringProp, "Hello change2!!");
            CollectionAssert.AreEqual(json.StringArrayProp.ToArray(), new string[] {"Adam", "Janos", "Diablo"});
            CollectionAssert.AreEqual(json.NumberArrayProp.ToArray(), new int[] { 1, 2, 3, 4, 5 });
        }
    }
}
