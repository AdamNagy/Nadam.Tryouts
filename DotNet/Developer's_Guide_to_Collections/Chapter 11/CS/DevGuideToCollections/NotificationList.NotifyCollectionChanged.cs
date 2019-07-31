using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace DevGuideToCollections
{
    public partial class NotificationList<T> : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        void OnSet(int index, T oldValue, T newValue)
        {
        }

        void OnSetComplete(int index, T oldValue, T newValue)
        {
            OnPropertyChanged("Item[]");
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldValue, newValue, index));
        }

        void OnClear(T[] itemsRemoved)
        {
        }

        void OnClearComplete(T[] itemsRemoved)
        {
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");

            object[] items = null;

            if (itemsRemoved != null)
            {
                items = new object[itemsRemoved.Length];

                for (int i = 0; i < itemsRemoved.Length; ++i)
                {
                    items[i] = itemsRemoved[i];
                }
            }

            OnCollectionChanged(new NotifyCollectionClearedEventArgs(items));
        }

        void OnInsert(T item, int index)
        {
        }

        void OnInsertComplete(T item, int index)
        {
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        void OnRemove(T item, int index)
        {
        }

        void OnRemoveComplete(T item, int index)
        {
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

    }
}
