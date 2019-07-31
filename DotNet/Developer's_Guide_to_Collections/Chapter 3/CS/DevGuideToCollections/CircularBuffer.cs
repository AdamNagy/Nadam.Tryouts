using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{
    /// <summary>
    /// Enumeration of what should happen if the circular buffer is full.
    /// </summary>
    public enum FullOperations
    {
        /// <summary>
        /// The value being pushed should be ignored.
        /// </summary>
        Ignore,
        /// <summary>
        /// The first value should be popped off.
        /// </summary>
        Pop,
        /// <summary>
        /// An exception should be thrown.
        /// </summary>
        Error
    }

    /// <summary>
    /// Represents a strongly typed circular buffer.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the circular buffer.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(ArrayDebugView))]
    public class CircularBuffer<T>
    {
        // The beginning of valid data
        int m_start;
        // The end of valid data
        int m_end;
        // How many items are in the buffer
        int m_count;
        // The internal array for holding the data
        T[] m_data;
        // The maximum capacity of the buffer.
        int m_capacity;
        // Enumeration of what to do when the buffer is full
        FullOperations m_fullOperation;
        int m_updateCode;

        /// <summary>
        /// Initializes a new instance of the CircularBuffer(T) class with the specified size as the capacity.
        /// </summary>
        /// <param name="size">The capacity of the buffer</param>
        public CircularBuffer(int size)
        {
            Initialize(size);
        }

        /// <summary>
        /// Initializes a new instance of the CircularBuffer(T) class with the specified items and capacity.
        /// </summary>
        /// <param name="size">The capacity of the buffer</param>
        /// <param name="items">The collection of items you want to add to the buffer.</param>
        public CircularBuffer(int size, IEnumerable<T> items)
        {
            Initialize(size);

            foreach (T item in items)
            {
                if (m_count >= size)
                {
                    throw new ArgumentOutOfRangeException("size", "The number of items in the collection is larger than the size");
                }
                m_data[m_end++] = item;
                ++m_count;
            }
        }

        void Initialize(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("size");
            }
            m_data = new T[size];
            m_capacity = size;
            FullOperation = FullOperations.Ignore;
        }


        /// <summary>
        /// States if the CircularBuffer(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_count <= 0; }
        }

        /// <summary>
        /// States if the CircularBuffer(T) is full.
        /// </summary>
        public bool IsFull
        {
            get { return m_count == m_capacity; }
        }

        /// <summary>
        /// Gets or sets what to do when a user pushes to a full buffer.
        /// </summary>
        public FullOperations FullOperation
        {
            get { return m_fullOperation; }
            set { m_fullOperation = value; }
        }


        /// <summary>
        /// Gets the number of elements actually contained in the CircularBuffer(T).
        /// </summary>
        public int Count
        {
            get { return m_count; }
        }

        /// <summary>
        /// Gets or sets the capacity of the CircularBuffer(T).
        /// </summary>
        public int Capacity
        {
            get { return m_capacity; }
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
                    m_capacity = value;
                    m_data = tmp;
                    return;
                }

                // We can simply copy the data if the end hasn't wrapped around yet.
                if (m_start < m_end)
                {
                    // We will need to copy the data from the old array to the new one
                    // All data will be copied to the beginning of the new array
                    Array.Copy(m_data, m_start, tmp, 0, m_count);

                    m_data = tmp;
                    m_capacity = value;
                    m_start = 0;
                    m_end = m_count;
                    return;
                }

                // First we will copy all data from the start to the physical end of the buffer
                Array.Copy(m_data, m_start, tmp, 0, m_data.Length - m_start);

                // Next we will copy the items from the physical start of the buffer to the end
                Array.Copy(m_data, 0, tmp, m_data.Length - m_start, m_end);


                m_data = tmp;
                m_start = 0;
                m_end = m_count;
                m_capacity = value;
            }
        }


        /// <summary>
        /// Adds the item to the end of the CircularBuffer(T).
        /// </summary>
        /// <param name="item">The item to add to the end of the buffer.</param>
        public void Push(T item)
        {
            if (IsFull)
            {
                switch (m_fullOperation)
                {
                    case FullOperations.Ignore:
                        // Do not do anything
                        return;
                    case FullOperations.Pop:
                        Pop();
                        break;
                    case FullOperations.Error:
                    default:
                        throw new InvalidOperationException("You cannot add to a full buffer");
                }
            }

            m_data[m_end] = item;
            ++m_count;
            ++m_updateCode;
            m_end = (m_end + 1) % m_capacity;
        }

        /// <summary>
        /// Removes the first item from the CircularBuffer(T).
        /// </summary>
        /// <returns>The first item in the CircularBuffer(T).</returns>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot remove an item from an empty collection.");
            }

            T value = m_data[m_start];
            m_start = (m_start + 1) % m_capacity;
            --m_count;
            ++m_updateCode;
            return value;
        }

        /// <summary>
        /// Gets the item the first item in the CircularBuffer(T) without removing it.
        /// </summary>
        /// <returns>The first item in the CircularBuffer(T).</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("You cannot viem an item in an empty collection.");
            }

            return m_data[m_start];
        }

        /// <summary>
        /// Copies the elements of the CircularBuffer(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the CircularBuffer(T).</returns>
        public T[] ToArray()
        {
            T[] retval = new T[m_count];

            if (IsEmpty)
            {
                return retval;
            }

            if (m_start < m_end)
            {
                Array.Copy(m_data, m_start, retval, 0, m_count);
            }
            else
            {
                Array.Copy(m_data, m_start, retval, 0, m_capacity - m_start);
                Array.Copy(m_data, 0, retval, m_capacity - m_start, m_end);
            }

            return retval;
        }

        /// <summary>
        /// Checks if the specified data is present in the CircularBuffer(T).
        /// </summary>
        /// <param name="data">The data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            int i = m_start;
            int index = 0;
            while (index < m_count)
            {
                if (comparer.Equals(m_data[i], item))
                {
                    return true;
                }
                i = (i + 1) % m_capacity;
                ++index;
            }

            return false;
        }

        /// <summary>
        /// Removes all items from the CircularBuffer(T).
        /// </summary>
        public void Clear()
        {
            m_count = 0;
            m_start = 0;
            m_end = 0;
            ++m_updateCode;
        }
    }
}
