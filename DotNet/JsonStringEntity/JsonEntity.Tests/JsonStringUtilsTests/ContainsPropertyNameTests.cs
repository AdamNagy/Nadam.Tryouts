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
            Assert.AreEqual("\"somePropName\":", result["propertyName"].Value);
        }
    }
}
