using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Testloginapp1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Trainer()
        {
            ViewBag.Message = "Your Trainer page.";

            return View();
        }
        public ActionResult Classes()
        {
            ViewBag.Message = "Your Classes page.";

            return View();
        }
        public ActionResult Blog()
        {
            ViewBag.Message = "Your Blog page.";

            return View();
        }
        public ActionResult Pricing()
        {
            ViewBag.Message = "Your Pricing page.";

            return View();
        }
    
    }
}
