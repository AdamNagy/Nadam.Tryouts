using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{
    /// <summary>
    /// Represents a strongly typed associative array that is implemented using a hash table.
    /// </summary>
    /// <typeparam name="TKey">Specifies the key type.</typeparam>
    /// <typeparam name="TValue">Specifies the value type.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(AssociativeArrayDebugView))]
    public class AssociativeArrayHT<TKey, TValue>
    {
        IEqualityComparer<TKey> m_comparer;

        int[] m_buckets;
        Entry[] m_entries;

        // Indexed of first unused hash table entry
        int m_nextUnusedEntry;

        // Number of unused hash table entries
        int m_unusedCount;

        // Number of entries
        int m_count;

        // Value that is updated everytime the hash table is updated.
        int m_updateCode;

        int m_capacity;
         
        // Array of prime numbers to use for the capacity.
        readonly int[] PRIME_NUMBERS = new int[] 
                {
                    53,97,193,389,769,1543,3079,6151,12289,24593,49157,98317,196613,393241,786433,1572869,3145739,6291469,12582917,25165843,50331653,100663319,201326611,402653189,805306457,1610612741 
                };


        private struct EntryData
        {
            AssociativeArrayHT<TKey, TValue> m_hashtable;
            int m_index;
            int m_bucketIndex;
            int m_previous;

            public int Index { get { return m_index; } }
            public int BucketIndex { get { return m_bucketIndex; } }
            public bool IsEmpty { get { return Index == NULL_REFERENCE; } }
            public int Previous { get { return m_previous; } }
            public int Next { get { return m_hashtable.m_entries[Index].Next; } }
            public TKey Key { get { return m_hashtable.m_entries[Index].Key; } }
            public TValue Value
            {
                set { m_hashtable.m_entries[Index].Value = value; }
                get { return m_hashtable.m_entries[Index].Value; }
            }

            public static readonly EntryData EMPTY = new EntryData(null, NULL_REFERENCE, NULL_REFERENCE, NULL_REFERENCE);


            public EntryData(AssociativeArrayHT<TKey, TValue> hashtable, int index, int previous, int bucketIndex)
            {
                m_index = index;
                m_previous = previous;
                m_hashtable = hashtable;
                m_bucketIndex = bucketIndex;
            }
        }

        private struct Entry
        {
            public TKey Key;
            public TValue Value;
            public int HashCode;
            public int Next;
        }

        // Constant of what index represents null
        public const int NULL_REFERENCE = -1;
         
        /// <summary>
        /// Initializes a new instance of the AssociativeArrayHT(TKey,TValue) class that is empty.
        /// </summary>
        public AssociativeArrayHT()
        {
            m_comparer = EqualityComparer<TKey>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the AssociativeArrayHT(TKey,TValue) class that is empty and uses the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer to use for the keys.</param>
        public AssociativeArrayHT(IEqualityComparer<TKey> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            
            m_comparer = comparer;
        }

        /// <summary>
        /// Gets an array of current keys.
        /// </summary>
        public TKey[] Keys
        {
            get
            {
                int index = 0;
                TKey[] keys = new TKey[Count];

                int bucketIndex = NULL_REFERENCE;
                int entryIndex = NULL_REFERENCE;
                while(MoveNext(ref bucketIndex, ref entryIndex))
                {
                    keys[index++] = m_entries[entryIndex].Key;
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
                TValue[] values = new TValue[Count];
                int index = 0;

                int bucketIndex = NULL_REFERENCE;
                int entryIndex = NULL_REFERENCE;
                while (MoveNext(ref bucketIndex, ref entryIndex))
                {
                    values[index++] = m_entries[entryIndex].Value;
                }

                return values;
            }
        }

        /// <summary>
        /// Gets the number of items in the AssociativeArrayHT(TKey,TValue). 
        /// </summary>
        public int Count
        {
            get { return (m_count); }
        }

        /// <summary>
        /// States if the AssociativeArrayHT(TKey,TValue) empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return m_count <= 0; }
        }

        /// <summary>
        /// Removes all items from the AssociativeArrayHT(TKey,TValue).
        /// </summary>
        public void Clear()
        {
            // Set each bucket to empty
            for (int i = 0; i < m_buckets.Length; ++i)
            {
                m_buckets[i] = NULL_REFERENCE;
            }

            // Point each entry to the next entry
            for (int i = 0; i < m_capacity - 1; ++i)
            {
                m_entries[i].Key = default(TKey);
                m_entries[i].Value = default(TValue);
                m_entries[i].HashCode = 0;
                m_entries[i].Next = i + 1;
            }
            m_entries[m_capacity - 1].Next = NULL_REFERENCE;

            // Set the first unused entry to the first entry
            m_nextUnusedEntry = 0;

            m_unusedCount = m_capacity;
            m_count = 0;

            ++m_updateCode;
        }

        /// <summary>
        /// Checks to see if the specified key is present in the AssociativeArrayHT(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return !(FindKey(key).IsEmpty);
        }

        /// <summary>
        /// Checks to see if the AssociativeArrayHT(TKey,TValue) contains the specified value.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        public bool ContainsValue(TValue value)
        {
            return !(FindValue(value).IsEmpty);
        }

        int HashFunction(int hashcode)
        {
            return  hashcode % m_buckets.Length;
        }

        /// <summary>
        /// Adds the key value pair to the AssociativeArrayHT(TKey,TValue)
        /// </summary>
        /// <param name="key">The key to associate with the specified value.</param>
        /// <param name="value">The value to add to the AssociativeArrayHT(TKey,TValue).</param>
        /// <param name="overwrite">True if the value should be overwritten if it exist, false if an error should be thrown.</param>
        void Add(TKey key, TValue value, bool overwrite)
        {
            EntryData entry = FindKey(key);
            if (!entry.IsEmpty)
            {
                if (!overwrite)
                {
                    throw new InvalidOperationException("The specified key is already present in the array");
                }
                else
                {
                    entry.Value = value;
                }

                return;
            }

            if (m_unusedCount <= 0)
            {
                Rehash();
            }

            int uHashcode = CalculateHashCode(key);

            int bucketIndex = HashFunction(uHashcode);

            int thisEntry = m_nextUnusedEntry;

            // Pop the first unused entry off of the unused bucket
            m_nextUnusedEntry = m_entries[m_nextUnusedEntry].Next;
            --m_unusedCount;

            ++m_count;

            m_entries[thisEntry].Key = key;
            m_entries[thisEntry].Value = value;
            m_entries[thisEntry].HashCode = uHashcode;
            m_entries[thisEntry].Next = m_buckets[bucketIndex];

            m_buckets[bucketIndex] = thisEntry;

            ++m_updateCode;
        }

        /// <summary>
        /// Adds the key value pair to the AssociativeArrayHT(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to add.</param>
        public void Add(TKey key, TValue value)
        {
            Add(key, value, false);
        }

        /// <summary>
        /// Removes the specified key from the AssociativeArrayHT(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to remove from the AssociativeArrayHT(TKey,TValue).</param>
        /// <returns>True if the key was removed, false otherwise.</returns>
        public bool Remove(TKey key)
        {
            EntryData entry = FindKey(key);

            if (entry.IsEmpty)
            {
                return false;
            }

            return Remove(entry);
        }

        /// <summary>
        /// Tries to get the value at the specified key without throwing an exception.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="value">The value that is associated with the specified key or the default value for the type.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            EntryData entry = FindKey(key);

            if (!entry.IsEmpty)
            {
                value = entry.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>True if the value was removed, false if it wasn't present in the AssociativeArrayHT(TKey,TValue).</returns>
        public bool RemoveValue(TValue value)
        {
            return RemoveValue(value, false);
        }

        /// <summary>
        ///  Removes the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <param name="alloccurrences">True if all occarances of the value should be removed, false if not.</param>
        /// <returns>True if the value was removed, false if it wasn't present in the AssociativeArrayHT(TKey,TValue).</returns>
        public bool RemoveValue(TValue value, bool alloccurrences)
        {
            bool removed = false;

            EntryData entry = FindValue(value);
            while (!entry.IsEmpty)
            {
                removed = Remove(entry) || removed;

                if (!alloccurrences)
                {
                    return removed;
                }

                entry = FindValue(value);
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
                EntryData entry = FindKey(key);

                if (entry.IsEmpty)
                {
                    throw new KeyNotFoundException("The specified key couldn't be located");
                }

                return entry.Value;
            }
            set
            {
                Add(key, value, true);
            }
        }

        /// <summary>
        /// Finds the entry that contains the specified key.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>The entry that contains the specified key, otherwise EntryData.EMPTY</returns>
        EntryData FindKey(TKey key)
        {
            if (IsEmpty)
            {
                return EntryData.EMPTY;
            }

            // Call out hashing function.
            int uHashcode = CalculateHashCode(key);

            // Calculate which bucket the key belongs in by doing a mod operation
            int bucketIndex = HashFunction(uHashcode);

            // Store the previous index in case the item is removed
            int previous = NULL_REFERENCE;
            for (int entryIndex = m_buckets[bucketIndex]; entryIndex != NULL_REFERENCE; entryIndex = m_entries[entryIndex].Next)
            {
                // Check to see if the hash code matches before doing a more complex search.
                if (m_entries[entryIndex].HashCode != uHashcode)
                {
                    previous = entryIndex;
                    continue;
                }

                if (m_comparer.Equals(m_entries[entryIndex].Key, key))
                {
                    return new EntryData(this, entryIndex, previous, bucketIndex);
                }
                previous = entryIndex;
            }

            return EntryData.EMPTY;
        }

        /// <summary>
        /// Finds the entry that contains the specified value.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>The first entry that contains the specified value, otherwise EntryData.EMPTY.</returns>
        EntryData FindValue(TValue value)
        {
            if (IsEmpty)
            {
                return EntryData.EMPTY;
            }

            EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;

            for (int bucketIndex = 0; bucketIndex < m_buckets.Length; ++bucketIndex)
            {
                if (m_buckets[bucketIndex] == NULL_REFERENCE)
                {
                    continue;
                }

                // Store the previous index in case the item is removed
                int previous = NULL_REFERENCE;
                for (int entryIndex = m_buckets[bucketIndex]; entryIndex != NULL_REFERENCE; entryIndex = m_entries[entryIndex].Next)
                {
                    if (comparer.Equals(m_entries[entryIndex].Value, value))
                    {
                        return new EntryData(this, entryIndex, previous, bucketIndex);
                    }

                    previous = entryIndex;
                }
            }

            return EntryData.EMPTY;
        }

        int CalculateHashCode(TKey key)
        {
            // Make sure the hash code is positive.
            return m_comparer.GetHashCode(key) & 0x7fffffff;
        }

        /// <summary>
        /// Locates the next prime number that should be used for the capacity.
        /// </summary>
        /// <param name="size">The minimum size needed.</param>
        /// <returns>A prime number large enough to hold the specified number of items.</returns>
        int CalculateCapacity(int size)
        {
            for (int i = 0; i < PRIME_NUMBERS.Length; ++i)
            {
                if (size <= PRIME_NUMBERS[i])
                {
                    return PRIME_NUMBERS[i];
                }
            }

            return int.MaxValue;
        }

        void Rehash()
        {
            m_capacity = CalculateCapacity(m_capacity + 1);

            Entry[] oldData = m_entries;
            int[] oldBuckets = m_buckets;

            m_entries = new Entry[m_capacity];

            // Create new buckets and set them to null
            m_buckets = new int[m_capacity];
            for (int bucketIndex = 0; bucketIndex < m_buckets.Length; ++bucketIndex)
            {
                m_buckets[bucketIndex] = NULL_REFERENCE;
            }

            int unusedStart = 0;

            if (oldData != null)
            {
                // Copy the old bucket to the new one
                Array.Copy(oldData, m_entries, oldData.Length);

                // All entries that were just created are unused.
                // They all begin at index oldData.Length
                unusedStart = oldData.Length;

                if (m_unusedCount <= 0)
                {
                    // Assign the next deleted entry to the beginning of the free data
                    m_nextUnusedEntry = oldData.Length;
                }
                else
                {
                    // Find the last deleted entry and set its next to the beginning 
                    // of the appended unused entries
                    for (int i = m_nextUnusedEntry; i != NULL_REFERENCE; i = m_entries[i].Next)
                    {
                        if (m_entries[i].Next == NULL_REFERENCE)
                        {
                            m_entries[i].Next = oldData.Length;
                            break;
                        }
                    }
                }

                // Add all of the newly created entry to the unused count
                m_unusedCount += (m_capacity - oldData.Length);
            }
            else
            {
                m_unusedCount = m_capacity;
                m_nextUnusedEntry = 0;
            }

            // Set the next pointer of all unused entries to the next unused entry
            for (int i = unusedStart; i < m_capacity - 1; ++i)
            {
                m_entries[i].Next = i + 1;
            }
            m_entries[m_capacity - 1].Next = NULL_REFERENCE;

            // Recalculate everyones location in the hash table
            if (oldBuckets != null)
            {
                // Traverses the old buckets and move the entries to their new buckets
                // in the new hash table.
                for (int bucketIndex = 0; bucketIndex < oldBuckets.Length; ++bucketIndex)
                {
                    int index = oldBuckets[bucketIndex];

                    // Traverse each entry in this bucket
                    while (index != NULL_REFERENCE)
                    {
                        int nextIndex = m_entries[index].Next;

                        // Make sure the hash code is positive.
                        int newBucketIndex = HashFunction(m_entries[index].HashCode);

                        // Set the next of the current entry equal to the first entry in the bucket
                        m_entries[index].Next = m_buckets[newBucketIndex];

                        // Set this entry as the first one in the bucket
                        m_buckets[newBucketIndex] = index;


                        index = nextIndex;
                    }
                }

            }

            ++m_updateCode;
        }

        /// <summary>
        /// Removes the entry specified by the user.
        /// </summary>
        /// <param name="entry">The entry to remove.</param>
        /// <returns>True if the entry is removed, false if not.</returns>
        bool Remove(EntryData entry)
        {
            if (entry.IsEmpty)
            {
                return false;
            }

            EntryData data = (EntryData)entry;
            if (data.Previous != NULL_REFERENCE)
            {
                // Link the previous and next entry
                m_entries[data.Previous].Next = m_entries[data.Index].Next;
            }
            else
            {
                // The entry is at the beginning of the bucket
                // So set the bucket to reference the next entry
                m_buckets[data.BucketIndex] = m_entries[data.Index].Next;
            }

            // Add the removed entry to the front of the unused bucket.
            m_entries[data.Index].Next = m_nextUnusedEntry;
            m_nextUnusedEntry = data.Index;
            ++m_unusedCount;
            --m_count;

            // Clear the entry
            m_entries[data.Index].Key = default(TKey);
            m_entries[data.Index].Value = default(TValue);
            m_entries[data.Index].HashCode = 0;

            ++m_updateCode;

            return true;
        }

        /// <summary>
        /// Traverses the entries from the beginning to the end.
        /// </summary>
        /// <param name="bucketIndex">The index of the bucket to start at or NULL_REFERENCE to start from the beginning.</param>
        /// <param name="entryIndex">The index of the entry to start at or NULL_REFERENCE to start from the beginning.</param>
        /// <returns>False if the end of the bucket is reached, false otherwise.</returns>
        internal bool MoveNext(ref int bucketIndex, ref int entryIndex)
        {
            if (entryIndex == NULL_REFERENCE)
            {
                ++bucketIndex;

                // Check to see if we have reach the end of bucket array
                if (bucketIndex > m_buckets.Length)
                {
                    return false;
                }

                // Keep checking bucket until we find one that isn't empty.
                while (m_buckets[bucketIndex] == NULL_REFERENCE)
                {

                    // Check to see if we have reach the end of bucket array
                    ++bucketIndex;
                    if (bucketIndex >= m_buckets.Length)
                    {
                        return false;
                    }
                }

                // Set the entryIndex to the first entry in the nonempty bucket
                entryIndex = m_buckets[bucketIndex];
            }
            else
            {
                entryIndex = m_entries[entryIndex].Next;

                // Recursively call this method if we have moved past the end of the current bucket
                if (entryIndex == NULL_REFERENCE)
                {
                    return MoveNext(ref bucketIndex, ref entryIndex);
                }
            }

            return true;
        }
    }
}
