using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.JsonDb.DatabaseGraph
{
    public class DbModelGraphDependencyEnumerator : IEnumerator<TableNode>, IEnumerable<TableNode>
    {
        public DbModelGraph Graph { get; set; }

        public Stack<TableNode> TableStack { get; set; }
        public List<TableNode> SingleTables { get; set; }

        private TableNode _current;

        public DbModelGraphDependencyEnumerator(DbModelGraph _graph)
        {
            Graph = _graph;            
        }

        public TableNode Current
        {
            get { return _current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            Dispose();
        }

        public bool MoveNext()
        {
            if(TableStack.Any())
            {
                _current = TableStack.Pop();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            TableStack = new Stack<TableNode>();
            SingleTables = new List<TableNode>();

            BuildTableLis(Graph.Root);
            BuildTableStack();
        }

        private void BuildTableLis(TableNode currentRoot)
        {
            IEnumerable<TableNode> children;
            TableNode node = currentRoot;

            SingleTables.Add(node);

            children = node.Neighbors.Select(p => (TableNode)p);
            foreach (var table in children)
            {
                BuildTableLis(table);
            }
        }

        private void BuildTableStack()
        {
            SingleTables.Reverse();

            SingleTables = SingleTables.Distinct(new TableNodeComparer())
                                       .ToList();
            SingleTables.Reverse();
            foreach (var item in SingleTables.Skip(1))  // skip(1) is to skip the root node
            {
                TableStack.Push(item);
            }
        }

        public IEnumerator<TableNode> GetEnumerator()
        {
            Reset();
            return TableStack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
