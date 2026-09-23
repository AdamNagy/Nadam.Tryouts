namespace Nadam.Playwright.POC;

internal sealed class CapturedImage(int id, string url, string contentType, byte[] content)
{
    public int Id { get; } = id;
    public string Url { get; } = url;
    public string ContentType { get; } = contentType;
    public int ByteLength => content.Length;

    public string ToDataUrl() => $"data:{ContentType};base64,{Convert.ToBase64String(content)}";
}
