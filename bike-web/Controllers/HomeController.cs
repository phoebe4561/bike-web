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
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                return View();
            }
            
        }

        public ActionResult memberPage()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult routeEditPage()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}