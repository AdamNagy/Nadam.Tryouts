using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.JsonSeekerTests
{
    [TestClass]
    public class IsJsonValueClosedTests
    {
        [TestMethod]
        public void ShouldBe_Array_Closed()
        {
            var rawJsonString = "[1,2,3,4], \"pro";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);
            var expected = "[1,2,3,4]";

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.array, out seekIndex);

            Assert.IsTrue(isClosed);
            Assert.AreEqual(expected, normalized.Substring(0, seekIndex));
        }

        [TestMethod]
        public void ShouldBe_NestedArray_Closed()
        {
            var rawJsonString = "[['a', 'b', 'c'],[1, 23, 4], [123, 234, 345]], \"pro";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);
            var expected = "[['a','b','c'],[1,23,4],[123,234,345]]";

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.array, out seekIndex);

            Assert.IsTrue(isClosed);
            Assert.AreEqual(expected, normalized.Substring(0, seekIndex));
        }

        [TestMethod]
        public void ShouldBe_Complex_Closed()
        {
            var rawJsonString = "{\"prop1\": 123,\"prop2\": 'sdfuhuh'}, \"someOther\":[]";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);
            var expected = JsonSeeker.NormalizeJsonString("{\"prop1\": 123,\"prop2\": 'sdfuhuh'}");

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.complex, out seekIndex);

            Assert.IsTrue(isClosed);
            Assert.AreEqual(expected, normalized.Substring(0, seekIndex));
        }

        [TestMethod]
        public void ShouldBe_NestedComplex_Closed()
        {
            var rawJsonString = "{\"prop1\": {\"prop1\": 123,\"prop2\": 'sdfuhuh'},\"prop2\": {\"prop1\": 123,\"prop2\": 'sdfuhuh'}}, \"someOther\":[]";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);
            var expected = JsonSeeker.NormalizeJsonString("{\"prop1\": {\"prop1\": 123,\"prop2\": 'sdfuhuh'},\"prop2\": {\"prop1\": 123,\"prop2\": 'sdfuhuh'}}");

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.complex, out seekIndex);

            Assert.IsTrue(isClosed);
            Assert.AreEqual(expected, normalized.Substring(0, seekIndex));
        }

        [TestMethod]
        public void ShouldBe_Text_Closed()
        {
            var rawJsonString = "\"hello json text\", \"prop2\": 'sdfuhuh'}, \"someOther\":[]";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.text, out seekIndex);

            Assert.IsTrue(isClosed);
            Assert.AreEqual("\"hello json text\"", normalized.Substring(0, seekIndex));
        }

        [TestMethod]
        public void ShouldBe_Number_ClosedByComma()
        {
            var rawJsonString = "123, \"prop2\": 'sdfuhuh'}, \"someOther\":[]";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.number, out seekIndex);

            Assert.IsTrue(isClosed);
            Assert.AreEqual("123", normalized.Substring(0, seekIndex));
        }

        [TestMethod]
        public void Not_ShouldBe_Array_Closed()
        {
            var rawJsonString = "[1,2,3,4, \"pro";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.array, out seekIndex);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShouldBe_Complex_Closed()
        {
            var rawJsonString = "{\"prop1\": 123,\"prop2\": 'sdfuh";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.complex, out seekIndex);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShouldBe_Text_Closed()
        {
            var rawJsonString = "\"hello json tex";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.text, out seekIndex);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShouldBe_Number_ClosedByComma()
        {
            var rawJsonString = "123234";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.number, out seekIndex);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShouldBe_NestedComplex_Closed()
        {
            var rawJsonString = "{\"prop1\": {\"prop1\": 123,\"prop2\": 'sdfuhuh'},\"prop2\": {\"prop1\": 123,\"prop2";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);
            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.complex, out seekIndex);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShouldBe_NestedArray_Closed()
        {
            var rawJsonString = "[['a', 'b', 'c'],[1, 23, 4], [123, 234, 345]";
            var normalized = JsonSeeker.NormalizeJsonString(rawJsonString);

            int seekIndex = 0;
            var isClosed = JsonSeeker.IsJsonValueClosed(normalized, JsonPropertyType.array, out seekIndex);

            Assert.IsFalse(isClosed);
        }
    }
}
