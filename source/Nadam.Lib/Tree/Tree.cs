using Nadam.Global.Lib.DirectedGraph;
using Nadam.Global.Lib.Tree.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Global.Lib.Tree
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
            if (ContainsNode(parent) == false)
                throw new Exception("Parent node does not exist in the graph");

            if (ContainsNode(child) == false)
                AddNode(child);
            else if (ContainsEdge(child, parent))
                throw new Exception("Child node is already parent node for the given 'parent', thus not possible to add.");

            AddEdgeFor(parent, child);
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
            return new LevelOrderEnumerator<TNode>(this);
        }
    }
}
