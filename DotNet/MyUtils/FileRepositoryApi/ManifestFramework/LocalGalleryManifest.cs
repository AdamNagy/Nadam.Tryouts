using System;

namespace ManifestRepositoryApi.ManifestFramework
{
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