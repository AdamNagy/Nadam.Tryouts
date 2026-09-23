using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace Nadam.Playwright.POC;

internal class MyPlaywrightBrowser
{
    private readonly ScrapeSession _session;
    private readonly ILogger<MyPlaywrightBrowser> _logger;
    private IPlaywright? _playwright;
    private IBrowserContext? _browserContext;

    public MyPlaywrightBrowser(ScrapeSession session, ILogger<MyPlaywrightBrowser> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task Init()
    {
        var html = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Session.html"));
        var panelScript = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "ScrapPanel.js"));
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browserContext = await _playwright.Chromium.LaunchPersistentContextAsync("user-data", new()
        {
            Headless = false
        });

        _browserContext.Response += OnResponse;

        await _browserContext.ExposeBindingAsync("pocScrapPanelAction", (BindingSource source) =>
        {
            _logger.LogInformation("Scrap panel button clicked on {Url}", source.Page.Url);
            return "C# received the click.";
        });
        await _browserContext.AddInitScriptAsync(script: panelScript);

        var page = _browserContext.Pages.FirstOrDefault() ?? await _browserContext.NewPageAsync();
        await page.ExposeFunctionAsync("notifyScraper", (string data) =>
        {
            _logger.LogInformation("Session page message: {Message}", data);
            return "C# received the message.";
        });
        await page.SetContentAsync(html);
        // SetContentAsync does not create a new document via navigation.
        foreach (var openPage in _browserContext.Pages)
            await openPage.EvaluateAsync(panelScript);
        await page.BringToFrontAsync();
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
            _playwright?.Dispose();
            _playwright = null;
        }
    }
}
