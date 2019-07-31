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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevGuideToCollections;

namespace Driver
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();

            UnitTests.RunTests();
        }

        private void ComboBoxButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxBinding window = new ComboBoxBinding();

            window.Show();
        }

        private void ListBoxButton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxBinding window = new ListBoxBinding();

            window.Show();
        }

        private void ListViewButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewBinding window = new ListViewBinding();

            window.Show();
        }

        private void TreeViewButton_Click(object sender, RoutedEventArgs e)
        {
            TreeViewBinding window = new TreeViewBinding();

            window.Show();
        }

        private void CollectionViewButton_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewBinding window = new CollectionViewBinding();

            window.Show();
        }
    }
}
