using StringNum;
using Xunit;

namespace Whole_Tests
{
    public class Divide
    {
        [Fact]
        public void SimpleDivide()
        {
            var result = "6".ToStringNum() / "2".ToStringNum();

            Assert.Equal("3".ToStringNum(), result);
        }

        [Fact]
        public void DevideWithFraction()
        {
            var result = "7".ToStringNum() / "3".ToStringNum();

            Assert.Equal("2.3333".ToStringNum(), result);
        }

        [Fact]
        public void DevideWithFraction2()
        {
            var result = "14".ToStringNum() / "5".ToStringNum();

            Assert.Equal("2.8", result.ToString());
        }

        [Fact]
        public void Pi1()
        {
            var result = "22".ToStringNum() / "7".ToStringNum();

            Assert.Equal("3.1428".ToStringNum(), result);
        }
    }
}
