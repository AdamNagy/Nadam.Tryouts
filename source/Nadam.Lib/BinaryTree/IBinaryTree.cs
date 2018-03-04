using System.Collections.Generic;

namespace Nadam.Global.Lib.BinaryTree
{
    public interface IBinaryTree<TNode>
    {
        int NodesCount();

        BinaryNode<TNode> AddNode(TNode newNode);
        BinaryNode<TNode> GetRoot();

        BinaryNode<TNode> this[int index] { get; }

        //IEnumerator<TNode> PreOrder();
        //IEnumerator<TNode> PostOrder();
        //IEnumerator<TNode> LevelOrder();
        IEnumerator<TNode> InOrder();
    }
}
