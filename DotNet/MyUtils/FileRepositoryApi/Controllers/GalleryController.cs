using ManifestRepositoryApi.ManifestFramework;
using ManifestRepositoryApi.ViewModels;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace ManifestRepositoryApi.Controllers
{
    public class GalleryController : Controller
    {
        private ManifestRepository _repository;

        public GalleryController()
        {
            _repository = ManifestRepository.Instance;
        }

        public GalleryController(ManifestRepository repository = null)
        {
            _repository = repository;
        }

        /*
         * home
         * home?page=4
         * home?page=5&pagesize=6
         */
        [System.Web.Mvc.HttpGet]
        public ActionResult Thumbnails([FromUri] int page = 1, [FromUri] int pagesize = 10)
        {
            var content = _repository.All()
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .Select(p => new JObjectGallery()
                    {
                        Type = p.type,
                        JsonGallery = JObject.Parse(p.ReadThumbnail()),
                    })
                .Select(p => new GalleryViewModel(p));

            var viewModel = new ThumbnailsViewModel()
            {
                thumbnails = content,
                CurrentPage = page,
                Pages = _repository.Count / pagesize
            };

            return View("thumbnails", viewModel);
        }

        /*
         * home/test1
         * home/test1?page=4
         * home/test1?page=5&pagesize=6
         */
        [System.Web.Mvc.HttpGet]
        public ActionResult ThumbnailsForTitle(
            [FromUri] string title = "",
            [FromUri] int page = 1,
            [FromUri] int pagesize = 10)
        {
            return View();
        }


        [System.Web.Mvc.HttpGet]
        public ActionResult Gallery([FromUri]string title)
        {
            return View();
        }
    }
}