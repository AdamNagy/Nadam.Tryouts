using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ManifestRepositoryApi.Models;

namespace ManifestRepositoryApi.Controllers
{
    /*
        /api/manifest/test1

        /api/thumbnails
        /api/thumbnails?page=2
        /api/thumbnails?pagesize=30
        /api/thumbnails?page=5&pagesize=6
        /api/thumbnails/test1
        /api/thumbnails/test1?page=4
        /api/thumbnails/test1?page=5&pagesize=6
     */

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
         * api/thumbnails/jenni
         * api/thumbnails/jenni?page=4
         * api/thumbnails/jenni?page=5&pagesize=6
         */

        // ?page={page:int}&pagesize={pageSize:int} , int page = 1, int pageSize = 10
        [HttpGet]
        [Route("api/thumbnails/{title}")]
        public ThumbnailsResponseModel GetThumbnailsByTitleFragment(string title = "", [FromUri] int page = 1)
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

        /*
         * api/thumbnails
         * api/thumbnails?page=2
         * api/thumbnails?pagesize=30
         * api/thumbnails?page=5&pagesize=6
         */
         
        [HttpGet]
        [Route("api/thumbnails")]
        public ThumbnailsResponseModel GetThumbnails([FromUri] int page = 1, [FromUri] int pagesize = 10)
        {
            var manifests = ManifestRepository.GetFilesByFileTitleSegment("");
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
