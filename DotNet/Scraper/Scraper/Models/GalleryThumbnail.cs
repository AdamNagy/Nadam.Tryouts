using System.Collections.Generic;

namespace Scraper.Models
{
    public class GalleryThumbnail
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public IEnumerable<string> ImageLinks { get; set; }
    }
}