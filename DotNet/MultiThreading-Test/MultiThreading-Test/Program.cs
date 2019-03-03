using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using MultiThreading_Test.ConsoleClasses;

namespace MultiThreading_Test
{
    public delegate void ObserverMethod(IEnumerable<string> newFiles);

    class Program
    {
        delegate void TestDelegate(string root, string fileToProcess, int random, int id);

        static void Main(string[] args)
        {
            var count = 120;
            using (var progress = new ConsoleProgressBar(3, count))
            {
                for (int i = 0; i <= count+1; i++)
                {
                    progress.Progress();
                    Thread.Sleep(20);
                }
            }
            // Console.WriteLine("Done.");

            // Console.SetBufferSize(120, 100);
            // Test2Runner();
            // RunTest3();

            Console.WriteLine("\nDone");
            Console.ReadKey();
        }

        #region Test 3
        static void RunTest3()
        {
            //var length = 3;
            //var managers = new List<ConsoleProgressBarManager>(3);
            //var thredPool = new List<Thread>(length);
            //var locker = new object();
            //var line = 1;

            //managers.Add(new ConsoleProgressBarManager(1, 30, locker, $"Title {1}"));
            //managers.Add(new ConsoleProgressBarManager(3, 50, locker, $"Title {2}"));
            //managers.Add(new ConsoleProgressBarManager(6, 70, locker, $"Title {3}"));

            //for (int i = 0; i < length; i++)
            //{
            //    thredPool.Add(new Thread(new ThreadStart(managers[i].Run)));
            //}

            //thredPool.ForEach(thred => thred.Start());
        }
        #endregion

        #region Test 2
        static void Test2Runner()
        {
            var folderWatcherThread = Test2();
            while (true)
            {
                var line = Console.ReadLine();
                if (string.Equals(line, "exit"))
                {
                    folderWatcherThread.Abort();
                    Console.WriteLine("aborted");
                    break;
                }
            }
        }

        public static void OnNewFile(Object sender, IEnumerable<string> newFiles)
        {
            foreach (var file in newFiles)
            {
                Console.WriteLine(file);
            }
        }

        static Thread Test2()
        {            
            var listener = new FolderChangeListener(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            listener.NewFileDetected += Program.OnNewFile;
            var thread = new Thread(new ThreadStart(listener.Listen));
            thread.IsBackground = true;
            thread.Start();

            return thread;
        }
        #endregion

        #region Test 1
        static void Test1Runner()
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Random rnd = new Random();
            var files = new List<string>()
            {
                "File-1", "File-2","File-3", "File-4", "File-5"
            };

            var thredPool = new List<Thread>(files.Count);
            FaikDownloadManager manager;
            for (int i = 0; i < files.Count; i++)
            {
                manager = new FaikDownloadManager(currentDir, files[i], rnd.Next(3, 10), i);
                thredPool.Add(new Thread(new ThreadStart(manager.Download)));
                thredPool[i].Start();
            }

            for (int i = 0; i < files.Count; i++)
            {
                thredPool[i].Join();
            }
        }

        static void PrintOut(object parameter)
        {
            Console.WriteLine(parameter);
        }
        #endregion
    }
}
