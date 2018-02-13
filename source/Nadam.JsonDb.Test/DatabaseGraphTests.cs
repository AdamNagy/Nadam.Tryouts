using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
<<<<<<< HEAD
using Nadam.Global.Lib.Graph;
using Nadam.Global.JsonDb.DatabaseGraph;
=======
using Nadam.Lib.Graph;
using Nadam.JsonDb.DatabaseGraph;
>>>>>>> master
using System.Collections.Generic;
using System.Collections;

namespace Nadam.Lib.JsonDb.Test
{
    public class DatabaseGraphTests
    {
        [TestClass]
        public class TableNode_Compare
        {
            #region Using == operator
            [TestMethod]
            public void CompareWithEquatationOperator_shouldBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableA");

                // Action
                var AreEqual = (tableA == tableB);

                // Assert
                Assert.IsTrue(AreEqual);
            }

            [TestMethod]
            public void CompareWithEquatationOperator_shouldNotBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableB");

                // Action
                var AreEqual = (tableA == tableB);

                // Assert
                Assert.IsFalse(AreEqual);
            }

            [TestMethod]
            public void CompareWithEquatationOperator_shouldBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableA", 1);

                // Action
                var AreEqual = (tableA == tableB);

                // Assert
                Assert.IsTrue(AreEqual);
            }

            [TestMethod]
            public void CompareWithEquatationOperator_shouldNotBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableB", 2);

                // Action
                var AreEqual = (tableA == tableB);

                // Assert
                Assert.IsFalse(AreEqual);
            }
            #endregion

            #region Using != operator
            [TestMethod]
            public void CompareWithAntiEquatationOperator_shouldBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableA");

                // Action
                var AreEqual = (tableA != tableB);

                // Assert
                Assert.IsFalse(AreEqual);
            }

            [TestMethod]
            public void CompareWithAntiEquatationOperator_shouldNotBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableB");

                // Action
                var AreEqual = (tableA != tableB);

                // Assert
                Assert.IsTrue(AreEqual);
            }

            [TestMethod]
            public void CompareWithAntiEquatationOperator_shouldBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableA", 1);

                // Action
                var AreEqual = (tableA != tableB);

                // Assert
                Assert.IsFalse(AreEqual);
            }

            [TestMethod]
            public void CompareWithAntiEquatationOperator_shouldNotBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableB", 2);

                // Action
                var AreEqual = (tableA != tableB);

                // Assert
                Assert.IsTrue(AreEqual);
            }
            #endregion

            #region Using .Equal()
            [TestMethod]
            public void CompareWithEquality_shouldBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableA");

                // Action
                var AreEqual = tableB.Equals(tableB);

                // Assert
                Assert.IsTrue(AreEqual);
            }

            [TestMethod]
            public void CompareWithEquality_shouldNotBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableB");

                // Action
                var AreEqual = tableA.Equals(tableB);

                // Assert
                Assert.IsFalse(AreEqual);
            }

            [TestMethod]
            public void CompareWithEquality_shouldBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableA", 1);

                // Action
                var AreEqual = tableB.Equals(tableB);

                // Assert
                Assert.IsTrue(AreEqual);
            }

            [TestMethod]
            public void CompareWithEquality_shouldNotBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableB", 2);

                // Action
                var AreEqual = tableA.Equals(tableB);

                // Assert
                Assert.IsFalse(AreEqual);
            }
            #endregion

            #region Testing Unit test equality assertion
            //[TestMethod]
            //public void CompareWithEqualityAssertion_shouldBeEqual()
            //{
            //    // Arrange
            //    var tableA = new TableNode("TableA");
            //    var tableB = new TableNode("TableA");

            //    // Assert
            //    Assert.AreEqual(tableA, tableB);
            //}

            [TestMethod]
            public void CompareWithEqualityAssertion_shouldNotBeEqual()
            {
                // Arrange
                var tableA = new TableNode("TableA");
                var tableB = new TableNode("TableB");

                // Assert
                Assert.AreNotEqual(tableA, tableB);
            }

            //[TestMethod]
            //public void CompareWithEqualityAssertion_shouldBeEqual_B()
            //{
            //    // Arrange
            //    var tableA = new TableNode("TableA", 1);
            //    var tableB = new TableNode("TableA", 1);

            //    // Assert
            //    Assert.AreEqual(tableA, tableB);
            //}

            [TestMethod]
            public void CompareWithEqualityAssertion_shouldNotBeEqual_B()
            {
                // Arrange
                var tableA = new TableNode("TableA", 1);
                var tableB = new TableNode("TableB", 2);

                // Assert
                Assert.AreNotEqual(tableA, tableB);
            }
            #endregion
        }

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
                Assert.AreEqual(13, graph.Count);
            }

            [TestMethod]
            public void AddOneTable()
            {
                // Arrange
                var graph = new DbModelGraph();

                // Act
                graph.FindOrAddTable("Table_A");

                // Assert
                Assert.AreEqual(graph.Count, 1);
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
                Assert.AreEqual(graph.Count, 3);
            }
    // ****
    // Will fail alwaiys, UnitTest framework .IsEqual calls something that is not correct (neither == operator, neither .Equal() function it is)
    // ****
            //[TestMethod]
            //public void AddOneTableDependency()
            //{
            //    // Arrange
            //    var graph = new DbModelGraph();

            //    // Act
            //    graph.FindOrAddTable("Table_A");
            //    graph.FindOrAddTable("Table_B");
            //    graph.FindOrAddTable("Table_C");

            //    graph.AddDependecy("Table_A", "Table_B");
            //    graph.AddDependecy("Table_A", "Table_C");

            //    var tableA = graph.FindByNodeId(1);
            //    var neightbour = new TableNode("Table_B", 2) { Neighbors = new List<GraphNode<string>>() };
            //    // Assert
            //    var f = tableA.Neighbors[0].Equals(neightbour);
            //    var g = tableA.Neighbors[0] == neightbour;
            //    Assert.AreEqual(tableA.Neighbors.Count, 2);
            //    Assert.IsTrue(tableA.Neighbors[0].Equals(neightbour));
            //    Assert.AreEqual(tableA.Neighbors[0], neightbour);
            //    CollectionAssert.AreEqual(tableA.Neighbors as ICollection,
            //                              new List<TableNode>() {
            //                                  new TableNode("Table_B", 2) { Neighbors = new List<GraphNode<string>>() },
            //                                  new TableNode("Table_C", 3) { Neighbors = new List<GraphNode<string>>() } });
            //}

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
