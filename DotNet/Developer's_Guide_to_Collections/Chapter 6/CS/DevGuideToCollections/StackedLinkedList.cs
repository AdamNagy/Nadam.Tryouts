using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace DevGuideToCollections
{
    /// <summary>
    /// Represents a strongly typed stack that uses an array for its internal data storage.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(ArrayDebugView))]
    public partial class StackedLinkedList<T>
    {
        DoubleLinkedList<T> m_data;

        /// <summary>
        /// Initializes a new instance of the StackedLinkedList(T) class that is empty.
        /// </summary>
        public StackedLinkedList()
        {
            m_data = new DoubleLinkedList<T>();
        }

        /// <summary>
        /// Initializes a new instance of the StackedLinkedList(T) class with the specified items.
        /// </summary>
        /// <param name="items">The items to add to the stack.</param>
        public StackedLinkedList(IEnumerable<T> items)
        {
            m_data = new DoubleLinkedList<T>(items);
        }

        /// <summary>
        /// States if the StackedLinkedList(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_data.IsEmpty; }
        }

        /// <summary>
        /// Gets the number of elements actually contained in the StackedLinkedList(T).
        /// </summary>
        public int Count
        {
            get { return m_data.Count; }
        }

        /// <summary>
        /// Checks if the specified data is present in the StackedLinkedList(T).
        /// </summary>
        /// <param name="data">The data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T item)
        {
            return m_data.Contains(item);
        }

        /// <summary>
        /// Removes all items from the StackedLinkedList(T).
        /// </summary>
        public void Clear()
        {
            m_data.Clear();
        }

        /// <summary>
        /// Removes the an item from the top of the StackedLinkedList(T).
        /// </summary>
        /// <returns>The iem at the top of the StackedLinkedList(T).</returns>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot pop from an empty stack.");
            }

            T retval = m_data.Tail.Data;

            m_data.Remove(m_data.Tail);

            return retval;
        }

        /// <summary>
        /// Gets the item at the top of the StackedLinkedList(T) without removing it.
        /// </summary>
        /// <returns>The item at the top of the StackedLinkedList(T)</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot get the top item of an empty stack.");
            }

            return m_data.Tail.Data;
        }

        /// <summary>
        /// Adds the item to the top of the StackedLinkedList(T).
        /// </summary>
        /// <param name="item">The item to add to the top of the StackedLinkedList(T).</param>
        public void Push(T item)
        {
            m_data.AddToEnd(item);
        }

        /// <summary>
        /// Copies the elements of the StackedLinkedList(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the StackedLinkedList<T>. The data in the array will be in LIFO order.</returns>
        public T[] ToArray()
        {
            T[] tmp = new T[Count];

            int i = 0;
            for (DoubleLinkedListNode<T> curr = m_data.Tail; curr != null; curr = curr.Previous)
            {
                tmp[i++] = curr.Data;
            }

            return tmp;
        }
    }
}
