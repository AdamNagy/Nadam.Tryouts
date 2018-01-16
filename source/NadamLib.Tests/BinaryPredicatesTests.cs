using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NadamLib.Tests.TestModels;
using static Nadam.Lib.BinaryPredicates;

namespace NadamLib.Tests
{
    class BinaryPredicatesTests
    {
        [TestClass]
        public class LessThan
        {
            // Exception assert
            [TestMethod]
            [ExpectedException(typeof(NotImplementedException))]
            public void LeftOperantIsNUllTest()
            {
                var less = LessThan(null, "10");
            }

            [TestMethod]
            [ExpectedException(typeof(NotImplementedException))]
            public void BothOperandsNUllTest()
            {
                var less = LessThan(null, null);
            }

            [TestMethod]
            [ExpectedException(typeof(NotImplementedException))]
            public void Convertion()
            {
                var less = LessThan("Not a number", "null");
            }
            /*********************************************************/


            [TestMethod]
            public void ValidTestUsingInt()
            {
                var less = LessThan(1, 2);
                Assert.IsTrue(less);

                less = LessThan(3, 2);
                Assert.IsFalse(less);

                less = LessThan(2, 2);
                Assert.IsFalse(less);

                less = LessThan(-2, 2);
                Assert.IsTrue(less);
            }

            [TestMethod]
            public void ValidTestUsingDouble()
            {
                var less = LessThan(1.0, 2.2);
                Assert.IsTrue(less);

                less = LessThan(-14234.234234, 2342.4234);
                Assert.IsTrue(less);

                less = LessThan(3.321, 2.432);
                Assert.IsFalse(less);

                less = LessThan(2.123, 2.123);
                Assert.IsFalse(less);
            }

            [TestMethod]
            [ExpectedException(typeof(NotImplementedException))]
            public void ValidTestUsingString()
            {
                var less = LessThan("1", "2");
                //Assert.IsTrue(less);

                //less = LessThan("-4231", "42342");
                //Assert.IsTrue(less);

                //less = LessThan("14234", "2342234");
                //Assert.IsTrue(less);

                //less = LessThan("3", "2");
                //Assert.IsFalse(less);

                //less = LessThan("2", "2");
                //Assert.IsFalse(less);
            }
        }

        [TestClass]
        public class Equlity
        {
            [TestMethod]
            public void FilterInNumbersShouldBe1()
            {
                // Arrange
                var numbers = TestDataEntityTableSeeder.SeedNumbers();

                // Action
                var result = numbers.Where(p => Equality(p, 1)).ToList();

                // Assert
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual(1, result[0]);
            }

            [TestMethod]
            public void FilterInNumbersShouldBe0()
            {
                // Arrange
                var numbers = TestDataEntityTableSeeder.SeedNumbers();

                // Action
                var result = numbers.Where(p => Equality(p, -1)).ToList();

                // Assert
                Assert.AreEqual(0, result.Count());
            }

            [TestMethod]
            public void FilterInNumbersShouldBe3()
            {
                // Arrange
                var numbers = TestDataEntityTableSeeder.SeedNumbers3TimesEach();

                // Action
                var result = numbers.Where(p => Equality(p, 5)).ToList();

                // Assert
                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(5, result[0]);
                Assert.AreEqual(5, result[1]);
                Assert.AreEqual(5, result[2]);
            }

            [TestMethod]
            public void FilterInStringShouldBe1()
            {
                // Arrange
                var numbers = TestDataEntityTableSeeder.SeedStrings();

                // Action
                var result = numbers.Where(p => Equality(p, "three")).ToList();

                // Assert
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual("three", result[0]);
            }

            [TestMethod]
            public void FilterInStringShouldBe2()
            {
                // Arrange
                var numbers = TestDataEntityTableSeeder.SeedStrings2TimesEach();

                // Action
                var result = numbers.Where(p => Equality(p, "three")).ToList();

                // Assert
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual("three", result[0]);
                Assert.AreEqual("three", result[1]);
            }

            [TestMethod]
            public void FilterInStringShouldBe0()
            {
                // Arrange
                var numbers = TestDataEntityTableSeeder.SeedStrings2TimesEach();

                // Action
                var result = numbers.Where(p => Equality(p, "nem létező")).ToList();

                // Assert
                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
