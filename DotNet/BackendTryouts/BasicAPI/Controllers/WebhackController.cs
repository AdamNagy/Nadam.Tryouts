using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;

namespace BasicAPI.Controllers
{
    public class WebhackController : Controller
    {
        // GET: Webhack
        public ActionResult Index()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://images.calzedonia.com/get/w/400/h/560/LIC042_wear_009_F.jpg", "c:\\LIC042_wear_009_F.jpg");

            return View();
        }
    }
}