namespace Nadam.Playwright.POC;

internal sealed class ScrapeSession
{
    private readonly object _sync = new();
    private readonly List<string> _visitedUrls = new();
    private readonly List<CapturedImage> _images = new();
    private readonly ImageCaptureFilter _imageFilter;

    public ScrapeSession(uint minimumImageSide = 256)
    {
        _imageFilter = new ImageCaptureFilter(minimumImageSide);
    }

    public IReadOnlyList<CapturedImage> Images
    {
        get
        {
            lock (_sync)
                return _images.ToArray();
        }
    }

    public bool TryRecordImage(string url, string contentType, byte[] content)
    {
        if (!_imageFilter.TryGetAcceptedSize(content, out var width, out var height)) return false;

        lock (_sync)
        {
            _images.Add(new CapturedImage(_images.Count + 1, url, contentType, content, width, height));
        }

        return true;
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
