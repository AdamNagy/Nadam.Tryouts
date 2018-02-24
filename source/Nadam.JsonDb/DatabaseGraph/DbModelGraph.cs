using System;
using System.Collections.Generic;
using System.Linq;
using Nadam.Global.Lib.DirectedGraph;
using Nadam.Global.Lib.Graph;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public class DbModelGraph : DirectedGraph<string>
    {
        public Node<string> Root { get; set; }

        public DbModelGraph()
        {
            Root = AddNode("root");
        }

        /// <summary>
        /// Add new table to the graph, and need to define if any other table in graph is depends on this 
        /// new one and make the directed edges according to that
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dependecies"></param>
        public void AddTable(string newTable, IEnumerable<string> dependecies)
        {
            FindOrAddTable(newTable);
            AddEdgeFor(Root.Value, newTable);

            foreach (var dependecy in dependecies)
            {
                FindOrAddTable(dependecy);
                AddEdgeFor(newTable, dependecy);
            }
        }

        public void AddTable(string newTable)
        {
            FindOrAddTable(newTable);
            AddEdgeFor(Root.Value, newTable);
        }

        public void FindOrAddTable(string tableName)
        {
            var tableNode = ContainsNode(tableName);
            if (!tableNode)
            {
                AddNode(tableName);
            }
        }

        public IEnumerable<string> GetDependentTables(string tableName)
        {
            var tableNode = GetNode(tableName).First();

            // var dependencies = FindByValue(tableNode.TableName).DirectedNeighbors.Select(p => p.Value);
            var edgesTo = EdgeSet.Where(p => p.To.Equals(tableNode.NodeId));
            var dependencies = edgesTo.Select(p => NodeSet.Single(q => q.NodeId.Equals(p.From)));

            return dependencies.Select(p => p.Value);
        }

        public IEnumerable<string> GetDependencyTables(string tableName)
        {
            var tableNode = GetNode(tableName).First();

            // var dependencies = FindByValue(tableNode.TableName).DirectedNeighbors.Select(p => p.Value);
            var edgesTo = EdgeSet.Where(p => p.From.Equals(tableNode.NodeId));
            var dependencies = edgesTo.Select(p => NodeSet.Single(q => q.NodeId.Equals(p.To)));

            return dependencies.Select(p => p.Value);
        }

        public DbModelGraphDependencyEnumerator DependecyIteration()
        {
            return new DbModelGraphDependencyEnumerator(this);
        }
    }
}
