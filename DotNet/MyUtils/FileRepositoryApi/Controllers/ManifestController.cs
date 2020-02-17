using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FileRepositoryApi.Models;

namespace FileRepositoryApi.Controllers
{
    public class ManifestController : ApiController
    {
        // GET api/manifest/test1
        [HttpGet]
        [Route("api/manifest/{title}")]
        public GalleryResponseModel GetByTitle(string title)
        {
            var manifest = ManifestRepository.GetFileByTitle(title);
            return new GalleryResponseModel()
            {
                type = manifest.type,
                content = manifest.ReadFile()
            };
        }

        /*
         * api/thumbnails
         * api/thumbnails?page=2
         * api/thumbnails?pagesize=30
         * api/thumbnails?page=5&pagesize=6
         * api/thumbnails/jenni
         * api/thumbnails/jenni?page=4
         * api/thumbnails/jenni?page=5&pagesize=6
         */
        [HttpGet]
        [Route("api/thumbnails/{title}?page={page}&pagesize={pageSize}")]
        public ThumbnailsResponseModel GetThumbnailsByTitleFragment(string title = "", int page = 1, int pageSize = 10)
        {
            var manifests = ManifestRepository.GetFilesByFileTitleSegment(title);
            var s = manifests.Select(p => new GalleryResponseModel()
            {
                type = p.type,
                content = p.ReadThumbnail(),
            });

            return new ThumbnailsResponseModel()
            {
                currentPage = 123,
                pages = 4325,
                thumbnails = s
            };
        }

        // POST api/manifest
        public void Post([FromBody]ManifestRequestModel value)
        {

        }
    }
}
