using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
<<<<<<< HEAD
using Nadam.Global.ConsoleShell.CommandModels;
using Nadam.Global.ConsoleShell.Helpers;
=======
using Nadam.ConsoleShell.CommandModels;
using Nadam.ConsoleShell.Helpers;
using Nadam.Lib;
>>>>>>> master

namespace Nadam.Global.ConsoleShell.ConsoleCommand
{
	/// <summary>
	/// Register class that will scan the app domain and gather all classes and turn them into a CommandLibrary
	/// TODO: Implementation will come from the Program.cs (kind a refactor)

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

			//foreach (var commandClass in RegisterDefaultCommandClasses(ref domainTypes))
			//{
			//	commandClasses.Add(commandClass);
			//}


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

		private IList<CommandClass> RegisterDefaultCommandClasses(ref IList<Type> defaultDomainTypes)
		{
			var commandLibraries = new List<CommandClass>();
			foreach (var commandClass in defaultDomainTypes.Where(p => p.Name.Contains("DefaultCommands")))
			{
				// Load the method info from each class into a dictionary:
				// BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly
				var methods = commandClass.GetMethods(BindingFlags.Static | BindingFlags.Public);
				var methodDictionary = new List<CommandFunction>();
				foreach (var method in methods)
				{
					//var hasCommandAttribute = method.Attributes;
					string commandName = method.Name;
					methodDictionary.Add(new CommandFunction
					{
						Name = commandName,
						Parameters = method.GetParameters()
					});
				}
				// Add the dictionary of methods for the current class into a dictionary of command classes:
				//CommandLibraries.Add(commandClass.Name, methodDictionary);
				commandLibraries.Add(new CommandClass(commandClass.Name, methodDictionary){Type = commandClass});
			}

			return commandLibraries;
		}

		private IList<Type> GetDomainClassed()
		{
<<<<<<< HEAD
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
=======
			var commandClasses = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(p => p.IsClass && // p.FullName.Contains("Nadam") &&
							!p.FullName.Contains("Nadam.ConsoleShell"))
				.ToList();

			// need to add logic to filte out unnecessary dll-s
			List<Assembly> allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			foreach (string dll in Directory.GetFiles(CurrentAssemblyDirectory, "*.dll"))
				allAssemblies.Add(Assembly.LoadFile(dll));

			foreach (var assemblyName in allAssemblies.Select(p => p.FullName))
			{
				if (assemblyName.Contains(CommandNamespace))

>>>>>>> master
				{
					commandClasses.Add(type);
				}
			}

			return commandClasses;
		}
	}
}
