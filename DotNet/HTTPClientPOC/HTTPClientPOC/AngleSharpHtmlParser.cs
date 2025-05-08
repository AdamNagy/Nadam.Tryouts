using AngleSharp.Dom;
using AngleSharp;

namespace HTTPClientPOC;

public class AngleSharpHtmlParser : IHtmlParser
{
    public async Task<IDocument> Parse(string htmlString)
    {
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var rootNode = await context.OpenAsync(req => req.Content(htmlString));
        return rootNode;
    }
}
