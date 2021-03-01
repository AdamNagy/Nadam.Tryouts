using Azure.Search.Documents.Models;
using SchTest.Models;
using System.Collections.Generic;

namespace SchTest.Infrastructure
{
    public interface ICognitiveSearchHandler
    {
        public IndexDocumentsResult UpdateIndex(GitApiModel updatedModel);
        public IEnumerable<GitApiModel> Search(string searchTerm);
    }
}
