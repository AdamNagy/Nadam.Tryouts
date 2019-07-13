using DirectedGraph;
using Tree.Iterators;
using System;
using System.Collections.Generic;

namespace Tree
{
    public class Tree<TNode> : DirectedGraph<TNode>, ITree<TNode>
    {
        bool hasRoot;
        private TNode root;

        public void AddRoot(TNode rootNode)
        {
            if (!hasRoot)
            {
                AddNode(rootNode);
                hasRoot = true;
                root = rootNode;
            }
            else
                throw new Exception("Tree already has a root. Changing it possible via given method");
        }

        public void AddChildFor(TNode parent, TNode child)
        {
            if (!ContainsNode(parent))
                throw new Exception("Parent node does not exist");

            if (!ContainsNode(child))
                AddNode(child);

            if (ContainsEdge(child, parent))
                throw new Exception("There is already and edge from child to parent");

            AddReferenceFor(parent, child);
        }

        public TNode GetRoot()
        {
            if (hasRoot)
                return root;
            throw new Exception("Current graph does not hav a root");
        }

        public IEnumerable<TNode> GetChildrenFor(TNode parent)
        {
            return GetReferencedNodesFor(parent);            
        }

        public IEnumerator<TNode> PreOrder()
        {
            return new PreOrderTreeEnumerator<TNode>(this);
        }

        public IEnumerator<TNode> PostOrder()
        {
            return new PostOrderTreeEnumerator<TNode>(this);
        }

        public IEnumerator<TNode> LevelOrder()
        {
            return new LevelOrderTreeEnumerator<TNode>(this);
        }
    }
}
