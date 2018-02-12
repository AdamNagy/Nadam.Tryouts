using System.Collections.Generic;
using Nadam.Global.Lib.Graph;

namespace Nadam.Global.Lib.DirectedGraph
{
	public class DirectedNode<T> : Node<T>
	{
		public IList<DirectedNode<T>> DirectedNeighbors { get; set; }

		public DirectedNode(T value, int id) : base(value, id)
		{
			DirectedNeighbors = new List<DirectedNode<T>>();
		}

		public DirectedNode(T value, int id, List<DirectedNode<T>> neighbours) : base(value, id)
		{
			DirectedNeighbors = neighbours;
		}

		public void AddNeighbour(DirectedNode<T> n)
		{
			DirectedNeighbors.Add(n);
		}
	}
}
