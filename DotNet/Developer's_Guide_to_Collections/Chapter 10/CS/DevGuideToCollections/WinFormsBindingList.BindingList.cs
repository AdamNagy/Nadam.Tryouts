using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DevGuideToCollections
{
    public partial class WinFormsBindingList<T> : IBindingList, ICancelAddNew 
    {
        public event ListChangedEventHandler ListChanged;

        ListSortDirection m_sortDirection;
        bool m_isSorted;
        PropertyDescriptor m_sortDescriptor;
        List<IndexData> m_indexes;
        List<T> m_originalList;
        bool m_supportsPropertyChanging;
        bool m_supportsPropertyChanged;
        bool m_canIndex;
        int ?m_newIndex;

        struct IndexData
        {
            public PropertyDescriptor PropertyDescriptor;
            public Dictionary<object, List<int>> Indexes;
        }

        void InitializeBindingSupport()
        {
            m_supportsPropertyChanged = typeof(T).GetInterface("INotifyPropertyChanged") != null;
            m_supportsPropertyChanging = typeof(T).GetInterface("INotifyPropertyChanging") != null;
            m_newIndex = null;
            m_canIndex = m_supportsPropertyChanged && m_supportsPropertyChanging;
        }

        #region Properties

        public bool AllowEdit
        {
            get { return m_supportsPropertyChanged; }
        }

        public bool AllowNew
        {
            get 
            { 
                System.Collections.IList list = this as System.Collections.IList;

                return !list.IsFixedSize && !list.IsReadOnly; 
            }
        }

        public bool AllowRemove
        {
            get
            {
                System.Collections.IList list = this as System.Collections.IList;

                return !list.IsFixedSize && !list.IsReadOnly;
            }
        }

        public bool SupportsChangeNotification
        {
            get { return true; }
        }

        public bool SupportsSearching
        {
            get { return true; }
        }


        #region Sort Properties

        public bool IsSorted
        {
            get
            {
                if (SupportsSorting)
                {
                    return m_isSorted;
                }

                throw new NotSupportedException();
            }
            private set
            {
                m_isSorted = value;
            }
        }

        public ListSortDirection SortDirection
        {
            get
            {
                if (SupportsSorting)
                {
                    return m_sortDirection;
                }

                throw new NotSupportedException();
            }
        }

        public PropertyDescriptor SortProperty
        {
            get
            {
                if (SupportsSorting)
                {
                    return m_sortDescriptor;
                }

                throw new NotSupportedException();
            }
        }

        public bool SupportsSorting
        {
            get { return true; }
        }

        #endregion

        #endregion
        
        public object AddNew()
        {
            if (!AllowNew)
            {
                throw new InvalidOperationException();
            }

            T retval = Activator.CreateInstance<T>();

            Add(retval);

            m_newIndex = Count - 1;

            return retval;
        }

        #region Search Support

        void UnRegisterForPropertyChanges(T item)
        {
            // No need to register for property changes if none of the IBindingList features are supported
            if ((!m_supportsPropertyChanged && !m_supportsPropertyChanging) || (!SupportsSorting && !SupportsChangeNotification && !SupportsSearching))
            {
                return;
            }

            INotifyPropertyChanged changed = item as INotifyPropertyChanged;
            INotifyPropertyChanging changing = item as INotifyPropertyChanging;

            if (changing != null && SupportsSearching)
            {
                changing.PropertyChanging -= new PropertyChangingEventHandler(T_OnPropertyChanging);
            }
            if (changed != null)
            {
                changed.PropertyChanged -= new PropertyChangedEventHandler(T_OnPropertyChanged);
            }
        }

        void RegisterForPropertyChanges(T item)
        {
            // No need to register for property changes if none of the IBindingList features are supported
            if ((!m_supportsPropertyChanged && !m_supportsPropertyChanging) || (!SupportsSorting && !SupportsChangeNotification && !SupportsSearching))
            {
                return;
            }

            INotifyPropertyChanged changed = item as INotifyPropertyChanged;
            INotifyPropertyChanging changing = item as INotifyPropertyChanging;

            if (changing != null && SupportsSearching)
            {
                changing.PropertyChanging += new PropertyChangingEventHandler(T_OnPropertyChanging);
            }
            if (changed != null)
            {
                changed.PropertyChanged += new PropertyChangedEventHandler(T_OnPropertyChanged);
            }
        }

        void AddIndexData(IndexData data, object value, int index)
        {
            List<int> indexes = null;

            if (data.Indexes.ContainsKey(value))
            {
                indexes = data.Indexes[value];
            }
            else
            {
                indexes = new List<int>();
                data.Indexes[value] = indexes;
            }

            if (!indexes.Contains(index))
            {
                indexes.Add(index);
            }
        }

        void RemoveIndexData(IndexData data, object value, int index)
        {
            List<int> indexes = null;

            if (data.Indexes.ContainsKey(value))
            {
                indexes = data.Indexes[value];
            }
            else
            {
                return;
            }

            if (indexes.Contains(index))
            {
                indexes.Remove(index);
            }
        }

        void T_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsSorted && e.PropertyName == SortProperty.Name)
            {
                Sort();
            }

            if (m_indexes == null)
            {
                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, IndexOf((T)sender)));
                return;
            }

            foreach (var data in m_indexes)
            {
                if (data.PropertyDescriptor.Name == e.PropertyName)
                {
                    try
                    {
                        int index = IndexOf((T)sender);

                        if (index < 0)
                        {
                            return;
                        }

                        object value = data.PropertyDescriptor.GetValue(sender);

                        AddIndexData(data, value, index);
                    }
                    catch
                    {
                    }

                    break;
                }
            }

            var properties = TypeDescriptor.GetProperties(typeof(T));
            var prop = properties.Find(e.PropertyName, true);

            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, IndexOf((T)sender), prop));
        }

        void T_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (m_indexes == null)
            {
                return;
            }

            foreach (var data in m_indexes)
            {
                if (data.PropertyDescriptor.Name == e.PropertyName)
                {
                    try
                    {
                        int index = IndexOf((T)sender);

                        if (index < 0)
                        {
                            return;
                        }

                        object value = data.PropertyDescriptor.GetValue(sender);

                        RemoveIndexData(data, value, index);
                    }
                    catch
                    {
                    }

                    break;
                }
            }
        }

        public int Find(string propertyName, object key)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var prop = properties.Find(propertyName, true);

            if (prop == null)
            {
                return -1;
            }
            else
            {
                return Find(prop, key);
            }
        }

        public int Find(PropertyDescriptor property, object key)
        {
            if (!SupportsSearching)
            {
                throw new NotSupportedException();
            }

            IndexData? data = FindIndexData(property);

            // See if the property has been indexed
            if (data.HasValue)
            {
                if (data.Value.Indexes.ContainsKey(key))
                {
                    var indexes = data.Value.Indexes[key];
                    if (indexes.Count > 0)
                    {
                        return indexes[0];
                    }
                }

                return -1;
            }

            // Find the key by iterating over every element
            for (int i = 0; i < Count; ++i)
            {
                T item = m_data[i];

                try
                {
                    object value = property.GetValue(item);

                    if (System.Collections.Comparer.Default.Compare(value, key) == 0)
                    {
                        return i;
                    }
                }
                catch
                {
                }
            }

            return -1;
        }

        IndexData ?FindIndexData(PropertyDescriptor property)
        {
            if (m_indexes == null)
            {
                return null;
            }

            foreach (var data in m_indexes)
            {
                if (data.PropertyDescriptor == property)
                {
                    return data;
                }
            }

            return null;
        }

        public bool VerifyIndexing()
        {
            if (!SupportsSearching || !m_canIndex)
            {
                return false;
            }

            if (m_indexes == null)
            {
                return Count <= 0;
            }

            foreach (var indexData in m_indexes)
            {
                int count = 0;

                List<int> itemsNotChecked = new List<int>();
                for (int i = 0; i < Count; ++i)
                {
                    itemsNotChecked.Add(i);
                }

                foreach (var kvp in indexData.Indexes)
                {
                    // Make sure the current value equals the value that is stored as the indexed value
                    foreach (var itemIndex in kvp.Value)
                    {
                        T item = InnerList[itemIndex];

                        if (!itemsNotChecked.Contains(itemIndex))
                        {
                            return false;
                        }

                        itemsNotChecked.Remove(itemIndex);

                        object actualPropertyValue = indexData.PropertyDescriptor.GetValue(item);

                        if (System.Collections.Comparer.Default.Compare(actualPropertyValue, kvp.Key) != 0)
                        {
                            return false;
                        }
                    }

                    count += kvp.Value.Count;
                }

                // Make sure every value is indexed
                if (count != Count || itemsNotChecked.Count > 0)
                {
                    return false;
                }
            }


            return true;
        }

        public void AddIndex(string propertyName)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var prop = properties.Find(propertyName, true);

            if (prop != null)
            {
                AddIndex(prop);
            }
        }

        public void AddIndex(PropertyDescriptor property)
        {
            IndexData ?data = FindIndexData(property);

            if (!data.HasValue)
            {
                if (m_indexes == null)
                {
                    m_indexes = new List<WinFormsBindingList<T>.IndexData>();
                }

                m_indexes.Add
                    (
                        new IndexData()
                        {
                            Indexes = new Dictionary<object,List<int>>(),
                            PropertyDescriptor = property
                        }
                    );
            }

            ReIndex();
        }

        public void RemoveIndex(string propertyName)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var prop = properties.Find(propertyName, true);

            if (prop != null)
            {
                RemoveIndex(prop);
            }
        }
        
        public void RemoveIndex(PropertyDescriptor property)
        {
            if (m_indexes == null)
            {
                return;
            }

            IndexData? data = FindIndexData(property);

            if (data.HasValue)
            {
                m_indexes.Remove(data.Value);
            }

            ReIndex();
        }

        void ReIndex()
        {
            if (m_indexes == null)
            {
                return;
            }

            // Remove the old indexes
            foreach (var index in m_indexes)
            {
                index.Indexes.Clear();
            }

            if (m_indexes.Count == 0 || !SupportsSearching || !m_canIndex)
            {
                return;
            }

            // Iterate over each item and add the index to the collection
            for (int i = 0; i < Count; ++i)
            {
                T item = m_data[i];

                foreach (var data in m_indexes)
                {
                    try
                    {
                        object value = data.PropertyDescriptor.GetValue(item);

                        AddIndexData(data, value, i);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion

        #region Sort Support

        public void ApplySort(string propertyName, ListSortDirection direction)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var prop = properties.Find(propertyName, true);

            if (prop != null)
            {
                ApplySort(prop, direction);
            }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            if (!SupportsSorting)
            {
                throw new NotSupportedException();
            }

            if (!IsSorted)
            {
                if (m_originalList == null)
                {
                    m_originalList = m_data;
                    m_data = new List<T>(m_originalList);
                }
            }
            m_sortDirection = direction;
            m_sortDescriptor = property;
            IsSorted = true;
            Sort();
        }

        public void RemoveSort()
        {
            if (!SupportsSorting)
            {
                throw new NotSupportedException();
            }
            m_sortDescriptor = null;
            IsSorted = false;
            if (m_originalList != null)
            {
                m_data = m_originalList;
                m_originalList = null;
            }
            if (SupportsSearching)
            {
                ReIndex();
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        void Sort()
        {
            if (m_sortDescriptor == null)
            {
                return;
            }
            
            m_data.Sort(
                new LambdaComparer<T>
                    (
                        (x, y) =>
                        {
                            object xValue = m_sortDescriptor.GetValue(x);
                            object yValue = m_sortDescriptor.GetValue(y);

                            if (m_sortDirection == ListSortDirection.Descending)
                            {
                                return System.Collections.Comparer.Default.Compare(xValue, yValue) * -1;
                            }

                            return System.Collections.Comparer.Default.Compare(xValue, yValue);
                        }
                    ));

            if (SupportsSearching)
            {
                ReIndex();
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        #endregion

        void OnListChanged(ListChangedEventArgs e)
        {
            if (!SupportsChangeNotification)
            {
                return;
            }

            if (ListChanged != null)
            {
                ListChanged(this, e);
            }
        }

        void OnSet(int index, T oldValue, T newValue)
        {
            if (IsSorted)
            {
                throw new NotSupportedException("You cannot set items in a sorted list");
            }
        }

        void OnSetComplete(int index, T oldValue, T newValue)
        {
            
            UnRegisterForPropertyChanges(oldValue);
            RegisterForPropertyChanges(newValue);

            if (SupportsSearching)
            {
                ReIndex();
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        void OnClear(T[] itemsRemoved)
        {
        }

        void OnClearComplete(T[] itemsRemoved)
        {
            foreach (var item in itemsRemoved)
            {
                UnRegisterForPropertyChanges(item);
            }

            if (SupportsSearching)
            {
                ReIndex();
            }

            if (m_originalList != null)
            {
                m_originalList.Clear();
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        void OnInsert(T item, int index)
        {
            // You can only add to the end of the list when sorting is on
            if (IsSorted && index != Count)
            {
                throw new NotSupportedException();
            }
        }

        void OnInsertComplete(T item, int index)
        {
            RegisterForPropertyChanges(item);
            if (IsSorted)
            {
                Sort();
            }
            else
            {
                if (SupportsSearching)
                {
                    ReIndex();
                }

                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            }

            if (m_originalList != null)
            {
                m_originalList.Insert(index, item);
            }
        }

        void OnRemove(T item, int index)
        {
        }

        void OnRemoveComplete(T item, int index)
        {
            UnRegisterForPropertyChanges(item);
            if (SupportsSearching)
            {
                ReIndex();
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));

            if (m_originalList != null)
            {
                m_originalList.Remove(item);
            }
        }


        #region ICancelAddNew Members

        public void CancelNew(int itemIndex)
        {
            if (m_newIndex.HasValue && m_newIndex.Value == itemIndex)
            {
                RemoveAt(itemIndex);
            }
        }

        public void EndNew(int itemIndex)
        {
            if (m_newIndex.HasValue && m_newIndex.Value == itemIndex)
            {
                m_newIndex = null;
            }
        }

        #endregion
    }
}
