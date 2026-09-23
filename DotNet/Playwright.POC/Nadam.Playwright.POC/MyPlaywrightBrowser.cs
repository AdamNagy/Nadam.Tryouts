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
    private string _sessionHtml = "";
    private string _panelScript = "";

    public MyPlaywrightBrowser(ScrapeSession session, ILogger<MyPlaywrightBrowser> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task Init()
    {
        _sessionHtml = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Session.html"));
        _panelScript = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "ScrapPanel.js"));

        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browserContext = await _playwright.Chromium.LaunchPersistentContextAsync("user-data", new()
        {
            Headless = false
        });

        _browserContext.Response += OnResponse;

        await _browserContext.ExposeBindingAsync("pocGetVisitedUrls", (BindingSource source) =>
        {
            if (source.Page != _sessionPage || source.Frame != source.Page.MainFrame
                || source.Frame.Url != "about:blank")
                throw new InvalidOperationException("Visited URLs are only available on the session page.");

            return _session.VisitedUrls;
        });

        await _browserContext.ExposeBindingAsync("pocScrapPanelAction", (BindingSource source) =>
        {
            _logger.LogInformation("Scrap panel button clicked on {Url}", source.Page.Url);
            return "C# received the click.";
        });
        await _browserContext.AddInitScriptAsync(script: _panelScript);
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
        _sessionPage = _browserContext.Pages.FirstOrDefault();
        await ShowSessionAsync();
        // SetContentAsync does not create a new document via navigation.
        foreach (var openPage in _browserContext.Pages)
            await openPage.EvaluateAsync(_panelScript);
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
                await page.SetContentAsync(_sessionHtml);
                await page.EvaluateAsync(_panelScript);
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

    // SPA navigation without a network request is not captured yet.
    private void OnResponse(object? sender, IResponse response)
    {
        try
        {
            var request = response.Request;
            // Capture document navigation across all tabs, excluding iframe and asset traffic.
            if (!request.IsNavigationRequest || request.Frame.ParentFrame != null)
                return;

            _session.RecordVisit(response.Url);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to record navigation response");
        }
    }

    public async Task Stop()
    {
        try
        {
            if (_browserContext != null)
            {
                _browserContext.Response -= OnResponse;
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
