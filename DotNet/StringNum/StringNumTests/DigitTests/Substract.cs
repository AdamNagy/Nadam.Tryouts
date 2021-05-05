//using StringNumSet;
//using System;
//using Xunit;

//namespace DigitTests
//{
//    public class Substract
//    {       
//        [Fact]
//        public void Substract1()
//        {
//            var result = Digit.Substract('0', '9');

//            Assert.Equal((whole: '9', fraction: '/', isNegative: true), result);
//        }

//        [Fact]
//        public void Substract2()
//        {
//            var result = Digit.Substract('1', '9');

//            Assert.Equal((whole: '8', fraction: '/', isNegative: true), result);
//        }

//        [Fact]
//        public void Substract3()
//        {
//            var result = Digit.Substract('9', '1');

//            Assert.Equal((whole: '8', fraction: '/', isNegative: false), result);
//        }

//        [Fact]
//        public void Substract4()
//        {
//            var result = Digit.Substract('9', '0');

//            Assert.Equal((whole: '9', fraction: '/', isNegative: false), result);
//        }

//        [Fact]
//        public void Substract5()
//        {
//            var result = Digit.Substract('1', '1');

//            Assert.Equal((whole: '0', fraction: '/', isNegative: false), result);
//        }

//        [Fact]
//        public void Substract6()
//        {
//            var result = Digit.Substract('0', '0');

//            Assert.Equal((whole: '0', fraction: '/', isNegative: false), result);
//        }
//    }
//}
