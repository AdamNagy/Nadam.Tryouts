using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class QueuedLinkedList<T> : System.Collections.ICollection
    {
        #region ICollection Members

        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            ((System.Collections.ICollection)m_data).CopyTo(array, index);
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
