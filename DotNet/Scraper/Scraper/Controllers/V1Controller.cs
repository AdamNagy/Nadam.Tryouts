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
    public class V1Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //async public Task<String> GetHome(string url, int p)
        //{
        //    var page = await web.LoadFromWebAsync($"{url}?p={p}");
        //    var scripts = page.DocumentNode.SelectNodes("//script");
        //    var iframes = page.DocumentNode.SelectNodes("//iframe");

        //    if(scripts != null)
        //    {
        //        foreach (var script in scripts)            
        //            script.Remove();
        //    }

        //    if(iframes != null)
        //    {
        //        foreach (var iframe in iframes)
        //            iframe.Remove();
        //    }

        //    return page.ParsedText;
        //}

        
    }
}