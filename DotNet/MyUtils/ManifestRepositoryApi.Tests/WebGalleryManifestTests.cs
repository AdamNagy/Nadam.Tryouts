using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ManifestRepositoryApi.ManifestFramework;

namespace ManifestRepositoryApi.Tests
{
    [TestClass]
    public class WebGalleryManifestTests
    {
        [TestMethod]
        public void ReadThumbnailTest()
        {
            var testFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\test-files\\test1.gallery.json";
            if (!File.Exists(testFilePath))
                throw new Exception("test file does not exist");

            var manifest = new WebGalleryManifest(testFilePath);
            var content = manifest.ReadThumbnail();
            JObject gallery = JObject.Parse(content);

            Assert.AreEqual("https://myfavouritegirls.urlgalleries.net/porn-gallery-7452241/Brooke-Ashleigh-Set", gallery["SourceUrl"].ToString());
            Assert.AreEqual("image", gallery["Type"].ToString());

            Assert.AreEqual(8, ((JArray)gallery["ImagesMetaData"]).Count);
        }

        [TestMethod]
        public void ReadWholeTest()
        {
            var testFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\test-files\\test1.gallery.json";
            if (!File.Exists(testFilePath))
                throw new Exception("test file does not exist");

            var manifest = new WebGalleryManifest(testFilePath);
            var content = manifest.ReadWhole();
            JObject gallery = JObject.Parse(content);

            Assert.AreEqual("https://myfavouritegirls.urlgalleries.net/porn-gallery-7452241/Brooke-Ashleigh-Set", gallery["SourceUrl"].ToString());
            Assert.AreEqual("image", gallery["Type"].ToString());

            Assert.AreEqual(130, ((JArray)gallery["ImagesMetaData"]).Count);
        }
    }
}
