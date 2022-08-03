namespace MyMessageQueue
{
    public class UriTree
    {
        public string Domain => _root.Segment;
        private readonly UriTreeNode _root;
        private readonly Uri _uriObj;
        public int Count { get; private set; }

        public UriTree(string uri)
        {
            _uriObj = new Uri(uri);
            _root = new UriTreeNode(_uriObj.Host);

            _root.Add(GetSegments(_uriObj).ToList());
        }

        public bool AddUri(string uri)
        {
            var uriObj = new Uri(uri);

            if (uriObj.Host != _uriObj.Host)
                return false;

            var success = _root.Add(GetSegments(uriObj).ToList());
            
            if(success)
                ++Count;

            return success;
        }

        public bool Contains(string uri)
        {
            var uriObj = new Uri(uri);

            if (uriObj.Host != _uriObj.Host)
                return false;

            return _root.Contains(GetSegments(uriObj).ToList());
        }

        public IEnumerable<string> GetUris()
        {
            var basePath = $"{_uriObj.Scheme}://{_uriObj.Host}";

            foreach (var uri in _root.Enumerate())
            {
                yield return $"{basePath}/{uri}";
            }
        }

        public IEnumerable<string> Enumerate()
        { 
            return Enumerable.Empty<string>();
        }

        private IEnumerable<string> GetSegments(Uri uri)
            => uri.Segments.Select(p => p.Trim('/')).Where(p => p != "/" && !string.IsNullOrEmpty(p));
    }

    public class UriTreeNode
    {
        public string Segment { get; private set; }
        public IList<UriTreeNode> Children { get; private set; }

        public UriTreeNode(string segment)
        {
            Segment = segment;
            Children = new List<UriTreeNode>();
        }

        public bool Add(string segment)
        {
            var contains = Children.FirstOrDefault(x => x.Segment == segment);

            if(!Children.Any(x => x.Segment == segment))
            {
                Children.Add(new UriTreeNode(segment));
                return true;
            }

            return false;
        }

        public bool Add(IList<string> segments)
        {
            if( !segments.Any() )
                return false;

            var segmentText = segments.First();
            var segmentNode = GetChild(segmentText);

            if (segmentNode == null)
            {
                segmentNode = new UriTreeNode(segmentText);
                Children.Add(segmentNode);
            }

            if (!segments.Skip(1).Any())
                return true;

            return segmentNode.Add(segments.Skip(1).ToList());
        }

        public bool Contains(string segment)
            => Children.Any(p => p.Segment == segment);

        public bool Contains(IEnumerable<string> segments)
        {
            if(!segments.Any() )
                return true;

            var first = segments.First();

            var child = GetChild(first);
            if (child == null)
                return false;

            return child.Contains(segments.Skip(1));
        }

        public IEnumerable<string> Enumerate()
        {
            foreach (var child in Children)
            {
                yield return child.Segment;

                foreach (var granChild in child.Enumerate())
                {
                    yield return $"{child.Segment}/{granChild}";
                }
            }
        }

        private UriTreeNode GetChild(string segment)
            => Children.FirstOrDefault(p => p.Segment == segment);
    }
}
