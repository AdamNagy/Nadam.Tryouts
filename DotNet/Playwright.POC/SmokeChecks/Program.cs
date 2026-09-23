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
    if (context.Pages.Count != 1 || context.Pages[0] != sessionPage)
        throw new Exception("Startup should open only the session tab in a fresh profile.");
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
    var imageRequests = 0;
    var png = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+aY1cAAAAASUVORK5CYII=");
    await context.RouteAsync("https://smoke.test/images/**", async route =>
    {
        switch (new Uri(route.Request.Url).AbsolutePath)
        {
            case "/images/page":
                await route.FulfillAsync(new() { ContentType = "text/html", Body = "<html><body><img src='/images/photo'><div style=\"width:40px;height:40px;background-image:url('/images/background')\"></div><iframe src='/images/frame'></iframe><img src='/images/missing'></body></html>" });
                break;
            case "/images/frame":
                await route.FulfillAsync(new() { ContentType = "text/html", Body = "<html><body><img src='/images/frame-photo'></body></html>" });
                break;
            case "/images/background":
                Interlocked.Increment(ref imageRequests);
                await route.FulfillAsync(new() { ContentType = "image/svg+xml; charset=utf-8", Body = "<svg xmlns='http://www.w3.org/2000/svg' width='20' height='20'><rect width='20' height='20' fill='blue'/></svg>" });
                break;
            case "/images/missing":
                await route.FulfillAsync(new() { Status = 404, ContentType = "image/png", Body = "Missing" });
                break;
            default:
                Interlocked.Increment(ref imageRequests);
                await route.FulfillAsync(new() { ContentType = "image/png", BodyBytes = png });
                break;
        }
    });
    await page.GotoAsync("https://smoke.test/images/page");
    await latest.Locator("#images-tab").ClickAsync();
    await latest.WaitForFunctionAsync("() => document.querySelectorAll('.image-card').length === 3 && [...document.querySelectorAll('.image-card img')].every(img => img.complete && img.naturalWidth > 0)", null, new() { Timeout = 10000 });
    if (session.Images.Count != 3 || session.Images.Any(image => image.ByteLength == 0))
        throw new Exception("Image capture missed data or included the failed response.");
    var capturedPng = session.Images.Single(image => image.Url.EndsWith("/images/photo"));
    if (capturedPng.ToDataUrl() != "data:image/png;base64," + Convert.ToBase64String(png))
        throw new Exception("Captured bytes differ from the original response.");
    if (imageRequests != 3) throw new Exception("Previews fetched images from the network again.");
    // Other websites must not be able to read images from the session bindings.
    if (!await page.EvaluateAsync<bool>("async () => { try { await window.pocGetImages(); return false; } catch { return true; } }"))
        throw new Exception("Session image data was exposed to a visited website.");
    await latest.CloseAsync();
    await page.Locator("#poc-scrap-panel .toggle").ClickAsync();
    await page.Locator("#poc-scrap-panel .action").ClickAsync();
    await page.WaitForFunctionAsync("() => document.querySelector('#poc-scrap-panel').shadowRoot.querySelector('[role=status]').textContent === 'Session opened.'", null, new() { Timeout = 5000 });
    var imageSession = (IPage)typeof(MyPlaywrightBrowser).GetField("_sessionPage", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(browser)!;
    await imageSession.Locator("#images-tab").ClickAsync();
    await imageSession.WaitForFunctionAsync("() => document.querySelectorAll('.image-card').length === 3 && [...document.querySelectorAll('.image-card img')].every(img => img.complete && img.naturalWidth > 0)", null, new() { Timeout = 10000 });
    if (imageRequests != 3) throw new Exception("Reopening session refetched images.");
    Console.WriteLine("PASS: PNG, CSS SVG and iframe images captured; previews decode original bytes without network requests; reopen retains images.");
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

