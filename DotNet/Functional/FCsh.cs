using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    public delegate int GetInt();
    public delegate string GetString();

    public delegate string GetStringFor(string path);
    public delegate IEnumerable<string> GetStringsFor(string path);

    public class Context
    {
        public readonly GetInt Age;
        public readonly GetStringFor ReadFile;

        public Context()
        {
            Age += () => 29;
            ReadFile += (path) =>
                {
                    if (File.Exists(path))
                        File.ReadAllText(path);
                    
                    throw new ArgumentException($"{path} does not exist");
                };

        }
    }
}
