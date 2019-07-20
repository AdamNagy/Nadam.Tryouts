using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CshTryouts.Tests
{
    public class StringNumTests
    {
        [TestClass]
        public class DefaultTests
        {
            [TestMethod]
            public void CreateValid()
            {
                var strNum = new StringNum("123");

                Assert.IsNotNull(strNum);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "Given string contains non numerical characters")]
            public void CreateInvalid()
            {
                new StringNum("qwe");
            }

            [TestMethod]
            public void ToStringTest()
            {
                var strNum = new StringNum("123");
                Assert.AreEqual("123", strNum.ToString());
            }

            [TestMethod]
            public void StringEquality()
            {
                var strNum = new StringNum("123");

                var result = strNum == "123";
                Assert.IsTrue(result);
            }
        }

        [TestClass]
        public class AddingTests_Simple
        {
            [TestMethod]
            public void SimpleToSimple()
            {
                var a = new StringNum("3");
                var b = new StringNum("2");

                var sum = a + b;
                Assert.AreEqual("5", sum.ToString());
            }

            [TestMethod]
            public void DoubleToSimple()
            {
                var a = new StringNum("10");
                var b = new StringNum("2");

                var sum = a + b;
                Assert.AreEqual("12", sum.ToString());
            }

            [TestMethod]
            public void SimpleToDouble()
            {
                var a = new StringNum("3");
                var b = new StringNum("20");

                var sum = a + b;
                Assert.AreEqual("23", sum.ToString());
            }

            [TestMethod]
            public void LongToSimple()
            {
                var a = new StringNum("10000000");
                var b = new StringNum("1");

                var sum = a + b;
                Assert.AreEqual("10000001", sum.ToString());
            }

            [TestMethod]
            public void SimpleToLong()
            {
                var a = new StringNum("1");
                var b = new StringNum("10000000");

                var sum = a + b;
                Assert.AreEqual("10000001", sum.ToString());
            }


        }

        [TestClass]
        public class AddingTests_Overflow
        {
            [TestMethod]
            public void Simple1()
            {
                var a = new StringNum("9");
                var b = new StringNum("1");

                var sum = a + b;
                Assert.AreEqual("10", sum.ToString());
            }

            [TestMethod]
            public void Simple2()
            {
                var a = new StringNum("19");
                var b = new StringNum("1");

                var sum = a + b;
                Assert.AreEqual("20", sum.ToString());
            }
        }

        [TestClass]
        public class ToDigitsTest
        {
            [TestMethod]
            public void IntToDigitsFromCharArr()
            {
                var digits = 123.ToDigits();
                CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, digits);                
            }

            [TestMethod]
            public void StringToDigitsFromString()
            {
                var digits = "123".ToDigits();
                CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, digits);
            }
        }

        [TestClass]
        public class Comparsiontests
        {
            [TestMethod]
            public void LeftGtRight()
            {
                StringNum sn1 = new StringNum(10),
                          sn2 = new StringNum(5);

                var result = sn1 > sn2;
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void LeftGtRight2()
            {
                StringNum sn1 = new StringNum(10),
                          sn2 = new StringNum(-5);

                var result = sn1 > sn2;
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void LeftLtRight()
            {
                StringNum sn1 = new StringNum(10),
                          sn2 = new StringNum(5);

                var result = sn1 < sn2;
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void LeftLtRight2()
            {
                StringNum sn1 = new StringNum(10),
                          sn2 = new StringNum(-5);

                var result = sn1 < sn2;
                Assert.IsTrue(result);
            }
        }
    }
}
