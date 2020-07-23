using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{
    /// <summary>
    /// Represents a node in a DoubleLinkedList(T).
    /// </summary>
    /// <typeparam name="T">Specifies the type of data in the node.</typeparam>
    [DebuggerDisplay("Data={Data}")]
    public class DoubleLinkedListNode<T>
    {
        DoubleLinkedList<T> m_owner;
        DoubleLinkedListNode<T> m_prev;
        DoubleLinkedListNode<T> m_next;
        T m_data;

        /// <summary>
        /// Initializes a new instance of the DoubleLinkedListNode(T) class with the specified data.
        /// </summary>
        /// <param name="data">The data that this node will contain.</param>
        public DoubleLinkedListNode(T data)
        {
            m_data = data;
        }

        /// <summary>
        /// Initializes a new instance of the DoubleLinkedListNode(T) class with the specified data and owner.
        /// </summary>
        /// <param name="data">The data that this node will contain.</param>
        internal DoubleLinkedListNode(DoubleLinkedList<T> owner, T data)
        {
            m_data = data;
            m_owner = owner;
        }

        /// <summary>
        /// Gets the next node.
        /// </summary>
        public DoubleLinkedListNode<T> Next
        {
            get { return m_next; }
            internal set { m_next = value; }
        }

        /// <summary>
        /// Gets or sets the owner of the node.
        /// </summary>
        internal DoubleLinkedList<T> Owner
        {
            get { return m_owner; }
            set { m_owner = value; }
        }

        /// <summary>
        /// Gets the previous node.
        /// </summary>
        public DoubleLinkedListNode<T> Previous
        {
            get { return m_prev; }
            internal set { m_prev = value; }
        }

        /// <summary>
        /// Gets the data contained in the node.
        /// </summary>
        public T Data
        {
            get { return m_data; }
            internal set { m_data = value; }
        }
    }
}
