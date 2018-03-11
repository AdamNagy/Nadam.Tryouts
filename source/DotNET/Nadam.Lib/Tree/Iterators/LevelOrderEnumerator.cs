using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Global.Lib.Tree.Iterators
{
    public class LevelOrderTreeEnumerator<T> : IEnumerator<T>
    {
        private ITree<T> tree;
        private T current;

        private Queue<T> queue;
        private Dictionary<int, List<T>> dict;
        private int currentLevel;

        public LevelOrderTreeEnumerator(ITree<T> _tree)
        {
            tree = _tree;
            Reset();
        }

        public T Current => current;

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
            dict = new Dictionary<int, List<T>>();
            currentLevel = 1;

            dict.Add(currentLevel, new List<T> { current });
            LevelOrder(current);
            BuildQueue();
        }

        private void LevelOrder(T currentRoot)
        {
            currentLevel++;
            if (!dict.Keys.Contains(currentLevel))
                dict.Add(currentLevel, new List<T>());

            foreach (var node in tree.GetChildrenFor(currentRoot))
                dict[currentLevel].Add(node);

            foreach (var node in tree.GetChildrenFor(currentRoot))
                LevelOrder(node);
            currentLevel--;
        }

        private void BuildQueue()
        {
            foreach (var level in dict)
            {
                foreach (var node in level.Value)
                    queue.Enqueue(node);
            }
        }
    }
}
