using System.IO;
using CefSharp.Handler;
using CefSharp.ResponseFilter;

namespace CefSharp.WpfApp;

public sealed class ImageRequestHandler : RequestHandler
{
    private readonly ImageStore store;

    public ImageRequestHandler(string contentRoot)
    {
        store = new ImageStore(contentRoot);
    }

    protected override IResourceRequestHandler GetResourceRequestHandler(
        IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request,
        bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
    {
        return new ImageResourceRequestHandler(store);
    }

    private sealed class ImageResourceRequestHandler : ResourceRequestHandler
    {
        private readonly ImageStore store;
        private MemoryStream responseBody;

        public ImageResourceRequestHandler(ImageStore store) => this.store = store;

        protected override IResponseFilter GetResourceResponseFilter(
            IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
            IRequest request, IResponse response)
        {
            if (!ImageStore.IsSupported(response.MimeType) || response.StatusCode != 200)
                return null;

            responseBody?.Dispose();
            responseBody = new MemoryStream();
            // The built-in filter copies the body while passing it unchanged to Chromium.
            return new StreamResponseFilter(responseBody);
        }

        protected override void OnResourceLoadComplete(
            IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
            IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            try
            {
                if (status == UrlRequestStatus.Success && responseBody != null)
                {
                    var body = responseBody.ToArray();
                    if (body.Length > 0)
                        store.SaveInBackground(request.Url, response.MimeType, body);
                }
            }
            finally
            {
                responseBody?.Dispose();
                responseBody = null;
            }
        }

        protected override void Dispose()
        {
            responseBody?.Dispose();
            responseBody = null;
            base.Dispose();
        }
    }
}
