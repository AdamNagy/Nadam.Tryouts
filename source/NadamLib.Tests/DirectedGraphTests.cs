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
				Assert.Equals(3, graph.NodesCount());
			}
		}
	}
}
