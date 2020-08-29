using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;
using System.IO;

namespace JsonStringEntityTests
{
    [TestClass]
    public class Read
    {
        private static string TEST_FILE_PATH = "..\\..\\App_Data\\JsonStringEntityTests\\Read_Tests.json";
        private static TestJsonModel TEST_MODEL = TestJsonModel.GetDefault();

        [TestInitialize]
        public void BeforeAll()
        {
            if (!File.Exists(TEST_FILE_PATH))
            {
                var content = TEST_MODEL.ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH))
                    file.Write(content, 0, content.Length);
            }
        }

        [TestMethod]
        public void Read_String()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.Read("stringProp");
            Assert.AreEqual(propVal, TEST_MODEL.StringProp);
        }

        [TestMethod]
        public void Read_Number()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.Read("numberProp");
            Assert.AreEqual(propVal, TEST_MODEL.NumberProp.ToString());
        }

        [TestMethod]
        public void Read_Complex()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.Read("complexProp");
            var expected = TEST_MODEL.ComplexProp.ToJsonString();

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_StringArray()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.Read("stringArrayProp");
            var expected = TEST_MODEL.StringArrayProp.ToJsonString();

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_IntArray()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.Read("numberArrayProp");
            var expected = TEST_MODEL.NumberArrayProp.ToJsonString();

            Assert.AreEqual(expected, propVal);
        }

        [TestMethod]
        public void Read_ComplexArray()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.Read("complexArrayProp");
            var expected = TEST_MODEL.ComplexArrayProp.ToJsonString();

            Assert.AreEqual(expected, propVal);
        }
    }
}
