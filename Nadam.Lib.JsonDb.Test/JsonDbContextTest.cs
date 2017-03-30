using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Lib.JsonDb.Test.TestHelpers;

namespace Nadam.Lib.JsonDb.Test
{
    class JsonDbContextTest
    {
        [TestClass]
        public class JsonDbContextCreationEndAddTest
        {
            [TestMethod]
            public void CreateContext()
            {
                // Arrange
                // -
                // Act
                var context = new NorthwindJsonContext("");
                context.SaveChanges();

                // Assert
                Assert.AreNotEqual(context, null);
            }
        }
    }
}
