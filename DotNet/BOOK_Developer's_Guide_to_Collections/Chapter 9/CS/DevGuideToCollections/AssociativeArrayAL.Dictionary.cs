using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class AssociativeArrayAL<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public ICollection<TKey> Keys
        {
            get { return new KeyCollection(this); }
        }

        public ICollection<TValue> Values
        {
            get { return new ValueCollection(this); }
        }


        public class KeyCollection : ICollection<TKey>
        {
            AssociativeArrayAL<TKey, TValue> m_aa;

            internal KeyCollection(AssociativeArrayAL<TKey, TValue> aa)
            {
                m_aa = aa;
            }

            #region ICollection<TKey> Members

            void ICollection<TKey>.Add(TKey item)
            {
                throw new NotSupportedException();
            }

            void ICollection<TKey>.Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TKey item)
            {
                return m_aa.ContainsKey(item);
            }

            public void CopyTo(TKey[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get { return m_aa.Count; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            bool ICollection<TKey>.Remove(TKey item)
            {
                throw new NotSupportedException();
            }

            #endregion

            #region IEnumerable<TKey> Members

            public IEnumerator<TKey> GetEnumerator()
            {
                return new Enumerator(m_aa);
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion

            /// <summary>
            /// Enumerates the keys of AssociativeArrayAL(TKey, TValue).
            /// </summary>
            public struct Enumerator : IEnumerator<TKey>
            {
                AssociativeArrayAL<TKey, TValue> m_aa;
                DoubleLinkedListNode<KVPair> m_currentNode;
                int m_updateCode;
                bool m_end;
                TKey m_current;

                internal Enumerator(AssociativeArrayAL<TKey, TValue> aa)
                {
                    m_aa = aa;

                    m_current = default(TKey);
                    m_updateCode = m_aa.m_updateCode;
                    m_currentNode = null;
                    m_end = false;
                }

                #region IEnumerator<TKey> Members

                /// <summary>
                /// Gets the key at the current position of the enumerator. 
                /// </summary>
                public TKey Current
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
                /// Gets the key at the current position of the enumerator. 
                /// </summary>
                object System.Collections.IEnumerator.Current
                {
                    get { return m_current; }
                }

                /// <summary>
                /// Advances the enumerator to the next key.
                /// </summary>
                /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext()
                {
                    if (m_updateCode != m_aa.m_updateCode)
                    {
                        throw new InvalidOperationException("The hash table was updated while traversing");
                    }

                    if (m_end)
                    {
                        return false;
                    }

                    if (m_currentNode == null)
                    {
                        if (m_aa.IsEmpty)
                        {
                            m_end = true;
                            return false;
                        }

                        m_currentNode = m_aa.m_list.Head;

                        m_current = m_currentNode.Data.Key;

                        return true;
                    }

                    m_currentNode = m_currentNode.Next;

                    if (m_currentNode == null)
                    {
                        m_end = true;
                        return false;
                    }

                    m_current = m_currentNode.Data.Key;

                    return true;
                }

                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element in the collection.
                /// </summary>
                public void Reset()
                {
                    m_currentNode = null;
                    m_end = false;
                    m_updateCode = m_aa.m_updateCode;
                    m_current = default(TKey);
                }

                #endregion
            }
        }

        public class ValueCollection : ICollection<TValue>
        {
            AssociativeArrayAL<TKey, TValue> m_aa;

            internal ValueCollection(AssociativeArrayAL<TKey, TValue> aa)
            {
                m_aa = aa;
            }

            #region ICollection<TKey> Members

            void ICollection<TValue>.Add(TValue item)
            {
                throw new NotSupportedException();
            }

            void ICollection<TValue>.Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TValue item)
            {
                return m_aa.ContainsValue(item);
            }

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get { return m_aa.Count; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            bool ICollection<TValue>.Remove(TValue item)
            {
                throw new NotSupportedException();
            }

            #endregion

            #region IEnumerable<TValue> Members

            public IEnumerator<TValue> GetEnumerator()
            {
                return new Enumerator(m_aa);
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion

            /// <summary>
            /// Enumerates the values of AssociativeArrayAL(TKey, TValue).
            /// </summary>
            public struct Enumerator : IEnumerator<TValue>
            {
                AssociativeArrayAL<TKey, TValue> m_aa;
                DoubleLinkedListNode<KVPair> m_currentNode;
                int m_updateCode;
                bool m_end;
                TValue m_current;

                internal Enumerator(AssociativeArrayAL<TKey, TValue> aa)
                {
                    m_aa = aa;

                    m_current = default(TValue);
                    m_updateCode = m_aa.m_updateCode;
                    m_currentNode = null;
                    m_end = false;
                }

                #region IEnumerator<TValue> Members

                /// <summary>
                /// Gets the value at the current position of the enumerator. 
                /// </summary>
                public TValue Current
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
                /// Gets the value at the current position of the enumerator. 
                /// </summary>
                object System.Collections.IEnumerator.Current
                {
                    get { return m_current; }
                }

                /// <summary>
                /// Advances the enumerator to the next value.
                /// </summary>
                /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext()
                {
                    if (m_updateCode != m_aa.m_updateCode)
                    {
                        throw new InvalidOperationException("The hash table was updated while traversing");
                    }

                    if (m_end)
                    {
                        return false;
                    }

                    if (m_currentNode == null)
                    {
                        if (m_aa.IsEmpty)
                        {
                            m_end = true;
                            return false;
                        }

                        m_currentNode = m_aa.m_list.Head;

                        m_current = m_currentNode.Data.Value;

                        return true;
                    }

                    m_currentNode = m_currentNode.Next;

                    if (m_currentNode == null)
                    {
                        m_end = true;
                        return false;
                    }

                    m_current = m_currentNode.Data.Value;

                    return true;
                }

                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element in the collection.
                /// </summary>
                public void Reset()
                {
                    m_currentNode = null;
                    m_end = false;
                    m_updateCode = m_aa.m_updateCode;
                    m_current = default(TValue);

                }

                #endregion
            }

        }
    }
}
