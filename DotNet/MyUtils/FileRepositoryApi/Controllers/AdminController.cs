using System.Linq;
using System.Web.Mvc;
using ManifestRepositoryApi.ManifestFramework;
using ManifestRepositoryApi.ViewModels;

namespace ManifestRepositoryApi.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var viewModel =  new AdminViewModel();
            viewModel.files = ManifestRepository.Instance.GetFileNames().ToArray();

            return View("admin" ,viewModel);
        }
    }
}