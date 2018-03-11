using System.Collections.Generic;
using System.Reflection;

namespace Nadam.Global.ConsoleShell.CommandModels
{
	/// <summary>
	/// Represent a function of a class to use as a command later
	/// </summary>
	public class CommandFunction
	{
		public string Name { get; set; }
		public IEnumerable<ParameterInfo> Parameters { get; set; }
		public bool IsStatic { get; set; }
	}
}
