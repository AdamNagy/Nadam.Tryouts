namespace HTTPClientPOC;

public class CustomWebClient : IWebClient
{
    private readonly HttpClient _client;
    private readonly IHtmlParser _htmlParser;

    public CustomWebClient()
    {
        _client = new HttpClient();
        _htmlParser = new AngleSharpHtmlParser();
    }

    public async Task<IEnumerable<string>> Requeset(string url)
    {
        var response = await _client.GetAsync(url);

        var content = await response.Content.ReadAsStringAsync();
        var dom = await _htmlParser.Parse(content);

        var imageElements = dom.QuerySelectorAll("img");

        if(!imageElements?.Any() ?? false) 
        {
            return Enumerable.Empty<string>();
        }

        return imageElements.Select((image) => image.GetAttribute("src")).Where(p => !string.IsNullOrWhiteSpace(p));
    }
}

