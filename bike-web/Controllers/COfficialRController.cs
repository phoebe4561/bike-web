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

        public ActionResult theRoute()/*string articleID這裡一直進來null*/
        {
            OfficialVM vm = null;
            //List<routeInfo> 路線資訊 = null;
            //List<routeComment> 路線評論 = null;
            //var theNo = articleID.ToString();
            
            //var intOK = int.TryParse(articleID, out int theNo);
            //int theNUMBER = 2;
            int theNUMBER = 3;

            var 路線資訊 = (new OfficialR_F()).QrouteInfo(theNUMBER);
            var 路線評論 = (new OfficialR_F()).QrouteComment(theNUMBER);
            var 路線 = (new OfficialR_F()).QrouteHome(theNUMBER);

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