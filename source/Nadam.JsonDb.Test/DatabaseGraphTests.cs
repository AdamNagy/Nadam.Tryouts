using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Global.Lib.Graph;
using Nadam.Global.JsonDb.DatabaseGraph;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Nadam.Lib.JsonDb.Test
{
    
    public class DatabaseGraphTests
    {
        [TestClass]
        public class AddAndTableCounts
        {
            [TestMethod]
            public void CreatingNewGraphAndCountMustBe0()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                Assert.AreEqual(graph.TablesCount(), 0);
            }

            [TestMethod]
            public void AddOneTableAndCountMustBe1()
            {
                // Arrange
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();

                // Act
                graph.AddTable("Table_A");

                // Assert
                Assert.AreEqual(graph.TablesCount(), 1);
            }

            [TestMethod]
            public void Add3TableAndCountMustBe3()
            {
                // Arrange
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();

                // Act
                graph.AddTable("Table_A");
                graph.AddTable("Table_B");
                graph.AddTable("Table_C");

                // Assert
                Assert.AreEqual(graph.TablesCount(), 3);
            }

            [TestMethod]
            public void AddOneTableDependencyAndDependentTablesMustBe1()
            {
                // Arrange
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A");
                graph.AddTable("Table_B");

                // Act
                graph.AddTable("Table_A", "Table_B");
                var tableADependencies = graph.GetDependencyTables("Table_A");

                // Assert
                Assert.AreEqual(1, tableADependencies.Count());
            }

            [TestMethod]
            public void Add3TableDependencyAndDependentTablesMustBe3()
            {
                // Arrange
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A");
                graph.AddTable("Table_B");
                graph.AddTable("Table_C");
                graph.AddTable("Table_D");

                // Act
                graph.AddTable("Table_A", "Table_B");
                graph.AddTable("Table_A", "Table_C");
                graph.AddTable("Table_A", "Table_D");
                var tableADependencies = graph.GetDependencyTables("Table_A");

                // Assert
                Assert.AreEqual(3, tableADependencies.Count());
            }

            [TestMethod]
            public void Add3TableDependencyInArrayAndDependentTablesMustBe3()
            {
                // Arrange
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A");
                graph.AddTable("Table_B");
                graph.AddTable("Table_C");
                graph.AddTable("Table_D");

                // Act
                graph.AddTable("Table_A", new string[] { "Table_B", "Table_C", "Table_D" });
                var tableADependencies = graph.GetDependencyTables("Table_A");

                // Assert
                Assert.AreEqual(3, tableADependencies.Count());
            }

            [TestMethod]
            public void AddingTableMultipleTimesShouldResultIn1()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A");
                graph.AddTable("Table_A");
                graph.AddTable("Table_A");


                Assert.AreEqual(1, graph.TablesCount());
            }
        }

        [TestClass]
        public class DependencyEnumeratorTests
        {
            [TestMethod]
            public void Given3TablesWithNoDependency()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A");
                graph.AddTable("Table_B");
                graph.AddTable("Table_C");

                var iterationOrder = new List<string>(3);
                foreach (var table in graph)
                    iterationOrder.Add(table);

                CollectionAssert.AreEqual(new string[] { "Table_C", "Table_B", "Table_A" }, iterationOrder);
            }

            [TestMethod]
            public void Given1TableWith1Dependency()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A", "Table_B");

                var iterationOrder = new List<string>(2);
                foreach (var table in graph)                
                    iterationOrder.Add(table);                

                CollectionAssert.AreEqual(new string[] { "Table_B", "Table_A" }, iterationOrder);
            }

            [TestMethod]
            public void Given1TableWith3Dependency()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A", "Table_B");
                graph.AddTable("Table_A", "Table_C");
                graph.AddTable("Table_A", "Table_D");

                var iterationOrder = new List<string>(2);
                foreach (var table in graph)
                    iterationOrder.Add(table);

                CollectionAssert.AreEqual(new string[] { "Table_D", "Table_C", "Table_B", "Table_A" }, iterationOrder);
            }

            [TestMethod]
            public void Given1TableWith1DependencyWith1Dependency()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A", "Table_B");
                graph.AddTable("Table_B", "Table_C");

                var iterationOrder = new List<string>(2);
                foreach (var table in graph)
                    iterationOrder.Add(table);

                CollectionAssert.AreEqual(new string[] { "Table_C", "Table_B", "Table_A" }, iterationOrder);
            }

            [TestMethod]
            public void Complex()
            {
                IRelationalDatabaseGraph graph = new RelationalDatabaseGraph();
                graph.AddTable("Table_A", "Table_B");
                graph.AddTable("Table_B", "Table_C");
                graph.AddTable("Table_A", "Table_C");

                var iterationOrder = new List<string>(2);
                foreach (var table in graph)
                    iterationOrder.Add(table);

                CollectionAssert.AreEqual(new string[] { "Table_C", "Table_B", "Table_A" }, iterationOrder);
            }
        }
    }
}
