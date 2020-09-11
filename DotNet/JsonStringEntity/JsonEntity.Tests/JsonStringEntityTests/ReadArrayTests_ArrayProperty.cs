using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using TestData;
using DataEntity;

namespace JsonStringEntityTests
{
    [TestClass]
    public class ReadArrayTests_ArrayProperty
    {
        private static readonly string TEST_FILE_PATH_1 = "..\\..\\App_Data\\JsonStringEntityTests\\ReadArray_Tests.json";
        private static readonly TestJsonModel TEST_MODEL_1 = TestJsonModel.GetDefault();

        [TestInitialize]
        public void BeforeAll()
        {
            if (!File.Exists(TEST_FILE_PATH_1))
            {
                var content = TEST_MODEL_1.ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH_1))
                    file.Write(content, 0, content.Length);
            }
        }

        #region string array
        [TestMethod]
        public void String_All()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).ToList();

            CollectionAssert.AreEqual(
                TEST_MODEL_1.StringArrayProp.ToArray(),
                propVal);
        }

        [TestMethod]
        public void String_SkipOne_All()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).Skip(1);

            CollectionAssert.AreEqual(
                TEST_MODEL_1.StringArrayProp.Skip(1).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void String_First()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).First();

            Assert.AreEqual(
                TEST_MODEL_1.StringArrayProp.First(),
                propVal);
        }

        [TestMethod]
        public void String_FirstThree()
        {
            //  "Lorem ipsum dolor sit amet consectetur adipiscing elit."
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("stringArrayProp").Select(p => p.Trim('\"')).Take(3);

            CollectionAssert.AreEqual(
                TEST_MODEL_1.StringArrayProp.Take(3).ToArray(),
                propVal.ToArray());
        }
        #endregion

        #region number array
        [TestMethod]
        public void Number_All()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("numberArrayProp");

            CollectionAssert.AreEqual(
                TEST_MODEL_1.NumberArrayProp.Select(p => p.ToString()).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void Number_First()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("numberArrayProp").First();

            Assert.AreEqual(
                TEST_MODEL_1.NumberArrayProp.First().ToString(),
                propVal);
        }

        [TestMethod]
        public void Number_FirstThree()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("numberArrayProp").Take(3).Select(p => Convert.ToInt32(p));

            CollectionAssert.AreEqual(
                TEST_MODEL_1.NumberArrayProp.Take(3).ToArray(),
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

            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("complexArrayProp").ToArray();

            Assert.AreEqual(3, propVal.Length);

            CollectionAssert.AreEqual(
                TEST_MODEL_1.ComplexArrayProp.Select(p => p.ToJsonString()).ToArray(),
                propVal);
        }

        [TestMethod]
        public void Complex_First()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("complexArrayProp").First();

            Assert.AreEqual(
                TEST_MODEL_1.ComplexArrayProp.First().ToJsonString(),
                propVal);
        }

        [TestMethod]
        public void Complex_SkipOne_First()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_1);
            var propVal = sut.ReadArray("complexArrayProp").Skip(1).First();

            Assert.AreEqual(
                TEST_MODEL_1.ComplexArrayProp.Skip(1).First().ToJsonString(),
                propVal);
        }
        #endregion
    }
}
