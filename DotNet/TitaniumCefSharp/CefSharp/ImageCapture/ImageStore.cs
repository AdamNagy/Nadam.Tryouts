using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;

namespace CefSharp.WpfApp;

internal sealed class ImageStore
{
    private readonly string contentRoot;
    private readonly object saveLock = new object();

    public ImageStore(string contentRoot) => this.contentRoot = contentRoot;

    public static bool IsSupported(string mimeType) => Normalize(mimeType) is
        "image/jpeg" or "image/png" or "image/gif" or "image/webp" or "image/avif";

    private static string Normalize(string mimeType) =>
        (mimeType ?? "").Split(';')[0].Trim().ToLowerInvariant();

    public void SaveInBackground(string url, string mimeType, byte[] body)
    {
        // Only managed snapshots cross threads; CEF request/response objects are callback-scoped.
        Task.Run(() =>
        {
            try
            {
                lock (saveLock)
                {
                    Save(url, mimeType, body);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Image capture failed for {0}: {1}", url, ex);
            }
        });
    }

    private void Save(string url, string mimeType, byte[] body)
    {
        var type = Normalize(mimeType);
        if (!IsSupported(type)) return;

        var fileName = Uri.UnescapeDataString(new Uri(url).Segments.LastOrDefault() ?? "image");
        fileName = string.Concat(fileName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
        fileName = fileName.TrimEnd('.', ' ');
        if (string.IsNullOrWhiteSpace(fileName)) fileName = "image";

        var extension = type == "image/jpeg" ? "jpg" : type.Substring("image/".Length);
        if (type == "image/avif")
            fileName += ".jpeg";
        else if (!new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" }
                     .Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase))
            fileName += "." + extension;

        var path = Path.Combine(contentRoot, fileName);
        if (File.Exists(path)) return;

        using var image = new MagickImage(body);
        if (type != "image/avif" && image.Width <= 600 && image.Height <= 600) return;

        if (type == "image/avif")
        {
            image.Quality = 100;
            body = image.ToByteArray(MagickFormat.Jpeg);
        }

        Directory.CreateDirectory(contentRoot);
        File.WriteAllBytes(path, body);
    }
}
