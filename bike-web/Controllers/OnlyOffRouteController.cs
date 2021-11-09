using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class OnlyOffRouteController : Controller
    {
        // GET: OnlyOffRoute
        public ActionResult List(int id)
        {
            KSBikeEntities db = new KSBikeEntities();
            offRouteViewModel vm = new offRouteViewModel();
            official_route_data offRoutes = db.official_route_data.FirstOrDefault(r => r.home_id == id);
            if (offRoutes != null)
            {
                var 單一官方路線 = (from h in db.Homes
                              join od in db.official_route_data on h.id equals od.home_id
                              where od.home_id == id
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  od_homeID =od.home_id,
                                  od_offRouteID = od.id,
                                  od_official_data_catalog =od.official_data_catalog,
                                  od_official_data_img=od.official_data_img,
                                  od_official_data_img_content = od.official_data_content,
                                  od_official_data_img_info =od.official_data_img_info,
                              }).ToList();


                vm.offcialAllRoute = 單一官方路線;

            }
            return View(vm);
        }
    }
}