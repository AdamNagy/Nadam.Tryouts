using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Global.Lib.Tree.Iterators
{
    public class PostOrderTreeEnumerator<T> : IEnumerator<T>
    {
        private Tree<T> tree;
        private T current;

        private Queue<T> queue;

        public PostOrderTreeEnumerator(Tree<T> _tree)
        {
            tree = _tree;
            Reset();
        }

        public T Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (queue.Any())
            {
                current = queue.Dequeue();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            current = tree.GetRoot();
            queue = new Queue<T>();
            BuildQueue(current);
        }

        private void BuildQueue(T currentRoot)
        {            
            foreach (var node in tree.GetChildrenFor(currentRoot))
            {
                BuildQueue(node);
            }
            queue.Enqueue(currentRoot);
        }
    }
}
