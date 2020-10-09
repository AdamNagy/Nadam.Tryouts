using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    public class ConsoleCommandAttribute : Attribute
    {
        public string Name { get; private set; }
        public string ShortName { get; set; }
        public string Description { get; private set; }

        public Type Input { get; private set; }
        public Type Result { get; private set; }
        
        public ConsoleCommandAttribute(
            string name,
            string shortName,
            string description,
            Type input,
            Type result)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
            Input = input;
            Result = result;
        }
    }

    public static class ConsoleCommands
    {
        [ConsoleCommand(
            "migratev2",
            "-mg2",
            "migrats the old type of gallery to new, decoupled json entity",
            typeof(IEnumerable<string>),
            typeof(void))]
        public static void MigrateV2()
        {

        }
    }
}
