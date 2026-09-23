namespace Nadam.Playwright.POC;

internal sealed class ScrapeSession
{
    private readonly object _sync = new();
    private readonly List<string> _visitedUrls = new();

    public IReadOnlyList<string> VisitedUrls
    {
        get
        {
            lock (_sync)
                return _visitedUrls.ToArray();
        }
    }

    public void RecordVisit(string url)
    {
        lock (_sync)
            _visitedUrls.Add(url);
    }
}
