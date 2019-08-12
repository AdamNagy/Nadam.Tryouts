using System;
using System.IO;
using System.Drawing;

namespace TechnologyTryouts.ImageManipulation
{
    public class ImageManipulation
    {
        public static void CreateHtmlImgData()
        {
            var self = new ImageManipulation();

            var image = self.ReadImage();
            var base64 = Convert.ToBase64String(image);
            self.ToFile(ref base64);
        }

        private void ToFile(ref string data)
        {
            using (StreamWriter outputFile = new StreamWriter("..\\..\\Image2Base64\\imagedata.txt"))
            {               
                outputFile.WriteLine(data);
            }
        }

        public byte[] ReadImage()
        {
            var image = File.ReadAllBytes(@"C:\Users\adna01\Documents\Learning\Nadam.Tryouts\DotNet\CshTryouts\CshTryouts\ImageManipulation\images\02_Bugatti-VGT_photo_ext_WEB.jpg");
            return image;
        }

        public static void ToQuarterSize()
        {
            var image = Image.FromFile(@"C:\Users\adna01\Documents\Learning\Nadam.Tryouts\DotNet\CshTryouts\CshTryouts\ImageManipulation\images\02_Bugatti-VGT_photo_ext_WEB.jpg");
            var resized = (Image)(new Bitmap(image, new Size(480, 270)));
            resized.Save(@"C:\Users\adna01\Documents\Learning\Nadam.Tryouts\DotNet\CshTryouts\CshTryouts\ImageManipulation\images\02_Bugatti-VGT_photo_ext_WEB_t.jpg");
        }
    }
}
