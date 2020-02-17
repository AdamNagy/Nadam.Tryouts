using System;
using System.IO;

namespace FileRepositoryApi.Models
{
    public abstract class ReadonlyManifest
    {
        public string type;

        private string _filePath;

        public ReadonlyManifest(string filePath)
        {
            _filePath = filePath;
        }

        public virtual string ReadFile()
            => File.ReadAllText(_filePath);

        public abstract string ReadThumbnail();
    }

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public class WebGalleryManifest : ReadonlyManifest
    {
        public WebGalleryManifest(string file) : base(file)
        {
            type = "web-gallery";
        }

        public override string ReadThumbnail()
        {
            throw new NotImplementedException();
        }
    }


/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


    public class LocalGalleryManifest : ReadonlyManifest
    {
        public LocalGalleryManifest(string file) : base(file)
        {
            type = "local-gallery";
        }

        public override string ReadThumbnail()
        {
            throw new NotImplementedException();
        }
    }
}