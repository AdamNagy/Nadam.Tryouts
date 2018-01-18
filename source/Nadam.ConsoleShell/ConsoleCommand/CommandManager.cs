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
			string commandClass, commandFunction;
			bool isStatic = false;

			// case 1: input is an alias name, does not contain dot (.)
			if (stringArray[0].Contains('.'))
			{
				var classAndFunc = stringArray[0].Split('.');
				var className = classAndFunc[0];
				commandFunction = classAndFunc[1];
				commandClass = commandLibrary.GetCommandClass(className).Name;
			}
			// case 2: input is a full reference, contains <class-name>.<function-name>
			else
			{
				commandClass = "DefaultCommands";
				commandFunction = stringArray[0];
				isStatic = true;
			}
			
			var cmd = new Command()
			{
				FunctionName = commandFunction,
				ClassName = commandClass,
				IsStatic = isStatic
			};
			ParseArguments(stringArray, ref cmd);
			return cmd;
		}

		private void ParseArguments(string[] input, ref Command cmd)
		{
			var Arguments = new List<string>();
			for (int i = 1; i < input.Length; i++)
			{

				var inputArgument = input[i];

				// Assume that most of the time, the input argument is NOT quoted text:
				string argument = inputArgument;

				// Is the argument a quoted text string?
				var regex = new Regex("\"(.*?)\"", RegexOptions.Singleline);
				var match = regex.Match(inputArgument);

				// If it IS quoted, there will be at least one capture:
				if (match.Captures.Count > 0)
				{
					// Get the unquoted text from within the qoutes:
					var captureQuotedText = new Regex("[^\"]*[^\"]");
					var quoted = captureQuotedText.Match(match.Captures[0].Value);

					// The argument should include all text from between the quotes
					// as a single string:
					argument = quoted.Captures[0].Value;
				}
				Arguments.Add(argument);
			}
			cmd.Arguments = Arguments;
		}

		public string ExecuteCommand(Command command)
		{
			throw new NotImplementedException();
		}
	}
}
