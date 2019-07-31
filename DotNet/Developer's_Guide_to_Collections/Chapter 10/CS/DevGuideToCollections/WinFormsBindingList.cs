using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DevGuideToCollections
{
    public partial class WinFormsBindingList<T>
    {
        List<T> m_data;

        /// <summary>
        /// Initializes a new instance of the WinFormsBindingList(T) class that is empty.
        /// </summary>
        public WinFormsBindingList()
        {
            m_data = new List<T>();
            InitializeBindingSupport();
        }

        /// <summary>
        /// Initializes a new instance of the WinFormsBindingList(T) class that contains the items in the array.
        /// </summary>
        /// <param name="items">Adds items to the WinFormsBindingList(T).</param>
        public WinFormsBindingList(IEnumerable<T> items)
        {
            m_data = new List<T>();
            InitializeBindingSupport();
            foreach (T item in items)
            {
                Add(item);
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the WinFormsBindingList(T) class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new array can initially store.</param>
        public WinFormsBindingList(int capacity)
        {
            m_data = new List<T>(capacity);
            InitializeBindingSupport();
        }

        List<T> InnerList
        {
            get
            {
                return m_data;
            }
        }

        /// <summary>
        /// States if the WinFormsBindingList(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return Count <= 0; }
        }


        /// <summary>
        /// Gets the number of elements actually contained in the WinFormsBindingList(T).
        /// </summary>
        public int Count
        {
            get { return InnerList.Count; }
        }

        /// <summary>
        /// Gets or sets the size of the internal data array.
        /// </summary>
        public int Capacity
        {
            get { return InnerList.Capacity; }
            set
            {
                InnerList.Capacity = value;
            }
        }

        /// <summary>
        /// Adds an object to the end of the WinFormsBindingList(T).
        /// </summary>
        /// <param name="item">The item to add to the end of the WinFormsBindingList(T).</param>
        public void Add(T item)
        {
            OnInsert(item, Count);
            InnerList.Add(item);
            OnInsertComplete(item, Count - 1);
        }

        /// <summary>
        /// Checks to see if the item is present in the WinFormsBindingList(T).
        /// </summary>
        /// <param name="item">The item to see if the array contains.</param>
        /// <returns>True if the item is in the array, false if it is not.</returns>
        public bool Contains(T item)
        {
            return InnerList.Contains(item);
        }

        /// <summary>
        /// Gets the index of the specified item.
        /// </summary>
        /// <param name="item">The item to get the index of.</param>
        /// <returns>-1 if the item isn't found in the array, the index of the found item otherwise.</returns>
        public int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }

        /// <summary>
        /// Clears all values from the WinFormsBindingList(T).
        /// </summary>
        public void Clear()
        {
            var removed = ToArray();
            OnClear(removed);
            InnerList.Clear();
            OnClearComplete(removed);
        }

        /// <summary>
        /// Removes the first occurrence of the specified item from the WinFormsBindingList(T).
        /// </summary>
        /// <param name="item">The item to remove from the WinFormsBindingList(T).</param>
        /// <returns>True if an item was removed, false otherwise.</returns>
        public bool Remove(T item)
        {
            if (!AllowRemove)
            {
                throw new NotSupportedException();
            }

            int index = IndexOf(item);

            if (index >= 0)
            {
                OnRemove(item, index);

                InnerList.Remove(item);

                OnRemoveComplete(item, index);
            }

            return index >= 0;
        }

        /// <summary>
        /// Removes the item located at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to remove</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                // Item has already been removed.
                return;
            }

            if (!AllowRemove)
            {
                throw new NotSupportedException();
            }
            
            if (IsSorted)
            {
                throw new NotSupportedException("You cannot remove by index on a sorted list.");
            }

            T item = InnerList[index];

            OnRemove(item, index);

            InnerList.RemoveAt(index);

            OnRemoveComplete(item, index);
        }

        /// <summary>
        /// Inserts an item into the WinFormsBindingList(T) at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, T item)
        {
            OnInsert(item, index);

            InnerList.Insert(index, item);

            OnInsertComplete(item, index);
        }

        /// <summary>
        /// Gets or sets an element in the WinFormsBindingList(T).
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The value of the element.</returns>
        public T this[int index]
        {
            get
            {
                return InnerList[index];
            }
            set
            {
                T oldValue = InnerList[index];

                OnSet(index, oldValue, value);

                InnerList[index] = value;

                OnSetComplete(index, oldValue, value);
            }
        }

        /// <summary>
        /// Copies the elements of the WinFormsBindingList(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the WinFormsBindingList(T).</returns>
        public T[] ToArray()
        {
            return InnerList.ToArray();
        }
    }
}
