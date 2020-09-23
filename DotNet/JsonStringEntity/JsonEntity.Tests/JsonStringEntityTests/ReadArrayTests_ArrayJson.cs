using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using TestData;

namespace JsonStringEntityTests
{
    [TestClass]
    public class ReadArrayTests_ArrayJson
    {
        private static readonly string TEST_FILE_PATH_2 = "..\\..\\App_Data\\JsonStringEntityTests\\ReadArray_Tests_2.json";
        private static readonly IEnumerable<string> TEST_MODEL_2 = MockData.LONG_STRING_ARRAY1;

        private static readonly string TEST_FILE_PATH_3 = "..\\..\\App_Data\\JsonStringEntityTests\\ReadArray_Tests_3.json";
        private static readonly IEnumerable<int> TEST_MODEL_3 = MockData.NUMBERS_ARRAY1;

        private static readonly string TEST_FILE_PATH_4 = "..\\..\\App_Data\\JsonStringEntityTests\\ReadArray_Tests_4.json";
        private static readonly IEnumerable<TestJsonModel2> TEST_MODEL_4 = MockData.COMPLEX_ARRAY;

        [TestInitialize]
        public void BeforeAll()
        {
            if (!File.Exists(TEST_FILE_PATH_2))
            {
                var content = TEST_MODEL_2.Select(p => p.Replace(',', '|')).ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH_2))
                    file.Write(content, 0, content.Length);
            }

            if (!File.Exists(TEST_FILE_PATH_3))
            {
                var content = TEST_MODEL_3.ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH_3))
                    file.Write(content, 0, content.Length);
            }

            if (!File.Exists(TEST_FILE_PATH_4))
            {
                var content = TEST_MODEL_4.ToJsonString().ToByArray();
                using (var file = File.Create(TEST_FILE_PATH_4))
                    file.Write(content, 0, content.Length);
            }
        }

        #region string
        [TestMethod]
        public void Strings_All()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadArray().Select(p => p.Trim('\"').Replace('|', ','));

            CollectionAssert.AreEqual(
                TEST_MODEL_2.ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void Strings_Skip2_Read3()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_2);
            var propVal = sut.ReadArray().Select(p => p.Trim('\"').Replace('|', ','));

            CollectionAssert.AreEqual(
                TEST_MODEL_2.Skip(2).Take(3).ToArray(),
                propVal.Skip(2).Take(3).ToArray());
        }
        #endregion

        #region number
        [TestMethod]
        public void Numbers_All()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_3);
            var propVal = sut.ReadArray().Select(p => p.Trim('\"')).Select(q => Convert.ToInt32(q));

            CollectionAssert.AreEqual(
                TEST_MODEL_3.ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void Numbers_Skip2_Read3()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_3);
            var propVal = sut.ReadArray().Select(p => p.Trim('\"')).Select(q => Convert.ToInt32(q));

            CollectionAssert.AreEqual(
                TEST_MODEL_3.Skip(2).Take(3).ToArray(),
                propVal.Skip(2).Take(3).ToArray());
        }
        #endregion

        #region complex
        [TestMethod]
        public void Complex_All()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_4);
            var propVal = sut.ReadArray();

            var expected = JArray.Parse(File.ReadAllText(TEST_FILE_PATH_4));

            Assert.AreEqual(expected.Count(), propVal.Count());

            CollectionAssert.AreEqual(
                propVal.ToArray(),
                propVal.ToArray());
        }

        [TestMethod]
        public void Complex_Skip2_Read3()
        {
            var sut = new JsonStringEntity(TEST_FILE_PATH_4);
            var propVal = sut.ReadArray();

            var expected = JArray.Parse(File.ReadAllText(TEST_FILE_PATH_4));

            Assert.AreEqual(expected.Count(), propVal.Count());

            CollectionAssert.AreEqual(
                expected.Skip(2).Take(3).Select(p => p.ToJsonString()).ToArray(),
                propVal.Skip(2).Take(3).ToArray());
        }
        #endregion
    }
}
