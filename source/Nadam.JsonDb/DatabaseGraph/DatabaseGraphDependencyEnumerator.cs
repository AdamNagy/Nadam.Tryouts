using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Global.JsonDb.DatabaseGraph
{
   // public class DbModelGraphDependencyEnumerator : IEnumerator<DbTable>, IEnumerable<DbTable>
   // {
   //     public DbModelGraph Graph { get; set; }

   //     public Stack<DbTable> TableStack { get; set; }
   //     public List<DbTable> SingleTables { get; set; }

   //     private DbTable _current;

   //     public DbModelGraphDependencyEnumerator(DbModelGraph _graph)
   //     {
   //         Graph = _graph;            
   //     }

   //     public DbTable Current
   //     {
   //         get { return _current; }
   //     }

   //     object IEnumerator.Current
   //     {
   //         get { return Current; }
   //     }

   //     public void Dispose()
   //     {
   //         Dispose();
   //     }

   //     public bool MoveNext()
   //     {
   //         if(TableStack.Any())
   //         {
   //             _current = TableStack.Pop();
   //             return true;
   //         }
   //         return false;
   //     }

   //     public void Reset()
   //     {
   //         TableStack = new Stack<DbTable>();
   //         SingleTables = new List<DbTable>();

   //         BuildTableLis(Graph.Root);
   //         BuildTableStack();
   //     }

   //     private void BuildTableLis(DbTable currentRoot)
   //     {
   //      //   IEnumerable<DbTable> children;
	  //      //DbTable node = currentRoot;

   //      //   SingleTables.Add(node);

   //      //   children = Graph.GetN node.Neighbors.Select(p => (DbTable)p);
   //      //   foreach (var table in children)
   //      //   {
   //      //       BuildTableLis(table);
   //      //   }
		 //throw new NotImplementedException();
   //     }

   //     private void BuildTableStack()
   //     {
   //         SingleTables.Reverse();

   //         SingleTables = SingleTables.Distinct(new TableNodeComparer())
   //                                    .ToList();
   //         SingleTables.Reverse();
   //         foreach (var item in SingleTables.Skip(1))  // skip(1) is to skip the root node
   //         {
   //             TableStack.Push(item);
   //         }
   //     }

   //     public IEnumerator<DbTable> GetEnumerator()
   //     {
   //         Reset();
   //         return TableStack.GetEnumerator();
   //     }

   //     IEnumerator IEnumerable.GetEnumerator()
   //     {
   //         return GetEnumerator();
   //     }
   // }
}
