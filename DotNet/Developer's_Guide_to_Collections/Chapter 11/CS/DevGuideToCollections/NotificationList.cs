using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace DevGuideToCollections
{
    [DebuggerDisplay("Count={Count}")]
    public partial class NotificationList<T>
    {
        List<T> m_data;

        /// <summary>
        /// Initializes a new instance of the NotificationList(T) class that is empty.
        /// </summary>
        public NotificationList()
        {
            m_data = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the NotificationList(T) class that contains the items in the array.
        /// </summary>
        /// <param name="items">Adds items to the NotificationList(T).</param>
        public NotificationList(IEnumerable<T> items)
        {
            m_data = new List<T>();
            foreach (T item in items)
            {
                Add(item);
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the NotificationList(T) class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new array can initially store.</param>
        public NotificationList(int capacity)
        {
            m_data = new List<T>(capacity);
        }

        List<T> InnerList
        {
            get
            {
                return m_data;
            }
        }

        /// <summary>
        /// States if the NotificationList(T) is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return Count <= 0; }
        }


        /// <summary>
        /// Gets the number of elements actually contained in the NotificationList(T).
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
                if (InnerList.Capacity == value)
                {
                    return;
                }
                InnerList.Capacity = value;
                OnPropertyChanged("Capacity");
            }
        }

        /// <summary>
        /// Adds an object to the end of the NotificationList(T).
        /// </summary>
        /// <param name="item">The item to add to the end of the NotificationList(T).</param>
        public void Add(T item)
        {
            OnInsert(item, Count);
            InnerList.Add(item);
            OnInsertComplete(item, Count - 1);
        }

        /// <summary>
        /// Checks to see if the item is present in the NotificationList(T).
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
        /// Clears all values from the NotificationList(T).
        /// </summary>
        public void Clear()
        {
            var removed = ToArray();
            OnClear(removed);
            InnerList.Clear();
            OnClearComplete(removed);
        }

        /// <summary>
        /// Removes the first occurrence of the specified item from the NotificationList(T).
        /// </summary>
        /// <param name="item">The item to remove from the NotificationList(T).</param>
        /// <returns>True if an item was removed, false otherwise.</returns>
        public bool Remove(T item)
        {
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

            T item = InnerList[index];

            OnRemove(item, index);

            InnerList.RemoveAt(index);

            OnRemoveComplete(item, index);
        }

        /// <summary>
        /// Inserts an item into the NotificationList(T) at the specified index.
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
        /// Gets or sets an element in the NotificationList(T).
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
        /// Copies the elements of the NotificationList(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the NotificationList(T).</returns>
        public T[] ToArray()
        {
            return InnerList.ToArray();
        }
    }
}
