using System.Text;
using Nadam.Lib.ConsoleShell;

// All console commands must be in the sub-namespace Commands:
namespace Nadam.ConsoleShell.DefaultCommands
{
    // Must be a public static class:
    public static class DefaultCommands
    {
		[CommandShell("exit")]
        public static string Exit() => "Exit";

	    [CommandShell("help")]
		public static string GetCommands()
        {
            var commandLibraries = Program.commandManager.commandLibrary.CommandClasses;        // dont't use static
            var pen = new StringBuilder();
            foreach (var commandClass in commandLibraries)
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
