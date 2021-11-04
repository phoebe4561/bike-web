using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bike_web.Models;
using bike_web.ViewModels;

namespace bike_web.Controllers
{
    public class COfficialRController : Controller
    {
        // GET: COfficialR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult theRoute(int id)
        {
            OfficialVM vm = null;
            
            
            var 路線資訊 = (new OfficialR_F()).QrouteInfo(id);
            var 路線評論 = (new OfficialR_F()).QrouteComment(id);
            var 路線 = (new OfficialR_F()).QrouteHome(id);

            vm = new OfficialVM()
            {                
                RouteInfo = 路線資訊,
                RouteComment = 路線評論,
                RouteHome = 路線
            };             

            return View(vm);
        }
    }
}