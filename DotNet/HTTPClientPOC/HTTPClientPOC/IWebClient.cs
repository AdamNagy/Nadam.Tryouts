namespace HTTPClientPOC;

internal interface IWebClient
{
    Task<IEnumerable<string>> Requeset(string url);
}
