using System;
using System.Collections.Generic;
using System.Linq;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TestData;

namespace JsonDictionaryTest
{
    [TestClass]
    public class ToObjectTests
    {
        private static TestJsonModel _testModel;
        private static (string, string)[] _testJDict;

        [TestInitialize]
        public void BeforeAll()
        {
            _testModel = TestJsonModel.GetDefault();
            _testJDict = GetExpected().ToArray();
        }

        public static IEnumerable<(string key, string value)> GetExpected()
        {
            var expected = new List<(string key, string value)>();
            expected.Add((key: "stringProp1", value: _testModel.ComplexProp.StringProp1));
            expected.Add((key: "stringProp2", value: _testModel.ComplexProp.StringProp2));
            expected.Add((key: "intProp1", value: _testModel.ComplexProp.IntProp1.ToString()));
            expected.Add((key: "intProp2", value: _testModel.ComplexProp.IntProp2.ToString()));
            expected.Add((key: "intArrayProp1", value: $"[{String.Join(",", _testModel.ComplexProp.IntArrayProp1.Select(p => p.ToString())) }]"));
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
        public void Test1()
        {
            var result = JsonDictionary.ToObject<TestJsonModel2>(new JsonDictionary(_testJDict));
            var expected = _testModel.ComplexProp;

            CollectionAssert.AreEqual(expected.IntArrayProp1.ToArray(), result.IntArrayProp1.ToArray());
            CollectionAssert.AreEqual(expected.IntArrayProp2.ToArray(), result.IntArrayProp2.ToArray());

            CollectionAssert.AreEqual(expected.StringArrayProp1.ToArray(), result.StringArrayProp1.ToArray());
            CollectionAssert.AreEqual(expected.StringArrayProp2.ToArray(), result.StringArrayProp2.ToArray());

            Assert.AreEqual(expected.IntProp1, result.IntProp1);
            Assert.AreEqual(expected.IntProp2, result.IntProp2);

            Assert.AreEqual(expected.StringProp1, expected.StringProp1);
            Assert.AreEqual(expected.StringProp2, expected.StringProp2);
        }

        [TestMethod]
        public void Test2()
        {
            var testModel = TestJsonModel.GetDefault();
            var keyValues = new List<(string, string)>()
            {
                ("complexArrayProp", ToJString(testModel.ComplexArrayProp)),
                ("complexProp", ToJString(testModel.ComplexProp))
            };
            var result = JsonDictionary.ToObject<TestJsonModel>(new JsonDictionary(keyValues));

            CollectionAssert.AreEqual(testModel.ComplexProp.IntArrayProp1.ToArray(), result.ComplexProp.IntArrayProp1.ToArray());
            CollectionAssert.AreEqual(testModel.ComplexProp.IntArrayProp2.ToArray(), result.ComplexProp.IntArrayProp2.ToArray());

            CollectionAssert.AreEqual(testModel.ComplexProp.StringArrayProp1.ToArray(), result.ComplexProp.StringArrayProp1.ToArray());
            CollectionAssert.AreEqual(testModel.ComplexProp.StringArrayProp2.ToArray(), result.ComplexProp.StringArrayProp2.ToArray());

            Assert.AreEqual(testModel.ComplexProp.IntProp1, result.ComplexProp.IntProp1);
            Assert.AreEqual(testModel.ComplexProp.IntProp2, result.ComplexProp.IntProp2);

            Assert.AreEqual(testModel.ComplexProp.StringProp1, result.ComplexProp.StringProp1);
            Assert.AreEqual(testModel.ComplexProp.StringProp2, result.ComplexProp.StringProp2);

            Assert.AreEqual(testModel.ComplexArrayProp.Count(), result.ComplexArrayProp.Count());
        }
    }
}
