using StringNum;
using Xunit;

namespace Whole_Tests
{
    public class Add
    {
        [Fact]
        public void AddingWhole()
        {
            var result = "1".ToStringNum() + "1".ToStringNum();

            Assert.Equal("2".ToStringNum(), result);
        }

        [Fact]
        public void AddingWhole2()
        {
            var result = "3".ToStringNum() + "5".ToStringNum();

            Assert.Equal("8".ToStringNum(), result);
        }

        [Fact]
        public void AddingWhole3()
        {
            var result = "0".ToStringNum() + "1".ToStringNum();

            Assert.Equal("1".ToStringNum(), result);
        }

        [Fact]
        public void AddingWhole4()
        {
            var result = "0".ToStringNum() + "0".ToStringNum();

            Assert.Equal("0".ToStringNum(), result);
        }

        [Fact]
        public void AddingOverflow()
        {
            var result = "9".ToStringNum() + "2".ToStringNum();

            Assert.Equal("11".ToStringNum(), result);
        }

        [Fact]
        public void AddingDifferentLength()
        {
            var result = "20".ToStringNum() + "2".ToStringNum();

            Assert.Equal("22".ToStringNum(), result);
        }

        [Fact]
        public void AddingDifferentLength2()
        {
            var result = "3".ToStringNum() + "50".ToStringNum();

            Assert.Equal("53".ToStringNum(), result);
        }

        [Fact]
        public void AddingDifferentLength3()
        {
            var result = "1".ToStringNum() + "12345".ToStringNum();

            Assert.Equal("12346".ToStringNum(), result);
        }

        [Fact]
        public void AddingDifferentLength4()
        {
            var result = "7380".ToStringNum() + "615".ToStringNum();

            Assert.Equal("7995".ToStringNum(), result);
        }

        [Fact]
        public void AddingLarge()
        {
            var result = "1000000000000000000000000000000000000".ToStringNum() + "1000000000000000000000000000000000000".ToStringNum();

            Assert.Equal("2000000000000000000000000000000000000".ToStringNum(), result);
        }

        [Fact]
        public void AddingLargeOverflow()
        {
            var result = "2000000000000000000000000000000000000".ToStringNum() + "9000000000000000000000000000000000000".ToStringNum();

            Assert.Equal("11000000000000000000000000000000000000".ToStringNum(), result);
        }

        [Fact]
        public void AddToNegative()
        {
            var result = "-1".ToStringNum() + "1".ToStringNum();

            Assert.Equal("0".ToStringNum(), result);
        }

        [Fact]
        public void AddToNegative2()
        {
            var result = "-3".ToStringNum() + "1".ToStringNum();

            Assert.Equal("-2".ToStringNum(), result);
        }

        [Fact]
        public void AddToNegativeSteppingOverZero()
        {
            var result = "-3".ToStringNum() + "5".ToStringNum();

            Assert.Equal("2".ToStringNum(), result);
        }

        [Fact]
        public void AddToNegativeSteppingOverZero2()
        {
            var result = "-13".ToStringNum() + "20".ToStringNum();

            Assert.Equal("7".ToStringNum(), result);
        }
    }
}
