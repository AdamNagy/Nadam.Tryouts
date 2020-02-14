using System;
using System.Collections.Generic;

namespace Graphs.Trees.BinaryTree
{
    public class NDimTreeNode<TNode>
    {
        protected NDimTreeNode<TNode>[] children;

        public NDimTreeNode(int dim)
        {
            children = new NDimTreeNode<TNode>[dim];
        }
    }

    public class TwoDimensionalTree<TNode> : NDimTreeNode<TNode>
    {
        public TwoDimensionalTree<TNode> left
        {
            get => (TwoDimensionalTree<TNode>) children[0];
        }

        public TwoDimensionalTree<TNode> right
        {
            get => (TwoDimensionalTree<TNode>)children[1];
        }

        public TwoDimensionalTree() : base(2) { }
        
        /// <summary>
        /// the function that sorts the children
        /// For 2 dimensinal tree (binary tree) this can be the 'less than' predicate which has 2 state like boolean
        /// </summary>
        protected Func<TNode, TNode, bool> sorter;

        public IEnumerable<TwoDimensionalTree<TNode>> InOrder(TwoDimensionalTree<TNode> root = null)
        {
            if( root == null )
                yield return this;

            foreach (var child in children)
            {
                foreach (var grand in InOrder((TwoDimensionalTree<TNode>) child))
                    yield return grand;
            }
        }
    }


}
