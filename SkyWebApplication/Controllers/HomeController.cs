using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
        public ActionResult List()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Datatable()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Add()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Charts()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Dictionary()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}