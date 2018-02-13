<<<<<<< HEAD
﻿using Nadam.Global.ConsoleShell;
using Nadam.TestServiceLibrary;
=======
﻿using Nadam.ConsoleShell;
>>>>>>> master

namespace OtherConsole
{
	class Program
	{
		public static ConsoleShellApp Shell { get; set; }
<<<<<<< HEAD

		static void Main(string[] args)
		{
			var rep = new EmployeeRepository();
			Shell = new ConsoleShellApp(false, "Nadam.TestServiceLibrary");
=======
		static void Main(string[] args)
		{
			Shell = new ConsoleShellApp(false, "OtherConsole");
>>>>>>> master
			Shell.Run();
		}
	}
}
