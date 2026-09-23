using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Playwright;

namespace Nadam.Playwright.POC;

internal sealed class ScrapPanelInjector
{
    private readonly string _script;

    private ScrapPanelInjector(string script) => _script = script;

    public static async Task<ScrapPanelInjector> LoadAsync()
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "ScrapPanel");
        var html = await File.ReadAllTextAsync(Path.Combine(directory, "ScrapPanel.html"));
        var css = await File.ReadAllTextAsync(Path.Combine(directory, "ScrapPanel.css"));
        var bridge = await File.ReadAllTextAsync(Path.Combine(directory, "ScraperBridge.js"));
        var panel = await File.ReadAllTextAsync(Path.Combine(directory, "ScrapPanel.js"));
        // Parse our XML-compatible HTML locally, avoiding TrustedHTML sinks in the target page.
        var markup = ToDomNode(XElement.Parse(html));

        // One isolated script keeps injection independent of module URLs and a web server.
        return new ScrapPanelInjector($$"""
            (() => {
                if (window !== window.top) return;
                {{bridge}}
                {{panel}}
                const key = Symbol.for('nadam.scrapPanel');
                const panel = window[key] ??= createScrapPanel(
                    {{JsonSerializer.Serialize(markup)}},
                    {{JsonSerializer.Serialize(css)}},
                    createScraperBridge());
                if (document.readyState === 'loading') {
                    document.addEventListener('DOMContentLoaded', panel.mount, { once: true });
                } else {
                    panel.mount();
                }
            })();
            """);
    }

    private static object ToDomNode(XNode node) => node switch
    {
        XElement element => new
        {
            tag = element.Name.LocalName,
            attributes = element.Attributes().ToDictionary(a => a.Name.LocalName, a => a.Value),
            children = element.Nodes().Where(n => n is XElement or XText).Select(ToDomNode).ToArray()
        },
        XText text => new { text = text.Value },
        _ => throw new InvalidOperationException("Unsupported panel markup node.")
    };

    public Task RegisterAsync(IBrowserContext context) => context.AddInitScriptAsync(script: _script);

    public Task InjectAsync(IPage page) => page.EvaluateAsync(_script);
}
