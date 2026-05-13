using System;

using Microsoft.Playwright;

namespace Nadam.Playwright.POC;

internal class MyPlaywrightBrowser
{
    private IPlaywright _playwright;
    private IBrowserContext _browserContext;
    private IPage _page;

    public async Task Init(string url)
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browserContext = await _playwright.Chromium.LaunchPersistentContextAsync("user-data", new BrowserTypeLaunchPersistentContextOptions
        {
            Headless = false, // Set to true for production
            Proxy = new Proxy { Server = "http://127.0.0.1:8001" }, // Adds a proxy server for the browser. Great for scraper.
            Args = new[]
            {
                "--ignore-certificate-errors",
                $"--app={url}"  // Option A: This line opens up the browser without tabs, url input etc. It will look like a native app window. Perfect for web-desktop apps.
            }
        });

        // Option B: From here the logic opens a normal chrome window and a tab
        _page = await _browserContext.NewPageAsync();

        await _page.ExposeFunctionAsync("notifyScraper", (string data) =>
        {
            Console.WriteLine($"[C# Received]: {data}");
            // Trigger your scraping logic here!
        });

        _page.Load += async (sender, e) =>
        {
            try
            {
                await _page.EvaluateAsync(@"() => {
                    const btn = document.createElement('button');
                    btn.innerHTML = '🚀 Start Scrape';
                    btn.style.position = 'fixed';
                    btn.style.top = '10px';
                    btn.style.right = '10px';
                    btn.style.zIndex = '9999';
                    btn.style.padding = '10px';
                    btn.style.backgroundColor = '#ff4757';
                    btn.style.color = 'white';
                    btn.style.border = 'none';
                    btn.style.borderRadius = '5px';
                    btn.style.cursor = 'pointer';

                    btn.onclick = () => {
                        // Call the C# function we exposed earlier!
                        window.notifyScraper('User clicked the button on: ' + window.location.href);
                    };

                    document.body.appendChild(btn);
                }");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error injecting button: {ex.Message}");
            }
        };

        await _page.GotoAsync("https://httpbin.org/get");

        Console.WriteLine("Browser launched. Press any key to exit.");
        Console.ReadKey();
    }

    public async Task Stop()
    {
        if (_browserContext != null)
        {
            await _browserContext.CloseAsync();
            _browserContext = null;
        }
        _playwright?.Dispose();
    }
}
