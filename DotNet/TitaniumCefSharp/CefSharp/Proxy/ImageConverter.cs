using System.IO;
using ImageMagick;

namespace CefSharp.WpfApp.Proxy;

internal class ImageConverter
{
    public static MemoryStream Convert(byte[] imageData)
    {
        using (var image = new MagickImage(imageData))
        {
            image.Quality = 100;
            image.Format = MagickFormat.Jpeg;

            var stream = new MemoryStream();

            image.Write(stream);
            return stream;
        }
    }
}
