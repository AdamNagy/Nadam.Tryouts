namespace Graphs.Graph
{
	public class Edge
	{
		public int EdgeId { get; set; }
		public int ANodeId { get; set; }
		public int BNodeId { get; set; }

		public Edge(int a, int b, int id)
		{
			ANodeId = a;
			BNodeId = b;
			EdgeId = id;
		}
	}
}
