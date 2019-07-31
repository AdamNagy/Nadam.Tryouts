using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace DevGuideToCollections
{
    public class NotifyCollectionClearedEventArgs : NotifyCollectionChangedEventArgs
    {
        object[] m_clearedItems;

        public NotifyCollectionClearedEventArgs(object[] clearedItems)
            : base(NotifyCollectionChangedAction.Reset)
        {
            m_clearedItems = clearedItems;
        }

        public object[] ClearedItems
        {
            get { return m_clearedItems; }
        }
    }
}
