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

using DevGuideToCollections;

namespace Driver
{
    /// <summary>
    /// Interaction logic for ListBoxView.xaml
    /// </summary>
    public partial class ListBoxBinding : Window
    {
        NotificationList<Company> m_datasource;
        Company m_showing;
        
        public ListBoxBinding()
        {
            InitializeComponent();

            m_datasource = DL.GetDataSource();

            ExampleListBox.ItemsSource = m_datasource;
            ExampleListBox.DisplayMemberPath = "Name";
            ExampleListBox.SelectedValuePath = "Id";

            AddIdTextBox.Text = DL.GetNextId().ToString();

        }

        void ShowCompany(Company company)
        {
            m_showing = company;
            if (company != null)
            {
                IdTextBox.Text = company.Id.ToString();
                NameTextBox.Text = company.Name;
                WebsiteTextBox.Text = company.Website;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            m_showing.Name = NameTextBox.Text;
            m_showing.Website = WebsiteTextBox.Text;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Company company = new Company();

            company.Id = int.Parse(AddIdTextBox.Text);
            company.Name = AddNameTextBox.Text;
            company.Website = AddWebsiteTextBox.Text;

            m_datasource.Add(company);

            AddIdTextBox.Text = DL.GetNextId().ToString();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExampleListBox.SelectedIndex >= 0)
            {
                m_datasource.RemoveAt(ExampleListBox.SelectedIndex);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExampleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExampleListBox.SelectedIndex < 0)
            {
                UpdateButton.IsEnabled = false;
                return;
            }

            ShowCompany(m_datasource[ExampleListBox.SelectedIndex]);
            UpdateButton.IsEnabled = true;
        }
    }
}
