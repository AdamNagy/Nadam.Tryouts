using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Driver
{
    public class Company : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        int m_id;
        string m_website;
        string m_name;

        public int Id
        {
            get { return m_id; }
            set
            {
                if (m_id == value)
                {
                    return;
                }

                NotifyPropertyChanging("Id");
                m_id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Website
        {
            get { return m_website; }
            set
            {
                if (m_website == value)
                {
                    return;
                }

                NotifyPropertyChanging("Website");
                m_website = value;
                NotifyPropertyChanged("Website");
            }
        }
        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name == value)
                {
                    return;
                }

                NotifyPropertyChanging("Name");
                m_name = value;
                NotifyPropertyChanged("Name");
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
}
