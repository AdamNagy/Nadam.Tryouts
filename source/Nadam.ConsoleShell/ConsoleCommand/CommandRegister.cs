using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nadam.Global.ConsoleShell.CommandModels;
using Nadam.Global.ConsoleShell.Helpers;

namespace Nadam.Global.ConsoleShell.ConsoleCommand
{
	/// <summary>
	/// Register class that will scan the app domain and gather all classes and turn them into a CommandLibrary
	/// </summary>
	class CommandRegister
	{
		public string CommandNamespace { get; set; }

		public CommandRegister(string defaultCommandNamespace)
		{
			CommandNamespace = defaultCommandNamespace;
		}

		public CommandLibrary RegisterCommands()
		{
			var library = new CommandLibrary();

			var domainTypes = GetDomainClassed();
			var commandClasses = RegisterInstanceFunctions(ref domainTypes);

			library.CommandClasses = commandClasses;
			return library;
		}

		private IList<CommandClass> RegisterInstanceFunctions(ref IList<Type> otherDomainTypes)
		{
			var commandLibraries = new List<CommandClass>();
			foreach (var commandClass in otherDomainTypes)
			{
				if(commandClass.HasIgnoreAsCommandAttribute())
					continue;

				try
				{
					var instanceMethods = commandClass.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName);
					var staticMethids = commandClass.GetMethods(BindingFlags.Static | BindingFlags.Public);
					var methodDictionary = new List<CommandFunction>();
					foreach (var method in instanceMethods)
					{
						if(method.HasIgnoreAsCommandAttribute())
							continue;

						methodDictionary.Add(new CommandFunction
						{
							Name = method.Name,
							Parameters = method.GetParameters(),
							IsStatic = false
						});
					}

					foreach (var method in staticMethids)
					{
						if (method.HasIgnoreAsCommandAttribute())
							continue;

						methodDictionary.Add(new CommandFunction
						{
							Name = method.Name,
							Parameters = method.GetParameters(),
							IsStatic = true
						});
					}


					if (methodDictionary.Count > 0 )
						commandLibraries.Add(new CommandClass(commandClass.Name, methodDictionary) { Type = commandClass });
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return commandLibraries;
		}

		private IList<Type> GetDomainClassed()
		{
			List<Type> commandClasses = new List<Type>();
			List<Assembly> allAssemblies = AppDomain.CurrentDomain
											.GetAssemblies()
											.Where(p => p.FullName.StartsWith(CommandNamespace))
											.ToList();

			allAssemblies.Add(
				AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(p => p.FullName.StartsWith("OtherConsole"))
				.ToList().First());

			foreach (var assembly in allAssemblies)
			{
				foreach (var type in assembly.GetTypes())
				{
					commandClasses.Add(type);
				}
			}

			return commandClasses;
		}
	}
}
