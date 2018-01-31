using System;
using System.Collections.Generic;

namespace Nadam.Global.Lib.Graph
{
    public class Node<T> //: IEquatable<T>
    {
        public T Value { get; set; }

        public Node() { }

        public Node(T value)
        {
            Value = value;
        }

        //public bool Equals(T other)
        //{
        //    return Value.Equals(other);
        //}
    }
}

