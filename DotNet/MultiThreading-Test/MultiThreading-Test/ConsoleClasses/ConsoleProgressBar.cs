using System;
using System.Text;
using System.Threading;

namespace MultiThreading_Test.ConsoleClasses
{
    public class ConsoleProgressBar : IDisposable, IProgress<double>
    {
        private const int BLOCKCOUNT = 50;

        private readonly int _count;
        private readonly int _line;

        private double currentQuotient = 0;
        private int currentCount = -1;
        private string currentText = string.Empty;

        private bool disposed = false;
        private static object locker = new object();

        public ConsoleProgressBar(int line, int count = 100)
        {
            _line = line;
            _count = count;
        }

        public void Report(double value)
        {
            // Make sure value is in [0..1] range
            value = Math.Max(0, Math.Min(1, value));
            Progress(value);
        }

        public void Progress(double step = 1)
        {
            ++currentCount;
            if (currentCount > _count || disposed)
                return;

            currentQuotient += step / _count;
            
            var progressBlockCount = (int)(currentQuotient * BLOCKCOUNT);
            var percentage = (int)(currentQuotient * 100);
            var remainingBlockNum = BLOCKCOUNT - progressBlockCount < 0 ? BLOCKCOUNT : BLOCKCOUNT - progressBlockCount;

            string text = string.Format("{0}{1} {2,3}% ({3}/{4})",
                    new string('>', progressBlockCount), new string('-', remainingBlockNum),
                    percentage <= 100 ? percentage : 100,
                    currentCount, _count);

            Console.SetCursorPosition(0, _line);
            Console.Write(text);
        }

        public void Dispose()
        {
            lock (locker)
            {
                disposed = true;
            }
        }

    }
}
