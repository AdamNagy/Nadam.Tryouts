using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using DataEntity;
using TestData;

namespace JsonStringEntityTests
{
    [TestClass]
    public class SetProperty
    {
        private static string TEST_FILE_PATH = "..\\..\\App_Data\\JsonSeeker_TestData\\SetProperty.json";
        private static TestJsonModel TEST_FILE;

        [TestInitialize]
        public void BeforeAll()
        {
            TEST_FILE = TestJsonModel.GetDefault();
            File.WriteAllText(TEST_FILE_PATH, TEST_FILE.ToJsonString());
        }

        [TestMethod]
        public void Set_String()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);

            sut.SetProperty("stringProp", "\"Hello change!!\"");
            var result = sut.Read("stringProp");

            Assert.AreEqual("Hello change!!", result);
        }

        [TestMethod]
        public void Set_Number()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);

            sut.SetProperty("numberProp", "666");
            var result = sut.Read("numberProp");

            Assert.AreEqual("666", result);
        }

        [TestMethod]
        public void Set_Complex()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);

            sut.SetProperty("complexProp", "{\"newProp1\": 1234}");
            var result = sut.Read("complexProp");

            Assert.AreEqual("{\"newProp1\": 1234}", result);
        }

        [TestMethod]
        public void Set_NumberArray()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);

            sut.SetProperty("numberArrayProp", "[1,2,3,4,5]");
            var result = sut.Read("numberArrayProp");

            Assert.AreEqual("[1,2,3,4,5]", result);
        }

        [TestMethod]
        public void Set_StringArray()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);

            sut.SetProperty("stringArrayProp", "[\"Adam\",\"Janos\", \"Diablo\"]");
            var result = sut.Read("stringArrayProp");

            Assert.AreEqual("[\"Adam\",\"Janos\", \"Diablo\"]", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Set_NotExistedProp_And_Should_Throw_Exception()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            sut.SetProperty("textProp", "\"some longer text to check\"");
        }

        [TestMethod]
        public void Should_Remain_Valid_Json()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);

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
