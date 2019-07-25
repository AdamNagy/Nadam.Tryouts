using System;
using System.Collections.Generic;

namespace MyCollection.AssociativeArrays
{
    public class MyHashTable<TKey, TValue>
    {
        private IEqualityComparer<TKey> comparer;
        private int[] buckets;
        private Entry[] entries;
        // Indexed of first unused hash table entry
        private int nextUnusedEntry;
        // Number of unused hash table entries
        private int unusedCount;
        // Number of entries
        private int count;
        // Value that is updated everytime the hash table is updated.
        private int updateCode;
        private int capacity;

        // Array of prime numbers to use for the capacity.
        readonly int[] PRIME_NUMBERS = new int[]
                {
                    53,97,193,389,769,1543,3079,6151,12289,24593,49157,98317,196613,
                    393241,786433,1572869,3145739,6291469,12582917,25165843,50331653,
                    100663319,201326611,402653189,805306457,1610612741
                };

        private struct EntryData
        {
            MyHashTable<TKey, TValue> hashtable;
            int index;
            int bucketIndex;
            int previous;

            public int Index { get { return index; } }
            public int BucketIndex { get { return bucketIndex; } }
            public bool IsEmpty { get { return Index == NULL_REFERENCE; } }
            public int Previous { get { return previous; } }
            public int Next { get { return hashtable.entries[Index].Next; } }
            public TKey Key { get { return hashtable.entries[Index].Key; } }
            public TValue Value
            {
                set { hashtable.entries[Index].Value = value; }
                get { return hashtable.entries[Index].Value; }
            }

            public static readonly EntryData EMPTY = new EntryData(null, NULL_REFERENCE, NULL_REFERENCE, NULL_REFERENCE);

            public EntryData(MyHashTable<TKey, TValue> hashtable, int index, int previous, int bucketIndex)
            {
                this.index = index;
                this.previous = previous;
                this.hashtable = hashtable;
                this.bucketIndex = bucketIndex;
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
        /// Initializes a new instance of the MyHashTable(TKey,TValue) class that is empty.
        /// </summary>
        public MyHashTable()
        {
            comparer = EqualityComparer<TKey>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the MyHashTable(TKey,TValue) class that is empty and uses the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer to use for the keys.</param>
        public MyHashTable(IEqualityComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            this.comparer = comparer;
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
                while (MoveNext(ref bucketIndex, ref entryIndex))
                {
                    keys[index++] = entries[entryIndex].Key;
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
                    values[index++] = entries[entryIndex].Value;
                }

                return values;
            }
        }

        /// <summary>
        /// Gets the number of items in the MyHashTable(TKey,TValue). 
        /// </summary>
        public int Count
        {
            get { return (count); }
        }

        /// <summary>
        /// States if the MyHashTable(TKey,TValue) empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return count <= 0; }
        }

        /// <summary>
        /// Removes all items from the MyHashTable(TKey,TValue).
        /// </summary>
        public void Clear()
        {
            // Set each bucket to empty
            for (int i = 0; i < buckets.Length; ++i)
            {
                buckets[i] = NULL_REFERENCE;
            }

            // Point each entry to the next entry
            for (int i = 0; i < capacity - 1; ++i)
            {
                entries[i].Key = default(TKey);
                entries[i].Value = default(TValue);
                entries[i].HashCode = 0;
                entries[i].Next = i + 1;
            }
            entries[capacity - 1].Next = NULL_REFERENCE;

            // Set the first unused entry to the first entry
            nextUnusedEntry = 0;

            unusedCount = capacity;
            count = 0;

            ++updateCode;
        }

        /// <summary>
        /// Checks to see if the specified key is present in the MyHashTable(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return !(FindKey(key).IsEmpty);
        }

        /// <summary>
        /// Checks to see if the MyHashTable(TKey,TValue) contains the specified value.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        public bool ContainsValue(TValue value)
        {
            return !(FindValue(value).IsEmpty);
        }

        int HashFunction(int hashcode)
        {
            return hashcode % buckets.Length;
        }

        /// <summary>
        /// Adds the key value pair to the MyHashTable(TKey,TValue)
        /// </summary>
        /// <param name="key">The key to associate with the specified value.</param>
        /// <param name="value">The value to add to the MyHashTable(TKey,TValue).</param>
        /// <param name="overwrite">True if the value should be overwritten if it exist, false if an error should be thrown.</param>
        void Add(TKey key, TValue value, bool overwrite)
        {
            EntryData entry = FindKey(key);
            if (!entry.IsEmpty)
            {
                if (!overwrite)
                    throw new InvalidOperationException("The specified key is already present in the array");
                else
                    entry.Value = value;

                return;
            }

            if (unusedCount <= 0)
                Rehash();

            int uHashcode = CalculateHashCode(key);

            int bucketIndex = HashFunction(uHashcode);

            int thisEntry = nextUnusedEntry;

            // Pop the first unused entry off of the unused bucket
            nextUnusedEntry = entries[nextUnusedEntry].Next;
            --unusedCount;

            ++count;

            entries[thisEntry].Key = key;
            entries[thisEntry].Value = value;
            entries[thisEntry].HashCode = uHashcode;
            entries[thisEntry].Next = buckets[bucketIndex];

            buckets[bucketIndex] = thisEntry;

            ++updateCode;
        }

        /// <summary>
        /// Adds the key value pair to the MyHashTable(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to add.</param>
        public void Add(TKey key, TValue value)
        {
            Add(key, value, false);
        }

