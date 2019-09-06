using System;
using System.Drawing;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        private struct Config
        {
            public string sourceDir;
            public string destDir;
        }

        static void Main(string[] args)
        {
            Config config;

            try
            {
                config = InitApp(args);
                foreach (var imageTitle in Directory.GetFiles(config.sourceDir))
                {
                    var thumbFileName = $"{Path.GetFileNameWithoutExtension(imageTitle)}_t";
                    if (File.Exists($"{config.destDir}\\{thumbFileName}.jpg"))
                        continue;

                    var image = Image.FromFile(imageTitle);
                    var resized = ToQuarterSize(image);

                    resized.Save($"{config.destDir}\\{thumbFileName}.jpg");
                    resized.Dispose();
                    Console.WriteLine(thumbFileName);
                }       
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Done! \nPress any key to exit..");
            Console.ReadKey();
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

        private static Config InitApp(string[] args)
        {
            var conf = new Config();
            switch(args.Length)
            {
                case 1:
                    conf.sourceDir = args[0];
                    conf.destDir = $"{args[0]}\\thumbnails";
                    break;

                case 2:            
                    conf.sourceDir = args[0];
                    conf.destDir = args[1];
                    break;

                default: throw new Exception("No source dir and/or des dir were provided for program");            
            }            

            if( !Directory.Exists(conf.sourceDir) )
                throw new Exception("The source dir does not exist, please provide an existing one");

            if (!Directory.Exists(conf.destDir))
            {
                Directory.CreateDirectory($"{conf.sourceDir}\\thumbnails");
                conf.destDir = $"{conf.sourceDir}\\thumbnails";
            }

            return conf;
        }
    }
}
