using System;
using System.Collections.Generic;
using System.Linq;
using Nadam.Global.Lib.Graph;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public class DbModelGraph : Graph<DbTable>
    {
        public DbTable Root { get; set; }

        public DbModelGraph()
        {
            Root = new DbTable("Root");
        }

        /// <summary>
        /// Add new table to the graph, and need to define if any other table in graph is depends on this 
        /// new one and make the directed edges according to that
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dependecies"></param>
        public void AddTable(string table, IEnumerable<string> dependecies)
        {
            var tableNode = FindOrAddTable(table);
            AddDirectedEdge(Root, tableNode);

            TableNode dependecyNode;
            foreach (var dependecy in dependecies)
            {
                tableNode.HaveDependency = true;
                dependecyNode = FindOrAddTable(dependecy);
                dependecyNode.DependedOn = true;             

                AddDirectedEdge(tableNode, dependecyNode);
                RemoveDirectedEdge(Root, dependecyNode);
                if(tableNode.DependedOn)
                    RemoveDirectedEdge(Root, tableNode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public TableNode FindOrAddTable(string table)
        {
            var tableNode = FindByValue(table);
            if (tableNode == null)
            {
                tableNode = new TableNode(table);
                AddNode(tableNode);
            }

            return (TableNode)tableNode;
        }

        public void AddTables(IEnumerable<string> tables)
        {
            foreach (var table in tables)
            {
                AddTable(table, new List<string>());
            }
        }

        public void AddDependecy(string _from, string _to)
        {
            var from = FindByValue(_from);
            var to = FindByValue(_to);

            if (from == null || to == null)
                throw new ArgumentException("tables not exist");

            AddDirectedEdge(from, to);
        }

        public DbTable FindByValue(string reference)
        {
            return (DbTable)NodeSet.SingleOrDefault(p => p.Value.Equals(reference));
        }

        public virtual DbTable FindByValue(DbTable reference)
        {
            return (DbTable)NodeSet.SingleOrDefault(p => p.Equals(reference));
        }

        public DbTable FindByNodeId(int id)
        {
            return (DbTable)NodeSet.SingleOrDefault(p => p.NodeId.Equals(id));
        }

        public IEnumerable<string> GetDependentTables(string tableName)
        {
            var tableNode = new DbTable(tableName);
            var dependencies = FindByValue(tableNode.TableName).DirectedNeighbors.Select(p => p.Value);

            return dependencies;
        }


        public DbModelGraphDependencyEnumerator DependecyIteration()
        {
            return new DbModelGraphDependencyEnumerator(this);
        }
    }
}
