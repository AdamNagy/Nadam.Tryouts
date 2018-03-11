﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Nadam.ConsoleShell.CommandModels;
using Nadam.ConsoleShell.ConsoleCommand;

namespace Nadam.ConsoleShell
{
	class Program
	{
		// public static CommandLibrary CommandLibrary { get; set; }
		public static CommandManager commandManager;

		private const string ReadPrompt = ">>> ";
		const string CommandNamespace = "DefaultCommands";

		static void Main(string[] args)
		{
			#region  <init_program>
			Console.Title = "Nadam Console Shell";
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.CursorVisible = true;
			Console.CursorSize = 100;
			Console.Clear();
			Console.WriteLine($"Copyright Adam, NAGY 2017\nv{Assembly.GetExecutingAssembly().GetName().Version}\n");

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(Program.CurrentAssemblyDirectory);
			#endregion </init_program>

			//var commandRegister = new CommandRegister(CommandNamespace);
			//CommandLibrary = commandRegister.RegisterCommands();
			commandManager = new CommandManager();

			Run();
		}

		static void Run()
		{
			bool auto = false;
			var exitied = false;
			while (!exitied)
			{
				var consoleInput = auto ? "GetCommands" : ReadFromConsole();
				auto = false;

				if (string.IsNullOrWhiteSpace(consoleInput)) continue;

				try
				{
					// Create a ConsoleCommand instance:
					//var cmd = new Command(consoleInput);
					//cmd.Type = CommandLibrary.CommandClasses.FirstOrDefault(p => p.Name.Equals(cmd.ClassName))?.Type;

					var command = commandManager.BuildCommand(consoleInput);

					// Execute the command:
					string result = Execute(command);
					exitied = result == "Exit";

					// Write out the result:
					WriteToConsole(result);
				}
				catch (Exception ex)
				{
					// OOPS! Something went wrong - Write out the problem:
					WriteToConsole(ex.Message);
				}
			}
		}

