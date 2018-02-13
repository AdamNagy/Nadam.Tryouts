using Nadam.Global.Lib.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Global.Lib.DirectedGraph
{
	public class DirectedGraph<TNode> : IDirectedGraph<TNode>
	{
		protected IList<Node<TNode>> NodeSet { get; set; }
		protected IList<DirectedEdge> EdgeSet { get; set; }

		public int NodesCount() { return NodeSet.Count; }
		protected int NodeId;

		public int EdgeCount() { return EdgeSet.Count; }
		protected int EdgeId;

		#region ctors
		public DirectedGraph()
		{
			NodeSet = new List<Node<TNode>>();
			EdgeSet = new List<DirectedEdge>();
			NodeId = 0;
			EdgeId = 0;
		}
		#endregion

		#region Add
		public Node<TNode> AddNewNode(TNode nodeVal)
		{
			var newNode = new Node<TNode>(nodeVal, NodeId++);
			NodeSet.Add(newNode);
			return newNode;
		}

		public Node<TNode> AddNewNodeFor(TNode nodeValA, TNode nodeValB)
		{
			var nodeA = GetNodeByValue(nodeValA);
			if( nodeA == null )
				throw new Exception("Node A does not belong to graph. Please add first");

			var nodeBId = Contains(nodeValB);
			Node<TNode> nodeB;
			if (nodeBId == -1)
				nodeB = AddNewNode(nodeValB);
			else
				nodeB = GetNodeById(nodeBId);

			var newEdge = new DirectedEdge(nodeA.NodeId, nodeB.NodeId, EdgeId++);
			EdgeSet.Add(newEdge);

			return nodeB;
		}

		public void AddExistingNodeFor(TNode nodeValA, TNode nodeValB)
		{
			var nodeA = GetNodeByValue(nodeValA);
			var nodeB = GetNodeByValue(nodeValB);

			if( nodeA == null || nodeB == null )
				throw new Exception("Nodes does not exist");

			var directedEdge = EdgeSet.SingleOrDefault(p => p.ANodeId.Equals(nodeA.NodeId) && p.BNodeId.Equals(nodeB.NodeId));
			if( directedEdge != null )
				return;

			directedEdge = new DirectedEdge(nodeA.NodeId, nodeB.NodeId, EdgeId++);
			EdgeSet.Add(directedEdge);
		}
		#endregion

		#region Contains
		public int Contains(TNode nodeValue)
		{
			var node = GetNodeByValue(nodeValue);
			return node?.NodeId ?? -1;
		}

		public int ContainsDirectedNode(TNode nodeValA, TNode nodeValB)
		{
			var referencedNodes = GetDirectedNodesFor(nodeValA);
			var node = referencedNodes.SingleOrDefault(p => p.Value.Equals(nodeValB));
			return node?.NodeId ?? -1;
		}
		#endregion

		#region Get
		public Node<TNode> GetNodeByValue(TNode nodeValue)
		{
			return NodeSet.SingleOrDefault(p => p.Value.Equals(nodeValue));
		}

		public Node<TNode> GetNode(Node<TNode> node)
		{
			return NodeSet.SingleOrDefault(p => p.Equals(node));
		}

		public Node<TNode> GetNodeById(int nodeId)
		{
			return NodeSet.SingleOrDefault(p => p.NodeId.Equals(nodeId));
		}

		public IEnumerable<Node<TNode>> GetDirectedNodesFor(TNode nodeA)
		{
			var node = GetNodeByValue(nodeA);
			var nodes = EdgeSet.Where(p => p.From.Equals(node.NodeId))
				.Select(p => GetNodeById(p.To));

			return nodes;
		}
		#endregion

		#region Private

		#endregion
	}
}
