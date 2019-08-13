using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using TechnologyTryouts.ImageManipulation;

namespace CshTryouts.Tests
{
    [TestClass]
    public class ImageManipulationTests
    {
        [TestMethod]
        public void Test1()
        {
            var baseDir =
                @"C:\Users\adna01\Documents\Learning\Nadam.Tryouts\DotNet\CshTryouts\CshTryouts\ImageManipulation\images";
            var image = Image.FromFile($"{baseDir}\\02_Bugatti-VGT_photo_ext_WEB.jpg");
            var resized = ImageManipulation.ToQuarterSize(image);

            resized.Save($"{baseDir}\\resized-1.jpg");
            Assert.AreEqual(resized.Height, 270);
            Assert.AreEqual(resized.Width, 480);
        }
    }
}
