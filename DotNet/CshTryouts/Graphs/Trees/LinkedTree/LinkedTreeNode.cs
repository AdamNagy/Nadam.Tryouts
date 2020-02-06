using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Graph2
{
    /// <summary>
    /// Tree graph element and graph itself. This is chained type graph implementation
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public class LinkedTreeNode<TNode> : IEnumerable<LinkedTreeNode<TNode>>
    {
        public TNode Value { get; private set; }
        public LinkedList<LinkedTreeNode<TNode>> Children { get; private set; }

        public LinkedTreeNode(TNode val)
        {
            Value = val;
            Children = new LinkedList<LinkedTreeNode<TNode>>();
        }

        public LinkedTreeNode<TNode> Add(TNode val)
        {
            var child = new LinkedTreeNode<TNode>(val);
            Children.AddLast(child);
            return child;
        }

        public LinkedTreeNode<TNode> Find(TNode val)
        {
            foreach (var child in Children)
            {
                if (child.Value.Equals(val))
                    return child;

                var found = child.Find(val);
                if (found != null)
                    return found;
            }

            return null;
        }

        public LinkedTreeNode<TNode> this[int idx]
        {
            get
            {
                var i = -1;
                foreach (var child in Children)
                {
                    ++i;
                    if (i == idx)
                        return child;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public int Count()
            => Children.Count();

        public void RemoveOne(TNode val)
        {
            var toRemove = Find(val);
            toRemove.RemoveAll();
            Children.Remove(toRemove);
        }

        public void RemoveAll()
        {
            foreach (var child in Children)
            {
                child.RemoveAll();
                Children.Remove(child);
            }
        }

        public IEnumerator<LinkedTreeNode<TNode>> GetEnumerator()
            => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        // (Root, Left, Right)
        public static IEnumerable<LinkedTreeNode<TNode>> PreOrder(LinkedTreeNode<TNode> currentRoot)
        {
            yield return currentRoot;
            foreach (var child in currentRoot.Children)
            {
                foreach (var grandChild in PreOrder(child))
                {
                    yield return grandChild;
                }
            }
        }

        public static IEnumerable<LinkedTreeNode<TNode>> PostOrder(LinkedTreeNode<TNode> currentRoot)
        {
            yield return currentRoot;
            foreach (var child in currentRoot.Children)
            {
                foreach (var grandChild in PreOrder(child))
                {
                    yield return grandChild;
                }
            }
        }
    }
}
