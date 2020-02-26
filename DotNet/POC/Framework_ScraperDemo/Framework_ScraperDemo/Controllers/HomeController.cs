using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using HtmlAgilityPack;
using Framework_ScraperDemo.Models;

namespace Framework_ScraperDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index([FromUri]string url = "https://www.w3school.com")
        {
            var web = new HtmlWeb();
            var doc = web.Load(url, "GET");

            RemoveElemetsByName(ref doc, "script");
            RemoveElemetsByName(ref doc, "iframe");

            MoveHeaderStylesToBody(ref doc);
            HackLinkTags(ref doc);

            var viewModel = new ScraperEchoViewModel(
                doc.DocumentNode
                    .SelectSingleNode("//body")
                    .InnerHtml
                    .ToString());

            viewModel.Url = url;

            return View(viewModel);
        }

        private void RemoveElemetsByName(ref HtmlDocument htmlDoc, string elementName)
        {
            var nodesToRemove = htmlDoc.DocumentNode.SelectNodes($"//{elementName}");
            if (nodesToRemove == null)
                return;

            foreach (var scriptNode in nodesToRemove)
                scriptNode.Remove();
        }

        private void MoveHeaderStylesToBody(ref HtmlDocument htmlDoc)
        {
            var styleNodes = htmlDoc.DocumentNode.SelectNodes("//head/style")
                .Select(p => HtmlNode.CreateNode($"<style>{p.InnerText}</style>"));

            foreach (var styleNode in styleNodes)
                htmlDoc.DocumentNode.SelectSingleNode("//body").AppendChild(styleNode);
        }

        private void HackLinkTags(ref HtmlDocument htmlDoc)
        {
            var linkTags = htmlDoc.DocumentNode.SelectNodes($"//a");
            if (linkTags == null)
                return;

            foreach (var linkTag in linkTags)
            {
                linkTag.SetAttributeValue("onclick", $"Navigate('{linkTag.Attributes["href"].Value.ToString()}')");
                linkTag.SetAttributeValue("href", "#");
            }
        }
    }
}