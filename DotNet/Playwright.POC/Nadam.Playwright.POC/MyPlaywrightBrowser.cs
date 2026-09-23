using Microsoft.Playwright;

namespace Nadam.Playwright.POC;

internal class MyPlaywrightBrowser
{
    private IPlaywright? _playwright;
    private IBrowserContext? _browserContext;

    public async Task Init()
    {
        var html = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Session.html"));
        var panelScript = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "ScrapPanel.js"));
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browserContext = await _playwright.Chromium.LaunchPersistentContextAsync("user-data", new()
        {
            Headless = false
        });

        await _browserContext.ExposeBindingAsync("pocScrapPanelAction", (BindingSource source) =>
        {
            Console.WriteLine($"[Scrap panel] Button clicked on: {source.Page.Url}");
            return "C# received the click.";
        });
        await _browserContext.AddInitScriptAsync(script: panelScript);

        var page = _browserContext.Pages.FirstOrDefault() ?? await _browserContext.NewPageAsync();
        await page.ExposeFunctionAsync("notifyScraper", (string data) =>
        {
            Console.WriteLine($"[C# Received]: {data}");
            return "C# received the message.";
        });
        await page.SetContentAsync(html);
        // SetContentAsync does not create a new document via navigation.
        foreach (var openPage in _browserContext.Pages)
            await openPage.EvaluateAsync(panelScript);
        await page.BringToFrontAsync();
    }

    public async Task Stop()
    {
        try
        {
            if (_browserContext != null)
                await _browserContext.CloseAsync();
        }
        finally
        {
            _browserContext = null;
            _playwright?.Dispose();
            _playwright = null;
        }
    }
}
