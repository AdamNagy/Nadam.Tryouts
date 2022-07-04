using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Driver
{
    class Song
    {
        TimeSpan m_duration;
        String m_name;

        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public TimeSpan Duration
        {
            get { return m_duration; }
            set { m_duration = value; }
        }
    }
}
