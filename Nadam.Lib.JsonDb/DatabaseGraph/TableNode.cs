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
        public TableNode(string tableName, int id) : base(tableName, id){}
        public bool Equals(TableNode other)
        {
            return  other?.NodeId != 0 ? NodeId == other?.NodeId : TableName.Equals(other.TableName);
        }

        public static bool operator ==(TableNode a, TableNode b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null) ||
                a.TableName == null || b.TableName == null)
            {
                return false;
            }

            if( a.NodeId != 0 && b.NodeId != 0 )
                return a.TableName == b.TableName &&
                        a.NodeId == b.NodeId;

            return a.TableName == b.TableName;
        }

        public static bool operator !=(TableNode a, TableNode b)
        {
            return !(a == b);
        }
    }

    public class TableNodeComparer : IEqualityComparer<TableNode>
    {
        public bool Equals(TableNode x, TableNode y)
        {
            return x == y;
        }

        public int GetHashCode(TableNode obj)
        {
            return obj.TableName.GetHashCode();
        }
    }
}
