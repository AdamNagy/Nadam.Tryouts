using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.BinaryTree
{
    class InOrderTreeEnumerator<T> : IEnumerator<T>
    {
        private IBinaryTree<T> bst;
        private T current;

        private Queue<BinaryNode<T>> queue;

        public InOrderTreeEnumerator(IBinaryTree<T> _tree)
        {
            bst = _tree;
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
                current = queue.Dequeue().Value;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            var currentNode = bst.GetRoot();
            queue = new Queue<BinaryNode<T>>();
            InOrder(currentNode);
        }

        private void InOrder(BinaryNode<T> currentRoot)
        {
            if( currentRoot != null )
            {
                if (currentRoot.LeftChild != -1)
                    InOrder(bst[(currentRoot.LeftChild)]);

                queue.Enqueue(currentRoot);

                if (currentRoot.RightChild != -1)
                    InOrder(bst[(currentRoot.RightChild)]);
            }
        }
    }
}
