using StringNum;
using Xunit;

namespace Whole_Tests
{
    public class Substract
    {
        [Fact]
        public void SubstractZero()
        {
            var result = "123".ToStringNum() - "0".ToStringNum();

            Assert.Equal("123".ToStringNum(), result);
        }

        [Fact]
        public void SubstractFromZero()
        {
            var result = "0".ToStringNum() - "123456".ToStringNum();

            Assert.Equal("-123456".ToStringNum(), result);
        }

        [Fact]
        public void SubstractOne()
        {
            var result = "123".ToStringNum() - "1".ToStringNum();

            Assert.Equal("122".ToStringNum(), result);
        }

        [Fact]
        public void SubstractFromOne()
        {
            var result = "1".ToStringNum() - "123456".ToStringNum();

            Assert.Equal("-123455".ToStringNum(), result);
        }

        [Fact]
        public void SubstractFromMinus()
        {
            var result = "-123".ToStringNum() - "1".ToStringNum();

            Assert.Equal("-124".ToStringNum(), result);
        }

        [Fact]
        public void SubstractFromMinus2()
        {
            var result = "-123".ToStringNum() - "10".ToStringNum();

            Assert.Equal("-133".ToStringNum(), result);
        }

        [Fact]
        public void SubstractMinusOne()
        {
            var result = "1".ToStringNum() - "-1".ToStringNum();

            Assert.Equal("2".ToStringNum(), result);
        }

        [Fact]
        public void SubstractFromMinusOneMinusOne()
        {
            var result = "-1".ToStringNum() - "-1".ToStringNum();

            Assert.Equal("0".ToStringNum(), result);
        }

        [Fact]
        public void SubstractSteppOverZero()
        {
            var result = "5".ToStringNum() - "8".ToStringNum();

            Assert.Equal("-3".ToStringNum(), result);
        }

        [Fact]
        public void SubstractSteppOverZero2()
        {
            var result = "-5".ToStringNum() - "-8".ToStringNum();

            Assert.Equal("3".ToStringNum(), result);
        }

        [Fact]
        public void NormasCase()
        {
            var result = "4625".ToStringNum() - "2154".ToStringNum();

            Assert.Equal("2471".ToStringNum(), result);
        }

        [Fact]
        public void NormasCase2()
        {
            var result = "123".ToStringNum() - "4321".ToStringNum();

            Assert.Equal("-4198".ToStringNum(), result);
        }
    }
}
