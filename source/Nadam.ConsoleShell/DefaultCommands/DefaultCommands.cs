using Nadam.ConsoleShell.Helpers;

// All console commands must be in the sub-namespace Commands:
namespace Nadam.ConsoleShell.DefaultCommands
{
    // Must be a public static class:
    public class DefaultCommands
    {
		[Command("exit")]
        public string Exit() => "Exit";
    }
}
