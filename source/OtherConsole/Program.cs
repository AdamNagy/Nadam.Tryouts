using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nadam.ConsoleShell;

namespace OtherConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var consoleApp = new ConsoleShellApp(false);
			consoleApp.Run();
		}
	}
}
