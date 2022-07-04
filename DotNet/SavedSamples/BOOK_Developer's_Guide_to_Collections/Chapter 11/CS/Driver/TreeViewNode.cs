using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevGuideToCollections;
using System.ComponentModel;

namespace Driver
{
    public class TreeViewNode : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string m_text;
        object m_tag;
        NotificationList<TreeViewNode> m_nodes;
        TreeViewNode m_parent;

        public TreeViewNode()
        {
            Nodes = new NotificationList<TreeViewNode>();
        }

        public TreeViewNode(string text)
        {
            Text = text;
            Nodes = new NotificationList<TreeViewNode>();
        }

        public TreeViewNode(string text, params TreeViewNode []nodes )
        {
            Text = text;
            Nodes = new NotificationList<TreeViewNode>();
            foreach (var node in nodes)
            {
                Nodes.Add(node);
            }
        }

        public string Text
        {
            get { return m_text; }
            set
            {
                if (m_text != value)
                {
                    m_text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public object Tag
        {
            get { return m_tag; }
            set
            {
                if (m_tag != value)
                {
                    m_tag = value;
                    OnPropertyChanged("Tag");
                }
            }
        }

        public TreeViewNode Parent
        {
            get { return m_parent; }
            private set
            {
                if (m_parent != value)
                {
                    m_parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }

        public NotificationList<TreeViewNode> Nodes 
        {
            get { return m_nodes; }
            private set
            {
                if (m_nodes == value)
                {
                    return;
                }
                m_nodes = value;
                if (m_nodes != null)
                {
                    m_nodes.CollectionChanged += 
                        (sender, e) =>
                            {
                                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                                {
                                    foreach (TreeViewNode item in e.NewItems)
                                    {
                                        item.Parent = this;
                                    }
                                }
                                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                                {
                                    foreach (TreeViewNode item in e.OldItems)
                                    {
                                        item.Parent = null;
                                    }
                                }
                                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                                {
                                    NotifyCollectionClearedEventArgs eCleared = e as NotifyCollectionClearedEventArgs;
                                    if (eCleared != null && eCleared.ClearedItems != null)
                                    {
                                        foreach (TreeViewNode item in eCleared.ClearedItems)
                                        {
                                            item.Parent = null;
                                        }
                                    }
                                }
                            };
                }
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
