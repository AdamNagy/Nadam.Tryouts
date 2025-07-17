using AngleSharp;
using AngleSharp.Dom;
using Lucene.Net.Documents;
using LuceneDNet.Domain.SearchIndex;
using LuceneDNet.Domain.WebContent;
using Document = Lucene.Net.Documents.Document;

namespace LuceneDNet.Domain.WebMirror;

public class WebScanner
{
    private readonly WebContentIndex _htmlIndex;
    private readonly WebContentIndex _scriptIndex;
    private readonly WebContentIndex _styleIndex;

    private readonly SearchIndexWriter _searchIndexer;
    private readonly HttpClient _client;

    private readonly HashSet<string> _indexedImages = new HashSet<string>();

    public WebScanner(
        WebContentIndex htmlIndex,
        WebContentIndex scriptIndex,
        WebContentIndex styleIndex,
        SearchIndexWriter searchIndexer,
        HttpClient client)
    {
        _htmlIndex = htmlIndex;
        _scriptIndex = scriptIndex;
        _styleIndex = styleIndex;

        _searchIndexer = searchIndexer;
        _client = client;
    }

    public async Task ScanDomain(string entryUrl)
    {
        if (!Uri.IsWellFormedUriString(entryUrl, UriKind.Absolute))
        {
            throw new ArgumentException($"Url is incorrect. '{entryUrl}'");
        }

        var uri = new Uri(entryUrl, UriKind.Absolute);
        var needToIndex = !_htmlIndex.Contains(uri);
        var page = await GetWebPage(uri);

        if (needToIndex)
        {
            IndexWebPageImages(page, uri);
        }

        var pageLinks = GetLinks(page, uri);

        // Index the child pages
        foreach (var childUri in pageLinks.Where(p => !_htmlIndex.Contains(p)))
        {
            var childPage = await GetWebPage(childUri);

            IndexWebPageImages(childPage, childUri);
        }

        // Scan the child pages
        foreach (var childUri in pageLinks)
        {
            await ScanDomain(childUri.OriginalString);
        }
    }

    public async Task ReIndex()
    {
        foreach (var item in _htmlIndex)
        {
            var document = await Parse(item.content);
            IndexWebPageImages(document, new Uri(item.url));
        }

        Console.WriteLine("Done reindexing");
    }

    private void IndexWebPageImages(IDocument document, Uri sourceUri)
    {
        // phase 1: collect all image tags and index their 'alt' attribute value and 
        // the 'src' value converted to URI, and take the name of the image, and index that
        _searchIndexer.OpenWrite();
        foreach (var linkElement in document.QuerySelectorAll("a"))
        {
            Document? indexDocument = null;

            var imageElement = linkElement.QuerySelector("img");

            if (imageElement is null)
            {
                continue;
            }

            var srcAttr = imageElement.GetAttribute("src");
            var altAttr = imageElement.GetAttribute("alt");
            var hrefAttr = linkElement.GetAttribute("href");

            if (!string.IsNullOrEmpty(srcAttr) && Uri.IsWellFormedUriString(srcAttr, UriKind.RelativeOrAbsolute) && !string.IsNullOrEmpty(altAttr))
            {
                indexDocument = new Document();

                if (Uri.IsWellFormedUriString(srcAttr, UriKind.Relative))
                {
                    srcAttr = $"{sourceUri.Scheme}://{sourceUri.Host}/{srcAttr.TrimStart('/')}";
                }

                if (!_indexedImages.Contains(srcAttr))
                {
                    indexDocument.Add(new TextField("link", hrefAttr, Field.Store.YES));
                    indexDocument.Add(new TextField("imgSrc", srcAttr, Field.Store.YES));
                    indexDocument.Add(new TextField("imgAlt", altAttr, Field.Store.YES));

                    _indexedImages.Add(srcAttr);

                    Console.WriteLine(srcAttr);
                }
            }

            if (indexDocument is null)
            {
                continue;
            }

            _searchIndexer.Write(indexDocument);
        }

        _searchIndexer.Commit();

        // phase 2: centrical enumeration: starting from the element which has inner text
        // and look for the closest image element and index the same properties as phase 1.
    }

    private IEnumerable<Uri> GetLinks(IDocument document, Uri sourceUrl)
    {
        return document.QuerySelectorAll("a")
            .Select(p => p.GetAttribute("href"))
            .Where(t => !t.StartsWith("javascript"))
            .Select(q =>
            {
                if (Uri.IsWellFormedUriString(q, UriKind.Relative))
                {
                    var builder = new UriBuilder(sourceUrl.Scheme, sourceUrl.Host);
                    builder.Path = q;
                    return builder.Uri;
                }
                else if (Uri.IsWellFormedUriString(q, UriKind.Absolute))
                {
                    return new Uri(q);
                }

                return null;
            })
            .Where(r => r is not null)
            .Select(s => s!)
            .Where(t => !IsDuplicateContent(t, sourceUrl));
    }

    private async Task SaveScripts(IDocument document, Uri sourceUrl)
    {
        foreach (var scriptSrc in document.QuerySelectorAll("script")
            .Select(p => p.GetAttribute("src"))
            .Select(p => NormalizeUri(p, sourceUrl))
            .Where(p => p is not null))
        {
            if (_scriptIndex.Contains(scriptSrc!))
            {
                continue;
            }

            var script = await Get(scriptSrc!);
            _scriptIndex.Set(scriptSrc!, script);
        }
    }

    private async Task SaveStyles(IDocument document, Uri sourceUrl)
    {
        foreach (var styleSrc in document.QuerySelectorAll("link")
            .Select(p =>
            {
                var linkType = p.GetAttribute("rel");

                if (linkType != "stylesheet")
                {
                    return null;
                }

                return p.GetAttribute("href");
            })
            .Select(p => NormalizeUri(p, sourceUrl))
            .Where(p => p is not null))
        {
            if (_styleIndex.Contains(styleSrc!))
            {
                continue;
            }

            var style = await Get(styleSrc!);
            _styleIndex.Set(styleSrc!, style);
        }
    }

    private Uri? NormalizeUri(string? uriString, Uri SourceUri)
    {
        if (string.IsNullOrEmpty(uriString))
        {
            return null;
        }

        if (Uri.IsWellFormedUriString(uriString, UriKind.Relative))
        {
            var builder = new UriBuilder(SourceUri.Scheme, SourceUri.Host);
            builder.Path = uriString;
            return builder.Uri;
        }
        else if (Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
        {
            return new Uri(uriString);
        }

        return null;
    }

    private async Task<IDocument> GetWebPage(Uri uri, bool force = false)
    {
        string documentText;
        if (!_htmlIndex.Get(uri, out documentText))
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _client.SendAsync(requestMessage);

            documentText = await response.Content.ReadAsStringAsync();
            _htmlIndex.Set(uri, documentText);
        }

        return await Parse(documentText);
    }

    private async Task<string> Get(Uri uri)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await _client.SendAsync(requestMessage);
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<IDocument> Parse(string htmlString)
    {
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        return await context.OpenAsync((req) => req.Content(htmlString));
    }

    public static bool IsDuplicateContent(Uri uri, Uri sourceUrl)
    {
        var queryStringKeys = uri.Query.TrimStart('?').Split('&').SelectMany(p => p.Split('='));
        if (queryStringKeys.Contains("currency"))
        {
            return true;
        }

        return sourceUrl.Host != uri.Host;
    }
}
