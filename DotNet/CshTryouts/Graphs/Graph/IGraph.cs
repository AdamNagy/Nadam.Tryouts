using System.Collections.Generic;

namespace Graphs.Graph
{
	public interface IGraph<TNode>
	{
		void AddNode(TNode nodeValue);
		void AddEdge(TNode a, TNode b);

		bool Contains(TNode nodeValue);
		bool Contains(TNode a, TNode b);

		TNode FindNode(TNode reference);
		// Edge FindEdge(TNode a, TNode b);

		bool Remove(TNode nodeValue);
	    bool Remove(TNode a, TNode b);

        IEnumerator<TNode> Nodes();
		// IEnumerator<Edge> Edges();
	}
}
