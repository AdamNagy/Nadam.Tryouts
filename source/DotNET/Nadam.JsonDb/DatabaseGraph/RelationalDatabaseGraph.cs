﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nadam.Global.Lib.DirectedGraph;
using Nadam.Global.Lib.Graph;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public class RelationalDatabaseGraph : DirectedGraph<string>, IEnumerable<string>, IRelationalDatabaseGraph
    {
        private Node<string> root { get; set; }

        public RelationalDatabaseGraph()
        {
            root = AddNode("root");
        }

        // Because of the root bode, ned to substract 1
        public int TablesCount() => NodesCount() - 1;
        public Node<string> GetRoot() { return root; }

        /// <summary>
        /// Add new table to the graph, and need to define if any other table in graph is depends on this 
        /// new one and make the directed edges according to that
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dependecies"></param>
        public void AddTable(string newTable, IEnumerable<string> dependecies)
        {
            AddTable(newTable);
            foreach (var dependecy in dependecies)
            {
                AddTable(dependecy);
                AddReferenceFor(newTable, dependecy);
            }
        }

        public void AddTable(string newTable, string dependecy)
        {
            AddTable(newTable);
            AddTable(dependecy);
            AddReferenceFor(newTable, dependecy);
        }

        public void AddTable(string newTable)
        {
            var tableNode = ContainsNode(newTable);
            if (!tableNode)
            {
                AddNode(newTable);
                AddReferenceFor(root.Value, newTable);
            }
        }

        public IEnumerable<string> GetDependencyTables(string tableName)
        {
            var tableNode = GetNode(tableName).First();

            var edgesTo = tableNode.GetReferences();
            var dependencies = edgesTo.Select(p => NodeSet.Single(q => q.NodeId.Equals(p)));

            return dependencies.Select(p => p.Value);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new DependencyEnumerator<string>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DependencyEnumerator<string>(this);
        }
    }
}