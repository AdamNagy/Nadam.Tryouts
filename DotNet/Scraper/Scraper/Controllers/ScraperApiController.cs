using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scraper.Controllers
{
    [RoutePrefix("api")]
    public class ScraperApiController : ApiController
    {
        private HtmlWeb web;

        private string baseUrl = "https://urlgalleries.net";

        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            int p = 1, t = 10; string q = "Jenni";
            var page = await web.LoadFromWebAsync($"{baseUrl}/?p={p}&t={t}&q={q}");
            var galleryTitleLinks = page.DocumentNode.SelectNodes("//table/tr[1]/td/a[2]");
            var galleryLinks = galleryTitleLinks.Select(a_tag => a_tag.Attributes["href"].Value);
            var galleryTitles = galleryTitleLinks.Select(a_tag => a_tag.InnerText);
            return null;
        }
    }
}
