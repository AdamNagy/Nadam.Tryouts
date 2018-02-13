using Nadam.Global.Lib.Graph;

namespace Nadam.Global.Lib.DirectedGraph
{
	public class DirectedEdge : Edge
	{
		public int From =>ANodeId;
		public int To => BNodeId;

		public DirectedEdge(int a, int b, int id) : base(a, b, id) {}
	}
}