		static string Execute(Command command)
		{
			// Validate the class name and command name:
			// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

			string badCommandMessage = string.Format(""
			                                         + "Unrecognized command \'{0}.{1}\'. "
			                                         + "Please type a valid command.",
				command.ClassName, command.FunctionName);

			// Validate the command name:
			//if (!CommandLibraries.ContainsKey(command.LibraryClassName))
			//if (CommandLibraries.SingleOrDefault(p => p.Name == command.LibraryClassName) == null)
			//{
			//    return badCommandMessage;
			//}
			//var methodDictionary = CommandLibraries[command.LibraryClassName];
			//var methodDictionary = CommandLibraries.Single(p => p.Name == command.LibraryClassName).Commands;
			//var methodDictionary = CommandLibrary.CommandClasses.First(p => p.Name == command.ClassName).CommandFunctions;

			// if (!methodDictionary.ContainsKey(command.Name))
			//if (methodDictionary.SingleOrDefault(p => p.Name.Equals(command.FunctionName)) == null)
			//{
			//	return badCommandMessage;
			//}

			// Make sure the corret number of required arguments are provided:
			// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

			var methodParameterValueList = new List<object>();
			// IEnumerable<ParameterInfo> paramInfoList = methodDictionary[command.Name].ToList();
			IEnumerable<ParameterInfo> paramInfoList = command.CommandFunction.Parameters; // methodDictionary.SingleOrDefault(p => p.Name.Equals(command.FunctionName)).Parameters;//.ToList()));

			// Validate proper # of required arguments provided. Some may be optional:
			var requiredParams = paramInfoList.Where(p => p.IsOptional == false);
			var optionalParams = paramInfoList.Where(p => p.IsOptional == true);
			int requiredCount = requiredParams.Count();
			int optionalCount = optionalParams.Count();
			int providedCount = command.Arguments.Count();

			if (requiredCount > providedCount)
			{
				return string.Format(
					"Missing required argument. {0} required, {1} optional, {2} provided",
					requiredCount, optionalCount, providedCount);
			}

			// Make sure all arguments are coerced to the proper type, and that there is a 
			// value for every emthod parameter. The InvokeMember method fails if the number 
			// of arguments provided does not match the number of parameters in the 
			// method signature, even if some are optional:
			// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

			if (paramInfoList.Any())
			{
				// Populate the list with default values:
				foreach (var param in paramInfoList)
				{
					// This will either add a null object reference if the param is required 
					// by the method, or will set a default value for optional parameters. in 
					// any case, there will be a value or null for each method argument 
					// in the method signature:
					methodParameterValueList.Add(param.DefaultValue);
				}

				// Now walk through all the arguments passed from the console and assign 
				// accordingly. Any optional arguments not provided have already been set to 
				// the default specified by the method signature:
				for (int i = 0; i < command.Arguments.Count(); i++)
				{
					var methodParam = paramInfoList.ElementAt(i);
					var typeRequired = methodParam.ParameterType;
					object value = null;
					try
					{
						// Coming from the Console, all of our arguments are passed in as 
						// strings. Coerce to the type to match the method paramter:
						value = CoerceArgument(typeRequired, command.Arguments.ElementAt(i));
						methodParameterValueList.RemoveAt(i);
						methodParameterValueList.Insert(i, value);
					}
					catch (ArgumentException ex)
					{
						string argumentName = methodParam.Name;
						string argumentTypeName = typeRequired.Name;
						string message =
							string.Format(""
							              + "The value passed for argument '{0}' cannot be parsed to type '{1}'",
								argumentName, argumentTypeName);
						throw new ArgumentException(message);
					}
				}
			}

			// Set up to invoke the method using reflection:
			// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

			Assembly current = typeof(Program).Assembly;

			// Need the full Namespace for this:
			Type commandLibaryClass = command.CommandClass.Type;
				// current.GetType($"Nadam.ConsoleShell.{command.ClassName}.{command.ClassName}");

			object[] inputArgs = null;
			if (methodParameterValueList.Count > 0)
			{
				inputArgs = methodParameterValueList.ToArray();
			}
			var typeInfo = commandLibaryClass;

			// This will throw if the number of arguments provided does not match the number 
			// required by the method signature, even if some are optional:
			try
			{
				Object result;
				if (command.IsStatic)
				{
					result = typeInfo.InvokeMember(
						command.FunctionName,
						BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
						null, null, inputArgs);
					return result.ToString();
				}

				var reference = Activator.CreateInstance(command.CommandClass.Type);
				typeInfo = command.CommandClass.Type;

				var methodInfo = typeInfo.GetMethod(command.FunctionName);
				result = methodInfo.Invoke(
					reference,
					BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly,
					null, inputArgs, null);
				return result.ToString();

			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}

		static object CoerceArgument(Type requiredType, string inputValue)
		{
			var requiredTypeCode = Type.GetTypeCode(requiredType);
			string exceptionMessage =
				string.Format("Cannnot coerce the input argument {0} to required type {1}",
					inputValue, requiredType.Name);

			object result = null;
			switch (requiredTypeCode)
			{
				case TypeCode.String:
					result = inputValue;
					break;

				case TypeCode.Int16:
					short number16;
					if (Int16.TryParse(inputValue, out number16))
					{
						result = number16;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;

				case TypeCode.Int32:
					int number32;
					if (Int32.TryParse(inputValue, out number32))
					{
						result = number32;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;

				case TypeCode.Int64:
					long number64;
					if (Int64.TryParse(inputValue, out number64))
					{
						result = number64;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;

				case TypeCode.Boolean:
					bool trueFalse;
					if (bool.TryParse(inputValue, out trueFalse))
					{
						result = trueFalse;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;

				case TypeCode.Byte:
					byte byteValue;
					if (byte.TryParse(inputValue, out byteValue))
					{
						result = byteValue;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;

				case TypeCode.Char:
					char charValue;
					if (char.TryParse(inputValue, out charValue))
					{
						result = charValue;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;

				case TypeCode.DateTime:
					DateTime dateValue;
					if (DateTime.TryParse(inputValue, out dateValue))
					{
						result = dateValue;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				case TypeCode.Decimal:
					Decimal decimalValue;
					if (Decimal.TryParse(inputValue, out decimalValue))
					{
						result = decimalValue;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				case TypeCode.Double:
					Double doubleValue;
					if (Double.TryParse(inputValue, out doubleValue))
					{
						result = doubleValue;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				case TypeCode.Single:
					Single singleValue;
					if (Single.TryParse(inputValue, out singleValue))
					{
						result = singleValue;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				case TypeCode.UInt16:
					UInt16 uInt16Value;
					if (UInt16.TryParse(inputValue, out uInt16Value))
					{
						result = uInt16Value;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				case TypeCode.UInt32:
					UInt32 uInt32Value;
					if (UInt32.TryParse(inputValue, out uInt32Value))
					{
						result = uInt32Value;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				case TypeCode.UInt64:
					UInt64 uInt64Value;
					if (UInt64.TryParse(inputValue, out uInt64Value))
					{
						result = uInt64Value;
					}
					else
					{
						throw new ArgumentException(exceptionMessage);
					}
					break;
				default:
					throw new ArgumentException(exceptionMessage);
			}
			return result;
		}

		public static void WriteToConsole(string message = "")
		{
			if (message.Length > 0)
			{
				Console.WriteLine(message);
			}
		}

		public static string ReadFromConsole(string promptMessage = "")
		{
			// Show a prompt, and get input:
			Console.Write(ReadPrompt + promptMessage);
			return Console.ReadLine();
		}

		public static string CurrentAssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}
	}
}