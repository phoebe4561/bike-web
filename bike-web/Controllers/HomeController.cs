using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class HomeController : Controller
    {
        private object db;

        public ActionResult Index()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 熱門路線 = (from h in db.Homes
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
                        select new popularRoute
                        {
                            h_ID=h.id,
                            h_description=h.hdescription,
                            h_name=h.hname,
                            h_distance= (double?)h.hdistance,
                            h_rank=h.hrank,
                            h_img=h.himg,
                            oc_artitleTitleID=oc.article_title_id,
                            oc_allStar=oc.all_star_summary,
                        }).OrderByDescending(e => e.oc_allStar).Take(3).ToList();

            var 所有官方路線 = (from h in db.Homes
                         join oc in db.official_route_comment on h.id equals oc.article_title_id
                         select new popularRoute
                         {
                              h_ID = h.id,
                              h_description = h.hdescription,
                              h_name = h.hname,
                              h_distance = (double?)h.hdistance,
                              h_rank = h.hrank,
                              h_img = h.himg,
                              oc_artitleTitleID = oc.article_title_id,
                              oc_allStar = oc.all_star_summary,
                          }).OrderBy(e => e.h_ID).ToList();

            var 熱門文章 = (from p in db.private_route
                        join u in db.users on p.user_id equals u.id
                        select new popularArticle
                        {
                            u_userID = u.id,
                            u_username = u.username,
                            p_userID = p.id,
                            p_artitle_title = p.article_title,
                            p_artitle_img_info = p.article_img_info,
                            p_datetime = (DateTime)p.datetime,
                            p_seen_num = p.seem_num,
                            p_star_num = p.star_num_sum,
                        }).OrderByDescending(e => e.p_userID).ToList();


            HomeViewModel vm = new HomeViewModel()
            {
                offcialPopularRoute = 熱門路線,
                offcialAllRoute = 所有官方路線,
                privatePopularArticle= 熱門文章,
            };

            return View(vm);

        }


        public ActionResult officialRoute()
        {


            KSBikeEntities db = new KSBikeEntities();

            var off_routes = db.Homes.OrderBy(m => m.id).ToList();

//            var off_routes = (from o in db.official_route_data
//                              join p in db.private_route
//on o.id equals p.user_id
//                              select new
//                              {
//                                  article = p.article_title,
//                              }).ToList();


            return View(off_routes);
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