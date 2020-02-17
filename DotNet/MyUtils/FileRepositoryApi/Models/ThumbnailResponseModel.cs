using System.Collections.Generic;

namespace ManifestRepositoryApi.Models
{
    public class ThumbnailsResponseModel
    {
        public int pages;
        public int currentPage;
        public IEnumerable<GalleryResponseModel> thumbnails;
    }

    // can hold thumbail only or the whole gallery as well
    public class GalleryResponseModel
    {
        public string type;
        // could be if type 'Gallery' but innecesarry to have type at this point
        public string content;
    }
}