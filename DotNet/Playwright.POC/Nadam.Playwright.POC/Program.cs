using Microsoft.Playwright;
using System.Net;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

Console.WriteLine("Hello, World!");

// --- 1. SETUP TITANIUM PROXY ---
var proxyServer = new ProxyServer();

// Explicit endpoint for the browser to connect to
var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8001, true);
proxyServer.AddEndPoint(explicitEndPoint);
//proxyServer.CertificateManager.TrustRootCertificate(true);
// Example: Intercept and modify traffic
proxyServer.BeforeRequest += async (sender, e) => {
    Console.WriteLine($"Proxy Intercepted Request: {e.HttpClient.Request.Url}");

    // You can add custom headers for your scraper here
    e.HttpClient.Request.Headers.AddHeader("X-Scraper-Bot", "Titanium-Playwright");

    await Task.CompletedTask;
};

proxyServer.BeforeResponse += async (object sender, SessionEventArgs e) =>
{
    if(!e.HttpClient.Response.HasBody) return;
};

proxyServer.Start();
Console.WriteLine("Titanium Proxy started on port 8001...");

// --- 2. LAUNCH PLAYWRIGHT ---
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchPersistentContextAsync("user-data", new BrowserTypeLaunchPersistentContextOptions
{
    Headless = false, // Set to true for production
    Proxy = new Proxy { Server = "http://127.0.0.1:8001" }
});

var page = await browser.NewPageAsync();

await page.ExposeFunctionAsync("notifyScraper", (string data) =>
{
    Console.WriteLine($"[C# Received]: {data}");
    // Trigger your scraping logic here!
});

page.Load += async (sender, e) =>
{
    try
    {
        await page.EvaluateAsync(@"() => {
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
    catch { /* Handle cases where page closes during injection */ }
};



await page.GotoAsync("https://httpbin.org/get");

Console.WriteLine("Browser launched. Press any key to exit.");
Console.ReadKey();

// Cleanup
await browser.CloseAsync();
proxyServer.Stop();