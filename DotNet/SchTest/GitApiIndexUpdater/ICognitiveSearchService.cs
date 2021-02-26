using Azure.Search.Documents.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitApiIndexUpdater
{
    public interface ICognitiveSearchService
    {
        public Task<IndexDocumentsResult> UpdateIndex();
    }
}
