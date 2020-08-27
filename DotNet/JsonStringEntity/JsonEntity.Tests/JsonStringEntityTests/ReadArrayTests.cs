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
        private static string TEST_FILE = "..\\..\\App_Data\\JsonSeeker_TestData\\ReadArray.json";

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

        #region string array
        [TestMethod]
        public void StringArray_All()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).ToList();

            CollectionAssert.AreEqual(
                MockData.TEXTS[0].Split(' '),
                propVal);
        }

        [TestMethod]
        public void StringArray_SkipOne_All()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).Skip(1);

            CollectionAssert.AreEqual(
                MockData.TEXTS[0].Split(' ').Skip(1).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void StringArray_First()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).First();

            Assert.AreEqual(
                MockData.TEXTS[0].Split(' ').First(),
                propVal);
        }

        [TestMethod]
        public void StringArray_FirstThree()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).Take(3);

            CollectionAssert.AreEqual(
                MockData.TEXTS[0].Split(' ').Take(3).ToArray(),
                propVal.ToArray());
        }
        #endregion

        #region number array
        [TestMethod]
        public void NumberArray_All()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("numberArrayProp");

            CollectionAssert.AreEqual(
                MockData.NUMBERS_ARRAY1.Select(p => p.ToString()).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void NumberArray_First()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("numberArrayProp").First();

            Assert.AreEqual(
                MockData.NUMBERS_ARRAY1.First().ToString(),
                propVal);
        }

        [TestMethod]
        public void NumberArray_FirstThree()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("numberArrayProp").Take(3).Select(p => Convert.ToInt32(p));

            CollectionAssert.AreEqual(
                MockData.NUMBERS_ARRAY1.Take(3).ToArray(),
                propVal.ToArray());
        }
        #endregion

        #region complex array
        /*
         * [{"stringProp1":"Proin tincidunt, ligula vel vulputate efficitur, diam ","stringProp2":"Nulla vitae ipsum a nisi blandit elementum.","intProp1":1989,"intProp2":2012,"intArrayProp1":[85,5,69,98,72],"intArrayProp2":[85,5,69,98,72],"stringArrayProp1":["Proin","tincidunt,","ligula","vel","vulputate","efficitur,","diam",""],"stringArrayProp2":["Nulla","vitae","ipsum","a","nisi","blandit","elementum."]},
            {"stringProp1":"Proin tincidunt, ligula vel vulputate efficitur, diam ","stringProp2":"Nulla vitae ipsum a nisi blandit elementum.","intProp1":1989,"intProp2":2012,"intArrayProp1":[85,5,69,98,72],"intArrayProp2":[85,5,69,98,72],"stringArrayProp1":["Proin","tincidunt,","ligula","vel","vulputate","efficitur,","diam",""],"stringArrayProp2":["Nulla","vitae","ipsum","a","nisi","blandit","elementum."]},
            {"stringProp1":"Proin tincidunt, ligula vel vulputate efficitur, diam ","stringProp2":"Nulla vitae ipsum a nisi blandit elementum.","intProp1":1989,"intProp2":2012,"intArrayProp1":[85,5,69,98,72],"intArrayProp2":[85,5,69,98,72],"stringArrayProp1":["Proin","tincidunt,","ligula","vel","vulputate","efficitur,","diam",""],"stringArrayProp2":["Nulla","vitae","ipsum","a","nisi","blandit","elementum."]}]
         */

        [TestMethod]
        public void Complex_All()
        {

            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("complexArrayProp").ToArray();

            Assert.AreEqual(3, propVal.Length);

            CollectionAssert.AreEqual(
                MockData.COMPLEX_ARRAY.Select(ToJString).ToArray(),
                propVal);
        }

        [TestMethod]
        public void Complex_First()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("complexArrayProp").First();

            Assert.AreEqual(
                ToJString(MockData.COMPLEX_ARRAY.First()),
                propVal);
        }

        [TestMethod]
        public void Complex_SkipOne_First()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadArray("complexArrayProp").Skip(1).First();

            Assert.AreEqual(
                ToJString(MockData.COMPLEX_ARRAY.Skip(1).First()),
                propVal);
        }
        #endregion
    }
}
