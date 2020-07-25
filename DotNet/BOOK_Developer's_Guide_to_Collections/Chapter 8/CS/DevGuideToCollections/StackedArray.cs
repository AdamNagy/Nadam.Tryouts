using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{

    /// <summary>
    /// Represents a strongly typed stack that uses an array for its internal data storage.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(ArrayDebugView))]
    public partial class StackedArray<T>
    {
        ArrayEx<T> m_data;

        /// <summary>
        /// Initializes a new instance of the StackedArray(T) class that is empty.
        /// </summary>
        public StackedArray()
        {
            m_data = new ArrayEx<T>();
        }

        /// <summary>
        /// Initializes a new instance of the StackedArray(T) class with the specified items.
        /// </summary>
        /// <param name="items">The items to add to the stack.</param>
        public StackedArray(IEnumerable<T> items)
        {
            m_data = new ArrayEx<T>(items);
        }

        /// <summary>
        /// States if the StackedArray(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_data.IsEmpty; }
        }

        /// <summary>
        /// Gets the number of elements in the StackedArray(T).
        /// </summary>
        public int Count
        {
            get { return m_data.Count; }
        }

        /// <summary>
        /// Removes all items from the StackedArray(T).
        /// </summary>
        public void Clear()
        {
            m_data.Clear();
        }

        /// <summary>
        /// Checks if the specified data is present in the StackedArray(T).
        /// </summary>
        /// <param name="data">The data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T item)
        {
            return m_data.Contains(item);
        }

        /// <summary>
        /// Removes the an item from the top of the StackedArray(T).
        /// </summary>
        /// <returns>The item at the top of the StackedArray(T).</returns>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot pop from an empty stack.");
            }

            T retval = m_data[m_data.Count - 1];

            m_data.RemoveAt(m_data.Count - 1);

            return retval;
        }

        /// <summary>
        /// Adds the item to the top of the StackedArray(T).
        /// </summary>
        /// <param name="item">The item to add to the top of the StackedArray(T).</param>
        public void Push(T item)
        {
            m_data.Add(item);
        }

        /// <summary>
        /// Gets the item at the top of the StackedArray(T) without removing it.
        /// </summary>
        /// <returns>The item at the top of the StackedArray(T)</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot peek at an empty stack.");
            }

            return m_data[m_data.Count - 1];
        }

        /// <summary>
        /// Copies the elements of the StackedArray(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the StackedArray(T). The data in the array will be in LIFO order.</returns>
        public T[] ToArray()
        {
            T[] tmp = new T[Count];

            for (int i = 0; i < Count; ++i)
            {
                tmp[i] = m_data[Count - i - 1];
            }

            return tmp;
        }
    }
}
