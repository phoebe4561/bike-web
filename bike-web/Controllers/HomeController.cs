using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult officialRoute()
        {
            return View();
        }

        public ActionResult privateRoute()
        {

            return View();
        }

        public ActionResult memberPage()
        {

            return View();
        }

        public ActionResult routeEditPage()
        {

            return View();
        }
    }
}