using System.Collections.Generic;
using System.Linq;

namespace Graph
{
	/// <summary>
	/// Base graph class, represents an undirected graph
	/// Nodes and undirected edges can added
	/// </summary>
	/// <typeparam name="NodeType"></typeparam>
	public class Graph<TNode> : IGraph<TNode>
    {
        protected IList<Node<TNode>> NodeSet { get; set; }
		protected IList<Edge> EdgeSet { get; set; }

        public int NodesCount => NodeSet.Count;
	    public int EdgeCount => EdgeSet.Count;

	    protected int NodeId;
	    protected int EdgeId;

        #region ctors
        public Graph() : this(null) { }

		public Graph(IList<Node<TNode>> nodeSet)
        {
            NodeSet = nodeSet ?? new List<Node<TNode>>();
	        NodeId = 0;
	        EdgeId = 0;

        }
		#endregion

		#region Add
		public virtual void AddNode(Node<TNode> node)
        {
            node.NodeId = NodeId++;
            NodeSet.Add(node);
        }

		public virtual void AddNode(TNode value)
        {
			NodeSet.Add(new Node<TNode>(value, NodeId++));
        }

	    public virtual void AddEdge(Node<TNode> a, Node<TNode> b)
        {
            // check if these nodes are part of the graph
			EdgeSet.Add(new Edge(a.NodeId, b.NodeId, EdgeId++));
        }
		#endregion

		#region Contains
	    public virtual bool Contains(TNode value)
        {
            return FindNode(value) != null;
        }

		public virtual bool Contains(Node<TNode> node)
        {
            return FindNode(node.NodeId) != null;
        }

	    public bool Contains(Edge edge)
	    {
		    var lookingFor = FindEdge(edge.ANodeId, edge.BNodeId);
		    return lookingFor != null;
	    }

	    #endregion

		#region Find
	    public virtual Node<TNode> FindNode(TNode reference)
        {
            return NodeSet.SingleOrDefault(p => p.Value.Equals(reference));
        }

		public virtual Node<TNode> FindNode(Node<TNode> reference)
        {
            return NodeSet.SingleOrDefault(p => p.Equals(reference));
        }

		public virtual Node<TNode> FindNode(int nodeId)
        {
            return NodeSet.SingleOrDefault(p => p.NodeId.Equals(nodeId));
        }

	    public Edge FindEdge(int aNodeId, int bNodeId)
	    {
		    return EdgeSet.SingleOrDefault(p => 
				p.ANodeId.Equals(aNodeId) && 
				p.BNodeId.Equals(bNodeId));
	    }

	    public Edge FindEdge(int edgeId)
	    {
		    return EdgeSet.SingleOrDefault(p => p.EdgeId.Equals(edgeId));
	    }
		#endregion

		#region Remove
		public virtual bool Remove(TNode nodeValue)
	    {
			var nodeToRemove = FindNode(nodeValue);
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
