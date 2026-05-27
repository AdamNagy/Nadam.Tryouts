#pragma warning disable SKEXP0070
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelChat;

// ── Configuration ────────────────────────────────────────────────────────────
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddJsonFile("appsettings.local.json", optional: true,  reloadOnChange: false)
    //.AddEnvironmentVariables()
    .Build();

var settings = config.GetSection("Gemini")
    ?? throw new InvalidOperationException("Missing 'Gemini' section in appsettings.json.");

// settings.Validate();

// ── Kernel setup ─────────────────────────────────────────────────────────────
var kernel = Kernel.CreateBuilder()
    // .AddOpenAIChatCompletion(settings.GetSection("ModelId").Value, settings.GetSection("ApiKey").Value)
    .AddGoogleAIGeminiChatCompletion(settings.GetSection("ModelId").Value, settings.GetSection("ApiKey").Value)
    .Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();
var history     = new ChatHistory();

history.AddSystemMessage(
    "You are a helpful assistant. Be concise and friendly.");

// ── REPL ─────────────────────────────────────────────────────────────────────
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   Semantic Kernel Chat  (type 'exit')    ║");
Console.WriteLine($"║   Model: {settings.Value,-33}║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine();

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("You: ");
    Console.ResetColor();

    var userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput))
        continue;

    if (userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Goodbye!");
        break;
    }

    history.AddUserMessage(userInput);

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Assistant: ");
    Console.ResetColor();

    try
    {
        // Stream the response token-by-token for a better UX
        var fullReply = new System.Text.StringBuilder();

        await foreach (var chunk in chatService.GetStreamingChatMessageContentsAsync(history))
        {
            var text = chunk.Content ?? string.Empty;
            Console.Write(text);
            fullReply.Append(text);
        }

        Console.WriteLine();
        Console.WriteLine();

        // Add the complete assistant reply to history for multi-turn context
        history.AddAssistantMessage(fullReply.ToString());
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Error] {ex.Message}");
        Console.ResetColor();
    }
}
