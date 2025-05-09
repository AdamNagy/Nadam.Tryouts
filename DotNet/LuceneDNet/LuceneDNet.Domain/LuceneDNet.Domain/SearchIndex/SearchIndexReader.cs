using Lucene.Net.Index;
using Lucene.Net.Search;
using Document = Lucene.Net.Documents.Document;

namespace LuceneDNet.Domain.SearchIndex;

public class SearchIndexReader
{
    private readonly IndexReader _reader;
    private readonly IndexSearcher _searcher;

    public SearchIndexReader(IndexReader reader)
    {
        _reader = reader;
        _searcher = new IndexSearcher(reader);
    }

    public IEnumerable<Document> Search(string searchTerm)
    {
        var term = new Term("alt", searchTerm);
        Query query = new TermQuery(term);
        TopDocs topDocs = _searcher.Search(query, n: 100);

        int numMatchingDocs = topDocs.TotalHits;

        for (int i = 0; i < Math.Min(numMatchingDocs, 100); i++)
        {
            yield return _searcher.Doc(topDocs.ScoreDocs[i].Doc);
        }

        if (numMatchingDocs > 100)
        {
            Console.WriteLine("There are more then 100.");
        }
    }
}
