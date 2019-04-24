using HtmlAgilityPack;
using Newtonsoft.Json;
using Scraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Scraper.Controllers
{
    public class HomeController : Controller
    {
        private HtmlWeb web;

        private string baseUrl = "https://urlgalleries.net";

        public HomeController()
        {
            web = new HtmlWeb();
        }

        public ActionResult Index()
        {
            return View();
        }

        async public Task<String> GetHome(string url, int p)
        {
            var page = await web.LoadFromWebAsync($"{url}?p={p}");
            var scripts = page.DocumentNode.SelectNodes("//script");
            var iframes = page.DocumentNode.SelectNodes("//iframe");

            if(scripts != null)
            {
                foreach (var script in scripts)            
                    script.Remove();
            }

            if(iframes != null)
            {
                foreach (var iframe in iframes)
                    iframe.Remove();
            }

            return page.ParsedText;
        }

        /// <summary>
        /// UrlGalleries home page scraper
        /// </summary>
        /// <param name="p">is the page</param>
        /// <param name="t"></param>
        /// <param name="q">search term</param>
        /// <returns></returns>
        async public Task<String> Get(int p, int t, string q)
        {
            var page = await web.LoadFromWebAsync($"{baseUrl}/?p={p}&t={t}&q={q}");

            var tables = page.DocumentNode.SelectNodes("//table");

            var galleryThumbnails = new List<GalleryThumbnail>();
            foreach (var galleryTable in tables)
            {
                var galleryTitleLink = galleryTable.SelectSingleNode("tr[1]/td/a[2]");
                if (galleryTitleLink == null)
                    continue;

                var galleryLink = galleryTitleLink.Attributes["href"].Value;
                var galleryTitle = galleryTitleLink.InnerText;
                var sampleImageElements = galleryTable.SelectNodes("tr[2]/td/div/div[last()]/a/img");
                if (sampleImageElements == null)
                    continue;

                var sampleImages = sampleImageElements.Select(img_element => img_element.Attributes["src"].Value).ToList();

                galleryThumbnails.Add(new GalleryThumbnail()
                {
                    Link = galleryLink,
                    Title = galleryTitle,
                    ImageLinks = sampleImages
                });                
            }

            return JsonConvert.SerializeObject(galleryThumbnails);
        }

        async public Task<string> Gallery(string url)
        {
            var page = await web.LoadFromWebAsync(url);
            var imageLinks = page.DocumentNode.SelectNodes("//img").Select(p => p.Attributes["src"].Value).ToList();

            return JsonConvert.SerializeObject(imageLinks);
        }
    }
}