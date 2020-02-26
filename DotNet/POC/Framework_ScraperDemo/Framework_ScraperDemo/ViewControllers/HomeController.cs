using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using HtmlAgilityPack;
using Framework_ScraperDemo.Models;
using System;

namespace Framework_ScraperDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index([FromUri]string urlString = "https://www.w3schools.com/tags/tag_a.asp")
        {
            var url = new UriBuilder(urlString);
            var web = new HtmlWeb();
            var doc = web.Load(urlString, "GET");

            RemoveElemetsByName(ref doc, "script");
            RemoveElemetsByName(ref doc, "iframe");

            MoveHeaderStylesToBody(ref doc);
            HackAnchorTags(ref doc);

            var htmlString = doc.DocumentNode
                .SelectSingleNode("//body")
                .InnerHtml
                .ToString();

            var viewModel = new ScraperEchoViewModel(htmlString);
            viewModel.Url = url.ToString();

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
            var body = htmlDoc.DocumentNode.SelectSingleNode("//body");
            var styleNodes = htmlDoc.DocumentNode.SelectNodes("//head/style")
                .Select(p => HtmlNode.CreateNode($"<style>{p.InnerText}</style>"));

            foreach (var styleNode in styleNodes)
                body.AppendChild(styleNode);

            var styleLinkNodes = htmlDoc.DocumentNode.SelectNodes("//head/link[@rel='stylesheet']")
                .Select(p => HtmlNode.CreateNode($"<link type='text/css' rel='stylesheet' href='{ExpandLinkWithDomain(p.Attributes["href"].Value)}'>"));

            foreach (var styleLinkNode in styleLinkNodes)
                body.AppendChild(styleLinkNode);
        }

        private string ExpandLinkWithDomain(string url)
        {
            if (url.StartsWith("http"))
                return url;

            return $"https://www.w3schools.com/{url}";
        }

        private void HackAnchorTags(ref HtmlDocument htmlDoc)
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