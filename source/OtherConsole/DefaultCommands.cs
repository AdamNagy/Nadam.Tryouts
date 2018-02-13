using System.Text;
<<<<<<< HEAD
using Nadam.Global.ConsoleShell.Helpers;

namespace OtherConsole
{
	public class DefaultCommands
	{
		[Command(new []{"help", "?"})]
		public static string GetCommands()
=======
using Nadam.ConsoleShell.DefaultCommands;
using Nadam.ConsoleShell.Helpers;

namespace OtherConsole
{
	public class OtherDefaultCommands : DefaultCommands
	{
		[Command("help")]
		public string GetCommands()
>>>>>>> master
		{
			var pen = new StringBuilder();
			var library = Program.Shell.GetCommandLibrary;

			foreach (var commandClass in library.CommandClasses)
			{
				pen.Append($"\n{commandClass.Name}:\n");
				foreach (var command in commandClass.CommandFunctions)
				{
					pen.Append($"\t{command.Name}\n");
				}
			}

			return pen.ToString();
		}
<<<<<<< HEAD

		[Command(new []{ "exit", "Exit", "terminate" })]
		public static string Exit() => "Exit";
=======
>>>>>>> master
	}
}
