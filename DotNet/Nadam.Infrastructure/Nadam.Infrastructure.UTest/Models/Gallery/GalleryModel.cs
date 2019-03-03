using System.Collections.Generic;

namespace Nadam.Infrastructure.UTest.Gallery
{
    public class GalleryModel : Entity
    {
        public IEnumerable<int> Urls { get; set; }

        public GalleryModel()
        {
            Init(this);
        }

        public void UpdateUrls(IEnumerable<int> newUrls)
        {
            Update("Urls", newUrls);
        }
    }
}
