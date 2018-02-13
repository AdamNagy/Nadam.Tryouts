using System;

<<<<<<< HEAD
namespace Nadam.Global.ConsoleShell.Helpers
=======
namespace Nadam.ConsoleShell.Helpers
>>>>>>> master
{
	/// <summary>
	/// Should go to a common lib (Nadam.Lib)
	/// </summary>
	public class CommandAttribute : Attribute
	{
		public string[] CommandAliases { get; set; }
		public CommandType CommandType { get; set; }

		public CommandAttribute(string _alias, CommandType type = CommandType.Func)
		{
			CommandAliases = new string[1];
			CommandAliases[0] = _alias;
			CommandType = type;
		}

		public CommandAttribute(string[] _aliases, CommandType type = CommandType.Func)
		{
			CommandAliases = _aliases;
			CommandType = type;
		}
	}
}
