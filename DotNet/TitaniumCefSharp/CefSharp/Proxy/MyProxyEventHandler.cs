using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using Titanium.Web.Proxy.Http;

namespace CefSharp.WpfApp;

public class MyProxyEventHandler
{
    private string[] _imageContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
    private string[] _imageExtendions = new[] { "jpg", "jpeg", "png", "gif", "webp" };
    private readonly string _contentRoot;

    private HashSet<string> files;

    public MyProxyEventHandler(string contentRoot)
    {
        _contentRoot = contentRoot;
    }

    public void Init()
    {
        files = new HashSet<string>();
        var imageTitles = Directory.GetFiles(_contentRoot)
            .Select(p => p.Split('\\'))
            .Select(q => q.Last().Split('/').Last());

        foreach (var file in imageTitles)
        {
            files.Add(file);
        }
    }

    public void Handle(Titanium.Web.Proxy.Http.Request request, Response response)
    {
        Task.Run(() =>
        {
            try
            {
                if (IsImageContent(response.ContentType ?? ""))
                {
                    var fileName = GetFilename(request.RequestUri, response.ContentType);
                    if (files.Contains(fileName))
                    {
                        return;
                    }

                    var needImage = false;
                    using (var stream = new MemoryStream(response.Body))
                    {
                        var bitmap = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        needImage = bitmap.Height > 600 || bitmap.Width > 600;
                    }

                    if(!needImage)
                    {
                        return;
                    }


                    File.WriteAllBytes(Path.Combine(_contentRoot, fileName),
                        response.Body);

                    files.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        });
    }

    private bool IsImageContent(string contentType)
    {
        foreach (var item in _imageContentTypes)
        {
            if (contentType.StartsWith(item, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
        }

        return false;
    }
    
    private string GetFilename(Uri contentUri, string contentType)
    {
        var fileName = contentUri.Segments.Last();
        foreach (var item in _imageExtendions)
        {
            if(fileName.EndsWith(item, StringComparison.OrdinalIgnoreCase))
            {
                return fileName;
            }
        }

        return $"{fileName}.{contentType.Split('/').Last()}";
    }
}
