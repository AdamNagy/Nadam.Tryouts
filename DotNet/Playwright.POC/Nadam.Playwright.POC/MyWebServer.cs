using Microsoft.AspNetCore.Mvc;

namespace Nadam.Playwright.POC;

internal class MyWebServer
{
    private string _url;
    public string Url { get => _url; }

    public async Task Init()
    {
        var location = AppContext.BaseDirectory;    // The location of the exe file.
        _url = "https://127.0.0.1:5055";

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {            
            ContentRootPath = location,
            WebRootPath = Path.Combine(location, "wwwroot") // Location of the FE app can be configured here.
        });
        builder.WebHost.UseUrls(_url);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenLocalhost(5055, listenOptions =>
            {
                listenOptions.UseHttps();
            });
        });

        var app = builder.Build();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Here we need to register the API endpoint the FE app will call.
        app.MapPost("/api/Config/validate", ([FromBody] DemoRequest req) => { 
            Console.WriteLine($"Received validation request for path: {req.Path} with type: {req.Type}");
            return new
            {
                IsSucceess = true,
                Name = "validation"
            };
        });

        app.MapGet("api/helo", () => "Hello word");

        // Need for the SPA app to work, otherwise it will return 404 for the route that is not registered in the backend, but it is a valid route in the FE app.
        app.MapFallbackToFile("index.html");

        await app.StartAsync();
    }
}
