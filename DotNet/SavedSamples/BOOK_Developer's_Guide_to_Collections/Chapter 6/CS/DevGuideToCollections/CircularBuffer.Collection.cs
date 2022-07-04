using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class CircularBuffer<T> : System.Collections.ICollection
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

            if (array.Length - index < m_count)
            {
                throw new ArgumentException("array", "The specified array is not large enough to hold the data.");
            }

            for (int i = 0; i < m_count; ++i)
            {
                array.SetValue(m_data[(m_start + i) % m_capacity], i + index);
            }
        }

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }

        object System.Collections.ICollection.SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
