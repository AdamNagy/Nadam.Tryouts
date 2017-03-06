using System;
using Nadam.Lib.Graph;
using System.Collections.Generic;

namespace Nadam.Lib.DatabaseGraphs
{
    public class TableNode : GraphNode<string>, IEquatable<TableNode>
    {
        public string TableName => Value;

        public bool HaveDependency { get; set; }
        public bool DependedOn { get; set; }

        public TableNode(string tableName) : base(tableName) { }
        public TableNode(int id, string tableName) : base(tableName, id){}
        public bool Equals(TableNode other)
        {
            return  other?.NodeId != 0 ? NodeId == other?.NodeId : TableName.Equals(other.TableName);
        }
    }

    public class TableNodeComparer : IEqualityComparer<TableNode>
    {
        public bool Equals(TableNode x, TableNode y)
        {
            return x.NodeId == y.NodeId;
        }

        public int GetHashCode(TableNode obj)
        {
            return obj.NodeId.GetHashCode();
        }
    }
}
