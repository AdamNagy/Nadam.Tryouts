using System;
using System.Collections.Generic;
using Graphs.DirectedGraph;
using Graphs.Trees.Iterators;

namespace Graphs.Trees
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
            if (!ContainsNode(parent) || !ContainsNode(child) || ContainsEdge(child, parent))
                throw new Exception("Parent node does not exist");
            
            AddReferenceFor(parent, child);
        }

        public TNode GetRoot()
        {
            if (hasRoot)
                return root;

            return default(TNode);
        }

        public IEnumerable<TNode> GetChildrenFor(TNode parent)
        {
            throw new NotImplementedException();
            // return GetReferencedNodesFor(parent);
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
