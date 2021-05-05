using StringNumSet;
using Xunit;

namespace StringNumTests
{
    public class Multiply
    {
        [Fact]
        public void MultiplySimple()
        {
            var result = "1".ToStringNum() * "3".ToStringNum();
            Assert.Equal("3".ToStringNum(), result);
        }

        [Fact]
        public void MultiplyMultiDigit()
        {
            var result = "3".ToStringNum() * "4".ToStringNum();
            Assert.Equal("12".ToStringNum(), result);
        }

        [Fact]
        public void MultiplyMultiDigit2()
        {
            var result = "5".ToStringNum() * "5".ToStringNum();
            Assert.Equal("25".ToStringNum(), result);
        }

        [Fact]
        public void MultiplyMultiDigit3()
        {
            var result = "10".ToStringNum() * "5".ToStringNum();
            Assert.Equal("50".ToStringNum(), result);
        }

        [Fact]
        public void MultiplyMultiDigit4()
        {
            var result = "123".ToStringNum() * "65".ToStringNum();
            Assert.Equal("7995".ToStringNum(), result);
        }

        [Fact]
        public void MultiplyWithZero()
        {
            var result = "0".ToStringNum() * "123425654".ToStringNum();
            Assert.Equal("0".ToStringNum(), result);
        }

        [Fact]
        public void MultiplyWithZero2()
        {
            var result = "245676542347".ToStringNum() * "0".ToStringNum();
            Assert.Equal("0".ToStringNum(), result);
        }
    }
}
