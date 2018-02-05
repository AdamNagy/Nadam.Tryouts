using Nadam.Global.ConsoleShell;
using Nadam.TestServiceLibrary;

namespace OtherConsole
{
	class Program
	{
		public static ConsoleShellApp Shell { get; set; }

		static void Main(string[] args)
		{
			var rep = new EmployeeRepository();
			Shell = new ConsoleShellApp(false, "Nadam.TestServiceLibrary");
			Shell.Run();
		}
	}
}
