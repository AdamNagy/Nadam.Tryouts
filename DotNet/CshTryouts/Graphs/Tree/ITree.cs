using System.Collections.Generic;

namespace Tree
{
    public interface ITree<TNode>
    {
        int NodesCount();               
        // depth

        void AddRoot(TNode rootNode); // kinda a 'init tree'
        void AddChildFor(TNode parent, TNode child);

        bool ContainsNode(TNode nodeValue);

        TNode GetRoot();
        IEnumerable<TNode> GetChildrenFor(TNode parent);

        bool RemoveNode(TNode nodeValue);        

        IEnumerator<TNode> PreOrder();        
        IEnumerator<TNode> PostOrder();
        IEnumerator<TNode> LevelOrder();
    }
}
