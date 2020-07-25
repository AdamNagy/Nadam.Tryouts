using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DevGuideToCollections
{
    public partial class WinFormsBindingListView<T> : IBindingListView
    {
        string m_filter;
        FilterParser.FilterNode m_filterRoot;
        bool m_isFiltering;

        #region Filtering Support 

        bool IsFiltering
        {
            get { return m_isFiltering; }
        }

        public string Filter
        {
            get
            {
                return m_filter;
            }
            set
            {
                if (m_filter == value)
                {
                    return;
                }

                m_filterRoot = FilterParser.Parse(value);
                m_filter = value;

                if (!string.IsNullOrEmpty(value))
                {
                    // Something needs to be filtered

                    if (!m_isFiltering)
                    {
                        ApplyFilter();
                    }
                    else
                    {
                        ReapplyFilter();
                    }
                }
                else
                {
                    RemoveFilterInternal();
                }
            }
        }

        void ApplyFilter()
        {
            if (m_isFiltering)
            {
                return;
            }

            if (m_originalList == null)
            {
                m_originalList = m_data;
                m_data = new List<T>();
            }

            m_isFiltering = true;

            ReapplyFilter();
        }

        bool IsFiltered(T item)
        {
            if (m_filterRoot == null)
            {
                return false;
            }

            return m_filterRoot.Eval(item);
        }

        void ReapplyFilter()
        {
            if (!IsFiltering)
            {
                return;
            }

            m_data.Clear();
            foreach (T item in m_originalList)
            {
                if (IsFiltered(item))
                {
                    m_data.Add(item);
                }
            }

            if (IsSorted)
            {
                Sort();
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        void RemoveFilterInternal()
        {
            if (!m_isFiltering)
            {
                return;
            }

            if (IsSorted)
            {
                m_data = new List<T>(m_originalList);
                Sort();
            }
            else 
            {
                m_data = m_originalList;
                m_originalList = null;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            m_isFiltering = false;
        }

        public void RemoveFilter()
        {
            if (!SupportsAdvancedSorting)
            {
                throw new NotSupportedException();
            }

            Filter = String.Empty;
        }

        public bool SupportsFiltering
        {
            get { return true; }
        }
        
        #endregion

        #region Sorting Support

        void SaveUnsortedList(bool advance)
        {
            if (!IsSorted)
            {
                if (m_originalList == null)
                {
                    m_originalList = m_data;
                    m_data = new List<T>(m_originalList);
                }
            }
        }


        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            if (!SupportsAdvancedSorting)
            {
                throw new NotSupportedException();
            }

            SaveUnsortedList(true);

            m_sortDescriptors.Clear();
            foreach (ListSortDescription sort in sorts)
            {
                m_sortDescriptors.Add(sort);
            }
            IsSorted = true;
            Sort();
        }

        public ListSortDescriptionCollection SortDescriptions
        {
            get { return new ListSortDescriptionCollection(m_sortDescriptors.ToArray()); }
        }

        public bool SupportsAdvancedSorting
        {
            get { return true; }
        }

        #endregion
    }
}
