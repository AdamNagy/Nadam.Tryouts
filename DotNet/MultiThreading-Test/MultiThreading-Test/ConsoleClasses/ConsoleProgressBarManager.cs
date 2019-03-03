using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading_Test.ConsoleClasses
{
    public class ConsoleProgressBarManager
    {
        private int _line;
        private readonly int _count;
        private readonly object _locker;
        private readonly string _title;

        public ConsoleProgressBarManager(int line, int count, string title)
        {
            _line = line;
            _count = count;
            _title = title;
        }

        public void Run()
        {
            ConsoleProgressBar progressBar;
            lock (_locker)
            {
                Console.SetCursorPosition(1, _line);
                Console.WriteLine(_title);
                ++_line;
                progressBar = new ConsoleProgressBar(_line, _count);
            }

            for (int i = 0; i < _count; i++)
            {
                lock (_locker)
                {
                    progressBar.Progress();
                }
                Thread.Sleep(20);
            }
        }
    }
}
