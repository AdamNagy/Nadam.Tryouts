using Nadam.Playwright.POC;

var browser = new MyPlaywrightBrowser();
try
{
    await browser.Init();
    Console.WriteLine("Browser launched. Press any key to exit.");
    Console.ReadKey();
}
finally
{
    await browser.Stop();
}
