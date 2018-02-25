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
		public Node<TNode> AddNode(TNode nodeVal)
		{
			var newNode = new Node<TNode>(nodeVal, NodeId++);
			NodeSet.Add(newNode);
			return newNode;
		}

		public DirectedEdge AddEdgeFor(TNode startNode, TNode referenced)
		{
            var nodeAs = GetNode(startNode);
            if (nodeAs.Count != 1)
                throw new Exception("From node does not exist");
            var nodeA = nodeAs.First();

            var nodeBs = GetNode(referenced);
            if (nodeBs.Count != 1)
                throw new Exception("To node does not exist");
            var nodeB = nodeBs.First();

            var newEdge = new DirectedEdge(nodeA.NodeId, nodeB.NodeId, EdgeId++);
            if (EdgeSet.SingleOrDefault(p => p.ANodeId.Equals(nodeA.NodeId) && p.BNodeId.Equals(nodeB.NodeId)) != null)
                throw new Exception("There is already edge between these 2 nodes");

            EdgeSet.Add(newEdge);
            return newEdge;
        }
		#endregion

		#region Contains
		public bool ContainsNode(TNode nodeValue)
		{
			var nodes = GetNode(nodeValue);
			return nodes.Count > 0;
		}

		public bool ContainsEdge(TNode nodeValA, TNode nodeValB)
		{
			var edges = GetEdgesFor(nodeValA);
            var bNode = GetNode(nodeValB).FirstOrDefault();
			var edge = edges.SingleOrDefault(p => p.BNodeId.Equals(bNode.NodeId));
			return edge != null;
		}
		#endregion

		#region Get
		public IList<Node<TNode>> GetNode(TNode nodeValue)
        {
			return NodeSet.Where(p => p.Value.Equals(nodeValue)).ToList();
		}

		public IList<DirectedEdge> GetEdgesFor(TNode nodeVal)
        {
			var nodes = GetNode(nodeVal);
            if (nodes.Count() != 1)
                throw new Exception("Node does not exist, or belong to graph multiple times");

            var node = nodes.First();
			var edges = EdgeSet.Where(p => p.From.Equals(node.NodeId) || p.To.Equals(node.NodeId));
			return edges.ToList();
		}
		#endregion

		#region Remove
		public bool RemoveNode(TNode nodeValue)
		{
			var nodes = GetNode(nodeValue);
            if (nodes.Count() != 1)
                throw new Exception("Node does not exist, or belong to graph multiple times");

            var nodeToRemove = nodes.First();
            RemoveIncomingEdgesFor(nodeToRemove.Value);
			RemoveOutgoingEdgesFor(nodeToRemove.Value);
            NodeSet.Remove(nodeToRemove);
			return true;
		}

		public bool RemoveEdge(TNode a, TNode b)
		{
			var nodeAs = GetNode(a);
            if (nodeAs.Count() != 1)
                throw new Exception("NodeA does not exist, or belong to graph multiple times");
            var nodeA = nodeAs.First();

            var nodeBs = GetNode(b);
            if (nodeBs.Count() != 1)
                throw new Exception("NodeA does not exist, or belong to graph multiple times");
            var nodeB = nodeAs.First();

			var edgeToRemove = EdgeSet.SingleOrDefault(p => p.From.Equals(nodeA.NodeId) && p.To.Equals(nodeB.NodeId));

			if (edgeToRemove == null)
				return false;

			EdgeSet.Remove(edgeToRemove);
			return true;
		}
        #endregion

        #region Protected
        protected void RemoveIncomingEdgesFor(TNode nodeVal)
		{
            var nodeAs = GetNode(nodeVal);
            if (nodeAs.Count() != 1)
                throw new Exception("NodeA does not exist, or belong to graph multiple times");
            var nodeA = nodeAs.First();
            foreach (var edge in EdgeSet.Where(p => p.To.Equals(nodeA.NodeId)))
				EdgeSet.Remove(edge);
		}

        protected void RemoveOutgoingEdgesFor(TNode nodeVal)
		{
            var nodeAs = GetNode(nodeVal);
            if (nodeAs.Count() != 1)
                throw new Exception("NodeA does not exist, or belong to graph multiple times");
            var nodeA = nodeAs.First();

            foreach (var edge in EdgeSet.Where(p => p.From.Equals(nodeA.NodeId)))
				EdgeSet.Remove(edge);
		}

        protected IEnumerable<TNode> GetReferencedNodesFor(TNode nodeVal)
        {
            if (!ContainsNode(nodeVal))
                throw new Exception($"Node with value {nodeVal} does not exist in the current graph");

            var referencedNodes = new List<TNode>();

            var node = GetNode(nodeVal).First();
            var outgoingEdges = EdgeSet.Where(p => p.From.Equals(node.NodeId)).ToList();

            if (outgoingEdges.Count == 0)
                return referencedNodes;

            outgoingEdges.ForEach(p => referencedNodes.Add(NodeSet.Single(q => q.NodeId.Equals(p.To)).Value));

            return referencedNodes;
        }
		#endregion
	}
}
