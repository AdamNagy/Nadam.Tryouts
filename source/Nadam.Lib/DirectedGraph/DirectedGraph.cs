using Nadam.Global.Lib.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

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
			var nodeA = GetNode(nodeValA);

			if( nodeA == null )
				throw new Exception("Node A does not belong to graph. Please add first");

			var nodeBId = GetNode(nodeValB);
			Node<TNode> nodeB;
			if (nodeBId == null)
				nodeB = AddNewNode(nodeValB);
			else
				nodeB = GetNode(nodeBId);

			var newEdge = new DirectedEdge(nodeA.NodeId, nodeB.NodeId, NodeId++);

			EdgeSet.Add(newEdge);

			return nodeB;
		}

		public void AddEdgeFor(TNode startNode, TNode referenced)
		{
			
		}
		#endregion

		#region Contains
		public bool ContainsNode(TNode nodeValue)
		{
			var node = GetNode(nodeValue);
			return node != null;
		}

		public bool ContainsEdge(TNode nodeValA, TNode nodeValB)
		{
			var referencedNodes = GetNodesFor(nodeValA);
			var node = referencedNodes.SingleOrDefault(p => p.Value.Equals(nodeValB));
			return node != null;
		}
		#endregion

		#region Get
		public Node<TNode> GetNode(TNode nodeValue)
		{
			return NodeSet.SingleOrDefault(p => p.Value.Equals(nodeValue));
		}

		public Node<TNode> GetNode(Node<TNode> node)
		{
			return NodeSet.SingleOrDefault(p => p.Equals(node));
		}

		public IEnumerable<Node<TNode>> GetNodesFor(TNode nodeA)
		{

			var node = GetNode(nodeA);
			var nodes = EdgeSet.Where(p => p.From.Equals(node.NodeId))
				.Select(p => GetNodeById(p.To));

			return nodes;
		}

		public IEnumerable<Node<TNode>> GetNodesFor(Node<TNode> nodeA)
		{

			var node = GetNode(nodeA);
			var nodes = EdgeSet.Where(p => p.From.Equals(node.NodeId))
				.Select(p => GetNodeById(p.To));

			return nodes;
		}
		#endregion

		#region Remove
		public bool RemoveNode(TNode nodeValue)
		{
			var nodeToRemove = GetNode(nodeValue);
			RemoveIncomingEdgesFor(nodeToRemove.Value);
			RemoveOutgoingEdgesFor(nodeToRemove.Value);
			return true;
		}

		public bool RemoveEdge(TNode a, TNode b)
		{
			var nodeA = GetNode(a);
			var nodeB = GetNode(b);

			if (nodeA == null || nodeB == null)
				return false;

			var edgeToRemove = EdgeSet.SingleOrDefault(p => p.From.Equals(nodeA.NodeId) && p.To.Equals(nodeB.NodeId));

			if (edgeToRemove == null)
				return false;

			EdgeSet.Remove(edgeToRemove);
			return true;
		}

		public void RemoveIncomingEdgesFor(TNode nodeVal)
		{
			var node = GetNode(nodeVal);
			foreach (var edge in EdgeSet.Where(p => p.To.Equals(node.NodeId)))
				EdgeSet.Remove(edge);
		}

		public void RemoveOutgoingEdgesFor(TNode nodeVal)
		{
			var node = GetNode(nodeVal);
			foreach (var edge in EdgeSet.Where(p => p.From.Equals(node.NodeId)))
				EdgeSet.Remove(edge);
		}
		#endregion

		#region Private
		private Node<TNode> GetNodeById(int nodeId)
		{
			return NodeSet.SingleOrDefault(p => p.NodeId.Equals(nodeId));
		}
		#endregion
	}
}
