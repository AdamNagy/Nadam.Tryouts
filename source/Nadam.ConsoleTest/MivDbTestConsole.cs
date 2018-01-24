using System;
using Nadam.ConsoleTest.MIV;
using Nadam.JsonDb;

namespace Nadam.ConsoleTest
{
    class MivDbTestConsole
    {
        public static void TestRunner()
        {
            var runner = new MivDbTestConsole();
            //runner.Run();
            runner.UpdateTest();
        }

        private void Run()
        {
            var context = new MivExtensionDbContext();
            foreach (var tighs in context.TightsTypes)
            {
                Console.WriteLine(tighs.Name);
            }
        }

        private void UpdateTest()
        {
            var context = new MivExtensionDbContext();
            HighHeelImage toUpdate;
            context.SelectForUpdate<HighHeelImage>(p => p.Id == 1, out toUpdate);
            if (toUpdate != null)
                toUpdate.HighHeelId = 135;

            context.SaveChanges();
        }
    }
}
