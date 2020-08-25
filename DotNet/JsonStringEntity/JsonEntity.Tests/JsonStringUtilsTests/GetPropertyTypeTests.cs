using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonStringUtilsTests
{
    [TestClass]
    public class GetPropertyType
    {
        [TestMethod]
        public void ShouldBe_JsonPropertyType_Complex()
        {
            var openingChar = '{';
            var propertyType = JsonStringUtils.GetPropertyType(openingChar);

            Assert.AreEqual(JsonPropertyType.complex, propertyType);
        }

        [TestMethod]
        public void ShouldBe_JsonPropertyType_Array()
        {
            var openingChar = '[';
            var propertyType = JsonStringUtils.GetPropertyType(openingChar);

            Assert.AreEqual(JsonPropertyType.array, propertyType);
        }

        [TestMethod]
        public void ShouldBe_JsonPropertyType_Number()
        {
            var openingChar = '3';
            var propertyType = JsonStringUtils.GetPropertyType(openingChar);

            Assert.AreEqual(JsonPropertyType.number, propertyType);
        }

        [TestMethod]
        public void ShouldBe_JsonPropertyType_Text()
        {
            var openingChar = '"';
            var propertyType = JsonStringUtils.GetPropertyType(openingChar);

            Assert.AreEqual(JsonPropertyType.text, propertyType);
        }
    }
}
