using MyCollection.DoubleLinkedList;
using System;
using System.Collections.Generic;

namespace MyCollection.AssociativeArrays
{
    class AssociativeList<TKey, TValue>
    {
        private IEqualityComparer<TKey> comparer;
        private DoubleLinkedList<KVPair> backbone;
        // Value that is updated everytime the list is updated.
        private int m_updateCode;

        /// <summary>
        /// Initializes a new instance of the AssociativeList(TKey,TValue) class that is empty.
        /// </summary>
        public AssociativeList()
        {
            comparer = EqualityComparer<TKey>.Default;
            backbone = new DoubleLinkedList<KVPair>();
        }

        /// <summary>
        /// Initializes a new instance of the AssociativeList(TKey,TValue) class that is empty and uses the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer to use for the keys.</param>
        public AssociativeList(IEqualityComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            this.comparer = comparer;
            backbone = new DoubleLinkedList<KVPair>();
        }

        /// <summary>
        /// Gets an array of current keys.
        /// </summary>
        public TKey[] Keys
        {
            get
            {
                var index = 0;
                var keys = new TKey[Count];

                for (var curr = backbone.Head; curr != null; curr = curr.Next)
                {
                    keys[index++] = curr.Data.Key;
                }

                return keys;
            }
        }

        /// <summary>
        /// Gets an array of current values.
        /// </summary>
        public TValue[] Values
        {
            get
            {
                var values = new TValue[Count];
                var index = 0;

                for (var curr = backbone.Head; curr != null; curr = curr.Next)
                {
                    values[index++] = curr.Data.Value;
                }

                return values;
            }
        }

        /// <summary>
        /// States if the AssociativeList(TKey,TValue) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return Count <= 0; }
        }

        /// <summary>
        /// Gets the number of items in the AssociativeList(TKey,TValue). 
        /// </summary>
        public int Count
        {
            get { return backbone.Count; }
        }

        /// <summary>
        /// Removes all items from the AssociativeList(TKey,TValue).
        /// </summary>
        public void Clear()
        {
            backbone.Clear();
            ++m_updateCode;
        }

        /// <summary>
        /// Checks to see if the specified key is present in the AssociativeList(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public bool ContainsKey(TKey key)
            => FindKey(key) != null;
        
        /// <summary>
        /// Checks to see if the AssociativeList(TKey,TValue) contains the specified value.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        public bool ContainsValue(TValue value)
            => FindValue(value) != null;

        /// <summary>
        /// Adds the key value pair to the AssociativeList(TKey,TValue)
        /// </summary>
        /// <param name="key">The key to associate with the specified value.</param>
        /// <param name="value">The value to add to the AssociativeList(TKey,TValue).</param>
        /// <param name="overwrite">True if the value should be overwritten if it exist, false if an error should be thrown.</param>
        void Add(TKey key, TValue value, bool overwrite)
        {
            var node = FindKey(key);
            if (node != null)
            {
                if (!overwrite)
                {
                    throw new InvalidOperationException("The specified key is already present");
                }
                else
                {
                    KVPair tmp = node.Data;
                    tmp.Value = value;
                    node.Data = tmp;
                }

                return;
            }

            KVPair kvp = new KVPair(key, value);

            backbone.AddToBeginning(kvp);

            ++m_updateCode;
        }

        /// <summary>
        /// Adds the key value pair to the AssociativeList(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to add.</param>
        public void Add(TKey key, TValue value)
        {
            Add(key, value, false);
        }

        /// <summary>
        /// Removes the specified key from the AssociativeList(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to remove from the AssociativeList(TKey,TValue).</param>
        /// <returns>True if the key was removed, false otherwise.</returns>
        public bool Remove(TKey key)
        {
            var node = FindKey(key);

            if (node == null)
                return false;

            return Remove(node);
        }

        /// <summary>
        /// Tries to get the value at the specified key without throwing an exception.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="value">The value that is associated with the specified key or the default value for the type.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var node = FindKey(key);

            if (node != null)
            {
                value = node.Data.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>True if the value was removed, false if it wasn't present in the AssociativeList(TKey,TValue).</returns>
        public bool RemoveValue(TValue value)
        {
            return RemoveValue(value, false);
        }

        /// <summary>
        ///  Removes the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <param name="alloccurrences">True if all occarances of the value should be removed, false if not.</param>
        /// <returns>True if the value was removed, false if it wasn't present in the AssociativeList(TKey,TValue).</returns>
        public bool RemoveValue(TValue value, bool alloccurrences)
        {
            bool removed = false;

            DoubleLinkedListNode<KVPair> node = FindValue(value);
            while (node != null)
            {
                removed = Remove(node) || removed;

                if (!alloccurrences)
                {
                    return removed;
                }

                node = FindValue(value);
            }

            return removed;
        }

        /// <summary>
        /// Gets or sets the value at the specified key.
        /// </summary>
        /// <param name="key">The key to use for finding the value.</param>
        /// <returns>The value associated with the specified key.</returns>
        public TValue this[TKey key]
        {
            get
            {
                DoubleLinkedListNode<KVPair> node = FindKey(key);

                if (node == null)
                {
                    throw new KeyNotFoundException("The specified key couldn't be located");
                }

                return node.Data.Value;
            }
            set
            {
                Add(key, value, true);
            }
        }

        /// <summary>
        /// Finds the node that contains the specified key.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>The node that contains the specified key, otherwise null</returns>
        DoubleLinkedListNode<KVPair> FindKey(TKey key)
        {
            if (IsEmpty)
                return null;

            for (DoubleLinkedListNode<KVPair> node = backbone.Head; node != null; node = node.Next)
            {
                if (comparer.Equals(node.Data.Key, key))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the node that contains the specified value.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>The first node that contains the specified value, otherwise null.</returns>
        DoubleLinkedListNode<KVPair> FindValue(TValue value)
        {
            if (IsEmpty)
                return null;

            var comparer = EqualityComparer<TValue>.Default;

            for (DoubleLinkedListNode<KVPair> node = backbone.Head; node != null; node = node.Next)
            {
                if (comparer.Equals(node.Data.Value, value))
                {
                    return node;
                }
            }

            return null;
        }

        bool Remove(DoubleLinkedListNode<KVPair> node)
        {
            if (node == null)
            {
                return false;
            }

            backbone.Remove(node);

            ++m_updateCode;

            return true;
        }

        /// <summary>
        /// Used for storing a key value pair.
        /// </summary>
        public struct KVPair
        {
            public TKey Key { get; }
            public TValue Value { get; set; }

            public KVPair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }

}
