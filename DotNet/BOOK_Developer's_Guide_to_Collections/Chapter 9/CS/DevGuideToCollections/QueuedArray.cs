using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{
    /// <summary>
    /// Represents a strongly typed queue that uses a array for it's internal data storage.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(ArrayDebugView))]
    public partial class QueuedArray<T>
    {
        const int GROW_BY = 10;

        T[] m_data;
        int m_count;
        int m_head;
        int m_tail;

        // Value that is updated everytime the list is updated.
        int m_updateCode;

        /// <summary>
        /// Initializes a new instance of the QueuedArray(T) class.
        /// </summary>
        public QueuedArray()
        {
            Initialize(GROW_BY);
        }

        /// <summary>
        /// Initializes a new instance of the QueuedArray(T) class.
        /// </summary>
        public QueuedArray(IEnumerable<T> items)
        {
            Initialize(GROW_BY);
            foreach (T item in items)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Initializes a new instance of the QueuedArray(T) class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new array can initially store.</param>
        public QueuedArray(int capacity)
        {
            Initialize(capacity);
        }

        void Initialize(int capacity)
        {
            m_data = new T[capacity];
        }

        /// <summary>
        /// States if the QueuedArray(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_count == 0; }
        }

        /// <summary>
        /// Gets the number of elements actually contained in the QueuedArray(T).
        /// </summary>
        public int Count
        {
            get { return m_count; }
        }

        /// <summary>
        /// Removes the first item from the QueuedArray(T).
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot pop from an empty queue.");
            }

            T retval = m_data[m_head];

            m_head = (m_head + 1) % m_data.Length;

            --m_count;
            ++m_updateCode;

            return retval;
        }

        /// <summary>
        /// Adds the item to the end of the QueuedArray(T).
        /// </summary>
        /// <param name="item">The item to add to the end of the QueuedArray(T).</param>
        public void Push(T item)
        {
            if (m_count >= m_data.Length)
            {
                Capacity += GROW_BY;
            }

            m_data[m_tail] = item;
            m_tail = (m_tail + 1) % m_data.Length;
            ++m_count;
            ++m_updateCode;
        }

        /// <summary>
        /// Checks if the specified data is present in the QueuedLinkedList(T).
        /// </summary>
        /// <param name="data">The data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < m_count; i++)
            {
                if (comparer.Equals(m_data[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the first item in the QueuedArray(T) without removing it.
        /// </summary>
        /// <returns>The item at the front of the QueuedArray(T)</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot peek at an empty queue.");
            }

            return m_data[m_head];
        }

        /// <summary>
        /// Removes all items from the QueuedArray(T).
        /// </summary>
        public void Clear()
        {
            m_count = 0;
            m_head = 0;
            m_tail = 0;
            ++m_updateCode;
        }

        /// <summary>
        /// Gets or sets the capacity of the QueuedArray(T).
        /// </summary>
        public int Capacity
        {
            get { return m_data.Length; }
            set
            {
                if (value < Count)
                {
                    throw new NotSupportedException("The capacity has to be larger than the current count.");
                }

                T[] tmp = new T[value];

                // We can just create a new buffer if it is empty.
                if (IsEmpty)
                {
                    m_data = tmp;
                    return;
                }

                // We can simply copy the data if the end hasn't wrapped around yet.
                if (m_head < m_tail)
                {
                    // We will need to copy the data from the old array to the new one
                    // All data will be copied to the beginning of the new array
                    Array.Copy(m_data, m_head, tmp, 0, m_count);

                    m_data = tmp;
                    m_head = 0;
                    m_tail = m_count;
                    return;
                }

                // First we will copy all data from the start to the physical end of the buffer
                Array.Copy(m_data, m_head, tmp, 0, m_data.Length - m_head);

                // Next we will copy the items from the physical start of the buffer to the end
                Array.Copy(m_data, 0, tmp, m_data.Length - m_head, m_tail);

                m_data = tmp;
                m_head = 0;
                m_tail = m_count;
            }
        }


        /// <summary>
        /// Copies the elements of the QueuedArray(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the QueuedArray(T).</returns>
        public T[] ToArray()
        {
            T[] retval = new T[m_count];

            if (IsEmpty)
            {
                return retval;
            }

            if (m_head < m_tail)
            {
                Array.Copy(m_data, m_head, retval, 0, m_count);
            }
            else
            {
                Array.Copy(m_data, m_head, retval, 0, m_data.Length - m_head);
                Array.Copy(m_data, 0, retval, m_data.Length - m_head, m_tail);
            }

            return retval;
        }
    }
}
