using Microsoft.Playwright;

namespace Nadam.Playwright.POC;

internal sealed class SessionPageLoader
{
    private readonly string _html;
    private readonly string _css;
    private readonly string _script;

    private SessionPageLoader(string html, string css, string script)
    {
        _html = html;
        _css = css;
        _script = script;
    }

    public static async Task<SessionPageLoader> LoadAsync()
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "SessionPage");
        var html = await File.ReadAllTextAsync(Path.Combine(directory, "SessionPage.html"));
        var css = await File.ReadAllTextAsync(Path.Combine(directory, "SessionPage.css"));
        var bridge = await File.ReadAllTextAsync(Path.Combine(directory, "SessionBridge.js"));
        var page = await File.ReadAllTextAsync(Path.Combine(directory, "SessionPage.js"));
        var script = $$"""
            (() => {
                {{bridge}}
                {{page}}
                createSessionPage(createSessionBridge()).start();
            })();
            """;
        return new SessionPageLoader(html, css, script);
    }

    public async Task RenderAsync(IPage page)
    {
        // This view is only rendered in its dedicated about:blank tab.
        await page.SetContentAsync(_html);
        await page.EvaluateAsync("""
            css => {
                const style = document.createElement('style');
                style.textContent = css;
                document.head.appendChild(style);
            }
            """, _css);
        await page.EvaluateAsync(_script);
    }
}
