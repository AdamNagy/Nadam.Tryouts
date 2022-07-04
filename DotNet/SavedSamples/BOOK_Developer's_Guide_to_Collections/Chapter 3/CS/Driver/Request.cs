using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Driver
{
    class Request
    {
        TimeSpan m_requestTime;
        TimeSpan m_startTime;

        Song m_requestSong;

        public TimeSpan StartTime
        {
            get { return m_startTime; }
            set { m_startTime = value; }
        }

        public TimeSpan RequestTime
        {
            get { return m_requestTime; }
            set { m_requestTime = value; }
        }

        public TimeSpan FinishTime
        {
            get { return m_startTime + m_requestSong.Duration; }
        }

        public Song RequestSong
        {
            get { return m_requestSong; }
            set { m_requestSong = value; }
        }
    }
}
