using Nadam.Global.Lib.DirectedGraph;
using System.Collections.Generic;

namespace Nadam.Global.Lib.Tree
{
    public interface ITree<TNode>
    {
        int NodesCount();
                

        void AddRoot(TNode rootNode);
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
