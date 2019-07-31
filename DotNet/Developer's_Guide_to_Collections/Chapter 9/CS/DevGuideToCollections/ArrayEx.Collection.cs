using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class ArrayEx<T> : ICollection<T>, System.Collections.ICollection
    {
        #region ICollection<T> Members

        /// <summary>
        /// Copies the elements of the array to the specified array.
        /// </summary>
        /// <param name="array">The array to copy the data to.</param>
        /// <param name="arrayIndex">The index in array where copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(m_data, 0, array, arrayIndex, m_count); 
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region ICollection Members

        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            Array.Copy(m_data, 0, array, index, m_count);
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
