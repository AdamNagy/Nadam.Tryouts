using System.Net;

using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace Nadam.Playwright.POC;

internal class MyProxy
{
    private ProxyServer _proxyServer;
    public void Init()
    {
        _proxyServer = new ProxyServer();

        // Explicit endpoint for the browser to connect to
        var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8001, true);
        _proxyServer.AddEndPoint(explicitEndPoint);
        //proxyServer.CertificateManager.TrustRootCertificate(true);
        // Example: Intercept and modify traffic
        _proxyServer.BeforeRequest += async (sender, e) => {
            Console.WriteLine($"Proxy Intercepted Request: {e.HttpClient.Request.Url}");

            // You can add custom headers for your scraper here
            e.HttpClient.Request.Headers.AddHeader("X-Scraper-Bot", "Titanium-Playwright");

            await Task.CompletedTask;
        };

        _proxyServer.BeforeResponse += async (object sender, SessionEventArgs e) =>
        {
            if (!e.HttpClient.Response.HasBody) return;

            var imageType = ContentDetector.GetImageType(e.HttpClient.Response.ContentType);
            var content = await e.GetResponseBody();
            var imageUri = e.HttpClient.Request.Url;
            var contentType = e.HttpClient.Response.ContentType;

            switch (imageType)
            {
                case ImageContentType.Classic:
                    ImageContentHandler.HandleClassicImage(content, imageUri, contentType);
                    break;
                case ImageContentType.Avif:
                    ImageContentHandler.HandleAvifImage(content, e.HttpClient.Request.Url, contentType);
                    break;
                case ImageContentType.NonImage:
                default:
                    return;
            }

            var imageContent = await e.GetResponseBody();
        };

        _proxyServer.Start();
    }

    public void Stop()
    {
        _proxyServer.Stop();
    }
}
