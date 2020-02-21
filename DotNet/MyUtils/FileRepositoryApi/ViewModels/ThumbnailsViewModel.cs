using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace ManifestRepositoryApi.ViewModels
{
    public class JObjectGallery
    {
        // type of the manifest: gallery or local-gallery
        public string Type { get; set; }

        // the parsed json object
        public JObject JsonGallery { get; set; }
    }

    public class ThumbnailsViewModel
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<GalleryViewModel> thumbnails;
    }

    public class GalleryViewModel
    {
        public string Title { get; set; }
        public IEnumerable<ImageMeta> Images { get; set; }

        public GalleryViewModel()
        {
            
        }

        public GalleryViewModel(JObjectGallery jobject)
        {
            switch (jobject.Type)
            {
                case "gallery":
                    Images =
                        ((JArray) jobject.JsonGallery["ImagesMetaData"])
                            .Select(p => new ImageMeta()
                            {
                                ThumbnailSrc = p["thumbnail"].ToString(),
                                ImageSrc = p["realImageSrc"].ToString()
                            });
                    break;
                case "local-gallery":
                    Images =
                        ((JArray)jobject.JsonGallery["Images"])
                            .Select(p => new ImageMeta()
                            {
                                ThumbnailSrc = $"<azure-blog-storage-root>/thumbnails/{p.ToString()}",
                                ImageSrc = $"<azure-blog-storage-root>/{p.ToString()}"
                            });
                    break;
            }
        }
    }

    public class ImageMeta
    {
        public string ThumbnailSrc { get; set; }
        public string ImageSrc { get; set; }
    }
}