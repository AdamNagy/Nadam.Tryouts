using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NadamLib.Tests.TestModels;
using Nadam.Lib;

namespace NadamLib.Tests
{
    class ExtensionsLibTests
    {
        #region Reflection extensions
        [TestClass]
        public class GetValueForTest
        {
            [TestMethod]
            public void ValidNumberTypePropertyGetingObjects()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(true);
                var referenceValues = new TestDataSeed();

                // Action
                var intResult = testObject.GetValueFor("IntTypeProp");
                var doubleResult = testObject.GetValueFor("DoubleTypeprop");
                var decimalResult = testObject.GetValueFor("DecimalTypeProp");

                // Assert
                Assert.AreEqual(intResult, referenceValues.IntTypeProp());
                Assert.AreEqual(doubleResult, referenceValues.DoubleTypeprop());
                Assert.AreEqual(decimalResult, referenceValues.DecimalTypeProp());
            }

            [TestMethod]
            public void ValidStringTypePropertyGetingObjects()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(true);
                var referenceValues = new TestDataSeed();

                // Action
                var stringResult = testObject.GetValueFor("StringTypeProp");
                var charResult = testObject.GetValueFor("CharTypeProp");
                var byteResult = testObject.GetValueFor("ByteTypeProp");
                var byteArrResult = (byte[])testObject.GetValueFor("ByteArrTypeProp");

                // Assert
                Assert.AreEqual(stringResult, referenceValues.StringTypeProp());
                Assert.AreEqual(charResult, referenceValues.CharTypeProp());
                Assert.AreEqual(byteResult, referenceValues.ByteTypeProp());
                CollectionAssert.AreEqual(byteArrResult, referenceValues.ByteArrTypeProp());
            }
        }

        [TestClass]
        public class SetValueForTest
        {
            [TestMethod]
            public void ValidNumberTypePropertySetting()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(false);
                var referenceValues = new TestDataSeed();

                // Action
                testObject.SetValueFor("IntTypeProp", referenceValues.IntTypeProp());
                testObject.SetValueFor("DoubleTypeprop", referenceValues.DoubleTypeprop());
                testObject.SetValueFor("DecimalTypeProp", referenceValues.DecimalTypeProp());

                // Assert
                Assert.AreEqual(testObject.IntTypeProp, referenceValues.IntTypeProp());
                Assert.AreEqual(testObject.DoubleTypeprop, referenceValues.DoubleTypeprop());
                Assert.AreEqual(testObject.DecimalTypeProp, referenceValues.DecimalTypeProp());
            }
        }

        [TestClass]
        public class SetValueToNullForTest
        {
            [TestMethod]
            public void ValidNumberTypePropsSetingToNull()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(true);

                // Action
                testObject.SetValueToNullFor("IntTypeProp");
                testObject.SetValueToNullFor("DoubleTypeprop");
                testObject.SetValueToNullFor("DecimalTypeProp");

                // Assert
                Assert.IsNull(testObject.IntTypeProp);
                Assert.IsNull(testObject.DoubleTypeprop);
                Assert.IsNull(testObject.DecimalTypeProp);
            }
        }
        #endregion
    }
}
