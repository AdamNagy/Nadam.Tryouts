using Nadam.Playwright.POC;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

var logDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "Nadam.Playwright.POC", "Logs");
Directory.CreateDirectory(logDirectory);

using var serilog = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        Path.Combine(logDirectory, "app-.log"),
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 14,
        shared: true,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
using var loggerFactory = new SerilogLoggerFactory(serilog, dispose: false);
var logger = loggerFactory.CreateLogger("Application");

var session = new ScrapeSession();
var browser = new MyPlaywrightBrowser(session, loggerFactory.CreateLogger<MyPlaywrightBrowser>());
try
{
    logger.LogInformation("Application starting. Logs: {LogDirectory}", logDirectory);
    await browser.Init();
    logger.LogInformation("Browser launched. Press any key to exit.");
    Console.ReadKey();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Application failed");
    Environment.ExitCode = 1;
}
finally
{
    try
    {
        await browser.Stop();
        logger.LogInformation("Application stopped");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Browser shutdown failed");
        Environment.ExitCode = 1;
    }
}
