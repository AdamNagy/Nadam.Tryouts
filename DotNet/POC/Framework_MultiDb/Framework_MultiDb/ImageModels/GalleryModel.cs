using System.Collections.Generic;

namespace MultiDb.ImageModels
{
    public abstract class GalleryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class StoredGallery : GalleryModel
    {
        public string StoragePath { get; set; }
        public ICollection<StoredImage> Images { get; set; }
    }

    public class WebGallery : GalleryModel
    {
        public string SourceUrl { get; set; }
        public ICollection<WebImage> Images { get; set; }
    }

    public class Album : GalleryModel
    {
        public string StoragePath { get; set; }
        public string SourceUrl { get; set; }

        public ICollection<HibridImage> Images { get; set; }
    }
}
