using System;

namespace Nadam.Global.Lib.Graph
{
    public class Node<T> : IEquatable<Node<T>>
    {
	    public int NodeId { get; set; }
		public T Value { get; set; }

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

