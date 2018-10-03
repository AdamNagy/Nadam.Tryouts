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

            return View();
        }

        [HttpGet]
        
        public string GetProducts()
        {
            var ser = new JsonSerializer();
            return ReadFromFile();
        }

        //private List<Product> ReadFromFile()
        private string ReadFromFile()
        {
            string jsonStr;
            try
            {
                using (var fs = new FileStream(@"C:\Documents\Angular2\APM\api\products\products.json",
                                FileMode.Open,
                                FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs, Encoding.Default))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            //return tableData;
            return jsonStr;
        }
    }
}
