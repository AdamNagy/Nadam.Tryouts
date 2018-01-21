using System;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.ConsoleShell.CommandModels
{
	/// <summary>
	/// List type class that represents all the commands with their aliasas
	/// </summary>
	public class CommandLibrary
	{
		public IEnumerable<CommandClass> CommandClasses { get; set; }
		public Dictionary<string, int> CommandClasseDict { get; set; }

		public CommandClass FindCommandClass(string className)
		{
			return CommandClasses.SingleOrDefault(p => p.Name.Equals(className));
		}
	}
}
