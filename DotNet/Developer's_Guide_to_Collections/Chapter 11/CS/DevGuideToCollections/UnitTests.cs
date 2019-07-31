using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace DevGuideToCollections
{
    public static class UnitTests
    {
        static object s_collectionChanged_Sender;
        static NotifyCollectionChangedEventArgs s_collectionChanged_EventArgs;
        static object s_propertyChanged_Sender;
        static List<PropertyChangedEventArgs> s_propertyChanged_EventArgs = new List<PropertyChangedEventArgs>();

        static void NotificationList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            s_collectionChanged_Sender = sender;
            s_collectionChanged_EventArgs = e;
        }

        static void NotificationList_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            s_propertyChanged_Sender = sender;
            s_propertyChanged_EventArgs.Add(e);
        }

        static void ClearNotifications()
        {
            s_collectionChanged_Sender = null;
            s_collectionChanged_EventArgs = default(NotifyCollectionChangedEventArgs);

            s_propertyChanged_EventArgs.Clear();
        }

        static void CheckPropertyChanges()
        {
            bool foundCount = false;
            bool foundItem = false;
            foreach (var e in s_propertyChanged_EventArgs)
            {
                if (e.PropertyName == "Count")
                {
                    foundCount = true;
                }
                else if (e.PropertyName == "Item[]")
                {
                    foundItem = true;
                }
            }

            Debug.Assert(foundCount && foundItem);
        }

        static void TestAdd<T>(NotificationList<T> list, T item)
        {
            int count = list.Count;

            list.Add(item);

            Debug.Assert(s_collectionChanged_Sender == list);
            Debug.Assert(list.Count == count + 1);
            Debug.Assert(s_collectionChanged_EventArgs.Action == NotifyCollectionChangedAction.Add);

            Debug.Assert(Comparer<T>.Default.Compare(item, list[count]) == 0);
            Debug.Assert(Comparer<T>.Default.Compare(item, (T)s_collectionChanged_EventArgs.NewItems[0]) == 0);
            Debug.Assert(s_collectionChanged_EventArgs.NewStartingIndex == count);

            CheckPropertyChanges();

            ClearNotifications();
        }

        static void TestClear<T>(NotificationList<T> list)
        {
            T[] array = list.ToArray();

            list.Clear();

            Debug.Assert(s_collectionChanged_Sender == list);
            Debug.Assert(list.Count == 0);
            Debug.Assert(s_collectionChanged_EventArgs.Action == NotifyCollectionChangedAction.Reset);

            NotifyCollectionClearedEventArgs e = (NotifyCollectionClearedEventArgs)s_collectionChanged_EventArgs;
            Debug.Assert(array.Length == e.ClearedItems.Length);
            for (int i = 0; i < array.Length; ++i)
            {
                Debug.Assert(Comparer<T>.Default.Compare(array[i], (T)e.ClearedItems[i]) == 0);
            }

            CheckPropertyChanges();

            ClearNotifications();
        }

        static void TestRemove<T>(NotificationList<T> list, T item)
        {
            int count = list.Count;

            int index = list.IndexOf(item);

            list.Remove(item);

            Debug.Assert(s_collectionChanged_Sender == list);
            Debug.Assert(list.Count == count - 1);
            Debug.Assert(s_collectionChanged_EventArgs.Action == NotifyCollectionChangedAction.Remove);

            Debug.Assert(Comparer<T>.Default.Compare(item, (T)s_collectionChanged_EventArgs.OldItems[0]) == 0);
            Debug.Assert(s_collectionChanged_EventArgs.OldStartingIndex == index);

            CheckPropertyChanges();

            ClearNotifications();
        }

        static void TestNotificationList()
        {
            NotificationList<int> list = new NotificationList<int>();

            list.CollectionChanged += new NotifyCollectionChangedEventHandler(NotificationList_CollectionChanged);
            list.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(NotificationList_PropertyChanged);

            TestAdd<int>(list, 22);
            TestAdd<int>(list, 45);
            TestAdd<int>(list, 1);

            TestRemove<int>(list, 45);
            TestClear<int>(list);

            TestAdd<int>(list, 88);

            TestRemove<int>(list, 88);

        }

        public static void RunTests()
        {
            try
            {
                TestNotificationList();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }
    }
}
