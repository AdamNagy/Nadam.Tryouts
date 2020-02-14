using System;
using System.Collections;
using System.Collections.Generic;
using MyCollection;

namespace Graphs.DirectedGraph
{
    /// <summary>
    /// represents:
    ///   directed (irányított) a -> b
    ///   coherant (összefüggő)
    /// This is a mediator type data structure
    /// </summary>
    /// <typeparam name="NodeType">type of the data that are present in the grapgh</typeparam>
    public class DirectedGraph<TNode> : IEnumerable<TNode>
    {
        // can not use linked implementation because nodes can point to each other and that would end up duplicity
        protected IndexList<TNode> Nodes;
        protected Dictionary<int, List<int>> NodeReferences;

        public int Count { get => Nodes.Count; }

        #region ctors
        public DirectedGraph()
        {
            Nodes = new IndexList<TNode>();
        }
        #endregion

        #region Add
        public void Add(TNode nodeVal)
        {
            var newNodeIdx = Nodes.Add(nodeVal);
            NodeReferences.Add(newNodeIdx, new List<int>());
        }

        public virtual void AddReferenceFor(TNode startNode, TNode referenced)
        {
            var nodeAs = Contains(startNode);
            if (Contains(startNode))
                throw new Exception("From node does not exist");

            var startNodeIdx = Nodes[startNode];

            if (Contains(referenced))
                throw new Exception("To node does not exist");

            var referencedNodeIdx = Nodes[referenced];

            if(!NodeReferences[startNodeIdx].Contains(referencedNodeIdx) )
                NodeReferences[startNodeIdx].Add(referencedNodeIdx);
        }
        #endregion

        #region Contains
        public bool Contains(TNode nodeValue)
            => Nodes.Contains(nodeValue) > -1;

        public bool ContainsEdge(TNode nodeValA, TNode nodeValB)
        {
            if( !Contains(nodeValA) || !Contains(nodeValB) )
                throw new ArgumentException();

            if ( NodeReferences[Nodes[nodeValA]].Contains(Nodes[nodeValB]) )
                return true;

            return false;
        }
        #endregion

        #region Remove
        public bool RemoveNode(TNode nodeValue)
        {
            if (!Contains(nodeValue))
                return false;

            NodeReferences.Remove(Nodes[nodeValue]);
            Nodes.Remove(nodeValue);
            
            return true;
        }
        #endregion

        public IEnumerable<TNode> GetReferencedNodesFor(TNode nodeValue)
        {
            if (!Contains(nodeValue))
                throw new ArgumentException();

            var indexes = NodeReferences[Nodes[nodeValue]];
            foreach (var index in indexes)
            {
                yield return Nodes[index];
            }
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
