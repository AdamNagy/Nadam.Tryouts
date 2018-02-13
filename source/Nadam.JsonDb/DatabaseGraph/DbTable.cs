using System;
using Nadam.Global.Lib.Graph;
using System.Collections.Generic;

<<<<<<< HEAD:source/Nadam.JsonDb/DatabaseGraph/DbTable.cs
namespace Nadam.Global.JsonDb.DatabaseGraph
=======
namespace Nadam.JsonDb.DatabaseGraph
>>>>>>> master:source/Nadam.JsonDb/DatabaseGraph/TableNode.cs
{
    public class DbTable : Node<string> //IEquatable<TableNode>
	{
	    public Node<string> Node { get; set; }
        public string TableName => Node.Value;

        public bool HaveDependency { get; set; }
        public bool DependedOn { get; set; }

	    public DbTableModel(string tableName)
	    {
		    Node = new Node<string>(tableName);
	    }

	    public DbTableModel(string tableName, int id)
	    {
			Node = new Node<string>(tableName, id);
		}

	    public DbTableModel(Node<string> node)
	    {
		    Node = node;
	    }

        public bool Equals(TableNode other)
        {
            return  other?.Node.NodeId != 0 ? Node.NodeId == other?.Node.NodeId : TableName.Equals(other.TableName);
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

	    public static explicit operator TableNode(Node<string> b)  // explicit byte to digit conversion operator
	    {
		    return new TableNode(b);
	    }

	    public static explicit operator Node<string>(TableNode b)  // explicit byte to digit conversion operator
	    {
		    return new Node<string>(b.TableName, b.Node.NodeId);
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
