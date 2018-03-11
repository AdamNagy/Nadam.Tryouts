using Nadam.Global.Lib.Graph;
using System.Collections.Generic;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public interface IRelationalDatabaseGraph
    {
        Node<string> GetRoot();
        int TablesCount();

        void AddTable(string newTable, IEnumerable<string> dependecies);
        void AddTable(string newTable, string dependecy);
        void AddTable(string newTable);

        IEnumerable<string> GetDependencyTables(string tableName);

        IEnumerator<string> GetEnumerator();
    }
}
