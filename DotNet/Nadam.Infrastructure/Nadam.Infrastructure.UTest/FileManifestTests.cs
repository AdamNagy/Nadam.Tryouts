using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nadam.Infrastructure.UTest
{
    [TestClass]
    public class FileManifestTests
    {
        private string rootDir;
        private const string testFilesExtension = "txt";

        #region Test helper methods
        [TestInitialize]
        public void BeforeAll()
        {

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            rootDir = Path.GetDirectoryName(path);
        }

        [TestCleanup]
        public void AfterAll()
        {
            var testFiles = Directory.GetFiles(rootDir).Where(p => p.EndsWith(testFilesExtension));
            foreach (var testFile in testFiles)
            {
                File.Delete(testFile);
            }
        }
        #endregion

        [TestMethod]
        public void PathTest()
        {
            var fileManifest = new FileManifest(rootDir, "TestFile", "txt");

            var expectedPath = $"{rootDir}\\TestFile.txt";
            Assert.AreEqual(expectedPath, fileManifest.Path);
        }


    }
}
