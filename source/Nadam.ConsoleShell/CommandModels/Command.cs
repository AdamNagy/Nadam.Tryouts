using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nadam.Global.ConsoleShell.CommandModels
{
    public class Command
    {
        public IList<string> Arguments { get; set; }
        public string FunctionName { get; set; }
        public string ClassName { get; set; }
        public Type Type { get; set; }
        public bool IsStatic { get; set; }
	    public CommandClass CommandClass { get; set; }
	    public CommandFunction CommandFunction { get; set; }

	    public Command()
	    {
		    
	    }

        public Command(string input)
        {
            // Ugly regex to split string on spaces, but preserve quoted text intact:
            var stringArray = Regex.Split(input, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            Arguments = new List<string>();
            for (int i = 0; i < stringArray.Length; i++)
            {
                // The first element is always the command:
                if (i == 0)
                {
                    this.FunctionName = stringArray[i];

                    // Set the default:
                    this.ClassName = "DefaultCommands";
                    string[] s = stringArray[0].Split('.');
                    if (s.Length == 2)
                    {
                        this.ClassName = s[0];
                        this.FunctionName = s[1];
                    }
                }
                else
                {
                    var inputArgument = stringArray[i];

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
            }
        }
    }
}
