using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class StackedLinkedList<T> : System.Collections.ICollection
    {
        #region ICollection Members

        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if (array.Rank > 1)
            {
                throw new RankException("Cannot handle multidimensional arrays");
            }

            if (array.Length - index < m_data.Count)
            {
                throw new ArgumentException("array", "The specified array is not large enough to hold the data.");
            }

            DoubleLinkedListNode<T> current = m_data.Tail;
            for (int i = 0; i < m_data.Count; ++i)
            {
                array.SetValue(current.Data, i + index);
                current = current.Previous;
            }
        }

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }

        object m_syncRoot;
        object System.Collections.ICollection.SyncRoot
        {
            get
            {
                if (m_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref m_syncRoot, new object(), null);
                }
                return m_syncRoot;
            }
        }

        #endregion
    }
}
