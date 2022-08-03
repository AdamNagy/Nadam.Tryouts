using System.Text.RegularExpressions;

namespace RegexFinder
{
    public class RegexDictionary
    {
        private readonly IDictionary<string, Regex> _patterns;

        public RegexDictionary()
        {
            _patterns = new Dictionary<string, Regex>();
        }

        public RegexDictionary(IDictionary<string, string> commands)
        {
            _patterns = commands.ToDictionary(
                keyVal => keyVal.Key,
                keyVal => new Regex(keyVal.Value, RegexOptions.Compiled | RegexOptions.Multiline));
        }

        public string Find(string text)
        {
            foreach (var pattern in _patterns)
            {
                var matches = pattern.Value.Match(text);
                if(matches.Success)
                    return pattern.Key;
            }

            return String.Empty;
        }

        public void Add(string key, string pattern)
        {
            if (_patterns.ContainsKey(key))
                throw new ArgumentException($"Key ({key}) already present in the dictionary");

            _patterns.Add(key, new Regex(pattern, RegexOptions.Compiled |
                RegexOptions.IgnoreCase));
        }
    }
}