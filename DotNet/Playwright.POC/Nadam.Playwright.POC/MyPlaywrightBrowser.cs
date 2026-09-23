using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace Nadam.Playwright.POC;

internal class MyPlaywrightBrowser
{
    private readonly ScrapeSession _session;
    private readonly ILogger<MyPlaywrightBrowser> _logger;
    private IPlaywright? _playwright;
    private IBrowserContext? _browserContext;
    private IPage? _sessionPage;
    private readonly SemaphoreSlim _sessionPageLock = new(1, 1);
    private SessionPageLoader _sessionPageLoader = null!;
    private ScrapPanelInjector _panelInjector = null!;

    public MyPlaywrightBrowser(ScrapeSession session, ILogger<MyPlaywrightBrowser> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task Init()
    {
        _sessionPageLoader = await SessionPageLoader.LoadAsync();
        _panelInjector = await ScrapPanelInjector.LoadAsync();

        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browserContext = await _playwright.Chromium.LaunchPersistentContextAsync("user-data", new()
        {
            Headless = false
        });

        _browserContext.Page += OnPage;
        _browserContext.RequestFinished += OnRequestFinished;
        foreach (var existingPage in _browserContext.Pages)
            OnPage(_browserContext, existingPage);

        await _browserContext.ExposeBindingAsync("pocGetVisitedUrls", (BindingSource source) =>
        {
            EnsureSessionPage(source);
            return _session.VisitedUrls;
        });

        await _browserContext.ExposeBindingAsync("pocGetImages", (BindingSource source) =>
        {
            EnsureSessionPage(source);
            // Poll metadata only; image bytes are requested once when an image enters view.
            return _session.Images.Select(image => new
            {
                id = image.Id, url = image.Url, contentType = image.ContentType, byteLength = image.ByteLength
            }).ToArray();
        });
        await _browserContext.ExposeBindingAsync("pocGetImageData", (BindingSource source, int id) =>
        {
            EnsureSessionPage(source);
            return _session.GetImage(id).ToDataUrl();
        });

        await _browserContext.ExposeBindingAsync("pocScrapPanelAction", (BindingSource source) =>
        {
            _logger.LogInformation("Scrap panel button clicked on {Url}", source.Page.Url);
            return "C# received the click.";
        });
        await _panelInjector.RegisterAsync(_browserContext);
        await _browserContext.ExposeBindingAsync("pocShowSession", async (BindingSource source) =>
        {
            try
            {
                await ShowSessionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to open session page");
                throw;
            }
        });

        await _browserContext.ExposeFunctionAsync("notifyScraper", (string data) =>
        {
            _logger.LogInformation("Session page message: {Message}", data);
            return "C# received the message.";
        });
        // Reuse Chromium's initial blank tab, but never overwrite a restored website.
        _sessionPage = _browserContext.Pages.FirstOrDefault(page => page.Url == "about:blank");
        await ShowSessionAsync();
        // SetContentAsync does not create a new document via navigation.
        foreach (var openPage in _browserContext.Pages)
            await _panelInjector.InjectAsync(openPage);
    }

    public async Task ShowSessionAsync()
    {
        await _sessionPageLock.WaitAsync();
        try
        {
            var context = _browserContext ?? throw new InvalidOperationException("Browser is not running.");
            if (_sessionPage == null || _sessionPage.IsClosed)
                _sessionPage = await context.NewPageAsync();

            var page = _sessionPage;
            // Rebuild after a reload; an existing session page is only brought to the front.
            if (!await page.EvaluateAsync<bool>("() => document.documentElement.hasAttribute('data-scraper-session')"))
            {
                await _sessionPageLoader.RenderAsync(page);
                await _panelInjector.InjectAsync(page);
                page.FrameNavigated += (_, frame) =>
                {
                    // A user may use this tab for browsing. Do not overwrite it later.
                    if (frame == page.MainFrame && _sessionPage == page)
                        _sessionPage = null;
                };
            }
            await page.BringToFrontAsync();
            _logger.LogInformation("Session page opened");
        }
        finally
        {
            _sessionPageLock.Release();
        }
    }

    private void EnsureSessionPage(BindingSource source)
    {
        if (source.Page != _sessionPage || source.Frame != source.Page.MainFrame
            || source.Frame.Url != "about:blank")
            throw new InvalidOperationException("Session data is only available on the session page.");
    }

    private async void OnRequestFinished(object? sender, IRequest request)
    {
        try
        {
            var response = await request.ResponseAsync();
            if (response == null || !response.Ok) return;

            var contentType = (await response.HeaderValueAsync("content-type"))?
                .Split(';')[0].Trim().ToLowerInvariant();
            if (contentType == null || !contentType.StartsWith("image/", StringComparison.Ordinal)) return;

            // RequestFinished fires after download; this reads the original bytes without refetching.
            var content = await response.BodyAsync();
            if (content.Length == 0) return;

            _session.RecordImage(response.Url, contentType, content);
            _logger.LogDebug("Captured image {Url} ({ContentType}, {ByteLength} bytes)",
                response.Url, contentType, content.Length);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not capture image response for {Url}", request.Url);
        }
    }

    private void OnPage(object? sender, IPage page)
    {
        page.PageError += (_, error) => _logger.LogError("Browser script failed on {Url}: {Error}", page.Url, error);
        page.FrameNavigated += (_, frame) => RecordNavigation(frame);
        // A popup's initial navigation may have committed before Context.Page fires.
        RecordNavigation(page.MainFrame);
    }

    private void RecordNavigation(IFrame frame)
    {
        if (frame.ParentFrame != null
            || !Uri.TryCreate(frame.Url, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            return;

        _session.RecordVisit(frame.Url);
        _logger.LogInformation("Visited page: {Url}", frame.Url);
    }
    public async Task Stop()
    {
        try
        {
            if (_browserContext != null)
            {
                _browserContext.Page -= OnPage;
                _browserContext.RequestFinished -= OnRequestFinished;
                await _browserContext.CloseAsync();
            }
        }
        finally
        {
            _browserContext = null;
            _sessionPage = null;
            _playwright?.Dispose();
            _playwright = null;
        }
    }
}
