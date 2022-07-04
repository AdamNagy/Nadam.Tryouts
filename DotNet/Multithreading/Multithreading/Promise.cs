using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    internal class Promise<T>
    {
        private Action<T> _action;

        public void Next(T next)
        {
            var tcs = new TaskCompletionSource<T>();
            Task.Factory.StartNew(() => _action(next)).ContinueWith((x) => tcs.SetResult(next));

        }

        public void Subscribe(Action<T> action)
        {
            _action = action;
        }
    }
}
