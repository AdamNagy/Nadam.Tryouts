using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Lib.JsonDb.Test.TestHelpers;
using Nadam.Lib.JsonDb.Test.TestHelpers.SimpleDb;
using Nadam.Lib.JsonDb.Test.TestHelpers.SimpleDb.SimpleModel;
using System.Collections.Generic;
using Nadam.Global.JsonDb;

namespace Nadam.Lib.JsonDb.Test
{
    class JsonDbContextTests
    {
        #region SimpleJsonContext
        [TestClass]
        public class SimpleJsonContext_CreateEndAddTests
        {
            /// <summary>
            /// Create empty context and save it
            /// Check that the table json files hav been generated properly
            /// </summary>
            [TestMethod]
            public void Create_Save()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestA", false);

                // Act
                context.SaveChanges();

                // Assert
                Assert.AreNotEqual(context, null);
            }

            /// <summary>
            /// This test intented to validate that if a table is not saved, that list shouldn't be null either
            /// but a freshly initialized list
            /// </summary>
            [TestMethod]
            public void CreateFromExistingWithInMemoryTrue_NotAllTableSaved()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestA", true);

                // Assert
                Assert.AreNotEqual(null, context.DimensionC);
            }

            /// <summary>
            /// Create empty context, seed test data into it and save it
            /// Check that the json table files contains the seeded data
            /// </summary>
            [TestMethod]
            public void Create_InitializeTables()
            {
                // Arrange
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestB", false);
                context.DimensionA = seeder.SeedDimensionATable().ToList();
                context.DimensionB = seeder.SeedDimensionBTable().ToList();
                context.DimensionC = seeder.SeedDimensionCTable().ToList();

                // Act

                // Assert
                Assert.AreNotEqual(context, null);
                Assert.AreEqual(context.DimensionA.Count, 3);
                Assert.AreEqual(context.DimensionB.Count, 3);
                Assert.AreEqual(context.DimensionC.Count, 3);
            }

            [TestMethod]
            public void Create_AddingRowsOneByOne()
            {
                // Arrange
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestC", false);
                seeder.SeedDimensionATable().ToList().ForEach(p => context.DimensionA.Add(p));
                seeder.SeedDimensionBTable().ToList().ForEach(p => context.DimensionB.Add(p));
                seeder.SeedDimensionCTable().ToList().ForEach(p => context.DimensionC.Add(p));

                // Act

                // Assert
                Assert.AreNotEqual(context, null);
                Assert.AreEqual(context.DimensionA.Count, 3);
                Assert.AreEqual(context.DimensionB.Count, 3);
                Assert.AreEqual(context.DimensionC.Count, 3);
            }

            /// <summary>
            /// Create empty context, initialize process should load existing data
            /// Check that the context contains data
            /// IMPORTAND: run the test before this to create test data
            /// </summary>
            [TestMethod]
            public void CreateFromExistingWithInMemoryTrue()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestB");

                // Action
                var dimA = context.DimensionA.Select(p => p);
                var dimB = context.DimensionB.Select(p => p);
                var dimC = context.DimensionC.Select(p => p);

                // Assert
                Assert.AreEqual(context.DimensionA.Count, 3);
                Assert.AreEqual(context.DimensionB.Count, 3);
                Assert.AreEqual(context.DimensionC.Count, 3);

