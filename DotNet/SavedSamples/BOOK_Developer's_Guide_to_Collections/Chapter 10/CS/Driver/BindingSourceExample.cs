using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Driver
{
    public partial class BindingSourceExample : Form
    {
        BindingSource m_source;
        Company m_showing;

        public BindingSourceExample()
        {
            InitializeComponent();

            m_source = new BindingSource();
            m_source.DataSource = new List<Company>(DL.GetData());
            listBox1.DataSource = m_source; 
            listBox1.DisplayMember = "Name";
            
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                UpdateButton.Enabled = false;
                return;
            }

            ShowCompany((Company)m_source[listBox1.SelectedIndex]);
            UpdateButton.Enabled = true;
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            Company company = new Company();

            company.Id = int.Parse(AddIdTextBox.Text);
            company.Name = AddNameTextBox.Text;
            company.Website = AddWebsiteTextBox.Text;

            m_source.Add(company);

            AddIdTextBox.Text = DL.GetNextId().ToString();
        }

        private void RemoveItemButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                m_source.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            m_showing.Name = NameTextBox.Text;
            m_showing.Website = WebsiteTextBox.Text;
        }
    }
}
