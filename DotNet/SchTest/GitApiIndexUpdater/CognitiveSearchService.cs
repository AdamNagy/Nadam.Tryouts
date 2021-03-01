using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitApiIndexUpdater
{
    public class CognitiveSearchService : ICognitiveSearchService
    {
        private readonly IConfiguration Configuration;
        private readonly HttpClient HttpClient;

        public CognitiveSearchService(IConfiguration config, HttpClient httpClient)
        {
            Configuration = config;
            HttpClient = httpClient;
        }

        public async Task<IndexDocumentsResult> UpdateIndex()
        {
            try
            {
                var updatedModel = await GetLatest();

                Uri serviceEndpoint = new Uri(Configuration["SearchServiceEndPoint"]);
                AzureKeyCredential credential = new AzureKeyCredential(Configuration["SearchServiceAdminApiKey"]);

                SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

                DeleteIndexIfExists(Configuration["IndexName"], adminClient);
                CreateIndex(Configuration["IndexName"], adminClient);

                SearchClient ingesterClient = adminClient.GetSearchClient(Configuration["IndexName"]);

                IndexDocumentsBatch<GitApiModel> batch = IndexDocumentsBatch.Create(
                    IndexDocumentsAction.Upload(updatedModel));

                IndexDocumentsResult result = ingesterClient.IndexDocuments(batch);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to index some of the documents: {0}");
                throw new Exception($"CognitiveSearchHandler.UpdateIndex\n", ex);
            }            
        }

        private void DeleteIndexIfExists(string indexName, SearchIndexClient adminClient)
        {
            try
            {
                if (adminClient.GetIndex(indexName) != null)
                {
                    adminClient.DeleteIndex(indexName);
                }
            }
            catch
            {
                return;
            }
        }
        
        private void CreateIndex(string indexName, SearchIndexClient adminClient)
        {
            FieldBuilder fieldBuilder = new FieldBuilder();
            var searchFields = fieldBuilder.Build(typeof(GitApiModel));

            var definition = new SearchIndex(indexName, searchFields);

            adminClient.CreateOrUpdateIndex(definition);
        }
    
        private async Task<GitApiModel> GetLatest()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://api.github.com"),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // User-Agent: PostmanRuntime/7.26.10
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("PostmanRuntime")));

            var response = await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(responseBody);

                var model = new GitApiModel();
                foreach (var item in typeof(GitApiModel).GetFields())
                {
                    FieldInfo myFieldInfo = typeof(GitApiModel).GetField(item.Name, BindingFlags.Public | BindingFlags.Instance);
                    myFieldInfo.SetValue(model, responseObject[item.Name].ToString());
                }

                var basModel = JsonSerializer.Deserialize<GitApiModel>(responseBody);
                basModel.key = Guid.NewGuid().ToString();
                return basModel;
                // return model;
            }

            throw new Exception($"CognitiveSearchService.GetLatest\n{response.StatusCode}\n");
        }
    }
}
