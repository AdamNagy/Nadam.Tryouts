using System.Net.Http.Json;

using HTTPClientPOC;

namespace WebClientPOC
{
    public class PlaywrightWebClient : IWebClient
    {
        private readonly HttpClient _client;

        public PlaywrightWebClient()
        {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<string>> Requeset(string url)

        {
            var content = JsonContent.Create(new
            {
                url
            });
            var response = await _client.PostAsync("http://localhost:3000", content);

            if(!response.IsSuccessStatusCode) 
            {
                throw new Exception("Playwright Webclient Api responded error");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>() ?? Enumerable.Empty<string>();
        }
    }
}
