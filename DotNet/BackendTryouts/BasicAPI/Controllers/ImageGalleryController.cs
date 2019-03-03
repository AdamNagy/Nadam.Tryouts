using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicAPI.Controllers
{
    public class ImageGalleryController : Controller
    {
        // GET: ImageGallery
        public ActionResult Index()
        {
            var model = new string[] { "https://images.calzedonia.com/get/w/400/h/560/LIC036_wear_004_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC037_wear_019_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC042_wear_009_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC047_wear_019_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC034_wear_019_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC044_wear_019_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC039_wear_1287_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC035_wear_1287_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC033_wear_2240_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC050_wear_3784_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC031_wear_004_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC048_wear_005_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC049_wear_019_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC038_wear_038_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC022_wear_315_F.jpg",
                        "https://images.calzedonia.com/get/w/400/h/560/LIC030_wear_315_F.jpg" };

            return View(model);
        }
    }
}