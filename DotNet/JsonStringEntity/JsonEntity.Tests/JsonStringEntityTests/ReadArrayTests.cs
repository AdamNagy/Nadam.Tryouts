using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using TestData;

namespace JsonStringEntityTests
{
    [TestClass]
    public class ReadArrayTests
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
        public void Read_StringArray_All()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).ToList();

            CollectionAssert.AreEqual(
                MockData.TEXTS[0].Split(' '),
                propVal);
        }

        [TestMethod]
        public void Read_StringArray_SkipOne_All()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).Skip(1);

            CollectionAssert.AreEqual(
                MockData.TEXTS[0].Split(' ').Skip(1).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void Read_StringArray_First()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).First();

            Assert.AreEqual(
                MockData.TEXTS[0].Split(' ').First(),
                propVal);
        }

        [TestMethod]
        public void Read_NumberArray_First()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("numberArrayProp").First();

            Assert.AreEqual(
                MockData.NUMBERS_ARRAY1.First().ToString(),
                propVal);
        }

        [TestMethod]
        public void Read_NumberArray_All()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("numberArrayProp");

            CollectionAssert.AreEqual(
                MockData.NUMBERS_ARRAY1.Select(p => p.ToString()).ToArray(),
                propVal.ToArray());
        }
    }
}
