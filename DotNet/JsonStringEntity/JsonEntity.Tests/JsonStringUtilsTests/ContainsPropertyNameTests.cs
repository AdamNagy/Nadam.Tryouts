using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonStringUtilsTests
{
    [TestClass]
    public class ContainsPropertyNameTests
    {
        [TestMethod]
        public void Contains_Beginging()
        {
            var text = "\"somePropName\": []";
            var exist = JsonStringUtils.ContainsPropertyName(text, out var result);

            /*
             groups["word"].Value,
             groups[0].Index,
             groups[1].Index
            */
            
            Assert.IsTrue(exist);
            Assert.AreEqual("somePropName", result.name);
            Assert.AreEqual(0, result.startPos);
            Assert.AreEqual("\"somePropName\":".Length, result.length);
        }

        [TestMethod]
        public void Contains_Middle()
        {
            var text = "123\"somePropName\": []";
            var exist = JsonStringUtils.ContainsPropertyName(text, out var result);

            Assert.IsTrue(exist);
            Assert.AreEqual("somePropName", result.name);
            Assert.AreEqual(3, result.startPos);
            Assert.AreEqual("\"somePropName\":".Length, result.length);
        }

        [TestMethod]
        public void Contains_End()
        {
            var text = "123456\"somePropName\":";
            var exist = JsonStringUtils.ContainsPropertyName(text, out var result);

            Assert.IsTrue(exist);
            Assert.AreEqual("somePropName", result.name);
            Assert.AreEqual(6, result.startPos);
            Assert.AreEqual("\"somePropName\":".Length, result.length);
        }

        [TestMethod]
        public void NotContains_ColonMissing()
        {
            var text = "123456\"somePropName\"";
            var exist = JsonStringUtils.ContainsPropertyName(text, out var result);

            Assert.IsFalse(exist);
            Assert.AreEqual("", result.name);
            Assert.AreEqual(-1, result.startPos);
            Assert.AreEqual(-1, result.length);
        }

        [TestMethod]
        public void NotContains_QuoteMarkMissing()
        {
            var text = "123456\"somePropName:";
            var exist = JsonStringUtils.ContainsPropertyName(text, out var result);

            Assert.IsFalse(exist);
            Assert.AreEqual("", result.name);
            Assert.AreEqual(-1, result.startPos);
            Assert.AreEqual(-1, result.length);
        }

        [TestMethod]
        public void StartPos_And_Length_ShouldBeOk()
        {
            var text = "\"somePropName\": {\"prop1\": 234, \"prop2\": \"some string\"}";
            var exist = JsonStringUtils.ContainsPropertyName(text, out var result);

            var propVale = text.Substring(result.startPos + result.length);
            Assert.AreEqual(" {\"prop1\": 234, \"prop2\": \"some string\"}", propVale);
        }
    }
}