        /// <summary>
        /// Removes the specified key from the MyHashTable(TKey,TValue).
        /// </summary>
        /// <param name="key">The key to remove from the MyHashTable(TKey,TValue).</param>
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
        /// <returns>True if the value was removed, false if it wasn't present in the MyHashTable(TKey,TValue).</returns>
        public bool RemoveValue(TValue value)
        {
            return RemoveValue(value, false);
        }

        /// <summary>
        ///  Removes the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <param name="alloccurrences">True if all occarances of the value should be removed, false if not.</param>
        /// <returns>True if the value was removed, false if it wasn't present in the MyHashTable(TKey,TValue).</returns>
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
                    throw new KeyNotFoundException("The specified key couldn't be located");

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
                return EntryData.EMPTY;

            // Call out hashing function.
            int uHashcode = CalculateHashCode(key);

            // Calculate which bucket the key belongs in by doing a mod operation
            int bucketIndex = HashFunction(uHashcode);

            // Store the previous index in case the item is removed
            int previous = NULL_REFERENCE;
            for (int entryIndex = buckets[bucketIndex]; entryIndex != NULL_REFERENCE; entryIndex = entries[entryIndex].Next)
            {
                // Check to see if the hash code matches before doing a more complex search.
                if (entries[entryIndex].HashCode != uHashcode)
                {
                    previous = entryIndex;
                    continue;
                }

                if (comparer.Equals(entries[entryIndex].Key, key))
                    return new EntryData(this, entryIndex, previous, bucketIndex);
                
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
                return EntryData.EMPTY;

            var comparer = EqualityComparer<TValue>.Default;

            for (int bucketIndex = 0; bucketIndex < buckets.Length; ++bucketIndex)
            {
                if (buckets[bucketIndex] == NULL_REFERENCE)
                    continue;

                // Store the previous index in case the item is removed
                int previous = NULL_REFERENCE;
                for (int entryIndex = buckets[bucketIndex]; entryIndex != NULL_REFERENCE; entryIndex = entries[entryIndex].Next)
                {
                    if (comparer.Equals(entries[entryIndex].Value, value))
                        return new EntryData(this, entryIndex, previous, bucketIndex);

                    previous = entryIndex;
                }
            }

            return EntryData.EMPTY;
        }

        int CalculateHashCode(TKey key)
        {
            // Make sure the hash code is positive.
            return comparer.GetHashCode(key) & 0x7fffffff;
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
                    return PRIME_NUMBERS[i];
            }

            return int.MaxValue;
        }

        void Rehash()
        {
            capacity = CalculateCapacity(capacity + 1);

            Entry[] oldData = entries;
            int[] oldBuckets = buckets;

            entries = new Entry[capacity];

            // Create new buckets and set them to null
            buckets = new int[capacity];
            for (int bucketIndex = 0; bucketIndex < buckets.Length; ++bucketIndex)
                buckets[bucketIndex] = NULL_REFERENCE;

            int unusedStart = 0;

            if (oldData != null)
            {
                // Copy the old bucket to the new one
                Array.Copy(oldData, entries, oldData.Length);

                // All entries that were just created are unused.
                // They all begin at index oldData.Length
                unusedStart = oldData.Length;

                if (unusedCount <= 0)
                {
                    // Assign the next deleted entry to the beginning of the free data
                    nextUnusedEntry = oldData.Length;
                }
                else
                {
                    // Find the last deleted entry and set its next to the beginning 
                    // of the appended unused entries
                    for (int i = nextUnusedEntry; i != NULL_REFERENCE; i = entries[i].Next)
                    {
                        if (entries[i].Next == NULL_REFERENCE)
                        {
                            entries[i].Next = oldData.Length;
                            break;
                        }
                    }
                }

                // Add all of the newly created entry to the unused count
                unusedCount += (capacity - oldData.Length);
            }
            else
            {
                unusedCount = capacity;
                nextUnusedEntry = 0;
            }

            // Set the next pointer of all unused entries to the next unused entry
            for (int i = unusedStart; i < capacity - 1; ++i)
            {
                entries[i].Next = i + 1;
            }
            entries[capacity - 1].Next = NULL_REFERENCE;

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
                        int nextIndex = entries[index].Next;

                        // Make sure the hash code is positive.
                        int newBucketIndex = HashFunction(entries[index].HashCode);

                        // Set the next of the current entry equal to the first entry in the bucket
                        entries[index].Next = buckets[newBucketIndex];

                        // Set this entry as the first one in the bucket
                        buckets[newBucketIndex] = index;


                        index = nextIndex;
                    }
                }

            }

            ++updateCode;
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
                entries[data.Previous].Next = entries[data.Index].Next;
            }
            else
            {
                // The entry is at the beginning of the bucket
                // So set the bucket to reference the next entry
                buckets[data.BucketIndex] = entries[data.Index].Next;
            }

            // Add the removed entry to the front of the unused bucket.
            entries[data.Index].Next = nextUnusedEntry;
            nextUnusedEntry = data.Index;
            ++unusedCount;
            --count;

            // Clear the entry
            entries[data.Index].Key = default(TKey);
            entries[data.Index].Value = default(TValue);
            entries[data.Index].HashCode = 0;

            ++updateCode;

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
                if (bucketIndex > buckets.Length)
                {
                    return false;
                }

                // Keep checking bucket until we find one that isn't empty.
                while (buckets[bucketIndex] == NULL_REFERENCE)
                {

                    // Check to see if we have reach the end of bucket array
                    ++bucketIndex;
                    if (bucketIndex >= buckets.Length)
                    {
                        return false;
                    }
                }

                // Set the entryIndex to the first entry in the nonempty bucket
                entryIndex = buckets[bucketIndex];
            }
            else
            {
                entryIndex = entries[entryIndex].Next;

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
