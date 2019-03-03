using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace MultiThreading_Test
{
    public class FolderChangeListener
    {
        private readonly string _dir;
        public event EventHandler<IEnumerable<string>> NewFileDetected;

        public FolderChangeListener(string directory)
        {
            _dir = directory;
        }

        public void Listen()
        {
            Console.WriteLine($"Watching {_dir} for changes...");
            var files = Directory.GetFiles(_dir).ToList();
            List <string> newFiles; 
            while(true)
            {
                newFiles = files.Except(Directory.GetFiles(_dir)).ToList();
                newFiles.AddRange(Directory.GetFiles(_dir).Except(files));

                if( newFiles.Any() )
                {
                    NewFileDetected?.Invoke(this, newFiles);
                    files = Directory.GetFiles(_dir).ToList();
                }

                Thread.Sleep(1000);
            }
        }
    }
}
