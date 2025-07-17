using System.Collections;

namespace LuceneDNet.Domain.WebContent;

public class WebContentIndex : IEnumerable<(string url, string content)>
{
    private readonly string _root;
    private readonly Dictionary<string, string> _contentDictionary = new Dictionary<string, string>();

    private readonly string _indexFileName;

    public WebContentIndex(string root)
    {
        _root = root;
        _indexFileName = "index.txt";

        Init();
    }

    public WebContentIndex(string root, string indexFileName)
    {
        _root = root;
        _indexFileName = indexFileName;

        Init();
    }

    public bool Contains(Uri uri)
        => _contentDictionary.ContainsKey(uri.OriginalString);

    public bool Get(Uri uri, out string content)
    {
        if (!_contentDictionary.ContainsKey(uri.OriginalString))
        {
            content = string.Empty;
            return false;
        }

        var cache = _contentDictionary[uri.OriginalString] as string;
        content = File.ReadAllText(Path.Combine(_root, cache!));
        return true;
    }

    public void Set(Uri uri, string content, bool force = false)
    {
        if (_contentDictionary.ContainsKey(uri.OriginalString) && !force)
        {
            throw new Exception($"{uri.OriginalString} is already present.");
        }

        var guid = Guid.NewGuid().ToString();

        _contentDictionary.Add(uri.OriginalString, guid);

        File.WriteAllText(Path.Combine(_root, guid), content);

        Append(uri.OriginalString, guid);
    }

    private void Init()
    {
        if (!File.Exists(Path.Combine(_root, _indexFileName)))
        {
            return;
        }

        foreach (var item in File.ReadAllLines(Path.Combine(_root, _indexFileName)))
        {
            var splitted = item.Split(',');
            _contentDictionary.Add(splitted[0], splitted[1]);
        }
    }

    private void Append(string hash, string guid)
    {
        using (StreamWriter sw = File.AppendText(Path.Combine(_root, _indexFileName)))
        {
            sw.WriteLine($"{hash},{guid}");
        }
    }

    public IEnumerator<(string url, string content)> GetEnumerator()
    {
        return _contentDictionary.Select(p => (p.Key, File.ReadAllText(Path.Combine(_root, p.Value)))).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _contentDictionary.Select(p => (p.Key, p.Value)).GetEnumerator();
    }
}
