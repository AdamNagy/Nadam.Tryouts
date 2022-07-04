using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevGuideToCollections;

namespace Driver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UnitTests.RunTests();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
