using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Global.Lib.DirectedGraph;
using System;
using System.Collections.Generic;

namespace NadamLib.Tests
{   
    public class DirectedGraphTests
	{
        [TestClass]
        public class Add
		{
            [TestMethod]
            public void Add3NodesAndNodeIdsIdMusBe012()
            {
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                var nodeIds = new List<int> { graph.AddNode(10).NodeId };
                nodeIds.Add(graph.AddNode(10).NodeId);
                nodeIds.Add(graph.AddNode(10).NodeId);

                CollectionAssert.AreEqual(new List<int> {0,1,2 }, nodeIds);
            }

            [TestMethod]
            public void Adding1NodeMustIncrementNodeCountWith1()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);
                var startNodeCount = graph.NodesCount();

                // Action
                graph.AddNode(3);
                graph.AddNode(4);
                var newNodeCount = graph.NodesCount();

                // Assert
                Assert.AreEqual((startNodeCount + 2), newNodeCount);
            }

            [TestMethod]
            public void Add3EdgesAndIdsMustBe012()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                                
                graph.AddNode(1);
                graph.AddNode(2);
                graph.AddNode(3);
                graph.AddNode(4);

                // Action
                var nodeIds = new List<int> { graph.AddEdgeFor(1, 3).EdgeId };
                nodeIds.Add(graph.AddEdgeFor(2, 3).EdgeId);
                nodeIds.Add(graph.AddEdgeFor(3, 4).EdgeId);

