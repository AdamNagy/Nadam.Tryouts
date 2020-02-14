using System;
using MyCollection;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Graph
{
	/// <summary>
	/// represents:
	///   undirected (nem irányított)
	///   not coherant (nem összefüggő)
    /// This is a mediator type data structure
	/// </summary>
	/// <typeparam name="NodeType">type of the data that are present in the grapgh</typeparam>
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
            int aIdx = NodeSet.Contains(a), 
                bIdx = NodeSet.Contains(b);

            // check if these nodes are part of the graph
            if( aIdx == -1 || bIdx == -1)
                throw new ArgumentException("Nodes does not exist in graph. Add them first");

			EdgeSet.Add((aIdx, bIdx));
        }
		#endregion

		#region Contains
	    public virtual bool Contains(TNode value)
            => NodeSet.Contains(value) > -1;

        public bool Contains(TNode a, TNode b)
        {
            int aNodeIdx = NodeSet.Contains(a),
                bNodeIdx = NodeSet.Contains(b);

            if (aNodeIdx == -1 || bNodeIdx == -1)
                return false;

            var edgeIdx = EdgeSet.Contains((aNodeIdx, bNodeIdx));
            if (edgeIdx == -1)
                return false;

            return true;

        }
		    
	    #endregion

		#region Remove
		public virtual bool Remove(TNode nodeValue)
	    {
			var nodeToRemoveIdx = NodeSet.Contains(nodeValue);
		    if (nodeToRemoveIdx == -1)
			    return false;

		    RemoveEdgesFor(nodeToRemoveIdx);
			NodeSet.Remove(NodeSet[nodeToRemoveIdx]);

		    return true;
		}

	    public virtual bool Remove(TNode a, TNode b)
	    {
	        int aNodeIdx = NodeSet.Contains(a),
	            bNodeIdx = NodeSet.Contains(b);

	        if (aNodeIdx == -1 || bNodeIdx == -1)
                return false;

	        var edgeIdx = EdgeSet.Contains((aNodeIdx, bNodeIdx));
	        if (edgeIdx > -1)
	        {
		        EdgeSet.Remove((aNodeIdx, bNodeIdx));
		        return true;
	        }

	        return false;
	    }
		#endregion

		#region Enumerators
		public IEnumerator<TNode> Nodes()
		{
		    foreach (var node in NodeSet)
		        yield return node;
		}

		public IEnumerator<TNode> ReferencedNodes(TNode node)
		{
		    throw new NotImplementedException();
		}
		#endregion

		#region Private
		private void RemoveEdgesFor(int nodeIdx)
	    {
			foreach (var edge in EdgeSet.Where(p => p.Item1.Equals(nodeIdx) || p.Item2.Equals(nodeIdx)))
				EdgeSet.Remove(edge);
		}
		#endregion
	}
}
