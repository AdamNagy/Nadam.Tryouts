using System;
using System.Collections.Generic;

namespace Nadam.Global.Lib.Graph
{
    public class Node<T> : IEquatable<Node<T>>
	{
	    public int NodeId { get; set; }
		public T Value { get; set; }

        protected Node(T value)
        {
            Value = value;
		}

	    public Node(T value, int id) : this(value)
	    {
		    NodeId = id;
	    }

	    public bool Equals(Node<T> other)
	    {
		    return NodeId == other.NodeId &&
		           Value.Equals(other.Value);
	    }
	}
}

