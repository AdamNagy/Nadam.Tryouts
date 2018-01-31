using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NadamLib.Tests.TestModels;
using Nadam.Global.Lib;
using static Nadam.Global.Lib.BinaryPredicates;

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
                Assert.AreEqual(testObject.IntTypeProp, 0);
                Assert.AreEqual(testObject.DoubleTypeprop, 0);
                Assert.AreEqual(testObject.DecimalTypeProp, 0);
            }

            [TestMethod]
            public void ValidStringTypePropsSetingToNull()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(true);

                // Action
                testObject.SetValueToNullFor("StringTypeProp");
                testObject.SetValueToNullFor("CharTypeProp");
                testObject.SetValueToNullFor("ByteTypeProp");
                testObject.SetValueToNullFor("ByteArrTypeProp");

                // Assert
                Assert.AreEqual(testObject.StringTypeProp, String.Empty);
                Assert.AreEqual(testObject.CharTypeProp, 0);
                Assert.AreEqual(testObject.ByteTypeProp, 0);
                Assert.IsNull(testObject.ByteArrTypeProp);
            }

            [TestMethod]
            public void ValidObjectTypePropsSetingToNull()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(true);

                // Act
                testObject.SetValueToNullFor("ComplexTypeList");

                // Assert
                Assert.IsNull(testObject.ComplexTypeList);
            }
        }
        
        [TestClass]
        public class SetValuesToNullForTest
        {
            [TestMethod]
            public void ValidSetingAllPropertieTest()
            {
                // Arrange
                var testObject = new UnitTestingModelBase(true);
                var properties = new string[]
                {
                    "IntTypeProp", "DoubleTypeprop", "DecimalTypeProp",
                    "StringTypeProp", "CharTypeProp", "ByteTypeProp", "ByteArrTypeProp",
                    "ComplexTypeList"
                };

                // Action
                testObject.SetValuesToNullFor(properties);

                // Assert
                Assert.AreEqual(testObject.IntTypeProp, 0);
                Assert.AreEqual(testObject.DoubleTypeprop, 0);
                Assert.AreEqual(testObject.DecimalTypeProp, 0);
                Assert.AreEqual(testObject.StringTypeProp, String.Empty);
                Assert.AreEqual(testObject.CharTypeProp, 0);
                Assert.AreEqual(testObject.ByteTypeProp, 0);
                Assert.IsNull(testObject.ByteArrTypeProp);
                Assert.IsNull(testObject.ComplexTypeList);
            }
        }
        #endregion

        #region Filters
        [TestClass]
        public class FilterTest
        {
            [TestMethod]
            public void FilterOnNameResultShouldBe1()
            {
                // Arrenge
                var dbTable = TestDataEntityTableSeeder.SeedTestDataEntityTable();

                // Act
                //(this IEnumerable<T> domain, string filter, object reference, Func< object, object, bool> pred)
                var filtered = dbTable.FilterBy("Name", "Duis Consectetur", Equality).ToList();

                // Assert
                Assert.AreEqual(1, filtered.Count());
                Assert.AreEqual("Duis Consectetur", filtered[0].Name);
            }

            [TestMethod]
            public void FilterOnNameResultShouldBenull()
            {
                // Arrenge
                var dbTable = TestDataEntityTableSeeder.SeedTestDataEntityTable();

                // Act
                var filtered = dbTable.FilterBy("Name", "Biztosan nem létező név", Equality); 

                // Assert
                Assert.AreEqual(0, filtered.Count());
            }

            [TestMethod]
            public void FilterOnColorResultShouldBenull()
            {
                // Arrenge
                var dbTable = TestDataEntityTableSeeder.SeedTestDataEntityTable();

                // Act
                var filtered = dbTable.FilterBy("ColorE", ColorEnum.black, Equality);

                // Assert
                Assert.AreEqual(16, filtered.Count());
            }

            [TestMethod]
            public void FilterOnColorResultShouldBe190()
            {
                // Arrenge
                var dbTable = TestDataEntityTableSeeder.SeedTestDataEntityTable(150);

                // Act
                var filtered = dbTable.FilterBy("ColorE", ColorEnum.black, Equality);

                // Assert
                Assert.AreEqual(16, filtered.Count());
            }

            [TestMethod]
            public void FilterOnColorResultShouldBe190_b()
            {
                // Arrenge
                var dbTable = TestDataEntityTableSeeder.SeedTestDataEntityTable(150);

                // Act
                var filtered = dbTable.FilterBy(p => p.ColorE, ColorEnum.black, Equality);

                // Assert
                Assert.AreEqual(16, filtered.Count());
            }
        }
        #endregion
    }
}
