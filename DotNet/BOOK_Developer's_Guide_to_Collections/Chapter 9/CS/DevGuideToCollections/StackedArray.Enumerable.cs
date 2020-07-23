using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class StackedArray<T> : IEnumerable<T>
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
            StackedArray<T> m_array;
            int m_index;
            int m_updateCode;

            internal Enumerator(StackedArray<T> array)
            {
                m_array = array;
                m_index = m_array.Count;
                m_updateCode = m_array.m_data.UpdateCode;
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// Gets the element at the current position of the enumerator. 
            /// </summary>
            public T Current
            {
                get { return m_array.m_data[m_index]; }
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
                get { return m_array.m_data[m_index]; }
            }

            /// <summary>
            /// Advances the enumerator to the next element.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                if (m_updateCode != m_array.m_data.UpdateCode)
                {
                    throw new InvalidOperationException("The stack was updated while traversing");
                } 
                
                --m_index;

                if (m_index <= 0)
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
                m_index = m_array.Count;
                m_updateCode = m_array.m_data.UpdateCode;
            }

            #endregion
        }
    }
}
