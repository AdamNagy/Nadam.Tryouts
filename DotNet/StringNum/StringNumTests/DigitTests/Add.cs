using StringNum;
using Xunit;

namespace DigitTests
{
    public class Add
    {
        [Fact]
        public void AddingWhole()
        {
            var result = Digit.Add('1', '1');

            Assert.Equal((ones: '2', tens: '0'), result);
        }

        [Fact]
        public void AddingWhole2()
        {
            var result = Digit.Add('0', '0');

            Assert.Equal((ones: '0', tens: '0'), result);
        }

        [Fact]
        public void AddingWhole3()
        {
            var result = Digit.Add('0', '9');

            Assert.Equal((ones: '9', tens: '0'), result);
        }

        [Fact]
        public void AddingWhole4()
        {
            var result = Digit.Add('9', '0');

            Assert.Equal((ones: '9', tens: '0'), result);
        }

        [Fact]
        public void AddingWhole5()
        {
            var result = Digit.Add('1', '9');

            Assert.Equal((ones: '0', tens: '1'), result);
        }

        [Fact]
        public void AddingWhole6()
        {
            var result = Digit.Add('9', '1');

            Assert.Equal((ones: '0', tens: '1'), result);
        }

        [Fact]
        public void AddingWhole7()
        {
            var result = Digit.Add('9', '9');

            Assert.Equal((ones: '8', tens: '1'), result);
        }

        [Fact]
        public void AddingWhole8()
        {
            var result = Digit.Add('5', '5');

            Assert.Equal((ones: '0', tens: '1'), result);
        }

        [Fact]
        public void AddingWhole9()
        {
            var result = Digit.Add('3', '2');

            Assert.Equal((ones: '5', tens: '0'), result);
        }
    }
}
