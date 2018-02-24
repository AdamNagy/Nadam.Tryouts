using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public class DbModelGraphDependencyEnumerator : IEnumerator<string>, IEnumerable<string>
    {
        public DbModelGraph Graph { get; set; }

        public Stack<string> TableStack { get; set; }
        public List<string> SingleTables { get; set; }

        private string _current;

        public DbModelGraphDependencyEnumerator(DbModelGraph _graph)
        {
            Graph = _graph;
        }

        public string Current
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
            if (TableStack.Any())
            {
                _current = TableStack.Pop();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            TableStack = new Stack<string>();
            SingleTables = new List<string>();

            BuildTableLis(Graph.Root.Value);
            BuildTableStack();
        }

        private void BuildTableLis(string currentRoot)
        {
            IEnumerable<string> dependencies;
            string node = currentRoot;

            SingleTables.Add(node);

            dependencies = Graph.GetDependencyTables(currentRoot);
            foreach (var table in dependencies)
            {
                BuildTableLis(table);
            }
        }

        private void BuildTableStack()
        {
            SingleTables.Reverse();

            SingleTables = SingleTables.Distinct().ToList();
            SingleTables.Reverse();
            foreach (var item in SingleTables.Skip(1))  // skip(1) is to skip the root node
            {
                TableStack.Push(item);
            }
        }

        public IEnumerator<string> GetEnumerator()
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
