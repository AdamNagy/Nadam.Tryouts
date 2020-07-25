using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DevGuideToCollections
{
    public class SynchronizedListWrapper<T> : IList<T>, ICollection
    {
        object m_syncRoot;
        IList<T> m_items;

        public SynchronizedListWrapper(IList<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            m_items = items;
            m_syncRoot = new object();
        }

        public SynchronizedListWrapper(IList<T> items, object syncRoot)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (syncRoot == null)
            {
                throw new ArgumentNullException("syncRoot");
            }
            m_items = items;
            m_syncRoot = syncRoot;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            lock (m_syncRoot)
            {
                return m_items.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (m_syncRoot)
            {
                m_items.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (m_syncRoot)
            {
                m_items.RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get
            {
                lock (m_syncRoot)
                {
                    return m_items[index];
                }
            }
            set
            {
                lock (m_syncRoot)
                {
                    m_items[index] = value;
                }
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            lock (m_syncRoot)
            {
                m_items.Add(item);
            }
        }

        public void Clear()
        {
            lock (m_syncRoot)
            {
                m_items.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (m_syncRoot)
            {
                return m_items.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (m_syncRoot)
            {
                m_items.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get 
            {
                lock (m_syncRoot)
                {
                    return m_items.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get 
            {
                return m_items.IsReadOnly;
            }
        }

        public bool Remove(T item)
        {
            lock (m_syncRoot)
            {
                return m_items.Remove(item);
            }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            lock (m_syncRoot)
            {
                Array.Copy(m_items.ToArray(), 0, array, index, Count);
            }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public object SyncRoot
        {
            get { return m_syncRoot; }
        }

        #endregion
    }
}
