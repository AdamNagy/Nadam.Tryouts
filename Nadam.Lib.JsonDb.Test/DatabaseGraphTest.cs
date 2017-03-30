using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Lib.Graph;

namespace Nadam.Lib.JsonDb.Test
{
    public class DatabaseGraphTest
    {
        [TestClass]
        public class DatabaseGraphCreationTest
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
                var suppliersNode = new GraphNode<string>("Suppliers", 1);

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
