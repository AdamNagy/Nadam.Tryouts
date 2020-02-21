using System;
using System.Linq;
using System.Web.Http;
using ManifestRepositoryApi.Actions;
using ManifestRepositoryApi.ManifestFramework;
using ManifestRepositoryApi.Models;

namespace ManifestRepositoryApi.Controllers
{
    public class ManifestController : ApiController
    {
        private ManifestRepository _repository;

        public ManifestController()
        {
            _repository = ManifestRepository.Instance;
        }

        public ManifestController(ManifestRepository repository = null)
        {
            _repository = repository;
        }

        // api/manifest/test1
        [HttpGet]
        [Route("api/manifest/{title}")]
        public GalleryResponseModel GetManifestByTitle(string title)
        {
            var manifest = _repository.GetFileByTitle(title);

            if (manifest != null)
            {
                var content = new GalleryModel()
                {
                    type = manifest.type,
                    content = manifest.ReadWhole()
                };

                return new GalleryResponseModel()
                {

                    success = true,
                    gallery = content
                };
            }

            return new GalleryResponseModel()
            {
                success = false,
                message = "No sucjóh file"
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
            [FromUri] string title = "",
            [FromUri] int page = 1,
            [FromUri] int pagesize = 10)
        {
            var selected = _repository
                .GetFilesByFileTitleSegment(title);

            if (selected.Count < 1)
            {
                return new ThumbnailsResponseModel()
                {
                    success = false,
                    message = "No galleries to return"
                };
            }

            var content = selected
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .Select(p => new GalleryModel()
                    {
                        type = p.type,
                        content = p.ReadThumbnail(),
                    });

            return new ThumbnailsResponseModel()
            {
                success = true,
                currentPage = page,
                pages = selected.Count() / pagesize,
                thumbnails = content
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
            if (_repository.Count < 1)
            {
                return new ThumbnailsResponseModel()
                {
                    success = false,
                    message = "No galleries to return"
                };
            }

            var content = _repository.All()
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .Select(p => new GalleryModel()
                {
                    type = p.type,
                    content = p.ReadThumbnail(),
                });

            return new ThumbnailsResponseModel()
            {
                success = true,
                currentPage = page,
                pages = _repository.Count / pagesize,
                thumbnails = content
            };
        }

        [HttpPost]
        [Route("api/manifest")]
        public GalleryResponseModel CreateManifest([FromBody]ManifestRequestModel value)
        {
            try
            {
                var newManifest = _repository.CreateManifest(value.fileName, value.content);
                return new GalleryResponseModel()
                {
                    success = true,
                    gallery = new GalleryModel()
                    {
                        content = newManifest.ReadThumbnail(),
                        type = newManifest.type
                    }
                };

            }
            catch (Exception e)
            {
                return new GalleryResponseModel()
                {
                    success = false,
                    message = $"Something went wrong during file creation. See inner message: {e.Message}"
                };
            }
        }

        [HttpPost]
        [Route("api/dispatch")]
        public bool DeleteManifest([FromBody]ActionWithPayload action)
        {
            return _repository.DeleteManifest(action).isSuccess;
        }
    }
}
