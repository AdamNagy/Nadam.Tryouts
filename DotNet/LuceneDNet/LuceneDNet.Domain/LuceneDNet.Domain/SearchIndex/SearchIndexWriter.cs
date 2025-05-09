using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Document = Lucene.Net.Documents.Document;

namespace LuceneDNet.Domain.SearchIndex;

public class SearchIndexWriter
{
    private readonly string _indexPath;
    private readonly IndexWriterConfig _config;

    private IndexWriter? writer;
    private FSDirectory? indexDirectory;

    private bool isOpen = false;

    public SearchIndexWriter(string indexPath, LuceneVersion version)
    {
        _indexPath = indexPath;

        var standardAnalyzer = new StandardAnalyzer(version);
        _config = new IndexWriterConfig(version, standardAnalyzer);
        _config.OpenMode = OpenMode.CREATE_OR_APPEND;
    }

    public void OpenWrite()
    {
        if (isOpen && writer is not null)
        {
            return;
        }

        indexDirectory = FSDirectory.Open(_indexPath);

        if (writer is null)
        {
            writer = new IndexWriter(indexDirectory, _config);
        }

        isOpen = true;
    }

    public IndexReader GetReader()
    {
        if (!isOpen)
        {
            throw new Exception("Writer needs to ne open to get reader.");
        }

        return writer!.GetReader(true);
    }

    public void Write(Document doc)
    {
        if (!isOpen)
        {
            throw new Exception("Writer is not open, please open it first.");
        }

        if (writer is null)
        {
            throw new ArgumentNullException("Writer is null.");
        }

        writer.AddDocument(doc);
    }

    public void Commit()
    {
        writer?.Commit();
    }
}
