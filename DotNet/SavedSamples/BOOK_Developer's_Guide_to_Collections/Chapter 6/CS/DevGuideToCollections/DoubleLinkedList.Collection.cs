﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class DoubleLinkedList<T> : ICollection<T>, System.Collections.ICollection
    {
        #region ICollection<T> Members

        /// <summary>
        /// Copies the elements of the array to the specified array.
        /// </summary>
        /// <param name="array">The array to copy the data to.</param>
        /// <param name="arrayIndex">The index in array where copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if (array.Length - arrayIndex < m_count)
            {
                throw new ArgumentOutOfRangeException("array", "The specified array is not large enough to hold the data.");
            }

            DoubleLinkedListNode<T> current = Head;
            for (int i = 0; i < m_count; ++i)
            {
                array[i + arrayIndex] = current.Data;
                current = current.Next;
            }
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

        void System.Collections.Generic.ICollection<T>.Add(T item)
        {
            AddToEnd(item);
        }

        #endregion


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

            DoubleLinkedListNode<T> current = Head;
            for (int i = 0; i < m_count; ++i)
            {
                array.SetValue(current.Data, i + index);
                current = current.Next;
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