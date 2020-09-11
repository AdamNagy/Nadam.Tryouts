using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestData;

namespace JsonStringEntityTests
{
    [TestClass]
    public class ReadObjectTests
    {
        private static string TEST_FILE_PATH = "..\\..\\App_Data\\JsonStringEntityTests\\ReadObject_Tests.json";
        private static string TEST_FILE_PATH_2 = "..\\..\\App_Data\\JsonStringEntityTests\\ReadObject_Tests_2.json";
        private static TestJsonModel _testModel;

        private static (string, string)[] _testJDict; 

        [TestInitialize]
        public void BeforeAll()
        {
            _testModel = TestJsonModel.GetDefault();
            _testJDict = GetExpected().ToArray();

            if (!File.Exists(TEST_FILE_PATH))
            {
                var content = _testModel.ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH))
                    file.Write(content, 0, content.Length);
            }

            if (!File.Exists(TEST_FILE_PATH_2))
            {
                var content = _testModel.ComplexProp.ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH_2))
                    file.Write(content, 0, content.Length);
            }
        }

        /*
         *"complexProp":{
             * "stringProp1":"Proin tincidunt, ligula vel vulputate efficitur, diam ",
             * "stringProp2":"Nulla vitae ipsum a nisi blandit elementum.",
             * "intProp1":1989,
             * "intProp2":2012,
             * "intArrayProp1":[24,78,25,2,30],
             * "intArrayProp2":[24,78,25,2,30],
             * "stringArrayProp1":["Proin","tincidunt,","ligula","vel","vulputate","efficitur,","diam",""],
             * "stringArrayProp2":["Nulla","vitae","ipsum","a","nisi","blandit","elementum."]
         * }
         */

        public static IEnumerable<(string key, string value)> GetExpected()
        {
            var expected = new List<(string key, string value)>();
            expected.Add((key: "stringProp1", value: _testModel.ComplexProp.StringProp1));
            expected.Add((key: "stringProp2", value: _testModel.ComplexProp.StringProp2));
            expected.Add((key: "intProp1", value: _testModel.ComplexProp.IntProp1.ToString()));
            expected.Add((key: "intProp2", value: _testModel.ComplexProp.IntProp2.ToString()));
            expected.Add((key: "intArrayProp1", value: $"[{String.Join(",", _testModel.ComplexProp.IntArrayProp1.Select(p => p.ToString())) }]" ));
            expected.Add((key: "intArrayProp2", value: $"[{String.Join(",", _testModel.ComplexProp.IntArrayProp2.Select(p => p.ToString())) }]"));
            expected.Add(
                (
                    key: "stringArrayProp1",
                    value: $"[{String.Join(",", _testModel.ComplexProp.StringArrayProp1.Select(p => $"\"{p}\""))}]"
                )
            );
            expected.Add(
                (
                    key: "stringArrayProp2",
                    value: $"[{String.Join(",", _testModel.ComplexProp.StringArrayProp2.Select(p => $"\"{p}\""))}]"
                )
            );

            return expected;
        }

        #region Read a propertywhich type is complex
        [TestMethod]
        public void All()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.ReadObject("complexProp").ToArray();

            CollectionAssert.AreEqual(
                _testJDict,
                propVal);
        }

        [TestMethod]
        public void SkipOne_ThenAll()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.ReadObject("complexProp").Skip(1).ToArray();

            CollectionAssert.AreEqual(
                _testJDict.Skip(1).ToArray(),
                propVal);
        }

        [TestMethod]
        public void First()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.ReadObject("complexProp").First();

            Assert.AreEqual(
                _testJDict.First(),
                propVal);
        }

        [TestMethod]
        public void FirstThree()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.ReadObject("complexProp").Take(3);
            
            CollectionAssert.AreEqual(
                _testJDict.Take(3).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void SkipTwo_Take3()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH);
            var propVal = sut.ReadObject("complexProp").Skip(2).Take(3);

            CollectionAssert.AreEqual(
                _testJDict.Skip(2).Take(3).ToArray(),
                propVal.ToArray());
        }
        #endregion

        #region Read from root, not a property
        [TestMethod]
        public void FromObjectJson_All()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadObject().ToArray();

            CollectionAssert.AreEqual(
                _testJDict,
                propVal);
        }

        [TestMethod]
        public void FromObjectJson_SkipOne_ThenAll()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadObject().Skip(1).ToArray();

            CollectionAssert.AreEqual(
                _testJDict.Skip(1).ToArray(),
                propVal);
        }

        [TestMethod]
        public void FromObjectJson_First()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadObject().First();

            Assert.AreEqual(
                _testJDict.First(),
                propVal);
        }

        [TestMethod]
        public void FromObjectJson_FirstThree()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadObject().Take(3);

            CollectionAssert.AreEqual(
                _testJDict.Take(3).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void FromObjectJson_SkipTwo_Take3()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadObject().Skip(2).Take(3);

            CollectionAssert.AreEqual(
                _testJDict.Skip(2).Take(3).ToArray(),
                propVal.ToArray());
        }
        #endregion
    }
}
