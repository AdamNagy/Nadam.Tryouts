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
using System.Xml.Linq;

namespace Driver
{
    /// <summary>
    /// Interaction logic for TreeViewWindow.xaml
    /// </summary>
    public partial class TreeViewBinding : Window
    {
        NotificationList<TreeViewNode> m_nodes;

        public TreeViewBinding()
        {
            InitializeComponent();

            m_nodes = new NotificationList<TreeViewNode>()
                {
                    new TreeViewNode("Colors",
                            new TreeViewNode("Red"),
                            new TreeViewNode("Green"),
                            new TreeViewNode("Blue")
                        ),
                    new TreeViewNode("Deserts",
                            new TreeViewNode("Pies",
                                new TreeViewNode("Cherry"),
                                new TreeViewNode("Apple")
                                ),
                            new TreeViewNode("Ice Cream")
                        )
                };

            ExampleTreeView.ItemsSource = m_nodes;
        }

        private void ExampleTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewNode node = (TreeViewNode)ExampleTreeView.SelectedItem;

            if (node != null)
            {
                TextTextBox.Text = node.Text;
                RemoveButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;
            }
            else
            {
                TextTextBox.Text = "";
                RemoveButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            TreeViewNode node = (TreeViewNode)ExampleTreeView.SelectedItem;

            if (node.Parent != null)
            {
                node.Parent.Nodes.Remove(node);
            }
            else
            {
                m_nodes.Remove(node);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TreeViewNode node = (TreeViewNode)ExampleTreeView.SelectedItem;

            if (node == null)
            {
                m_nodes.Add(new TreeViewNode(TextTextBox.Text));
            }
            else
            {
                node.Nodes.Add(new TreeViewNode(TextTextBox.Text));
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TreeViewNode node = (TreeViewNode)ExampleTreeView.SelectedItem;

            if (node == null)
            {
                return;
            }

            node.Text = TextTextBox.Text;
        }
    }
}
