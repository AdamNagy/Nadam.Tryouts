using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataEntity
{
    public class TitleStructuredList
    {
        private readonly char _titleIndicator;
        private readonly Stack<(int depth, string item)> _titleStack;
        private readonly Dictionary<string, List<string>> _contentDict;

        public TitleStructuredList()
        {
            _titleIndicator = '#';
            _titleStack = new Stack<(int depth, string item)>();
            _contentDict = new Dictionary<string, List<string>>();
        }

        public TitleStructuredList(char titleIndicator)
        {
            _titleIndicator = titleIndicator;
            _titleStack = new Stack<(int depth, string item)>();
            _contentDict = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> ProcessFile(string filePath)
        {
            // get title from the file path
            _titleStack.Push((0, filePath.Split('\\').Last().Split('.').First()));

            foreach (var line in File.ReadLines(filePath))
            {
                if( string.IsNullOrEmpty(line) )
                    continue;
                
                var depth = GetTitleDepth(line);

                // if line is a content line (not a title)
                if (depth == -1)
                {
                    var currentKey = GetCurrentTitle();
                    if ( !_contentDict.ContainsKey(currentKey) )
                        _contentDict.Add(currentKey, new List<string>());

                    _contentDict[currentKey].Add(line);
                }
                // if current line is title, and bigger than the previous
                else if(depth < _titleStack.Peek().depth)
                {
                    var popedDepth = _titleStack.Pop().depth;
                    while (popedDepth != depth)
                    {
                        popedDepth = _titleStack.Pop().depth;
                    }
                    _titleStack.Push((depth, line));
                }
                // if current line is title, and smaller (sub title) than the previous
                else if (depth > _titleStack.Peek().depth)
                {
                    _titleStack.Push((depth, line));
                }
                // // if current line is title, and is in the same level with the previous
                else if ( depth == _titleStack.Peek().depth)
                {
                    var popedDepth = _titleStack.Pop().depth;
                    while (_titleStack.Peek().depth == depth)
                    {
                        _titleStack.Pop();
                    }
                    _titleStack.Push((depth, line));
                }
            }

            return _contentDict;
        }
        
        public int GetTitleDepth(string titleLine)
        {
            if (!titleLine.StartsWith($"{_titleIndicator}"))
            {
                return -1;
            }

            int depth = 0;
            foreach (var character in titleLine)
            {
                if (character == _titleIndicator)
                    depth += 1;
            }

            return depth;
        }

        private string GetCurrentTitle()
        {
            var title = "";
            foreach (var titleSegment in _titleStack)
            {
                title = $"{titleSegment.item.Trim(_titleIndicator).Replace(' ', '_').ToLower()}-{title}";
            }

            return title.TrimEnd('-');
        }
    }
}
