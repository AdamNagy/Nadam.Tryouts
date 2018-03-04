using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
    public class DependencyEnumerator<T> : IEnumerator<string>
    {
        public IRelationalDatabaseGraph Graph { get; set; }

        public Stack<string> TableStack { get; set; }
        public ISet<string> SingleTables { get; set; }

        private string _current;

        public DependencyEnumerator(IRelationalDatabaseGraph _graph)
        {
            Graph = _graph;
            Reset();
        }

        public string Current
        {
            get { return _current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose() {}

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
            SingleTables = new HashSet<string>();

            BuildTableLis(Graph.GetRoot().Value);
            BuildTableStack();
        }

        private void BuildTableLis(string currentRoot)
        {
            IEnumerable<string> dependencies;
            string node = currentRoot;
            
            SingleTables.Add(node);

            dependencies = Graph.GetDependencyTables(currentRoot);
            foreach (var table in dependencies)            
                BuildTableLis(table);            
        }

        private void BuildTableStack()
        {
            SingleTables.Reverse();
            foreach (var item in SingleTables.Skip(1))  // skip(1) is to skip the root node            
                TableStack.Push(item);            
        }
    }
}
