using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{
    /// <summary>
    /// Represents a strongly typed queue that uses a linked list for it's internal data storage.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(ArrayDebugView))]
    public partial class QueuedLinkedList<T>
    {
        DoubleLinkedList<T> m_data;

        /// <summary>
        /// Initializes a new instance of the QueuedLinkedList(T) class.
        /// </summary>
        public QueuedLinkedList()
        {
            m_data = new DoubleLinkedList<T>();
        }

        /// <summary>
        /// Initializes a new instance of the QueuedLinkedList(T) class.
        /// </summary>
        public QueuedLinkedList(IEnumerable<T> items)
        {
            m_data = new DoubleLinkedList<T>(items);
        }

        /// <summary>
        /// States if the QueuedLinkedList(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_data.IsEmpty; }
        }

        /// <summary>
        /// Gets the number of elements actually contained in the QueuedLinkedList(T).
        /// </summary>
        public int Count
        {
            get { return m_data.Count; }
        }

        /// <summary>
        /// Removes the first item from the QueuedLinkedList(T).
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot pop from an empty queue.");
            }

            T retval = m_data.Head.Data;

            m_data.Remove(m_data.Head);

            return retval;
        }

        /// <summary>
        /// Adds the item to the end of the QueuedLinkedList(T).
        /// </summary>
        /// <param name="item">The item to add to the end of the QueuedLinkedList(T).</param>
        public void Push(T item)
        {
            m_data.AddToEnd(item);
        }

        /// <summary>
        /// Checks if the specified data is present in the QueuedLinkedList(T).
        /// </summary>
        /// <param name="data">The data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T item)
        {
            return m_data.Contains(item);
        }

        /// <summary>
        /// Gets the first item in the QueuedLinkedList(T) without removing it.
        /// </summary>
        /// <returns>The item at the front of the QueuedLinkedList(T)</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot peek at an empty queue.");
            }

            return m_data.Head.Data;
        }

        /// <summary>
        /// Removes all items from the QueuedLinkedList(T).
        /// </summary>
        public void Clear()
        {
            m_data.Clear();
        }

        /// <summary>
        /// Copies the elements of the QueuedLinkedList(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the QueuedLinkedList(T).</returns>
        public T[] ToArray()
        {
            return m_data.ToArray();
        }
    }
}
