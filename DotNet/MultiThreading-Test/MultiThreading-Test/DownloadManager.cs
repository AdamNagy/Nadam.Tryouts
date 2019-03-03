using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading_Test
{
    public class FaikDownloadManager
    {
        private readonly string _root;
        private readonly string _fileToProcess;
        private readonly int _random;
        private readonly int _thrredNum;
        private const string _LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ipsum risus, ullamcorper non ex ut, dictum faucibus felis. Vivamus vel ligula mi. Suspendisse blandit suscipit augue, eget semper quam feugiat eu. Donec sollicitudin, velit sed volutpat bibendum, magna turpis vestibulum eros, vitae eleifend leo augue sed leo. Pellentesque nunc dui, molestie vel rutrum eget, feugiat a nisi. Vestibulum quam neque, feugiat sed sollicitudin ut, mollis quis tellus. Ut luctus tortor risus, nec feugiat nibh fermentum in.";

        public FaikDownloadManager(string root, string fileToProcess, int random, int id)
        {
            _root = root;
            _fileToProcess = fileToProcess;
            _random = random;
            _thrredNum = id;
        }

        public void Download()
        {
            Directory.CreateDirectory($"{_root}\\{_fileToProcess}");
            var stream = File.Create($"{_root}\\{_fileToProcess}\\{_fileToProcess}.txt");
            stream.Close();

            for (int i = 0; i < _random; i++)
            {
                File.WriteAllText($"{_root}\\{_fileToProcess}\\{_fileToProcess}.txt", $"{_LoremIpsum}\n");
            }

            Console.WriteLine($"Thread is done: {_thrredNum}");
        }
    }
}
