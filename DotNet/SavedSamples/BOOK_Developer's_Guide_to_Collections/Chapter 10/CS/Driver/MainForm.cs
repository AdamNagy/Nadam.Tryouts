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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ListBoxButton_Click(object sender, EventArgs e)
        {
            ListBoxBinding form = new ListBoxBinding();


            form.Show();
        }

        private void DataGridViewButton_Click(object sender, EventArgs e)
        {
            DataGridViewBinding form = new DataGridViewBinding();

            form.Show();
        }

        private void ComboBoxButton_Click(object sender, EventArgs e)
        {
            ComboBoxBinding form = new ComboBoxBinding();

            form.Show();
        }

        private void DataGridAdvanceButton_Click(object sender, EventArgs e)
        {
            DataGridViewAdvanceBinding form = new DataGridViewAdvanceBinding();

            form.Show();
        }

        private void BindingSourceButton_Click(object sender, EventArgs e)
        {
            BindingSourceExample form = new BindingSourceExample();

            form.Show();
        }

    }
}
