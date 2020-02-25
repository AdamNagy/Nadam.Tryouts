using System.Linq;
using System.Web.Mvc;
using HtmlAgilityPack;
using Framework_ScraperDemo.Models;

namespace Framework_ScraperDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var url = "https://www.w3schools.com/colors/colors_groups.asp";
            var web = new HtmlWeb();
            var doc = web.Load(url, "GET");

            var scriptNodes = doc.DocumentNode.SelectNodes("//script");
            foreach (var scriptNode in scriptNodes)
                scriptNode.Remove();

            var htmlBody = doc.DocumentNode.SelectSingleNode("//body");

            var styleNodes = doc.DocumentNode.SelectNodes("//head/style")
                .Select(p => HtmlNode.CreateNode($"<style>{p.InnerText}</style>"));

            foreach (var styleNode in styleNodes)
                doc.DocumentNode.SelectSingleNode("//body").AppendChild(styleNode);

            var viewModel = new ScraperEchoViewModel()
            {
                HtmlString = doc.DocumentNode.SelectSingleNode("//body").InnerHtml.ToString()
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}