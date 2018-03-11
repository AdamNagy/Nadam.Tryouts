using System;
using System.Collections.Generic;

namespace Nadam.Global.ConsoleShell.CommandModels
{
	/// <summary>
	/// Represent a class declared somewhere in the referenced code to have its function saved as erll as commands
	/// </summary>
    public class CommandClass
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public IEnumerable<CommandFunction> CommandFunctions { get; set; }
        public bool IsStatic { get; set; }

        public CommandClass(string name, IEnumerable<CommandFunction> commandFunctions)
        {
            Name = name;
	        CommandFunctions = commandFunctions;
        }
    }
}