                Assert.AreEqual(dimA.Count(), 3);
                Assert.AreEqual(dimB.Count(), 3);
                Assert.AreEqual(dimC.Count(), 3);
            }

            [TestMethod]
            public void CreateFromExistingWithInMemoryTrue_AddOtherRows()
            {
                // Arrange
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestB");

                // Action
                seeder.SeedDimensionATable().ToList().ForEach(p => context.DimensionA.Add(p));
                seeder.SeedDimensionBTable().ToList().ForEach(p => context.DimensionB.Add(p));
                seeder.SeedDimensionCTable().ToList().ForEach(p => context.DimensionC.Add(p));
                var dimA = context.DimensionA.Select(p => p);
                var dimB = context.DimensionB.Select(p => p);
                var dimC = context.DimensionC.Select(p => p);

                // Assert
                Assert.AreEqual(context.DimensionA.Count, 6);
                Assert.AreEqual(context.DimensionB.Count, 6);
                Assert.AreEqual(context.DimensionC.Count, 6);

                Assert.AreEqual(dimA.Count(), 6);
                Assert.AreEqual(dimB.Count(), 6);
                Assert.AreEqual(dimC.Count(), 6);
            }

            [TestMethod]
            public void CreateFromExistingWithInMemoryTrue_AddOtherRows_Save()
            {
                // Arrange
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SimpleJson_TestD");
                var dimACount = context.DimensionA.Select(p => p).Count();
                var dimBCount = context.DimensionB.Select(p => p).Count();
                var dimCCount = context.DimensionC.Select(p => p).Count();

                // Action
                seeder.SeedDimensionATable().ToList().ForEach(p => context.DimensionA.Add(p));
                seeder.SeedDimensionBTable().ToList().ForEach(p => context.DimensionB.Add(p));
                seeder.SeedDimensionCTable().ToList().ForEach(p => context.DimensionC.Add(p));

                var dimA = context.DimensionA.Select(p => p);
                var dimB = context.DimensionB.Select(p => p);
                var dimC = context.DimensionC.Select(p => p);

                context.SaveChanges();

                // Assert
                Assert.AreEqual(context.DimensionA.Count, dimACount + 3);
                Assert.AreEqual(context.DimensionB.Count, dimBCount + 3);
                Assert.AreEqual(context.DimensionC.Count, dimCCount + 3);

                Assert.AreEqual(dimA.Count(), dimACount + 3);
                Assert.AreEqual(dimB.Count(), dimBCount + 3);
                Assert.AreEqual(dimC.Count(), dimCCount + 3);
            }


        }

        [TestClass]
        public class SimpleJsonContext_Delete
        {
            [TestInitialize]
            public void InitializeForDeleteTests()
            {
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_Delete", false);
                context.DimensionA = seeder.SeedDimensionATable().ToList();
                context.DimensionB = seeder.SeedDimensionBTable().ToList();
                context.DimensionC = seeder.SeedDimensionCTable().ToList();
                context.SaveChanges();
            }

            [TestMethod]
            public void DeleteOneRecord()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_Delete");

                // Act
                context.DimensionA.Remove(context.DimensionA.First());
                context.SaveChanges();

                // Assert
                var context2 = new SimpleJsonContext("path=../../app_data/SinpleJson_Delete");
                Assert.AreEqual(context2.DimensionA.Count(), 2);
            }

            [TestMethod]
            public void DeleteAllRecordsInATable()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_Delete");

                // Act
                context.DimensionA.Remove(context.DimensionA.First());
                context.DimensionA.Remove(context.DimensionA.First());
                context.DimensionA.Remove(context.DimensionA.First());
                context.SaveChanges();

                // Assert
                var context2 = new SimpleJsonContext("path=../../app_data/SinpleJson_Delete");
                Assert.AreEqual(context2.DimensionA.Count(), 0);
            }
        }

        [TestClass]
        public class SimpleJsonContext_Update
        {
            [TestInitialize]
            public void InitializeForDeleteTests()
            {
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_Update", false);
                context.DimensionA = seeder.SeedDimensionATable().ToList();
                context.DimensionB = seeder.SeedDimensionBTable().ToList();
                context.DimensionC = seeder.SeedDimensionCTable().ToList();
                context.SaveChanges();
            }

            [TestMethod]
            public void UpdateOneRecord()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_Update");

                // Act
                DimensionA toUpdate;
                context.SelectForUpdate<DimensionA>(p => p.PropertyInt == 5, out toUpdate);
                if (toUpdate != null)
                {
                    toUpdate.PropertyInt = 69;
                    context.SaveChanges();
                }

                // Assert
                var context2 = new SimpleJsonContext("path=../../app_data/SinpleJson_Update");
                Assert.AreEqual(context2.DimensionA.First().PropertyInt, 69);
            }
        }

        [TestClass]
        public class SimpleJsonContext_IdSeedTests
        {
            [TestInitialize]
            public void InitializeForDeleteTests()
            {
                var seeder = new SimpleDbSeeder();
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_IdSeed", false);
                context.DimensionA = seeder.SeedDimensionATable().ToList();
                context.DimensionB = seeder.SeedDimensionBTable().ToList();
                context.DimensionC = seeder.SeedDimensionCTable().ToList();
                context.SaveChanges();
            }

            [TestMethod]
            public void AddOneNewRows_SeedIds()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_IdSeed");

                // Act
                var newDimA = new DimensionA()
                {
                    PropertyInt = 10,
                    PropertyString = "some string"
                };
                context.DimensionA.Add(newDimA);
                var idBeforeSave = newDimA.Id;
                context.SaveChanges();
                var context2 = new SimpleJsonContext("path=../../app_data/SinpleJson_IdSeed");

                // Assert
                Assert.AreEqual(0, idBeforeSave);
                Assert.AreEqual(4, context2.DimensionA.Last().Id);
            }

            [TestMethod]
            public void AddMoreNewRows_SeedIds()
            {
                // Arrange
                var context = new SimpleJsonContext("path=../../app_data/SinpleJson_IdSeed");

                // Act
                var newDimA = new DimensionA()
                {
                    PropertyInt = 10,
                    PropertyString = "some string"
                };
                context.DimensionA.Add(newDimA);
                newDimA = new DimensionA()
                {
                    PropertyInt = 20,
                    PropertyString = "some string"
                };
                context.DimensionA.Add(newDimA);
                newDimA = new DimensionA()
                {
                    PropertyInt = 30,
                    PropertyString = "some string"
                };
                context.DimensionA.Add(newDimA);

                var idBeforeSave = newDimA.Id;
                context.SaveChanges();
                var context2 = new SimpleJsonContext("path=../../app_data/SinpleJson_IdSeed");
                var ids = new List<int>(6);
                for (int i = 1; i < 7; i++)
                {
                    ids.Add(i);
                }

                // Assert
                CollectionAssert.AreEqual(ids, context2.DimensionA.Select(p => p.Id).ToList());
            }
        }
        #endregion

        #region NorthwindJsonContext
        [TestClass]
        public class NorthwindJsonContext_CreationEndAddTest
        {
            [TestMethod]
            public void CreateContext()
            {
                // Arrange
                var context = new NorthwindJsonContext("path=../../app_data/NorthwindJson", false);

                // Act
                context.SaveChanges();

                // Assert
                // should check json files 
                Assert.AreNotEqual(context, null);
            }
        }
        #endregion
    }
}
