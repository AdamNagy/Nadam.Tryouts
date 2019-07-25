using System.Collections.Generic;

namespace Graphs.Graph2
{
    /// <summary>
    /// Tree graph element and graph itself. This is chained type graph implementation
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public class LinkedNode<TNode>
    {
        public TNode Value { get; private set; }
        public LinkedList<LinkedNode<TNode>> Children { get; private set; }

        public LinkedNode(TNode val)
        {
            Value = val;
            Children = new LinkedList<LinkedNode<TNode>>();
        }

        public void AddChild(TNode val)
        {
            var child = new LinkedNode<TNode>(val);
            Children.AddLast(child);
        }

        public static void AddNode(LinkedNode<TNode> parentNode, TNode childNode)
        {
            var child = new LinkedNode<TNode>(childNode);
            AddNode(parentNode, child);
        }

        public static void AddNode(LinkedNode<TNode> parentNode, LinkedNode<TNode> childNode)
            => parentNode.Children.AddLast(childNode);

        public static LinkedNode<TNode> FindNode(LinkedNode<TNode> rootNode, TNode val)
        {
            if (rootNode.Value.Equals(val))
                return rootNode;

            LinkedNode<TNode> found = null;
            foreach (var child in rootNode.Children)
            {
                found = LinkedNode<TNode>.FindNode(child, val);
                if (found != null)
                    break;
            }

            return found;
        }
    }
}
