using System.Text;
using Nadam.ConsoleShell.DefaultCommands;
using Nadam.ConsoleShell.Helpers;

namespace OtherConsole
{
	public class OtherDefaultCommands : DefaultCommands
	{
		[Command("help")]
		public string GetCommands()
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
	}
}
