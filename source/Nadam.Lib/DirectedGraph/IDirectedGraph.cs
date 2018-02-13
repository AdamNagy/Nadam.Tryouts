using System.Collections.Generic;
using Nadam.Global.Lib.Graph;

namespace Nadam.Global.Lib.DirectedGraph
{
	public interface IDirectedGraph<TNode>
	{
		int NodesCount();
		int EdgeCount();

		#region Add
		/// <summary>
		/// Add a new node to the graph with no edge to and from it.
		/// </summary>
		/// <param name="nodeVal">The value of node</param>
		/// <returns></returns>
		Node<TNode> AddNewNode(TNode nodeVal);

		/// <summary>
		/// Add a new directed node to an existing one. Node A need to belong to the graph. Node B is added if not belong to it yet.
		/// </summary>
		/// <param name="nodeValA">Existing node, FROM</param>
		/// <param name="nodeValB">New node, created if not exist</param>
		/// <returns></returns>
		Node<TNode> AddNewNodeFor(TNode nodeValA, TNode nodeValB);

		/// <summary>
		/// Binds an existing node to an other. Both need to belong to the graph, if any of the two does not, error throwns.
		/// </summary>
		/// <param name="nodeValA">FROM node</param>
		/// <param name="nodeValB">TO node</param>
		void AddExistingNodeFor(TNode nodeValA, TNode nodeValB);
		#endregion

		#region Contains
		/// <summary>
		/// Check if the given value belongs to the graph or not. Gives back the id of the node if exist,
		/// returns -1 otherwise
		/// </summary>
		/// <param name="nodeValue">Value to look for in the graph</param>
		/// <returns>Id of the node if exist, -1 otherwise</returns>
		int Contains(TNode nodeValue);

		/// <summary>
		/// Check if the given value belongs to the given node or not. Gives back the id of the node if exist
		/// </summary>
		/// <param name="nodeA">The parent, which childre are beeing checked</param>
		/// <param name="nodeB">The node which is beeing looked for</param>
		/// <returns>Id of the node if exist, -1 otherwise</returns>
		int ContainsDirectedNode(TNode nodeA, TNode nodeB);
		#endregion

		#region Get
		Node<TNode> GetNodeById(int nodeId);
		Node<TNode> GetNodeByValue(TNode nodeValue);
		Node<TNode> GetNode(Node<TNode> node);
		IEnumerable<Node<TNode>> GetDirectedNodesFor(TNode nodeA);
		#endregion

		// Add later
		//bool Remove(TNode nodeValue, bool withAllChildren);
		//bool Remove(int nodeId, bool withAllChildren);
		//bool RemoveChild(TNode nodeValue, TNode childValue);
		//bool RemoveChild(TNode nodeValue, int childId);
		//bool RemoveChildren(TNode nodeValue);
		//bool RemoveChildren(int node);
	}
}