                // Assert
                CollectionAssert.AreEqual(new List<int> { 0, 1, 2 }, nodeIds);
            }

            [TestMethod]
            public void Adding3NodeMustIncrementNodeCountWith3()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);graph.AddNode(2);graph.AddNode(3);graph.AddNode(4);
                graph.AddEdgeFor(1, 2);
                graph.AddEdgeFor(1, 3);
                var startEdgeCount = graph.EdgeCount();

                // Action
                graph.AddEdgeFor(2, 3);
                graph.AddEdgeFor(2, 4);
                var newEdgeCount = graph.EdgeCount();

                // Assert
                Assert.AreEqual((startEdgeCount+2), newEdgeCount);
            }
        }

		[TestClass]
		public class Contains
		{
			[TestMethod]
			public void ContainsNodeMustReturnTrue()
			{               
				IDirectedGraph<int> graph = new DirectedGraph<int>();

				var nodeA = graph.AddNode(234);
				var nodeB = graph.AddNode(345);

				var contains = graph.ContainsNode(234);

				Assert.IsTrue(contains);
			}

			[TestMethod]
			public void ContainsNodeMustReturnFalse()
			{
				IDirectedGraph<int> graph = new DirectedGraph<int>();
				graph.AddNode(234);
				graph.AddNode(345);

				var contains = graph.ContainsNode(1);

				Assert.IsFalse(contains);
			}

            [TestMethod]
            public void ContainsEdgeMustReturnTrue()
            {
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(234);
                graph.AddNode(345);
                graph.AddEdgeFor(234, 345);

                var contains = graph.ContainsEdge(234, 345);
                Assert.IsTrue(contains);
            }

            [TestMethod]
            public void ContainsEdgeMustReturnFalse()
            {
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                var nodeA = graph.AddNode(234);
                var nodeB = graph.AddNode(345);

                var contains = graph.ContainsEdge(234, 345);

                Assert.IsFalse(contains);
            }
        }

        [TestClass]
        public class Get
        {
            [TestMethod]
            public void GetNodeMustReturn0()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);

                // Action
                var nodes = graph.GetNode(3);

                // Assert
                Assert.AreEqual(0, nodes.Count);
            }

            [TestMethod]
            public void GetNodeMustReturn1()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);

                // Action
                var nodes = graph.GetNode(2);

                // Assert
                Assert.AreEqual(1, nodes.Count);
            }

            [TestMethod]
            public void GetNodeMustReturn6()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(1);
                graph.AddNode(1);
                graph.AddNode(1);
                graph.AddNode(1);
                graph.AddNode(1);

                // Action
                var nodes = graph.GetNode(1);

                // Assert
                Assert.AreEqual(6, nodes.Count);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void GetEdgesForMustThrowException()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);
                graph.AddNode(3);
                graph.AddNode(4);

                // Action
                var edges = graph.GetEdgesFor(5);
            }

            [TestMethod]
            public void GetEdgesForMustReturn1()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);
                graph.AddNode(3);
                graph.AddNode(4);

                graph.AddEdgeFor(1, 3);
                graph.AddEdgeFor(2, 3);
                graph.AddEdgeFor(3, 4);
                // Action
                var edges = graph.GetEdgesFor(1);

                // Assert
                Assert.AreEqual(1, edges.Count);
            }

            [TestMethod]
            public void GetEdgesForMustReturn3()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);
                graph.AddNode(3);
                graph.AddNode(4);

                graph.AddEdgeFor(1, 2);
                graph.AddEdgeFor(1, 3);
                graph.AddEdgeFor(1, 4);
                // Action
                var edges = graph.GetEdgesFor(1);

                // Assert
                Assert.AreEqual(3, edges.Count);
            }
        }

		[TestClass]
		public class Remove
		{
            [TestMethod]
            public void RemoveNodeMustReturnTrue()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);

                // Action
                var removal = graph.RemoveNode(1);

                // Assert
                Assert.IsTrue(removal);
            }

            [TestMethod]
            public void RemoveNodeWhenReturnsTrueMustDecrementNodeCount()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);
                var startNodeCount = graph.NodesCount();

                // Action
                graph.RemoveNode(1);
                var mewNodeCount = graph.NodesCount();

                // Assert
                Assert.AreEqual((startNodeCount - 1), mewNodeCount);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void RemoveNodeMustThrowException()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);
                graph.AddNode(2);

                // Action
                var removal = graph.RemoveNode(3);
            }

            [TestMethod]
            public void RemoveNodeWhenReturnsFalseMustNotChangeNodeCount()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();
                graph.AddNode(1);graph.AddNode(2);graph.AddNode(1);
                graph.AddNode(2);graph.AddNode(1);graph.AddNode(2);

                var startNodeCount = graph.NodesCount();

                // Action
                try
                {
                    graph.RemoveNode(5);
                }
                catch{}
                
                var mewNodeCount = graph.NodesCount();

                // Assert
                Assert.AreEqual(startNodeCount, mewNodeCount);
            }
        }

		[TestClass]
		public class Properties
		{
            [TestMethod]
            public void Add3NodesAndNodeCountMustBe0()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                // Asser
                Assert.AreEqual(0, graph.NodesCount());
            }

            [TestMethod]
            public void Add3NodesAndNodeCountMustBe1()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                // Action
                graph.AddNode(1);

                // Asser
                Assert.AreEqual(1, graph.NodesCount());
            }

            [TestMethod]
			public void Add3NodesAndNodeCountMustBe6()
			{
				// Arrange
				IDirectedGraph<int> graph = new DirectedGraph<int>();

				// Action
				graph.AddNode(1);
				graph.AddNode(2);
				graph.AddNode(3);
                graph.AddNode(1);
                graph.AddNode(2);
                graph.AddNode(3);

                // Asser
                Assert.AreEqual(6, graph.NodesCount());
			}

            [TestMethod]
            public void EdgesCountMustBeEqualTo0()
            {
                // Arrange
                IDirectedGraph<int> graph = new DirectedGraph<int>();

                // Action
                graph.AddNode(1);
                graph.AddNode(2);
                graph.AddNode(3);
                graph.AddNode(4);

                // Asser
                Assert.AreEqual(0, graph.EdgeCount());
            }

            [TestMethod]
			public void EdgesCountMustBeEqualTo3()
			{
				// Arrange
				IDirectedGraph<int> graph = new DirectedGraph<int>();

				// Action
				graph.AddNode(1);
				graph.AddNode(2);
                graph.AddNode(3);
                graph.AddNode(4);

                graph.AddEdgeFor(1, 3);
				graph.AddEdgeFor(2, 3);
                graph.AddEdgeFor(3, 4);

                // Asser
                Assert.AreEqual(3, graph.EdgeCount());
			}
		}
	}
}
