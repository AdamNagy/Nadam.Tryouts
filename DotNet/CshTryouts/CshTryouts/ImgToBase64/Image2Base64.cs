using System;
using System.IO;

namespace TechnologyTryouts.Image2Base64
{
    class Image2Base64
    {
        public static void CreateHtmlImgData()
        {
            var self = new Image2Base64();
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
            var image = File.ReadAllBytes("..\\..\\Image2Base64\\images\\02_Bugatti-VGT_photo_ext_WEB.jpg");
            return image;
        }
    }
}
