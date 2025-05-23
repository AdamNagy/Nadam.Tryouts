﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class CircularBuffer<T> : IEnumerable<T>
    {

        #region IEnumerable<T> Members

        /// <summary>
        /// Gets the enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator for this collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Gets the enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator for this collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        /// <summary>
        /// Enumerates the elements of CircularBuffer(T).
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            CircularBuffer<T> m_buffer;
            int m_index;
            int m_start;
            int m_updateCode;

            internal Enumerator(CircularBuffer<T> buffer)
            {
                m_buffer = buffer;
                m_index = -1;
                m_start = m_buffer.m_start;
                m_updateCode = m_buffer.m_updateCode;
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// Gets the element at the current position of the enumerator. 
            /// </summary>
            public T Current
            {
                get { return m_buffer.m_data[(m_index + m_start) % m_buffer.m_capacity]; }
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Called when the resources should be released.
            /// </summary>
            public void Dispose()
            {
            }

            #endregion

            #region IEnumerator Members

            /// <summary>
            /// Gets the element at the current position of the enumerator. 
            /// </summary>
            object System.Collections.IEnumerator.Current
            {
                get { return m_buffer.m_data[(m_index + m_start) % m_buffer.m_capacity]; }
            }

            /// <summary>
            /// Advances the enumerator to the next element.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                if (m_updateCode != m_buffer.m_updateCode)
                {
                    throw new InvalidOperationException("The array was updated while traversing");
                } 
                
                ++m_index;

                if (m_index >= m_buffer.Count)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                m_index = -1;
                m_start = m_buffer.m_start;
                m_updateCode = m_buffer.m_updateCode;
            }

            #endregion
        }
    }
}
