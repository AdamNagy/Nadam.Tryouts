using HtmlAgilityPack;
using Newtonsoft.Json;
using Scraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    SourceUrl = galleryLink,
                    Title = galleryTitle,
                    ThumbnailImageSources = sampleImages
                });
            }

            return JsonConvert.SerializeObject(galleryThumbnails);
        }

        async public Task<string> Gallery(string url)
        {
            // https://moggy.urlgalleries.net/blog_gallery.php?id=6984513&p=2
            var imageThumbs = new List<ImageMetaData>();
            var otherThumbs = new List<ImageMetaData>();
            var page = await web.LoadFromWebAsync(url);
            ConvertToImageThumbnails(page, out imageThumbs);

            var galleryId = getGalleryIdFromUrl(url);
            var nextPage = 2;
            var domain = GetDomainFromUrl(url);
            var otherPage = await web.LoadFromWebAsync($"https://{domain}/blog_gallery.php?id={galleryId}&p={nextPage}");
            while (ConvertToImageThumbnails(otherPage, out otherThumbs) && nextPage < 3)
            {
                ++nextPage;
                otherPage = await web.LoadFromWebAsync($"https://{domain}/blog_gallery.php?id={galleryId}&p={nextPage}");
                imageThumbs.AddRange(otherThumbs);
            }

            return JsonConvert.SerializeObject(imageThumbs);
        }

        private bool ConvertToImageThumbnails(HtmlDocument page, out List<ImageMetaData> imageThumbnails)
        {
            imageThumbnails = new List<ImageMetaData>();
            var hasThumbnails = false;

            var a_tags = page.DocumentNode.SelectNodes("//a");
            foreach (var a_tag in a_tags)
            {
                var image_tag = a_tag.SelectNodes("img");
                if (image_tag != null)
                {
                    var imageSrc = image_tag.First().Attributes["src"].Value;
                    imageThumbnails.Add(new ImageMetaData()
                    {
                        LinkHref = a_tag.Attributes["href"].Value,
                        ThumbnailImageSrc = imageSrc
                    });

                    hasThumbnails = true;
                }
            }

            return hasThumbnails;
        }

        private string getGalleryIdFromUrl(string url)
        {
            var splitted = url.Split('/');
            var idx = 0;
            string galleryId = "";
            while (string.IsNullOrEmpty(galleryId) || idx < splitted.Length)
            {
                if (splitted[idx].IndexOf("porn-gallery") > -1)
                {
                    galleryId = splitted[idx].Split('-').Last();
                }
                ++idx;
            }

            return galleryId;
        }

        private string GetDomainFromUrl(string url)
        {
            var splitted = url.Split('/');
            return splitted[2];
        }
    }
}