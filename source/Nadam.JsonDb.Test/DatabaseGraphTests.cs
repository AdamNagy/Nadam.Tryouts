using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Global.Lib.Graph;
using Nadam.Global.JsonDb.DatabaseGraph;
using System.Collections.Generic;
using System.Collections;

namespace Nadam.Lib.JsonDb.Test
{
    public class DatabaseGraphTests
    {
        [TestClass]
        public class DatabaseGraphCreationEndAddTest
        {
            [TestMethod]
            public void CreateAndInitialize()
            {
                // Arrange
                // -
                // Action
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();

                // Assert
                Assert.AreEqual(13, graph.NodesCount());
            }

            [TestMethod]
            public void AddOneTable()
            {
                // Arrange
                var graph = new DbModelGraph();

                // Act
                graph.FindOrAddTable("Table_A");

                // Assert
                Assert.AreEqual(graph.NodesCount(), 1);
            }

            [TestMethod]
            public void AddMoreTable()
            {
                // Arrange
                var graph = new DbModelGraph();

                // Act
                graph.FindOrAddTable("Table_A");
                graph.FindOrAddTable("Table_B");
                graph.FindOrAddTable("Table_C");

                // Assert
                Assert.AreEqual(graph.NodesCount(), 3);
            }
            // ****
            // Will fail alwaiys, UnitTest framework .IsEqual calls something that is not correct (neither == operator, neither .Equal() function it is)
            // ****
            [TestMethod]
            public void AddOneTableDependency()
            {
                // Arrange
                var graph = new DbModelGraph();

                // Act
                var tableA = graph.FindOrAddTable("Table_A");
                graph.FindOrAddTable("Table_B");
                graph.FindOrAddTable("Table_C");

                graph.AddEdgeFor("Table_A", "Table_B");
                graph.AddEdgeFor("Table_A", "Table_C");

                //var tableA = graph.FindByNodeId(1);
                var neightbour = new TableNode("Table_B", 2) { Neighbors = new List<GraphNode<string>>() };
                // Assert
                var f = tableA.Neighbors[0].Equals(neightbour);
                var g = tableA.Neighbors[0] == neightbour;
                Assert.AreEqual(tableA.Neighbors.Count, 2);
                Assert.IsTrue(tableA.Neighbors[0].Equals(neightbour));
                Assert.AreEqual(tableA.Neighbors[0], neightbour);
                CollectionAssert.AreEqual(tableA.Neighbors as ICollection,
                                          new List<TableNode>() {
                                              new TableNode("Table_B", 2) { Neighbors = new List<GraphNode<string>>() },
                                              new TableNode("Table_C", 3) { Neighbors = new List<GraphNode<string>>() } });
            }

            [TestMethod]
            public void AddOneDependencyes()
            {
                // Arrange
                var graph = new DbModelGraph();

                // Act
                graph.FindOrAddTable("Table_A");
                graph.FindOrAddTable("Table_B");
                graph.FindOrAddTable("Table_C");

                graph.AddDependecy("Table_A", "Table_B");
                graph.AddDependecy("Table_B", "Table_C");

                var tableA = graph.FindByNodeId(1);
                var tableB = graph.FindByNodeId(2);
                var tableC = graph.FindByNodeId(3);

                // Assert
                Assert.AreEqual(tableA.Neighbors.Count, 1);
                Assert.AreEqual(tableB.Neighbors.Count, 1);
                Assert.AreEqual(tableC.Neighbors.Count, 0);
            }
        }

        [TestClass]
        public class DatabaseFindTest
        {
            [TestMethod]
            public void FindByNodeIdTest_valid()
            {
                // Arrange
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();

                // Action
                var suppliers = graph.FindByNodeId(1);
                var productions = graph.FindByNodeId(5);
                var customerDemographi = graph.FindByNodeId(13);

                // Assert
                Assert.AreEqual("Suppliers", suppliers.Value);
                Assert.AreEqual("Productions", productions.Value);
                Assert.AreEqual("CustomerDemographi", customerDemographi.Value);
            }

            [TestMethod]
            public void FindByNodeIdTest_invalid()
            {
                // Arrange
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();

                // Action
                var table1 = graph.FindByNodeId(14);
                var table2 = graph.FindByNodeId(-15);
                var table3 = graph.FindByNodeId(0);

                // Assert
                Assert.IsNull(table1);
                Assert.IsNull(table2);
                Assert.IsNull(table3);
            }

            [TestMethod]
            public void FindByTableNameTest_valid()
            {
                // Arrange
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();

                // Action
                var suppliers = graph.FindByValue("Suppliers");
                var productions = graph.FindByValue("Productions");
                var customerDemographi = graph.FindByValue("CustomerDemographi");

                // Assert
                Assert.AreEqual(1, suppliers.NodeId);
                Assert.AreEqual(5, productions.NodeId);
                Assert.AreEqual(13, customerDemographi.NodeId);
            }

            [TestMethod]
            public void FindByTableNameTest_invalid()
            {
                // Arrange
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();

                // Action
                var suppliers = graph.FindByValue("NotExistTable");

                // Assert
                Assert.IsNull(suppliers);
            }

            [TestMethod]
            public void FindByNodeTest_valid()
            {
                // Arrange
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();
                var suppliersNode = new TableNode("Suppliers", 1);

                // Action
                var suppliers = graph.FindByValue(suppliersNode);

                // Assert
                Assert.AreEqual(suppliers.NodeId, suppliersNode.NodeId);
                Assert.AreEqual(suppliers.Value, suppliersNode.Value);
            }

            [TestMethod]
            public void FindByNodeTest_valid_b()
            {
                // Arrange
                var graph = NorthwindDbGraphSeeder.GenerateNorthwindDbGraph();
                var suppliersNode = graph.FindByNodeId(1);

                // Action
                var suppliers = graph.FindByValue(suppliersNode);

                // Assert
                Assert.AreEqual(suppliers.NodeId, suppliersNode.NodeId);
                Assert.AreEqual(suppliers.Value, suppliersNode.Value);
            }
        }
    }
}
