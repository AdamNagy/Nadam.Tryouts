namespace Nadam.Playwright.POC;

internal sealed class ScrapeSession
{
    private readonly object _sync = new();
    private readonly List<string> _visitedUrls = new();
    private readonly List<CapturedImage> _images = new();

    public IReadOnlyList<CapturedImage> Images
    {
        get
        {
            lock (_sync)
                return _images.ToArray();
        }
    }

    public void RecordImage(string url, string contentType, byte[] content)
    {
        lock (_sync)
            _images.Add(new CapturedImage(_images.Count + 1, url, contentType, content));
    }

    public CapturedImage GetImage(int id)
    {
        lock (_sync)
        {
            if (id < 1 || id > _images.Count) throw new ArgumentOutOfRangeException(nameof(id));
            return _images[id - 1];
        }
    }

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
