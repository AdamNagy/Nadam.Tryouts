using System.Collections.Generic;

namespace ManifestRepositoryApi.Models
{
    public class ThumbnailsResponseModel
    {
        public bool success;
        public string message;

        public int pages;
        public int currentPage;
        public IEnumerable<GalleryModel> thumbnails;
    }

    public class GalleryResponseModel
    {
        public bool success;
        public string message;

        public GalleryModel gallery;
    }

    // can hold thumbail only or the whole gallery as well
    public class GalleryModel
    {
        public string type;
        public string fileName;
        // could be if type 'Gallery' but innecesarry to have type at this point
        public string content;
    }
}