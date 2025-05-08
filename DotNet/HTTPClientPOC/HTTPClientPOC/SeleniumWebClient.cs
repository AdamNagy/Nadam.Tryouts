using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace HTTPClientPOC;

public class SeleniumWebClient : IWebClient
{
    public async Task<IEnumerable<string>> Requeset(string url)
    {
        var options = new FirefoxOptions() { };
        options.SetPreference("javascript.enabled", false);
        options.AddArgument("-headless");

        IWebDriver driver = new FirefoxDriver(options);        

        await driver.Navigate().GoToUrlAsync(url);

        var imageElements = driver.FindElements(By.TagName("img"));

        return imageElements.Select(p => p.GetAttribute("src"));
    }
}
