using Azure.Search.Documents.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test2.Data
{
    public interface ICognitiveSearchService
    {
        public Task<IndexDocumentsResult> UpdateIndex();
        public IEnumerable<GitApiModel> Search(string searchTerm);
    }
}
