using System.Collections.Generic;

namespace Scraper.Models
{
    public class GalleryThumbnail
    {
        public string SourceUrl { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> ThumbnailImageSources { get; set; }
    }
}