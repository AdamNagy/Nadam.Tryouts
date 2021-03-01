using System.Text;
using Azure.Search.Documents.Models;

namespace GitApiIndexUpdater
{
    public static class IndexDocumentsResultExtensions
    {
        public static string ToString(this IndexDocumentsResult domain)
        {
            var stringBuilder = new StringBuilder();

            foreach (var result in domain.Results)
            {
                stringBuilder.Append($"Key: {result.Key}, Status: {result.Status}, Has Succeeded: {result.Succeeded},\nError Message: {result.ErrorMessage}");
            }

            return stringBuilder.ToString();
        }
    }
}
