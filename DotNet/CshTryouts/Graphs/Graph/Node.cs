using System;

namespace Graphs.Graph
{
    public struct Node<T> : IEquatable<Node<T>>
    {
	    public int NodeId { get; set; }
		public T Value { get; set; }

        public Node(T value)
        {
            Value = value;
        }

	    public Node(T value, int id)
	    {
            Value = value;
            NodeId = id;
	    }

	    public bool Equals(Node<T> other)
	    {
		    return NodeId == other.NodeId &&
		           Value.Equals(other.Value);
	    }

        // Omitting getHashCode violates rule: OverrideGetHashCodeOnOverridingEquals.
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

