using StringNum;
using Xunit;

namespace DigitTests
{
    public class Multiply
    {
        [Fact]
        public void Multiply1()
        {
            var result = Digit.Multiply('2', '3');

            Assert.Equal((ones: '6', tens: '0'), result);
        }

        [Fact]
        public void Multiply2()
        {
            var result = Digit.Multiply('1', '7');

            Assert.Equal((ones: '7', tens: '0'), result);
        }

        [Fact]
        public void Multiply3()
        {
            var result = Digit.Multiply('3', '4');

            Assert.Equal((ones: '2', tens: '1'), result);
        }

        [Fact]
        public void Multiply4()
        {
            var result = Digit.Multiply('0', '9');

            Assert.Equal((ones: '0', tens: '0'), result);
        }

        [Fact]
        public void Multiply5()
        {
            var result = Digit.Multiply('9', '0');

            Assert.Equal((ones: '0', tens: '0'), result);
        }

        [Fact]
        public void Multiply6()
        {
            var result = Digit.Multiply('1', '9');

            Assert.Equal((ones: '9', tens: '0'), result);
        }

        [Fact]
        public void Multiply7()
        {
            var result = Digit.Multiply('9', '1');

            Assert.Equal((ones: '9', tens: '0'), result);
        }
    }
}
