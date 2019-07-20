using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Base folder is: {args[0]}");
            var util = new MyUtils(args[0], "SportsbookTestingModule");
            util.Process();

            Console.WriteLine("\nProcess is successful!\nPress any key to exit..");
            Console.ReadKey();
        }
    }

    public class MyUtils
    {
        public string BaseFolder { get; private set; }
        public string SearchTerm { get; private set; }

        private List<string> files;
        private Dictionary<string, bool> result;

        public MyUtils(string baseFolder, string searchTerm)
        {
            BaseFolder = baseFolder;
            SearchTerm = searchTerm;

            files = new List<string>();
            result = new Dictionary<string, bool>();
        }

        public bool Process()
        {
            try
            {
                SearchForFiles("", IsIntegrationTestFile);
                foreach (var file in files)
                    result.Add(file, ProcessFile($"{BaseFolder}\\{file}"));

                LogResult();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private void LogResult()
        {
            Console.WriteLine("The result is: ");
            Console.WriteLine($"Number of files processed: {files.Count}");
            foreach (var result in result)
                Console.WriteLine($"{result.Key, -100}: {result.Value}");
        }

        #region Folder and file filtering
        private void SearchForFiles(string relativePath, Func<string, bool> pred)
        {
            var currentDir = $"{BaseFolder}{relativePath}";

            foreach (var file in GetFiles(currentDir, pred))
                files.Add($"{relativePath}\\{file}");

            var subFolders = Directory.EnumerateDirectories(currentDir)
                .Select(p => p.Split('\\').Last());
            foreach (var subDir in subFolders)
            {
                if (string.IsNullOrEmpty(relativePath))
                    SearchForFiles($"\\{subDir}", pred);
                else
                    SearchForFiles($"\\{relativePath}\\{subDir}", pred);

            }
        }

        private List<string> GetFiles(string folder, Func<string, bool> pred)
            => Directory.GetFiles(folder)
                .Select(Path.GetFileName)
                .Where(pred)
                .ToList();

        private bool IsIntegrationTestFile(string fileName)
        {
            if (   fileName.EndsWith("effect.spec.ts")
                || fileName.EndsWith("action.spec.ts")
                || fileName.EndsWith("reducer.spec.ts")
                || fileName.EndsWith("selector.spec.ts")
                || fileName.EndsWith("effect.spec.ts")
                || fileName.EndsWith("model.spec.ts")
                || fileName.EndsWith("util.spec.ts")
                || fileName.EndsWith("utils.spec.ts")
                || fileName.EndsWith("service.spec.ts")
                || fileName.EndsWith("guard.spec.ts")
                || fileName.EndsWith("pipe.spec.ts")
                || !fileName.EndsWith(".spec.ts")
                )
            {
                return false;
            }

            return true;
        }
        #endregion

        #region File processing
        private bool ProcessFile(string path)
        {
            var file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if( IsLineIrrelevant(line) )
                    continue;

                if (line.Contains(SearchTerm))
                    return true;
            }

            file.Close();
            return false;
        }

        private bool IsLineIrrelevant(string line)
        {
            if (line.StartsWith("import")
                || line.StartsWith("}")
                || line.StartsWith("describe"))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
