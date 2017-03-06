using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Nadam.Lib.PredicatesLib;

namespace NadamLib.Tests
{
    class PredicatesTests
    {
        [TestClass]
        public class LessThan
        {
            // Exception assert
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void LeftOperantIsNUllTest()
            {
                var less = LessThanPredicate(null, "10");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void RightOperandIsNUllTest()
            {
                var less = LessThanPredicate(2.234, null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void BothOperandsNUllTest()
            {
                var less = LessThanPredicate(null, null);
            }

            [TestMethod]
            [ExpectedException(typeof(FormatException))]
            public void Convertion()
            {
                var less = LessThanPredicate("Not a number", "null");
            }
            /*********************************************************/


            [TestMethod]
            public void ValidTestUsingInt()
            {
                var less = LessThanPredicate(1, 2);
                Assert.IsTrue(less);

                less = LessThanPredicate(3, 2);
                Assert.IsFalse(less);
            
                less = LessThanPredicate(2, 2);
                Assert.IsFalse(less);

                less = LessThanPredicate(-2, 2);
                Assert.IsTrue(less);
            }

            [TestMethod]
            public void ValidTestUsingDouble()
            {
                var less = LessThanPredicate(1.0, 2.2);
                Assert.IsTrue(less);

                less = LessThanPredicate(-14234.234234, 2342.4234);
                Assert.IsTrue(less);

                less = LessThanPredicate(3.321, 2.432);
                Assert.IsFalse(less);

                less = LessThanPredicate(2.123, 2.123);
                Assert.IsFalse(less);
            }

            [TestMethod]
            public void ValidTestUsingString()
            {
                var less = LessThanPredicate("1", "2");
                Assert.IsTrue(less);

                less = LessThanPredicate("-4231", "42342");
                Assert.IsTrue(less);

                less = LessThanPredicate("14234", "2342234");
                Assert.IsTrue(less);

                less = LessThanPredicate("3", "2");
                Assert.IsFalse(less);

                less = LessThanPredicate("2", "2");
                Assert.IsFalse(less);
            }
        }


    }
}
