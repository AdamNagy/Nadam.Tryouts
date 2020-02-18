using System.Net.Http;
using System.Web.Http;
using ManifestRepositoryApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManifestRepositoryApi.Tests
{
    [TestClass]
    public class ManifestControllerTests
    {
        // url: /api/manifest/test1
        [TestMethod]
        public void GetManifest()
        {
            // Arrange
            var controller = new ManifestController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetManifestByTitle("test1");
        }
    }
}
