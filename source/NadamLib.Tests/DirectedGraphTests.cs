using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Global.Lib.DirectedGraph;

namespace NadamLib.Tests
{
	class DirectedGraphTests
	{
		[TestClass]
		public class NodeAndEdgeCountTests
		{
            [TestMethod]
			public void NodesCountMustBeEqualTo3()
			{
				// Arrange
				IDirectedGraph<int> graph = new DirectedGraph<int>();

				// Action
				graph.AddNewNode(1);
				graph.AddNewNode(2);
				graph.AddNewNode(3);

				// Asser
				Assert.AreEqual(3, graph.NodesCount());
                Assert.AreEqual(0, graph.EdgeCount());
			}

            [TestMethod]
            public void EdgesCountMustBeEqualTo2()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                // Action
                var nodeA = graph.AddNewNode(1);
                var nodeB = graph.AddNewNode(2);


                graph.AddNewNodeFor(nodeA.Value, 10);
                graph.AddNewNodeFor(nodeB.Value, 5);

                // Asser
                Assert.AreEqual(4, graph.NodesCount());
                Assert.AreEqual(2, graph.EdgeCount());
            }

            [TestMethod]
            public void AddExistingNodeFor()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                var nodeA = graph.AddNewNode(234);
                var nodeB = graph.AddNewNode(345);

                // Action
                graph.AddExistingNodeFor(nodeA.Value, nodeB.Value);

                // Assert
                Assert.AreEqual(1, graph.EdgeCount());
            }

            [TestMethod]
            public void ContainsExisting()
            {
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                var nodeA = graph.AddNewNode(234);
                var nodeB = graph.AddNewNode(345);

                var nodeId = graph.Contains(234);

                Assert.AreEqual(nodeA.NodeId, nodeId);
            }

            [TestMethod]
            public void ContainsNonExisting()
            {
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                var nodeA = graph.AddNewNode(234);
                var nodeB = graph.AddNewNode(345);

                var nodeId = graph.Contains(2345);

                Assert.AreEqual(nodeId, -1);
            }

            [TestMethod]
            public void ContainsDirectedChild()
            {
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                for (int i = 1; i < 10; i++)
                {
                    graph.AddNewNode(i * 3);
                }

                var node30 = graph.AddNewNodeFor(3, 30);
                graph.AddNewNodeFor(3, 31);
                graph.AddNewNodeFor(3, 32);

                var node30c = graph.ContainsDirectedNode(3, 30);

                Assert.AreEqual(node30.NodeId, node30c);
            }
        }
	}
}
