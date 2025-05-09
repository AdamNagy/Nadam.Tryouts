using AngleSharp;
using AngleSharp.Dom;
using Lucene.Net.Documents;
using LuceneDNet.SearchIndex;
using LuceneDNet.WebContent;
using Document = Lucene.Net.Documents.Document;

namespace LuceneDNet.WebMirror;

public class WebScanner
{
    private readonly WebContentIndex _contentIndex;
    private readonly SearchIndexWriter _searchIndexer;
    private readonly HttpClient _client;

    public WebScanner(
        WebContentIndex contentIndex,
        SearchIndexWriter searchIndexer,
        HttpClient client)
    {
        _contentIndex = contentIndex;
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
        var needToIndex = !_contentIndex.Contains(uri);
        var page = await GetWebPage(uri);

        if (needToIndex)
        {
            IndexWebPageImages(page);
        }

        var pageLinks = GetLinks(page, uri);

        foreach (var childUri in pageLinks)
        {
            if (childUri is null || _contentIndex.Contains(childUri))
            {
                continue;
            }

            await ScanDomain(childUri.OriginalString);
        }
    }

    private void IndexWebPageImages(IDocument document)
    {
        // phase 1: collect all imag tags and index their 'alt' attribute value and 
        // the 'src' value converted to URI, and take the name of the image, and index that
        _searchIndexer.OpenWrite();
        foreach (var imageElement in document.QuerySelectorAll("img"))
        {
            Document? indexDocument = null;

            //var srcAttr = imageElement.GetAttribute("src");
            //if (!string.IsNullOrEmpty(srcAttr) && Uri.IsWellFormedUriString(srcAttr, UriKind.RelativeOrAbsolute))
            //{
            //    indexDocument = indexDocument ?? new Document();

            //    var segments = string.Join(' ', new Uri(srcAttr, UriKind.RelativeOrAbsolute).Segments);
            //    indexDocument.Add(new TextField("segments", segments, Field.Store.YES));
            //    Console.WriteLine(segments);
            //}

            var altAttr = imageElement.GetAttribute("alt");
            if (!string.IsNullOrEmpty(altAttr))
            {
                indexDocument = indexDocument ?? new Document();

                indexDocument.Add(new TextField("alt", altAttr, Field.Store.YES));
                Console.WriteLine(altAttr);
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
            .Select(s => s!);
    }

    private async Task<IDocument> GetWebPage(Uri uri, bool force = false)
    {
        string documentText;
        if (!_contentIndex.Get(uri, out documentText))
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _client.SendAsync(requestMessage);

            documentText = await response.Content.ReadAsStringAsync();
            _contentIndex.Set(uri, documentText);
        }

        return await Parse(documentText);
    }

    private async Task<IDocument> Parse(string htmlString)
    {
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        return await context.OpenAsync((req) => req.Content(htmlString));
    }
}
