using Nadam.Playwright.POC;

Console.WriteLine("Hello, World!");

// --- 1. SETUP TITANIUM PROXY ---
var proxy = new MyProxy();
proxy.Init();
Console.WriteLine("Titanium Proxy started on port 8001...");

// --- 3. Host Angular ---
var webServer = new MyWebServer();
await webServer.Init();
Console.WriteLine("FE app hosted.");

// --- 2. LAUNCH PLAYWRIGHT ---
var browser = new MyPlaywrightBrowser();
await browser.Init(webServer.Url);

Console.WriteLine("Browser launched. Press any key to exit.");

// Cleanup
Console.ReadKey();
await browser.Stop();
proxy.Stop();