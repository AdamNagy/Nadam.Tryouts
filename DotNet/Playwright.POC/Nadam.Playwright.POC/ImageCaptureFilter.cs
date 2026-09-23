using ImageMagick;

namespace Nadam.Playwright.POC;

internal sealed class ImageCaptureFilter
{
    public uint MinimumSide { get; }

    public ImageCaptureFilter(uint minimumSide = 256)
    {
        if (minimumSide == 0) throw new ArgumentOutOfRangeException(nameof(minimumSide));
        MinimumSide = minimumSide;
    }

    public bool TryGetAcceptedSize(byte[] content, out uint width, out uint height)
    {
        width = height = 0;
        if (content.Length == 0) return false;
        try
        {
            // Read dimensions from the original file, not its CSS display size.
            // MagickImageInfo uses Ping, without decoding the full pixel buffer.
            var info = new MagickImageInfo(content);
            width = info.Width;
            height = info.Height;
            return width >= MinimumSide && height >= MinimumSide;
        }
        catch (MagickException)
        {
            // Unknown or invalid dimensions must not bypass the collection filter.
            return false;
        }
    }
}
