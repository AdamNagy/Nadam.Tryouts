using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class DoubleLinkedList<T> : IEnumerable<T>
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
        /// Enumerates the elements of ArrayEx&lt;T&gt.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            DoubleLinkedList<T> m_list;
            DoubleLinkedListNode<T> m_current;
            bool m_end;
            int m_updateCode;

            internal Enumerator(DoubleLinkedList<T> list)
            {
                m_list = list;
                m_current = null;
                m_end = false;
                m_updateCode = list.m_updateCode;
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// Gets the element at the current position of the enumerator. 
            /// </summary>
            public T Current
            {
                get { return m_current.Data; }
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
                get { return m_current.Data; }
            }

            /// <summary>
            /// Advances the enumerator to the next element.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                if (m_updateCode != m_list.m_updateCode)
                {
                    throw new InvalidOperationException("The list was updated while traversing");
                } 
                
                if (m_end || m_list.IsEmpty)
                {
                    return false;
                }

                if (m_current == null)
                {
                    m_current = m_list.Head;
                }
                else if (m_current == m_list.Tail)
                {
                    m_end = true;
                    m_current = null;
                    return false;

                }
                else
                {
                    m_current = m_current.Next;
                }

                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                m_current = null;
                m_end = false;
                m_updateCode = m_list.m_updateCode;
            }

            #endregion
        }
    }
}
