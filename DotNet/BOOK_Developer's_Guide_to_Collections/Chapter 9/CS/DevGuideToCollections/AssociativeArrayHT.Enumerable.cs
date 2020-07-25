using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class AssociativeArrayHT<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        #region IEnumerable<T> Members

        /// <summary>
        /// Gets the enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator for this collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
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
        /// Enumerates the elements of HashtableEx(TKey, TValue).
        /// </summary>
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            AssociativeArrayHT<TKey, TValue> m_aa;
            int m_entryIndex;
            int m_bucketIndex;
            int m_updateCode;
            KeyValuePair<TKey, TValue> m_current;

            internal Enumerator(AssociativeArrayHT<TKey, TValue> aa)
            {
                m_current = default(KeyValuePair<TKey, TValue>);
                m_aa = aa;
                m_updateCode = m_aa.m_updateCode;
                m_bucketIndex = NULL_REFERENCE;
                m_entryIndex = NULL_REFERENCE;
            }

            #region IEnumerator<KeyValuePair<TKey, TValue>> Members

            /// <summary>
            /// Gets the element at the current position of the enumerator. 
            /// </summary>
            public KeyValuePair<TKey, TValue> Current
            {
                get { return m_current; }
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
                get { return m_current; }
            }

            /// <summary>
            /// Advances the enumerator to the next element.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                if (m_updateCode != m_aa.m_updateCode)
                {
                    throw new InvalidOperationException("The hash table was updated while traversing");
                }

                if (m_aa.MoveNext(ref m_bucketIndex, ref m_entryIndex))
                {
                    m_current = new KeyValuePair<TKey, TValue>(m_aa.m_entries[m_entryIndex].Key, m_aa.m_entries[m_entryIndex].Value);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                m_bucketIndex = NULL_REFERENCE;
                m_updateCode = m_aa.m_updateCode;
                m_entryIndex = NULL_REFERENCE;
                m_current = new KeyValuePair<TKey, TValue>();
            }

            #endregion
        }
    }
}
