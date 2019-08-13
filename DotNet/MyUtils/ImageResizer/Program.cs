using System.Drawing;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDir = @"E:\Documents\MIV\app_data\Adam_Nagy\Babes\images\OnlyAllSites\Aimee_R",
                   thumbsDir = "thumbs";

            if (!Directory.Exists($"{baseDir}\\{thumbsDir}"))
                Directory.CreateDirectory($"{baseDir}\\{thumbsDir}");

            foreach (var imageTitle in Directory.GetFiles(baseDir))
            {
                var image = Image.FromFile(imageTitle);
                var resized = ToQuarterSize(image);
                var thumbFileName = $"{Path.GetFileNameWithoutExtension(imageTitle)}_t";
                resized.Save($"{baseDir}\\{thumbsDir}\\{thumbFileName}.jpg");
                resized.Dispose();
                System.Console.WriteLine(thumbFileName);
            }            
        }

        public static Image ToQuarterSize(Image origImage)
        {
            int origwidth = origImage.Width,
                origHeight = origImage.Height;

            int quarteredWidth = origwidth / 4,
                quarteredHeight = origHeight / 4;

            var resized = (Image)new Bitmap(origImage, new Size(quarteredWidth, quarteredHeight));
            return resized;
        }
    }
}
