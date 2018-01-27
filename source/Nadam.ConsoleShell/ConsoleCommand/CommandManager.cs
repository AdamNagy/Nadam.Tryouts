using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nadam.ConsoleShell.CommandModels;

namespace Nadam.ConsoleShell.ConsoleCommand
{
	public class CommandManager
	{
		public readonly CommandLibrary commandLibrary;
		private readonly string commandDomainPath;

		public CommandManager(string domainPath)
		{
			var register = new CommandRegister(domainPath);
			commandLibrary = register.RegisterCommands();
		}

		public CommandManager()
		{
			var register = new CommandRegister("Nadam.ConsoleShell.DefaultCommands");
			commandLibrary = register.RegisterCommands();
		}

		public Command BuildCommand(string input)
		{
			// Ugly regex to split string on spaces, but preserve quoted text intact:
			var stringArray = Regex.Split(input, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
			CommandClass commandClass;
			IEnumerable<ParameterInfo> parameters;
			CommandFunction cmdFunction = null;
			string commandFunction;
			bool isStatic = false;
			
			// case 2: input is a full reference, contains <class-name>.<function-name>
			if (stringArray[0].Contains('.'))
			{
				var classAndFunc = stringArray[0].Split('.');
				var className = classAndFunc[0];
				commandFunction = classAndFunc[1];
				commandClass = commandLibrary.FindCommandClass(className);
				cmdFunction = commandClass.CommandFunctions.Single(p => p.Name.Equals(commandFunction));
			}
			// case 1: input is an alias name, or only contains function and does not contain dot (.)			
			else
			{
				commandClass = commandLibrary.FindCommandClass("OtherDefaultCommands");
				commandFunction = stringArray[0];
				cmdFunction = commandClass.CommandFunctions.Single(p => p.Name.Equals(commandFunction));
				isStatic = true;
			}
			
			var cmd = new Command()
			{
				FunctionName = commandFunction,
				CommandClass = commandClass,
				IsStatic = isStatic,
				Arguments = ParseArguments(stringArray),
				CommandFunction = cmdFunction
			};
			// ParseArguments(stringArray, ref cmd);
			return cmd;
		}

		private IList<string> ParseArguments(string[] input)
		{
			var arguments = new List<string>();
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
				arguments.Add(argument);
			}
			// cmd.Arguments = Arguments;
			return arguments;
		}

		public string ExecuteCommand(Command command)
		{
			throw new NotImplementedException();
		}
	}
}
