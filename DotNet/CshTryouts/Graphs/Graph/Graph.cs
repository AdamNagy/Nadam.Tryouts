using System;
using MyCollection;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Graph
{
	/// <summary>
	/// Base graph class, represents an undirected graph
	/// Nodes and undirected edges can added
    /// This is a mediator type data structure
	/// </summary>
	/// <typeparam name="NodeType"></typeparam>
	public class Graph<TNode>
    {
        protected IIndexList<TNode> NodeSet { get; set; }
		protected IIndexList<(int, int)> EdgeSet { get; set; }

        public int NodesCount => NodeSet.Count;
	    public int EdgeCount => EdgeSet.Count;

		#region Add
        public virtual void AddNode(TNode val)
        {
            NodeSet.Add(val);
        }

        public virtual void AddEdge(TNode a, TNode b)
        {
            // check if these nodes are part of the graph
            if( !NodeSet.Contains(a) || !NodeSet.Contains(b))
                throw new ArgumentException("Nodes does not exist in graph. Add them first");

			EdgeSet.Add((NodeSet[a], NodeSet[b]));
        }
		#endregion

		#region Contains
	    public virtual bool Contains(TNode value)
            => NodeSet.Contains(value);

	    public bool Contains((int, int) edge)
		    => EdgeSet.Contains(edge);
	    #endregion

		#region Remove
		public virtual bool Remove(TNode nodeValue)
	    {
			var nodeToRemove = NodeSet.Contains(nodeValue);
		    if (nodeToRemove == null)
			    return false;

		    RemoveEdgesFor(nodeToRemove);
			Remove(nodeToRemove);

		    return true;
		}

	    public virtual bool Remove(int nodeId)
	    {
			var nodeToRemove = FindNode(nodeId);
		    if (nodeToRemove == null)
			    return false;

		    RemoveEdgesFor(nodeToRemove);
		    Remove(nodeToRemove);

		    return true;
		}

	    public virtual bool Remove(Node<TNode> nodeToRemove)
	    {
			if (nodeToRemove == null)
			    return false;

		    RemoveEdgesFor(nodeToRemove);
		    Remove(nodeToRemove);

		    return true;
		}

	    public virtual bool Remove(Edge edge)
	    {
		    var edgeToRemove = FindEdge(edge.EdgeId);

		    if (edgeToRemove == null)
			    return false;

		    EdgeSet.Remove(edgeToRemove);
		    return true;
	    }
		#endregion

		#region Enumerators
		public IEnumerator<Node<TNode>> GetNodeSetEnumerator()
		{
			return NodeSet.GetEnumerator();
		}

		public IEnumerator<Edge> GetEdgeSetEnumerator()
		{
			return EdgeSet.GetEnumerator();
		}
		#endregion

		#region Private
		private void RemoveEdgesFor(Node<TNode> node)
	    {
			foreach (var edge in EdgeSet.Where(p => p.ANodeId.Equals(node) || p.BNodeId.Equals(node)))
			{
				EdgeSet.Remove(edge);
			}
		}
		#endregion
	}
}
