using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CustomQueryable.Tests
{
    [TestClass]
    public class LinqToFolderSystemTests
    {
        [TestMethod]
        public void QuerySyntax1()
        {
            var query = from element in new FileSystemContext(@"C:\Users\adna01\Documents\Learning\Readings")
                where element.ElementType == ElementType.File
                orderby element.Path ascending
                select element;

            var files = query.ToList();
            Assert.AreEqual(4, files.Count());
        }

        [TestMethod]
        public void MethodSyntax1()
        {
            var query = new FileSystemContext(@"C: \Users\adna01\Documents\Learning\Readings")
                .Where(p => p.ElementType == ElementType.File);

            var files = query.ToList();
            Assert.AreEqual(4, files.Count());
        }
    }
}
