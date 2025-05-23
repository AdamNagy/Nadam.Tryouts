using LuceneDNet.Domain.SearchIndex;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PdfPig.Domain;

public class BookIndexer
{
    private readonly string _root;
    private readonly SearchIndexWriter _indexWriter;

    public BookIndexer(string root, SearchIndexWriter indexWriter)
    {
        _root = root;
        _indexWriter = indexWriter;
    }

    private void IndexBooks()
    {
        foreach (var item in Directory.GetFiles(_root).Where(p => p.EndsWith("pdf")))
        {
            using (PdfDocument document = PdfDocument.Open(item))
            {
                foreach (Page page in document.GetPages())
                {
                    string pageText = page.Text;

                    foreach (Word word in page.GetWords().Take(100))
                    {
                        Console.WriteLine(word.Text);
                    }
                }
            }
        }
    }
}

