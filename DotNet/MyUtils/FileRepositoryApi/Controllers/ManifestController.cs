using System.Linq;
using System.Web.Http;
using ManifestRepositoryApi.ManifestFramework;
using ManifestRepositoryApi.Models;

namespace ManifestRepositoryApi.Controllers
{
    public class ManifestController : ApiController
    {
        private ManifestRepository _repository;

        public ManifestController(ManifestRepository repository = null)
        {
            if (repository == null)
                _repository = ManifestRepository.Instance;
            else
                _repository = repository;
        }

        // api/manifest/test1
        [HttpGet]
        [Route("api/manifest/{title}")]
        public GalleryResponseModel GetManifestByTitle(string title)
        {
            var manifest = _repository.GetFileByTitle(title);
            return new GalleryResponseModel()
            {
                type = manifest.type,
                content = manifest.ReadWhole()
            };
        }

        /*
         * api/thumbnails/test1
         * api/thumbnails/test1?page=4
         * api/thumbnails/test1?page=5&pagesize=6
         */
        [HttpGet]
        [Route("api/thumbnails/{title}")]
        public ThumbnailsResponseModel GetThumbnailsByTitleFragment(
            string title = "",
            [FromUri] int page = 1,
            [FromUri] int pagesize = 10)
        {
            var response = _repository
                .GetFilesByFileTitleSegment(title)
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .Select(p => new GalleryResponseModel()
                    {
                        type = p.type,
                        content = p.ReadThumbnail(),
                    });

            return new ThumbnailsResponseModel()
            {
                currentPage = 123,
                pages = 4325,
                thumbnails = response
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
            var response = _repository
                .GetFilesByFileTitleSegment("")
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .Select(p => new GalleryResponseModel()
                    {
                        type = p.type,
                        content = p.ReadThumbnail(),
                    });

            return new ThumbnailsResponseModel()
            {
                currentPage = 123,
                pages = 4325,
                thumbnails = response
            };
        }

        // POST api/manifest
        public void Post([FromBody]ManifestRequestModel value)
        {

        }
    }
}
