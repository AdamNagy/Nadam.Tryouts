using System.IO;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace JsonStringEntityTests
{
    [TestClass]
    public class ExtendProperty
    {
        private static string TEST_FILE = "..\\..\\App_Data\\JsonStringEntityTests\\ExtendProperty_Tests.txt";
        private const string prop1 = "\"stringArrayProp\":[\"alma\",\"korte\",\"szilva\"]";
        private const string prop2 = "\"complexProp\":{\"prop1\":123,\"prop2\":\"alma\"}";
        private const string prop3 = "\"complexArrayProp\":[{\"prop1\":123,\"prop2\":\"alma\"},{\"prop1\":456,\"prop2\":\"korte\"},{\"prop1\":789,\"prop2\":\"szilva\"}]";


        [TestInitialize]
        public void BeforeAll()
        {
            File.WriteAllText(TEST_FILE, $"{{{prop1},{prop2},{prop3}}}");
        }

        [TestMethod]
        public void Append_Array_Begining()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("\"barack\"", "stringArrayProp", AppendPosition.begining);
            var result = sut.Read("stringArrayProp");

            Assert.AreEqual("[\"barack\",\"alma\",\"korte\",\"szilva\"]", result);
        }

        [TestMethod]
        public void Append_Array_End()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("\"barack\"", "stringArrayProp", AppendPosition.end);
            var result = sut.Read("stringArrayProp");

            Assert.AreEqual("[\"alma\",\"korte\",\"szilva\",\"barack\"]", result);
        }

        [TestMethod]
        public void Append_Complex_Begining()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("\"newProp\":24816", "complexProp", AppendPosition.begining);
            var result = sut.Read("complexProp");

            Assert.AreEqual("{\"newProp\":24816,\"prop1\":123,\"prop2\":\"alma\"}", result);
        }

        [TestMethod]
        public void Append_Complex_End()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("\"newProp\":24816", "complexProp", AppendPosition.end);
            var result = sut.Read("complexProp");

            Assert.AreEqual("{\"prop1\":123,\"prop2\":\"alma\",\"newProp\":24816}", result);
        }

        [TestMethod]
        public void Append_ComplexArray_Begining()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("{\"prop1\":000,\"prop2\":\"szolo\"}", "complexArrayProp", AppendPosition.begining);
            var result = sut.Read("complexArrayProp");

            Assert.AreEqual("[{\"prop1\":000,\"prop2\":\"szolo\"},{\"prop1\":123,\"prop2\":\"alma\"},{\"prop1\":456,\"prop2\":\"korte\"},{\"prop1\":789,\"prop2\":\"szilva\"}]", result);
        }

        [TestMethod]
        public void Append_ComplexArray_End()
        {
            var sut = new JsonStringEntity(TEST_FILE);

            sut.ExtendProperty("{\"prop1\":000,\"prop2\":\"szolo\"}", "complexArrayProp", AppendPosition.end);
            var result = sut.Read("complexArrayProp");

            Assert.AreEqual("[{\"prop1\":123,\"prop2\":\"alma\"},{\"prop1\":456,\"prop2\":\"korte\"},{\"prop1\":789,\"prop2\":\"szilva\"},{\"prop1\":000,\"prop2\":\"szolo\"}]", result);
        }

        [TestMethod]
        public void ShouldRemainValidJson()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            sut.ExtendProperty("\"newProp\":24816", "complexProp", AppendPosition.begining);
            sut.ExtendProperty("\"barack\"", "stringArrayProp", AppendPosition.end);
            sut.ExtendProperty("{\"prop1\":000,\"prop2\":\"szőlő\"}", "complexArrayProp", AppendPosition.begining);

            var parsed = JObject.Parse(sut.Read());
            Assert.IsNotNull(parsed);
        }
    }
}
