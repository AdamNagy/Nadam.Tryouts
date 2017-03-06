using System.Collections.Generic;

namespace Nadam.Lib.Graph
{
    public class Node<T>
    {
        public T Value { get; set; }

        public Node() { }

        public Node(T value)
        {
            Value = value;
        }
    }
}

