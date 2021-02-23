using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Configuration;
using SchTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchTest.Infrastructure
{
    public class CognitiveSearchHandler : ICognitiveSearchHandler
    {
        private readonly IConfiguration Configuration;

        public CognitiveSearchHandler(IConfiguration config)
        {
            Configuration = config;
        }

        public IndexDocumentsResult UpdateIndex(GitApiModel updatedModel)
        {
            Uri serviceEndpoint = new Uri(Configuration["SearchServiceEndPoint"]);
            AzureKeyCredential credential = new AzureKeyCredential(Configuration["SearchServiceAdminApiKey"]);

            SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

            // DeleteIndexIfExists(Configuration["IndexName"], adminClient);
            // CreateIndex(Configuration["IndexName"], adminClient);

            SearchClient ingesterClient = adminClient.GetSearchClient(Configuration["IndexName"]);

            IndexDocumentsBatch<GitApiModel> batch = IndexDocumentsBatch.Create(
            IndexDocumentsAction.Upload(updatedModel));

            try
            {
                IndexDocumentsResult result = ingesterClient.IndexDocuments(batch);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to index some of the documents: {0}");
                throw new Exception($"CognitiveSearchHandler.UpdateIndex\n", ex);
            }
        }

        public IEnumerable<GitApiModel> Search(string searchTerm)
        {
            SearchClient srchclient = new SearchClient(new Uri(Configuration["SearchServiceEndPoint"]), Configuration["IndexName"], new AzureKeyCredential(Configuration["SearchServiceAdminApiKey"]));

            SearchOptions options = new SearchOptions()
            {
                IncludeTotalCount = true,
                Filter = "",
                OrderBy = { "" }
            };

            // adding all field to search result
            foreach(var field in typeof(GitApiModel).GetFields())
                options.Select.Add(field.Name);

            var response = srchclient.Search<GitApiModel>(searchTerm, options);

            return response.Value.GetResults().Select(p => p.Document);
        }

        private void DeleteIndexIfExists(string indexName, SearchIndexClient adminClient)
        {
            try
            {
                var names = adminClient.GetIndexNames(); //.ToList();

                if( names.FirstOrDefault(p => p == indexName) != null )
                {
                    adminClient.DeleteIndex(indexName);
                }                
            }
            catch(Exception ex)
            {
                
            }
        }
        
        private void CreateIndex(string indexName, SearchIndexClient adminClient)
        {
            try
            {
                FieldBuilder fieldBuilder = new FieldBuilder();
                var searchFields = fieldBuilder.Build(typeof(GitApiModel));

                var definition = new SearchIndex(indexName, searchFields);

                var suggester = new SearchSuggester("sg", new[] { "current user url", "code search url" });
                definition.Suggesters.Add(suggester);

                adminClient.CreateOrUpdateIndex(definition);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
