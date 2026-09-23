using System.Reflection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Playwright;
using Nadam.Playwright.POC;

var profile = Path.Combine(Path.GetTempPath(), "Nadam.Playwright.Smoke", Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(profile);
Directory.SetCurrentDirectory(profile);
var session = new ScrapeSession();
var browser = new MyPlaywrightBrowser(session, NullLogger<MyPlaywrightBrowser>.Instance);
try
{
    await browser.Init();
    var context = (IBrowserContext)typeof(MyPlaywrightBrowser).GetField("_browserContext", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(browser)!;
    var sessionPage = (IPage)typeof(MyPlaywrightBrowser).GetField("_sessionPage", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(browser)!;
    Console.WriteLine("Initial status: " + await sessionPage.Locator("#visits-status").InnerTextAsync());
    await context.RouteAsync("https://smoke.test/**", route => route.FulfillAsync(new() { ContentType = "text/html", Body = "<html><body>Smoke page</body></html>" }));
    var page = await context.NewPageAsync();
    await page.GotoAsync("https://smoke.test/first");
    await page.Locator("#poc-scrap-panel .toggle").ClickAsync(new() { Timeout = 5000 });
    await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 5000 });
    await sessionPage.WaitForFunctionAsync("() => document.querySelectorAll('#visited-urls li').length === 1", null, new() { Timeout = 5000 });
    Console.WriteLine("List: " + await sessionPage.Locator("#visited-urls").InnerTextAsync());
    await sessionPage.CloseAsync();
    await page.Locator("#poc-scrap-panel .action").ClickAsync();
    await page.WaitForFunctionAsync("() => document.querySelector('#poc-scrap-panel').shadowRoot.querySelector('[role=status]').textContent === 'Session opened.'", null, new() { Timeout = 5000 });
    await Task.Delay(500);
    var reopened = (IPage)typeof(MyPlaywrightBrowser).GetField("_sessionPage", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(browser)!;
    await reopened.WaitForFunctionAsync("() => document.querySelectorAll('#visited-urls li').length === 1", null, new() { Timeout = 5000 });
    var popup = await page.RunAndWaitForPopupAsync(() => page.EvaluateAsync("() => window.open('https://smoke.test/popup')"));
    await popup.WaitForLoadStateAsync();
    if (!session.VisitedUrls.Contains("https://smoke.test/popup"))
        throw new Exception("Popup navigation was not recorded.");
    await reopened.GotoAsync("https://smoke.test/former-session");
    await page.Locator("#poc-scrap-panel .action").ClickAsync();
    await Task.Delay(500);
    var latest = (IPage)typeof(MyPlaywrightBrowser).GetField("_sessionPage", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(browser)!;
    if (latest == reopened || reopened.Url != "https://smoke.test/former-session")
        throw new Exception("Navigated tab was overwritten.");
    await latest.WaitForFunctionAsync("() => document.querySelectorAll('#visited-urls li').length === 3", null, new() { Timeout = 5000 });
    Console.WriteLine("PASS: popup captured; navigated tab preserved. ");
    Console.WriteLine("PASS: panel opens session; visited URL shown; closed session recreated.");
    await context.RouteAsync("https://smoke.test/strict", route => route.FulfillAsync(new()
    {
        ContentType = "text/html",
        Headers = new Dictionary<string, string> { ["Content-Security-Policy"] = "require-trusted-types-for 'script'; trusted-types 'none'" },
        Body = "<html><body>Trusted Types enforced</body></html>"
    }));
    await page.GotoAsync("https://smoke.test/strict");
    await page.Locator("#poc-scrap-panel .toggle").ClickAsync(new() { Timeout = 5000 });
    await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 5000 });
    await page.WaitForFunctionAsync("() => document.querySelector('#poc-scrap-panel').shadowRoot.querySelector('[role=status]').textContent === 'Session opened.'", null, new() { Timeout = 5000 });
    await page.EvaluateAsync("() => history.pushState({}, '', '/spa')");
    await latest.WaitForFunctionAsync("() => [...document.querySelectorAll('#visited-urls li')].some(li => li.textContent.endsWith('/spa'))", null, new() { Timeout = 5000 });
    if (!session.VisitedUrls.Contains("https://smoke.test/strict")) throw new Exception("Strict page not recorded.");
    Console.WriteLine("PASS: Trusted Types panel and session button; initial and SPA visits.");
    await page.EvaluateAsync("() => document.body.replaceChildren(document.createElement('main'))");
    await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 5000 });
    await page.EvaluateAsync("() => document.body.replaceWith(document.createElement('body'))");
    await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 5000 });
    var injector = await ScrapPanelInjector.LoadAsync();
    await injector.InjectAsync(page);
    await injector.InjectAsync(page);
    if (await page.Locator("#poc-scrap-panel").CountAsync() != 1)
        throw new Exception("Panel duplicated after re-injection.");
    await page.EvaluateAsync("() => document.getElementById('poc-scrap-panel').remove()");
    await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 5000 });
    await page.WaitForFunctionAsync("() => document.querySelector('#poc-scrap-panel').shadowRoot.querySelector('[role=status]').textContent === 'Session opened.'", null, new() { Timeout = 5000 });
    Console.WriteLine("PASS: panel survives body redraw/replacement/removal, preserves open state, and stays unique.");
    if (args.Contains("--pinterest"))
    {
        await page.GotoAsync("https://www.pinterest.com/", new() { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 45000 });
        // Allow the site's delayed hydration/redraw to complete before checking the panel.
        await Task.Delay(10000);
        await page.Locator("#poc-scrap-panel .toggle").ClickAsync(new() { Timeout = 10000 });
        await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 10000 });
        await page.WaitForFunctionAsync("() => document.querySelector('#poc-scrap-panel').shadowRoot.querySelector('[role=status]').textContent === 'Session opened.'", null, new() { Timeout = 10000 });
        Console.WriteLine("PASS: public Pinterest panel remains available and opens the session.");
    }
    if (args.Contains("--instagram"))
    {
        await page.GotoAsync("https://www.instagram.com/", new() { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 45000 });
        await page.Locator("#poc-scrap-panel .toggle").ClickAsync(new() { Timeout = 10000 });
        await page.Locator("#poc-scrap-panel .action").ClickAsync(new() { Timeout = 10000 });
        await page.WaitForFunctionAsync("() => document.querySelector('#poc-scrap-panel').shadowRoot.querySelector('[role=status]').textContent === 'Session opened.'", null, new() { Timeout = 10000 });
        if (!session.VisitedUrls.Any(url => new Uri(url).Host.EndsWith("instagram.com")))
            throw new Exception("Instagram navigation not recorded.");
        Console.WriteLine("PASS: public Instagram page recorded and session button works.");
    }
}
finally { await browser.Stop(); }

