using System.Collections.Generic;

namespace Graphs.DirectedGraph
{
	public interface IDirectedGraph<TNode>
	{
		int NodesCount();

        #region Add
        /// <summary>
        /// Add a new node to the graph with no edge to and from it.
        /// </summary>
        /// <param name="nodeVal">The value of node</param>
        /// <returns></returns>
        DirectedNode<TNode> AddNode(TNode nodeVal);
        void AddReferenceFor(TNode startNode, TNode referenced);
        #endregion

        #region Contains
        /// <summary>
        /// Check if the given value belongs to the graph or not. Gives back the id of the node if exist,
        /// returns -1 otherwise
        /// </summary>
        /// <param name="nodeValue">Value to look for in the graph</param>
        /// <returns>true if node found, false otherwise</returns>
        bool ContainsNode(TNode nodeValue);

        /// <summary>
        /// Check if the given value belongs to the given node or not. Gives back the id of the node if exist
        /// </summary>
        /// <param name="nodeA">The parent, which childre are beeing checked</param>
        /// <param name="nodeB">The node which is beeing looked for</param>
        /// <returns>true if node found, false otherwise</returns>
        bool ContainsEdge(TNode nodeValA, TNode nodeValB);
        #endregion

        #region Get
        IList<DirectedNode<TNode>> GetNode(TNode nodeValue);
        TNode this[int index] { get; }
        #endregion

        #region Remove
        /// <summary>
        /// Removes an existing node from the graph. Will remove all edge pointing to the removing node.
        /// </summary>
        /// <param name="nodeValue">the value of the node to remove</param>
        /// <returns>boolean about the reoval success</returns>
        bool RemoveNode(TNode nodeValue);
        bool RemoveReferenceFor(TNode a, TNode b);
        #endregion
    }
}
