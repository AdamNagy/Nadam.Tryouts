using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace DevGuideToCollections
{
    public static class UnitTests
    {
        public class TestData : INotifyPropertyChanged, INotifyPropertyChanging
        {
            public event PropertyChangingEventHandler PropertyChanging;
            public event PropertyChangedEventHandler PropertyChanged;

            string m_value;
            bool m_flag;

            public string Value 
            {
                get { return m_value; }
                set
                {
                    if (m_value == value)
                    {
                        return;
                    }

                    NotifyPropertyChanging("Value");
                    m_value = value;
                    NotifyPropertyChanged("Value");
                }
            }

            public bool Flag
            {
                get { return m_flag; }
                set
                {
                    if (m_flag == value)
                    {
                        return;
                    }

                    NotifyPropertyChanging("Flag");
                    m_flag = value;
                    NotifyPropertyChanged("Flag");
                }
            }

            void NotifyPropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }

            void NotifyPropertyChanging(string property)
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(property));
                }
            }
        }

        static void TestWinFormsBindingListView()
        {
            TestBindingList(new WinFormsBindingListView<TestData>());
        }

        static void TestWinFormsBindingList()
        {
            WinFormsBindingList<int> t = new WinFormsBindingList<int>();

            int j = (int)t.AddNew();

            TestBindingList(new WinFormsBindingList<TestData>());
        }

        static void TestBindingList(IBindingList list)
        {
            List<string> sortedvalues = new List<string>();

            var properties = TypeDescriptor.GetProperties(typeof(TestData));
            var valueProperty = properties.Find("Value", true);

            for (int i = (int)'A'; i <= (int)'Z'; ++i)
            {
                sortedvalues.Add(((char)i).ToString());
            }

            Random rnd = new Random();
            List<string> tmpvalues = new List<string>(sortedvalues);

            while (tmpvalues.Count > 0)
            {
                int index = rnd.Next(tmpvalues.Count);
                list.Add(new TestData() { Value = tmpvalues[index] });
                tmpvalues.RemoveAt(index);
            }

            Debug.Assert(list.Count == sortedvalues.Count);

            // Make sure the list isn't sorted
            Debug.Assert(!IsSorted(list, System.ComponentModel.ListSortDirection.Ascending) && !IsSorted(list, System.ComponentModel.ListSortDirection.Descending));

            // Check Asscending sort
            ApplySort(list, valueProperty, System.ComponentModel.ListSortDirection.Ascending);

            // Check Descending sort
            ApplySort(list, valueProperty, System.ComponentModel.ListSortDirection.Descending);

            // Remove sort
            RemoveSort(list);

            // Check adding items to a sorted list
            ApplySort(list, valueProperty, System.ComponentModel.ListSortDirection.Ascending);
            TestData tdZA = new TestData() { Value = "ZA" };
            TestData td9 = new TestData() { Value = "9" };
            list.Add(tdZA);
            list.Add(td9);
            Debug.Assert(IsSorted(list, System.ComponentModel.ListSortDirection.Ascending));
            Debug.Assert(((IList<TestData>)list)[0].Value == "9");
            Debug.Assert(((IList<TestData>)list)[list.Count - 1].Value == "ZA");

            // Test changing the sorted property
            td9.Value = "ZZ";
            Debug.Assert(IsSorted(list, System.ComponentModel.ListSortDirection.Ascending));
            Debug.Assert(((IList<TestData>)list)[list.Count - 1].Value == "ZZ");
            Debug.Assert(list.Count - 2 == sortedvalues.Count);

            // Test changing the sorted property back to the original value
            td9.Value = "9";
            Debug.Assert(IsSorted(list, System.ComponentModel.ListSortDirection.Ascending));
            Debug.Assert(((IList<TestData>)list)[0].Value == "9");
            Debug.Assert(list.Count - 2 == sortedvalues.Count);

            // Remove sort and make sure items are at the end of the list
            RemoveSort(list);
            Debug.Assert(((IList<TestData>)list)[list.Count - 2].Value == "ZA");
            Debug.Assert(((IList<TestData>)list)[list.Count - 1].Value == "9");

            // Check adding items to a descending sorted list
            ApplySort(list, valueProperty, System.ComponentModel.ListSortDirection.Descending);
            TestData tdZB = new TestData() { Value = "ZB" };
            TestData td8 = new TestData() { Value = "8" };
            list.Add(tdZB);
            list.Add(td8);
            Debug.Assert(IsSorted(list, System.ComponentModel.ListSortDirection.Descending));
            Debug.Assert(((IList<TestData>)list)[0].Value == "ZB");
            Debug.Assert(((IList<TestData>)list)[list.Count - 1].Value == "8");

            // Remove sort and make sure items are at the end of the list
            RemoveSort(list);
            Debug.Assert(((IList<TestData>)list)[list.Count - 2].Value == "ZB");
            Debug.Assert(((IList<TestData>)list)[list.Count - 1].Value == "8");

            // Check removing items from a sorted list
            ApplySort(list, valueProperty, System.ComponentModel.ListSortDirection.Ascending);
            list.Remove(tdZA);
            list.Remove(td9);
            Debug.Assert(list.SortDirection == System.ComponentModel.ListSortDirection.Ascending);
            Debug.Assert(list.Count - 2 == sortedvalues.Count);

            // Make sure we cannot do a removeat from a sorted list
            try
            {
                list.RemoveAt(2);
                Debug.Assert(false);
            }
            catch (NotSupportedException)
            {
                Debug.Assert(list.Count - 2 == sortedvalues.Count);
            }

            // Make sure we cannot do a insert on a sorted list
            try
            {
                list.Insert(3, tdZA);
                Debug.Assert(false);
            }
            catch (NotSupportedException)
            {
                Debug.Assert(list.Count - 2 == sortedvalues.Count);
            }

            // Remove sorting
            RemoveSort(list);


            // Turn on indexing
            list.AddIndex(valueProperty);
            VerifyIndexing(list);

            // Add some items to indexing
            list.Add(tdZA);
            VerifyIndexing(list);
            Debug.Assert(list.Count - 3 == sortedvalues.Count);
            list.Add(td9);
            VerifyIndexing(list);
            Debug.Assert(list.Count - 4 == sortedvalues.Count);

            // Add a duplicate
            list.Add(tdZA);
            VerifyIndexing(list);
            Debug.Assert(list.Count - 5 == sortedvalues.Count);

            // Remove items
            list.Remove(tdZA);
            list.Remove(tdZA);
            list.Remove(td9);
            VerifyIndexing(list);
            Debug.Assert(list.Count - 2 == sortedvalues.Count);

            // Test out reindexing
            list.AddIndex(valueProperty);
            VerifyIndexing(list);
            td8.Value = "ZZ";
            VerifyIndexing(list);

            // Test clearing a list while it is sorted
            ApplySort(list, valueProperty, ListSortDirection.Ascending);
            Debug.Assert(list.Count > 0);
            list.Clear();
            Debug.Assert(list.Count == 0);
            RemoveSort(list);
            Debug.Assert(list.Count == 0);
        }

        static void VerifyIndexing(IBindingList list)
        {
            if (list is WinFormsBindingList<TestData>)
            {
                ((WinFormsBindingList<TestData>)list).VerifyIndexing();
            }
            else
            {
                ((WinFormsBindingListView<TestData>)list).VerifyIndexing();
            }
        }

        static void RemoveSort(IBindingList list)
        {
            list.RemoveSort();
            Debug.Assert(list.SortProperty == null);
            if (list.Count == 0)
            {
                return;
            }
            Debug.Assert(!IsSorted(list, System.ComponentModel.ListSortDirection.Ascending) && !IsSorted(list, System.ComponentModel.ListSortDirection.Descending));
        }

        static void ApplySort(IBindingList list, PropertyDescriptor property, System.ComponentModel.ListSortDirection direction)
        {
            list.ApplySort(property, direction);
            Debug.Assert(list.SortProperty.Name == property.Name);
            Debug.Assert(list.SortDirection == direction);
            if (list.Count == 0)
            {
                return;
            }
            Debug.Assert(IsSorted(list, direction));
        }

        static bool IsSorted(IBindingList list, System.ComponentModel.ListSortDirection direction)
        {
            bool wrongdirection = false;

            int notexpected = direction == System.ComponentModel.ListSortDirection.Descending ? -1 : 1;

            for (int i = 0; i < list.Count - 1; ++i)
            {
                if (string.Compare(((TestData)list[i]).Value, ((TestData)list[i + 1]).Value) == notexpected)
                {
                    wrongdirection = true;
                    break;
                }
            }

            return !wrongdirection;
        }

        static void TestFilterParser()
        {
            try
            {
                TestData data = new TestData();
                FilterParser.FilterNode tmp;

                var idEqual2 = FilterParser.Parse("Id = 2");
                Debug.Assert(idEqual2 != null);
                Debug.Assert(((FilterParser.ExpressionFilterNode)idEqual2).Property == "Id");
                Debug.Assert(((FilterParser.ExpressionFilterNode)idEqual2).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)idEqual2).Value == "2");

                tmp = FilterParser.Parse("Id < 2");
                Debug.Assert(tmp != null);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Property == "Id");
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).ComparisonOperator == FilterParser.ComparisonOperator.LessThan);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Value == "2");

                tmp = FilterParser.Parse("Id <= 2");
                Debug.Assert(tmp != null);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Property == "Id");
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).ComparisonOperator == FilterParser.ComparisonOperator.LessThanEqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Value == "2");

                tmp = FilterParser.Parse("Id > 2");
                Debug.Assert(tmp != null);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Property == "Id");
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).ComparisonOperator == FilterParser.ComparisonOperator.GreaterThan);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Value == "2");

                tmp = FilterParser.Parse("[Id] <> 2");
                Debug.Assert(tmp != null);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Property == "Id");
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).ComparisonOperator == FilterParser.ComparisonOperator.NotEqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)tmp).Value == "2");
                
                var nameEqualSam = FilterParser.Parse("Name = 'Sam'");
                Debug.Assert(nameEqualSam != null);
                Debug.Assert(((FilterParser.ExpressionFilterNode)nameEqualSam).Property == "Name");
                Debug.Assert(((FilterParser.ExpressionFilterNode)nameEqualSam).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)nameEqualSam).Value == "Sam");

                FilterParser.LogicalFilterNode root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Name = 'Sam' AND Tmp = 4");
                Debug.Assert(root != null);
                Debug.Assert(root.LogicalOperator == FilterParser.LogicalOperator.And);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).Property == "Name");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).Value == "Sam");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).Property == "Tmp");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).Value == "4");

                root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Tmp = 4 OR Name = 'Sam'");
                Debug.Assert(root != null);
                Debug.Assert(root.LogicalOperator == FilterParser.LogicalOperator.Or);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).Property == "Name");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).Value == "Sam");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).Property == "Tmp");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).Value == "4");

                root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Not Flag");
                Debug.Assert(root != null);
                Debug.Assert(root.LogicalOperator == FilterParser.LogicalOperator.Not);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Right).Property == "Flag");
                data.Flag = true;
                Debug.Assert(root.Eval(data) == !data.Flag);
                data.Flag = false;
                Debug.Assert(root.Eval(data) == !data.Flag);

                root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Value = '4' AND Not Flag");
                Debug.Assert(root != null);
                Debug.Assert(root.LogicalOperator == FilterParser.LogicalOperator.And);
                var notOperator = (FilterParser.LogicalFilterNode)root.Right;
                Debug.Assert(notOperator.LogicalOperator == FilterParser.LogicalOperator.Not);
                Debug.Assert(((FilterParser.ExpressionFilterNode)notOperator.Right).Property == "Flag");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).Property == "Value");
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).ComparisonOperator == FilterParser.ComparisonOperator.EqualTo);
                Debug.Assert(((FilterParser.ExpressionFilterNode)root.Left).Value == "4");
                data.Value = "4";
                data.Flag = false;
                Debug.Assert(root.Eval(data));
                data.Value = "4";
                data.Flag = true;
                Debug.Assert(!root.Eval(data));
                data.Value = "3";
                data.Flag = false;
                Debug.Assert(!root.Eval(data));
                data.Value = "3";
                data.Flag = true;
                Debug.Assert(!root.Eval(data));
                root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Value = '4' OR Not Flag");
                data.Value = "3";
                data.Flag = false;
                Debug.Assert(root.Eval(data));
                data.Value = "4";
                data.Flag = true;
                Debug.Assert(root.Eval(data));

                root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Value = '4' OR Value = '1' OR Value = '2'");
                data.Value = "4";
                Debug.Assert(root.Eval(data));
                data.Value = "2";
                Debug.Assert(root.Eval(data));
                data.Value = "1";
                Debug.Assert(root.Eval(data));
                data.Value = "0";
                Debug.Assert(!root.Eval(data));

                root = (FilterParser.LogicalFilterNode)FilterParser.Parse("Value = '4' OR Not Flag OR Value = '2'");
                data.Value = "4";
                data.Flag = true;
                Debug.Assert(root.Eval(data));
                data.Value = "2";
                data.Flag = true;
                Debug.Assert(root.Eval(data));
                data.Value = "1";
                data.Flag = false;
                Debug.Assert(root.Eval(data));
                data.Value = "1";
                data.Flag = true;
                Debug.Assert(!root.Eval(data));
                

                data.Value = "6";
                tmp = FilterParser.Parse("Value = '6'");
                Debug.Assert(tmp.Eval(data));
                tmp = FilterParser.Parse("Value <= '6'");
                Debug.Assert(tmp.Eval(data));
                tmp = FilterParser.Parse("Value <= '8'");
                Debug.Assert(tmp.Eval(data));
                tmp = FilterParser.Parse("Value >= '6'");
                Debug.Assert(tmp.Eval(data));
                tmp = FilterParser.Parse("Value <= '4'");
                Debug.Assert(!tmp.Eval(data));
                tmp = FilterParser.Parse("Value >= '4'");
                Debug.Assert(tmp.Eval(data));
                tmp = FilterParser.Parse("Value >= '8'");
                Debug.Assert(!tmp.Eval(data));

                tmp = FilterParser.Parse("Value <> '4'");
                Debug.Assert(tmp.Eval(data));

                tmp = FilterParser.Parse("Value > '5'");
                Debug.Assert(tmp.Eval(data));

                tmp = FilterParser.Parse("Value < '8'");
                Debug.Assert(tmp.Eval(data));
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }

        public static void RunTests()
        {
            TestWinFormsBindingList();
            TestWinFormsBindingListView();
            TestFilterParser();
        }
    }
}
