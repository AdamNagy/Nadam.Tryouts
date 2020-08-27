using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private static string TEST_FILE = "..\\..\\App_Data\\JsonSeeker_TestData\\ReadObject.json";
        private static TestJsonModel _testModel;
        private static (string, string)[] _testJDict; 

        [TestInitialize]
        public void BeforeAll()
        {
            _testModel = TestJsonModel.GetDefault();
            File.WriteAllText(TEST_FILE, ToJString(_testModel));
            _testJDict = GetExpected().ToArray();
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
            expected.Add((key: "stringProp1", value: "Proin tincidunt, ligula vel vulputate efficitur, diam"));
            expected.Add((key: "stringProp2", value: "Zero vitae ipsum a nisi blandit elementum."));
            expected.Add((key: "intProp1", value: "1989"));
            expected.Add((key: "intProp2", value: "2012"));
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

        [TestMethod]
        public void All()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadObject("complexProp").ToArray();

            CollectionAssert.AreEqual(
                _testJDict,
                propVal);
        }

        [TestMethod]
        public void SkipOne_ThenAll()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadObject("complexProp").Skip(1).ToArray();

            CollectionAssert.AreEqual(
                _testJDict.Skip(1).ToArray(),
                propVal);
        }

        [TestMethod]
        public void First()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadObject("complexProp").First();

            Assert.AreEqual(
                _testJDict.First(),
                propVal);
        }

        [TestMethod]
        public void FirstThree()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadObject("complexProp").Take(3);
            
            CollectionAssert.AreEqual(
                _testJDict.Take(3).ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void SkipTwo_Take3()
        {
            var sut = new JsonStringEntity(TEST_FILE);
            var propVal = sut.ReadObject("complexProp").Skip(2).Take(3);

            CollectionAssert.AreEqual(
                _testJDict.Skip(2).Take(3).ToArray(),
                propVal.ToArray());
        }
    }
}
