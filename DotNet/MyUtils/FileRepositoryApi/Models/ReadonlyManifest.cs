using System;
using System.IO;

namespace FileRepositoryApi.Models
{
    public abstract class ReadonlyManifest
    {
        private string _filePath;
        public ReadonlyManifest(string filePath)
        {
            _filePath = filePath;
        }

        public string ReadFile()
            => File.ReadAllText(_filePath);

        public abstract string ReadThumbnail();
    }

    public class WebGalleryManifest : ReadonlyManifest
    {
        public WebGalleryManifest(string file) : base(file) { }

        public override string ReadThumbnail()
        {
            throw new NotImplementedException();
        }
    }

    public class GalleryManifest : ReadonlyManifest
    {
        public GalleryManifest(string file) : base(file) { }

        public override string ReadThumbnail()
        {
            throw new NotImplementedException();
        }
    }
}