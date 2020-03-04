using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

using HtmlAgilityPack;
using Framework_ScraperDemo.Models;
using Framework_ScraperDemo.Extensions;

namespace Framework_ScraperDemo.Controllers
{
    public class HomeController : Controller
    {
        private UriBuilder _url;

        [System.Web.Http.Route("{urlString}")]
        public ActionResult Index([FromUri]string url = "")
        {
            if (string.IsNullOrEmpty(url))
                return View(new ScraperEchoViewModel());

            _url = new UriBuilder(url);
            var web = new HtmlWeb();
            var doc = web.Load(url, "GET");

            // RemoveElemetsByName(ref doc, "script");
            RemoveElemetsByName(ref doc, "iframe");

            MoveHeaderStylesToBody(ref doc);
            HackAnchorTags(ref doc);

            var htmlString = doc.DocumentNode
                .SelectSingleNode("//body")
                .InnerHtml
                .ToString();

            var viewModel = new ScraperEchoViewModel(htmlString);
            viewModel.Schema = _url.Scheme;
            viewModel.Domain = _url.Host.ToString();
            viewModel.Path = _url.Path;

            return View(viewModel);
        }

        private void RemoveElemetsByName(ref HtmlDocument htmlDoc, string elementName)
        {
            var nodesToRemove = htmlDoc.DocumentNode
                                        .SelectNodes($"//{elementName}")
                                        .ToListOrEmpty();

            foreach (var scriptNode in nodesToRemove)
                scriptNode.Remove();
        }

        private void MoveHeaderStylesToBody(ref HtmlDocument htmlDoc)
        {
            var body = htmlDoc.DocumentNode.SelectSingleNode("//body");
            var styleNodes = htmlDoc.DocumentNode
                                    .SelectNodes("//head/style")
                                    .ToListOrEmpty()
                                    .Select(p => HtmlNode.CreateNode($"<style>{p.InnerText}</style>"));

            foreach (var styleNode in styleNodes)
                body.AppendChild(styleNode);

            var styleLinkNodes = htmlDoc.DocumentNode
                                        .SelectNodes("//head/link[@rel='stylesheet']")
                                        .ToListOrEmpty()
                                        .Select(ToStyleLink);

            foreach (var styleLinkNode in styleLinkNodes)
                body.AppendChild(styleLinkNode);
        }

        private string ExpandLinkWithDomain(string path)
        {
            if (path.StartsWith("http"))
                return path;

            return $"{_url.Scheme}://{_url.Host}/{path}";
        }

        private HtmlNode ToStyleLink(HtmlNode origNide)
            => HtmlNode.CreateNode(
                $"<link type='text/css' rel='stylesheet' href='{ExpandLinkWithDomain(origNide.Attributes["href"].Value)}'>");
        
        private void HackAnchorTags(ref HtmlDocument htmlDoc)
        {
            var linkTags = htmlDoc.DocumentNode.SelectNodes($"//a").ToListOrEmpty();

            foreach (var linkTag in linkTags)
            {
                linkTag.SetAttributeValue("onclick", $"Navigate('{linkTag.Attributes["href"]?.Value.ToString()}')");
                linkTag.SetAttributeValue("href", "#");
            }
        }
    }
}