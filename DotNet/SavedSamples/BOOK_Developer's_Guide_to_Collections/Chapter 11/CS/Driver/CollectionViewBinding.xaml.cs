using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;

namespace Driver
{
    /// <summary>
    /// Interaction logic for CollectionViewWindow.xaml
    /// </summary>
    public partial class CollectionViewBinding : Window
    {
        public CollectionViewBinding()
        {
            InitializeComponent();

            List<PropertyDescriptor> filterDescriptors = new List<PropertyDescriptor>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(Company)))
            {
                if (pd.Converter.CanConvertFrom(typeof(string)))
                {
                    filterDescriptors.Add(pd);
                }
            }

            FilterProperty.ItemsSource = filterDescriptors;
            FilterProperty.DisplayMemberPath = "Name";

            ExampleListView1.ItemsSource = new ListCollectionView(DL.GetDataSource());
            ExampleListView2.ItemsSource = new ListCollectionView(DL.GetDataSource());
            ExampleListView3.ItemsSource = new BindingListCollectionView(DL.GetDataSourceBL());
        }

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListView lv = (ListView)sender;
            ICollectionView view = (ICollectionView)lv.ItemsSource;

            if (!view.CanSort)
            {
                return;
            }

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    string header = headerClicked.Column.Header as string;

                    if (view.SortDescriptions.Count > 0)
                    {
                        ListSortDirection direction = ListSortDirection.Ascending;

                        foreach (var sd in view.SortDescriptions)
                        {
                            if (sd.PropertyName == header)
                            {
                                if (direction == sd.Direction)
                                {
                                    direction = ListSortDirection.Descending;
                                }
                                break;
                            }
                        }

                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription(header, direction));
                    }
                    else
                    {
                        view.SortDescriptions.Add(new SortDescription(header, ListSortDirection.Ascending));
                    }
                }
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = null;

            ComboBoxItem cbi = (ComboBoxItem)FilterView.SelectedItem;

            switch (Convert.ToInt32(cbi.Content))
            {
                case 1:
                    view = (ICollectionView)ExampleListView1.ItemsSource;
                    break;
                case 2:
                    view = (ICollectionView)ExampleListView2.ItemsSource;
                    break;
            }

            if (!view.CanFilter)
            {
                return;
            }

            view.Filter = null;

            PropertyDescriptor pd = (PropertyDescriptor)FilterProperty.SelectedItem;
            string text = FilterText.Text;

            cbi = (ComboBoxItem)FilterOperator.SelectedItem;
            switch (cbi.Content.ToString())
            {
                case ">":
                    view.Filter = item => { return Comparer.Default.Compare(pd.GetValue(item),pd.Converter.ConvertFromString(text)) > 0 ; };
                    break;
                case ">=":
                    view.Filter = item => { return Comparer.Default.Compare(pd.GetValue(item), pd.Converter.ConvertFromString(text)) >= 0; };
                    break;
                case "=":
                    view.Filter = item => { return Comparer.Default.Compare(pd.GetValue(item), pd.Converter.ConvertFromString(text)) == 0; };
                    break;
                case "<":
                    view.Filter = item => { return Comparer.Default.Compare(pd.GetValue(item), pd.Converter.ConvertFromString(text)) < 0; };
                    break;
                case "<=":
                    view.Filter = item => { return Comparer.Default.Compare(pd.GetValue(item), pd.Converter.ConvertFromString(text)) <= 0; };
                    break;
            }
        }

        private void FilterView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilterButton();
        }

        private void FilterProperty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilterButton();
        }

        private void FilterOperator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilterButton();
        }

        void UpdateFilterButton()
        {
            FilterButton.IsEnabled = FilterView.SelectedIndex >= 0 && FilterProperty.SelectedIndex >= 0 && FilterOperator.SelectedIndex >= 0;
        }

    }
}
