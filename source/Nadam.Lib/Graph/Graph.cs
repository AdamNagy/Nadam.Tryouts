using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Lib.Graph
{
    public abstract class Graph<T, TU> : IEnumerable<GraphNode<TU>>
                                        where T: GraphNode<TU>
                                        where TU: IEquatable<TU>
    {
        protected IList<GraphNode<TU>> NodeSet { get; set; }
        public int Count => NodeSet.Count;

        #region ctors
        protected Graph() : this(null) { }

        protected Graph(IList<GraphNode<TU>> nodeSet)
        {
            NodeSet = nodeSet ?? new List<GraphNode<TU>>();
        }
        #endregion

        #region Add node
        protected void AddNode(GraphNode<TU> node)
        {
            var nextId = Count+1;
            node.NodeId = nextId;
            NodeSet.Add(node);
        }

        protected void AddNode(TU value)
        {
            var nextId = Count+1;
            NodeSet.Add(new GraphNode<TU>(value, nextId));
        }
        #endregion

        #region Add edges
        protected void AddDirectedEdge(GraphNode<TU> from, GraphNode<TU> to)
        {
            from.Neighbors.Add(to);
        }

        protected void AddUndirectedEdge(GraphNode<TU> from, GraphNode<TU> to)
        {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
        }
        #endregion

        #region Contains and find by..
        protected bool Contains(TU value)
        {
            return NodeSet.SingleOrDefault(p => p.Value.Equals(value)) != null;
        }

        protected bool Contains(GraphNode<TU> node)
        {
            return NodeSet.SingleOrDefault(p => p.NodeId.Equals(node.NodeId)) != null;
        }

        protected GraphNode<TU> FindByValue(TU reference)
        {
            return NodeSet.SingleOrDefault(p => p.Value.Equals(reference));
        }

        protected virtual GraphNode<TU> FindByValue(GraphNode<TU> reference)
        {
            return NodeSet.SingleOrDefault(p => p.Equals(reference));
        }

        protected GraphNode<TU> FindByNodeId(int id)
        {
            return NodeSet.SingleOrDefault(p => p.NodeId.Equals(id));
        }
        #endregion

        #region Remove node
        public bool Remove(TU value)
        {
            // first remove the node from the nodeset
            GraphNode<TU> nodeToRemove = FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            NodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (GraphNode<TU> gnode in NodeSet)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                }
            }

            return true;
        }

        protected void RemoveDirectedEdge(GraphNode<TU> from, GraphNode<TU> to)
        {
            from.Neighbors.Remove(FindByValue(to.Value));
        }
        #endregion

        public IEnumerator<GraphNode<TU>> GetEnumerator()
        {
            return NodeSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
