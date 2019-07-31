using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class AssociativeArrayAL<TKey, TValue> : System.Collections.ICollection, ICollection<KeyValuePair<TKey, TValue>>
    {
        #region ICollection<KeyValuePair<TKey,TValue>> Members

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            DoubleLinkedListNode<KVPair> node = FindKey(item.Key);
            if (node == null)
            {
                return false;
            }

            return EqualityComparer<TValue>.Default.Equals(item.Value, node.Data.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException("array", "The specified array is not large enough to hold the data.");
            }

            int i = arrayIndex;
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                array[i] = kvp;
                ++i;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            DoubleLinkedListNode<KVPair> node = FindKey(item.Key);
            if (node == null)
            {
                return false;
            }

            if (!EqualityComparer<TValue>.Default.Equals(item.Value, node.Data.Value))
            {
                return false;
            }

            return Remove(node);
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

            if (array.Length - index < Count)
            {
                throw new ArgumentException("array", "The specified array is not large enough to hold the data.");
            }

            int i = index;
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                array.SetValue(kvp, i);
                ++i;
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
