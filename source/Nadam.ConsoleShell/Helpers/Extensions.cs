using System;
using System.Linq;
using System.Reflection;

namespace Nadam.ConsoleShell.Helpers
{
	public static class Extensions
	{
		public static bool HasIgnoreAsCommandAttribute(this MethodInfo method)
		{
			if (method.GetCustomAttributes(typeof(IgnoreAsCommandAttribute)).Any())
				return true;

			return false;
		}

		public static bool HasIgnoreAsCommandAttribute(this Type commansClass)
		{
			if (commansClass.GetCustomAttribute<IgnoreAsCommandAttribute>() != null)
				return true;
			return false;
		}

		public static string[] GetCommandAliasesFromAttribute(this MethodInfo method)
		{
			var f = method.GetCustomAttributes(typeof(CommandAttribute));
			CommandAttribute t;
			if (f.GetType().Name.Contains("CommandShellAttribute"))
			{
				t = (CommandAttribute)f.First();
				return t.CommandAliases;
			}

			return new string[1];
		}
	}
}
