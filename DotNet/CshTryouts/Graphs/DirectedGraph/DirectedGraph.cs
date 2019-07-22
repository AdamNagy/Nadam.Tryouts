using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectedGraph
{
    /// <summary>
    /// Mediator like graph implementation as this class holds the all the node references.
    /// Nodes can point to each other, but the control is in the mediator -this- class
    /// </summary>
    /// <typeparam name="TNode">type of the data the graph holds</typeparam>
	public class DirectedGraph<TNode> : IDirectedGraph<TNode> 
	{
		protected IList<DirectedNode<TNode>> NodeSet { get; set; }

        public int NodesCount() { return NodeSet.Count; }
		protected int NodeId;    

		#region ctors
		public DirectedGraph()
		{
			NodeSet = new List<DirectedNode<TNode>>();
			NodeId = 0;
		}
		#endregion

		#region Add
		public DirectedNode<TNode> AddNode(TNode nodeVal)
		{
			var newNode = new DirectedNode<TNode>(nodeVal, NodeId++);
			NodeSet.Add(newNode);
			return newNode;
		}

		public virtual void AddReferenceFor(TNode startNode, TNode referenced)
		{
            var nodeAs = GetNode(startNode);
            if (nodeAs.Count < 1)
                throw new Exception("From node does not exist");
            var nodeA = nodeAs.First();

            var nodeBs = GetNode(referenced);
            if (nodeBs.Count < 1)
                throw new Exception("To node does not exist");
            var nodeB = nodeBs.First();

            nodeA.AddReference(nodeB.NodeId);       
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
            var nodeB = GetNode(nodeValB).First();
			return GetNode(nodeValA).First().HasReferenceFor(nodeB.NodeId);
		}
		#endregion

		#region Get
		public IList<DirectedNode<TNode>> GetNode(TNode nodeValue)
        {
			return NodeSet.Where(p => p.Value.Equals(nodeValue)).ToList();
		}

        public TNode this[int index]
        {
            get
            {
                return NodeSet[index].Value;
            }
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
            nodeToRemove.RemoveReferences();
            NodeSet.Remove(nodeToRemove);
			return true;
		}

		public bool RemoveReferenceFor(TNode a, TNode b)
		{
			var nodeAs = GetNode(a);
            if (nodeAs.Count() != 1)
                throw new Exception("NodeA does not exist, or belong to graph multiple times");
            var nodeA = nodeAs.First();

            var nodeBs = GetNode(b);
            if (nodeBs.Count() != 1)
                throw new Exception("NodeA does not exist, or belong to graph multiple times");
            var nodeB = nodeAs.First();

            nodeA.RemoveReference(nodeB.NodeId);
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

            foreach (var nodes in NodeSet)            
                nodes.RemoveReference(nodeA.NodeId);            
		}

        protected IEnumerable<DirectedNode<TNode>> GetReferencedNodesFor(TNode nodeVal)
        {
            if (!ContainsNode(nodeVal))
                throw new Exception($"Node with value {nodeVal} does not exist in the current graph");

            var referencedNodes = new List<DirectedNode<TNode>>();

            var node = GetNode(nodeVal).First();

            foreach (var p in node.GetReferences())            
                referencedNodes.Add(NodeSet[p]);            

            return referencedNodes;
        }
		#endregion
	}
}
