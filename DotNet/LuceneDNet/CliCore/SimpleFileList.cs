namespace CliCore;

public class SimpleFileList
{
    private readonly string _path;
    private IEnumerable<string>? _content;

    public IEnumerable<string> Content
    {
        get
        {
            if (_content != null)
            {
                return _content;
            }

            _content = File.ReadAllLines(_path);
            return _content;
        }
    }

    public SimpleFileList(string path)
    {
        _path = path;
    }
}

