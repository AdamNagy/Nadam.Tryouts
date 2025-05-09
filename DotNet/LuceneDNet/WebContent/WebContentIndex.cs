namespace LuceneDNet.WebContent;

public class WebContentIndex
{
    private readonly string _root;
    private readonly Dictionary<string, string> _hashTable = new Dictionary<string, string>();

    public WebContentIndex(string root)
    {
        _root = root;
        Init();
    }

    public bool Contains(Uri uri)
        => _hashTable.ContainsKey(uri.OriginalString);

    public bool Get(Uri uri, out string content)
    {
        if (!_hashTable.ContainsKey(uri.OriginalString))
        {
            content = string.Empty;
            return false;
        }

        var cache = _hashTable[uri.OriginalString] as string;
        content = File.ReadAllText(Path.Combine(_root, cache!));
        return true;
    }

    public void Set(Uri uri, string content, bool force = false)
    {
        if (_hashTable.ContainsKey(uri.OriginalString) && !force)
        {
            throw new Exception($"{uri.OriginalString} is already present.");
        }

        var hash = uri.OriginalString;
        var guid = Guid.NewGuid().ToString();

        _hashTable.Add(hash, guid);

        File.WriteAllText(Path.Combine(_root, guid), content);

        Append(hash.ToString(), guid);
    }

    private void Init()
    {
        if (!File.Exists(Path.Combine(_root, "index.txt")))
        {
            return;
        }

        foreach (var item in File.ReadAllLines(Path.Combine(_root, "index.txt")))
        {
            var splitted = item.Split(',');
            _hashTable.Add(splitted[0], splitted[1]);
        }
    }

    private void Append(string hash, string guid)
    {
        using (StreamWriter sw = File.AppendText(Path.Combine(_root, "index.txt")))
        {
            sw.WriteLine($"{hash},{guid}");
        }
    }
}
