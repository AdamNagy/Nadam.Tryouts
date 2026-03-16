namespace Nadam.Playwright.POC;

internal static class ImageContentHandler
{
    private static string[] ImageExtendions = new[] { "jpg", "jpeg", "png", "gif", "webp" };

    public static void HandleClassicImage(byte[] content, string imageUri, string contentType)
    {
        var imgUri = new Uri(imageUri);
        var fileName = GetFilename(imgUri, contentType);

    }

    public static void HandleAvifImage(byte[] content, string imageUri, string contentType)
    {

    }

    private static string GetFilename(Uri contentUri, string contentType)
    {
        var fileName = contentUri.Segments.Last();
        foreach (var item in ImageExtendions)
        {
            if (fileName.EndsWith(item, StringComparison.OrdinalIgnoreCase))
            {
                return fileName;
            }
        }

        return $"{fileName}.{contentType.Split('/').Last()}";
    }
}
