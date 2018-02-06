using System.Text;
using Nadam.Global.ConsoleShell.Helpers;

namespace OtherConsole
{
	public class DefaultCommands
	{
		[Command(new []{"help", "?"})]
		public static string GetCommands()
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

		[Command(new []{ "exit", "Exit", "terminate" })]
		public static string Exit() => "Exit";
	}
}
