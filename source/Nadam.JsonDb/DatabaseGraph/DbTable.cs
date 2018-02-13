using System;
using Nadam.Global.Lib.Graph;
using System.Collections.Generic;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public class DbTable : Node<string> //IEquatable<TableNode>
	{
        public string TableName => Value;

        public bool HaveDependency { get; set; }
        public bool DependedOn { get; set; }

		public DbTable(string name, int id) : base(name, id) { }

        public bool Equals(DbTable other)
        {
            return  other?.NodeId != 0 ? other.NodeId == other?.NodeId : TableName.Equals(other.TableName);
        }

        public static bool operator ==(DbTable a, DbTable b)
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

        public static bool operator !=(DbTable a, DbTable b)
        {
            return !(a == b);
        }
	}

    public class TableNodeComparer : IEqualityComparer<DbTable>
    {
        public bool Equals(DbTable x, DbTable y)
        {
            return x == y;
        }

        public int GetHashCode(DbTable obj)
        {
            return obj.TableName.GetHashCode();
        }
    }
}
