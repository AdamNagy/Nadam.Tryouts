namespace Nadam.Playwright.POC;

internal static class ContentDetector
{
    private static string[] ImageContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
    public static string AvifContentType = "image/avif";

    // TODO: Handle GIF
    public static string GifContentType = "";

    public static ImageContentType GetImageType(string? contentType)
    {
        if (contentType is null) return ImageContentType.NonImage;

        foreach (var item in ImageContentTypes)
        {
            if (contentType.StartsWith(item, StringComparison.OrdinalIgnoreCase) == true)
            {
                return ImageContentType.Classic;
            }
        }

        if (contentType == AvifContentType) return ImageContentType.Avif;

        return ImageContentType.NonImage;
    }
}

public enum ImageContentType
{
    Classic, Avif, NonImage
}
