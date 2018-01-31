using System;
using System.Collections.Generic;

namespace Nadam.Global.Lib.Graph
{
    public class GraphNode<T> : Node<T>, IEquatable<GraphNode<T>>
    {
        public int NodeId { get; set; }
        public IList<GraphNode<T>> Neighbors { get; set; }

        public GraphNode(T value) : base(value)
        {
            Neighbors = new List<GraphNode<T>>();
        }

        public GraphNode(T value, int id) : base(value)
        {
            NodeId = id;
            Neighbors = new List<GraphNode<T>>();
        }

        public bool Equals(GraphNode<T> other)
        {
            return NodeId == other.NodeId &&
                   Value.Equals(other.Value);
        }
    }
}
