using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StreamSeeking.Tests.JsonStringUtilsTests
{
    [TestClass]
    public class IsFistParenthesesClosedTests
    {
        [TestMethod]
        public void ShuoldBe_Closed_NormalCase_1()
        {
            var text = "{main {first sub}.. {second sub}..}..{..";
            var closerCharIdx = 0;
            var isClosed = JsonStringUtils.IsJsonParenthesesClosed(text, '{', '}', out closerCharIdx);

            Assert.AreEqual("{main {first sub}.. {second sub}..}", text.Substring(0, closerCharIdx));
        }

        [TestMethod]
        public void ShuoldBe_Closed_EdgeCase()
        {
            var text = "{}";
            var closerCharIdx = 0;
            var isClosed = JsonStringUtils.IsJsonParenthesesClosed(text, '{', '}', out closerCharIdx);

            Assert.AreEqual("{}", text.Substring(0, closerCharIdx));
        }

        [TestMethod]
        public void ShuoldBe_Closed_NormalCase_2()
        {
            var text = "some text before {main {first sub}.. {second sub}..}..{..";
            var closerCharIdx = 0;
            var isClosed = JsonStringUtils.IsJsonParenthesesClosed(text, '{', '}', out closerCharIdx);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShuoldBe_Closed_1()
        {
            var text = "{main {first sub}.. {second sub}..";
            var closerCharIdx = 0;
            var isClosed = JsonStringUtils.IsJsonParenthesesClosed(text, '{', '}', out closerCharIdx);

            Assert.IsFalse(isClosed);
        }

        [TestMethod]
        public void Not_ShuoldBe_Closed_EdgeCase()
        {
            var text = "{";
            var closerCharIdx = 0;
            var isClosed = JsonStringUtils.IsJsonParenthesesClosed(text, '{', '}', out closerCharIdx);

            Assert.IsFalse(isClosed);
        }

    }
}
