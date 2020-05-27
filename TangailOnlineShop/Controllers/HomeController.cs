using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TangailOnlineShop.Models.Home;

namespace TangailOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel());
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