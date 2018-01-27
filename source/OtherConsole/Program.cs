using Nadam.ConsoleShell;

namespace OtherConsole
{
	class Program
	{
		public static ConsoleShellApp Shell { get; set; }
		static void Main(string[] args)
		{
			Shell = new ConsoleShellApp(false, "OtherConsole");
			Shell.Run();
		}
	}
}
