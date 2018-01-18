using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nadam.ConsoleShell.CommandModels;

namespace Nadam.ConsoleShell.ConsoleCommand
{
	public class CommandManager
	{
		private readonly CommandLibrary commandLibrary;

		public CommandManager()
		{
			var register = new CommandRegister("Nadam.ConsoleShell.DefaultCommands");
			commandLibrary = register.RegisterCommands();
		}

		public Command BuildCommand(string input)
		{
			// Ugly regex to split string on spaces, but preserve quoted text intact:
			var stringArray = Regex.Split(input, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

			// case 1: input is an alias name, does not contain dot (.)
			if (stringArray[0].Contains('.'))
			{
				var classAndFunc = stringArray[0].Split('.');
				var className = classAndFunc[0];
				var function = classAndFunc[1];
				var commandClass = commandLibrary.GetCommandClass(className);
			}
			// case 2: input is a full reference, contains <class-name>.<function-name>
			else
			{
				var commandFunction = stringArray[0];
			}
			throw new NotImplementedException();
		}

		public string ExecuteCommand(Command command)
		{
			throw new NotImplementedException();
		}
	}
}
