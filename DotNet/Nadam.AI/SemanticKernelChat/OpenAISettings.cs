namespace SemanticKernelChat;

/// <summary>
/// Strongly-typed binding for the "OpenAI" section in appsettings.json.
/// </summary>
public sealed class OpenAISettings
{
    /// <summary>Your OpenAI API key.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The model to use, e.g. "gpt-4o", "gpt-4o-mini", "gpt-3.5-turbo".
    /// </summary>
    public string ModelId { get; set; } = string.Empty;

    /// <summary>Throws if required settings are missing.</summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
            throw new InvalidOperationException(
                "OpenAI:ApiKey is not set. " +
                "Add it to appsettings.local.json or set the OPENAI__APIKEY environment variable.");

        if (string.IsNullOrWhiteSpace(ModelId))
            throw new InvalidOperationException(
                "OpenAI:ModelId is not set in appsettings.json.");
    }
}
