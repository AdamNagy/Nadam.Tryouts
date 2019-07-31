using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevGuideToCollections;


namespace Driver
{
    public partial class DataGridViewBinding : Form
    {
        WinFormsBindingList<PropertyDescriptor> m_indexed;
        WinFormsBindingList<PropertyDescriptor> m_unindexed;
        WinFormsBindingList<Company> m_datasource;

        public DataGridViewBinding()
        {
            InitializeComponent();

            m_datasource = DL.GetDataSource();
            dataGridView1.DataSource = m_datasource;

            m_unindexed = new WinFormsBindingList<PropertyDescriptor>();
            m_indexed = new WinFormsBindingList<PropertyDescriptor>();

            List<PropertyDescriptor> searchableDescriptors = new List<PropertyDescriptor>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(Company)))
            {
                m_unindexed.Add(pd);

                if (pd.Converter.CanConvertFrom(typeof(string)))
                {
                    searchableDescriptors.Add(pd);
                }

            }

            lbIndexedProperties.DataSource = m_indexed;
            lbUnindexedProperties.DataSource = m_unindexed;
            SearchPropertyComboBox.DataSource = searchableDescriptors;

            lbUnindexedProperties.DisplayMember = "Name";
            lbIndexedProperties.DisplayMember = "Name";
            SearchPropertyComboBox.DisplayMember = "Name";
        }

        private void AddIndexButton_Click(object sender, EventArgs e)
        {
            if (lbUnindexedProperties.SelectedIndex >= 0)
            {
                m_datasource.AddIndex(m_unindexed[lbUnindexedProperties.SelectedIndex]);

                m_indexed.Add(m_unindexed[lbUnindexedProperties.SelectedIndex]);
                m_unindexed.RemoveAt(lbUnindexedProperties.SelectedIndex);
            }
        }

        private void RemoveIndexButton_Click(object sender, EventArgs e)
        {
            if (lbIndexedProperties.SelectedIndex >= 0)
            {
                m_datasource.RemoveIndex(m_indexed[lbIndexedProperties.SelectedIndex]);

                m_unindexed.Add(m_indexed[lbIndexedProperties.SelectedIndex]);
                m_indexed.RemoveAt(lbIndexedProperties.SelectedIndex);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text) || SearchPropertyComboBox.SelectedIndex < 0)
            {
                return;
            }

            var pd = SearchPropertyComboBox.SelectedItem as PropertyDescriptor;

            int found = -1;

            try
            {
                found = m_datasource.Find(pd, pd.Converter.ConvertFromString(SearchTextBox.Text));

                if (found >= 0)
                {
                    MessageBox.Show(string.Format("Found '{0}' at index {1}", SearchTextBox.Text, found));
                }
                else
                {
                    MessageBox.Show(string.Format("Didn't find '{0}'", SearchTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
