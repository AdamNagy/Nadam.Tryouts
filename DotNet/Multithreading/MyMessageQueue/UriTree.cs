namespace MyMessageQueue
{
    public class UriTree
    {
        public string Domain => _root.Segment;
        private readonly UriTreeNode _root;
        private readonly Uri _uriObj;
        public int Count => GetUris().Count();

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

            yield return basePath;

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
        {
            var segments = uri.Segments.Select(p => p.Trim('/')).Where(p => p != "/" && !string.IsNullOrEmpty(p)).ToList();

            if (!string.IsNullOrEmpty(uri.Query))
                segments[segments.Count - 1] = $"{segments[segments.Count - 1]}{uri.Query}";

            return segments;
        }
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
            if(!Children.Any(x => x.Segment == segment))
            {
                Children.Add(new UriTreeNode(segment));
                return true;
            }

            return false;
        }

        public bool Add(IList<string> segments)
        {
            if(segments == null || !segments.Any())
                return false;

            var segment = segments.First();
            var segmentNode = GetChild(segment);

            if (segmentNode == null)
            {
                segmentNode = new UriTreeNode(segment);
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
            if(segments == null || !segments.Any() )
                return true;

            var segment = segments.First();

            var child = GetChild(segment);
            if (child == null)
                return false;

            if (!segments.Skip(1).Any())
                return true;

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

        public override string ToString()
            => Segment;

        private UriTreeNode GetChild(string segment)
            => Children.FirstOrDefault(p => p.Segment == segment);
    }
}
