using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Global.Lib.DirectedGraph
{
	public interface IDirectedGraph<TNode>
	{
		void AddNode(DirectedNode<TNode> node);
		void AddNode(TNode node);
		void AddChildFor(DirectedNode<TNode> node, DirectedNode<TNode> n);
		void AddChildFor(DirectedNode<TNode> node, TNode n);

		bool Contains(TNode nodeValue);
		bool Contains(DirectedNode<TNode> node);
		bool HasChild(DirectedNode<TNode> node, DirectedNode<TNode> n);
		bool HasChild(DirectedNode<TNode> node, TNode n);

		DirectedNode<TNode> FindNode(TNode reference);
		DirectedNode<TNode> FindNode(DirectedNode<TNode> reference);
		DirectedNode<TNode> FindNode(int nodeId);

		bool Remove(DirectedNode<TNode> node, bool withAllChildren);
		bool Remove(TNode nodeValue, bool withAllChildren);
		bool Remove(int nodeId, bool withAllChildren);
		bool RemoveChild(DirectedNode<TNode> node, DirectedNode<TNode> Child);
		bool RemoveChild(DirectedNode<TNode> node, TNode Child);
		bool RemoveChild(DirectedNode<TNode> node, int Child);
		bool RemoveChildren(DirectedNode<TNode> node);
		bool RemoveChildren(TNode node);
		bool RemoveChildren(int node);
	}
}
