using AngleSharp.Dom;

namespace HTTPClientPOC;

public interface IHtmlParser
{
    Task<IDocument> Parse(string htmlString);
}
