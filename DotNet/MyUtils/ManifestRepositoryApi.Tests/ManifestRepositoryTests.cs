using ManifestRepositoryApi.ManifestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManifestRepositoryApi.Tests
{
    [TestClass]
    public class ManifestRepositoryTests
    {
        // C:\Users\adna01\Documents\OBG-SB
        [TestMethod]
        public void InitRepository()
        {
            ManifestRepository.Instance.Init(@"C:\Users\adna01\Documents\OBG-SB");
            Assert.AreEqual(17, ManifestRepository.Instance.Count);
        }
    }
}
