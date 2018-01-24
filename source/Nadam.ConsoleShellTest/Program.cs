using Nadam.ConsoleShell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.ConsoleShellTest
{
	class Program
	{
		public static ConsoleShell.ConsoleShell ConsoleShell;
		static void Main(string[] args)
		{
			ConsoleShell = new ConsoleShell.ConsoleShell(false);
			ConsoleShell.Run();
		}
	}
}
