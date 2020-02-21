using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManifestRepositoryApi.ManifestFramework
{
    public class WebGalleryManifest : ReadonlyManifest
    {
        public WebGalleryManifest(string file) : base(file)
        {
            type = "gallery";
        }

        public override string ReadThumbnail()
        {
            bool loadMore = true;
            var fileContent = "";
            var numOfImages = 8;

            foreach (var item in ReadSegment(loadMore).Take(int.MaxValue))
            {
                fileContent += item;
                loadMore = fileContent.Count(ch => ch == '}') < numOfImages;
                if (!loadMore)
                    break;
            }

            var lastCommaIdx = fileContent.LastIndexOf('}');
            var diff = fileContent.Length - lastCommaIdx;
            var jsonString = fileContent.Substring(0, lastCommaIdx);

            return $"{jsonString}}}]}}";
        }
    }
}