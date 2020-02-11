using System;
using System.Collections.Generic;

namespace Graphs.BinaryTree
{
    public class BinaryTree<TNode>: IBinaryTree<TNode> 
        where TNode: IComparable
    {
        private int nodeId;
        protected IList<BinaryNode<TNode>> NodeSet { get; set; }

        public BinaryTree(int size = 10)
        {
            nodeId = 0;
            NodeSet = new List<BinaryNode<TNode>>(size);
        }

        public int NodesCount()
        {
            return NodeSet.Count;
        }

        public BinaryNode<TNode> AddNode(TNode newNodeVal)
        {
            var newNode = new BinaryNode<TNode>(newNodeVal, nodeId++);
            NodeSet.Add(newNode);

            if( NodesCount() > 1 )
                Insert(GetRoot(), newNode);

            return newNode;
        }

        private BinaryNode<TNode> Insert(BinaryNode<TNode> currentRoot, BinaryNode<TNode> newNode)
        {
            if (currentRoot.Value.CompareTo(newNode.Value) >= 0)
            {
                if (currentRoot.LeftChild != -1)
                    currentRoot.AddLeftChild(Insert(NodeSet[currentRoot.LeftChild], newNode).NodeId);
                else
                    currentRoot.AddLeftChild(newNode.NodeId);
            }                
            else if (currentRoot.Value.CompareTo(newNode.Value) <= 0)
            {
                if (currentRoot.RightChild != -1)
                    currentRoot.AddRightChild(Insert(NodeSet[currentRoot.RightChild], newNode).NodeId);
                else
                    currentRoot.AddRightChild(newNode.NodeId);
            }

            return currentRoot;
        }

        public BinaryNode<TNode> GetRoot()
        {
            if( NodesCount() > 0 )
                return NodeSet[0];
            return null;
        }

        public BinaryNode<TNode> this[int index]
        {
            get
            {
                return NodeSet[index];
            }
        }
       
        public IEnumerator<TNode> InOrder()
        {
            return new InOrderTreeEnumerator<TNode>(this);
        }

        //public IEnumerator<TNode> LevelOrder()
        //{
        //    return new LevelOrderTreeEnumerator<TNode>(this);
        //}

        //public IEnumerator<TNode> PostOrder()
        //{
        //    return new PostOrderTreeEnumerator<TNode>(tree);
        //}

        //public IEnumerator<TNode> PreOrder()
        //{
        //    return new PreOrderTreeEnumerator<TNode>(tree);
        //}
    }
}
