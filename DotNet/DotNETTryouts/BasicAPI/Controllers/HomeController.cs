using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Web.Http.Cors;
using System.Text;

namespace BasicAPI.Controllers
{
    [EnableCors(origins: "http://localhost:29790/", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var result = new ViewResult();

            return result;
        }
    }
}
