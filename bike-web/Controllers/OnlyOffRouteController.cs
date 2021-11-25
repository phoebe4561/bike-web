using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class OnlyOffRouteController : Controller
    {
        // GET: OnlyOffRoute
        public ActionResult List(int id)
        {
            ViewBag.homeID = id;
            var avgStar = (new OnlyOffR_F()).refreshAvgStar(id);
            ViewBag.avgStar = avgStar;
            int userID = Convert.ToInt32(Session["id"]);

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
                                  od_homeID = od.home_id,
                                  od_offRouteID = od.id,
                                  od_official_data_catalog = od.official_data_catalog,
                                  od_official_data_img = od.official_data_img,
                                  od_official_data_img_content = od.official_data_content,
                                  od_official_data_img_info = od.official_data_img_info,
                              }).ToList();

                var 路線評論 = (new OnlyOffR_F()).QrouteComment((int)id);

                var 路線標籤 = (from tag in db.hashtags
                            where tag.official_route_id == id
                            select new routeHashtag
                            {
                                id = tag.id,
                                hashtag_name = tag.hashtag_name,
                                private_route_id = tag.private_route_id
                            }).ToList();
                var 我的最愛 = (from h in db.Homes
                            join fav in db.user_favorite on h.id equals fav.official_route_id
                            where fav.user_fav_id == userID && fav.official_route_id > 1
                            select new userFav
                            {
                                id = fav.id,
                                official_route_id = fav.official_route_id
                            }).OrderBy(e => e.id).ToList();
                vm.offcialAllRoute = 單一官方路線;
                vm.RouteComment = 路線評論;
                if (路線標籤 != null) { vm.RouteHashtag = 路線標籤; }
                vm.checkFav = 我的最愛;

            }
            return View(vm);
        }



        [HttpPost]
        public ActionResult SaveReview()    //saveReview
        {
            SqlDateTime myDateTime = DateTime.Now;
            var homeID = int.Parse(Request.Form["homeID"]);
            var userID = int.Parse(Request.Form["txtUserID"]);
            int starnum = 0;

            string starnumtext = Request.Form["txtNum"].Trim();
            if (Regex.IsMatch(starnumtext, @"^[0-9]+$"))
            {
                starnum = int.Parse(starnumtext);
            }
            else starnum = 0;

            KSBikeEntities db = new KSBikeEntities();
            official_route_comment x = new official_route_comment();

            x.article_title_id = homeID;
            x.comment_user_id = (int)userID;
            x.user_give_star_num = starnum;
            x.comment = Request.Form["userComment"];
            x.datetime = (DateTime)myDateTime;
            db.official_route_comment.Add(x);
            db.SaveChanges();
            var avgStar = (new OnlyOffR_F()).refreshAvgStar(homeID);
            (new OnlyOffR_F()).updateAvgStar(homeID, avgStar);
            ViewBag.avgStar = avgStar;
            return RedirectToAction("List", new { id = homeID });
        }



        [HttpPost]
        public ActionResult UpdateFavorite(int articleid)
        {

            string message = "";
            int userID = Convert.ToInt32(Session["id"]);
            KSBikeEntities db = new KSBikeEntities();

            var myfav = db.user_favorite.Where(m => m.user_fav_id == userID && m.official_route_id == articleid).FirstOrDefault();


            if (myfav == null)
            {
                user_favorite x = new user_favorite();
                SqlDateTime myDateTime = DateTime.Now;
                x.datetime = (DateTime)myDateTime;
                x.user_fav_id = userID;
                x.private_route_id = 0;
                x.official_route_id = articleid;
                db.user_favorite.Add(x);
                db.SaveChanges();
                message = "add";
            }
            else
            {
                user_favorite x = new user_favorite();
                SqlDateTime myDateTime = DateTime.Now;
                x.datetime = (DateTime)myDateTime;
                x.official_route_id = 1;
                //db.user_favorite.Remove(myfav);
                db.SaveChanges();
                message = "delete";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }



        //[HttpPost]
        public ActionResult findHashtag(int homeID, string hashtag)
        {
            KSBikeEntities db = new KSBikeEntities();
            offRouteViewModel vm = new offRouteViewModel();
            var avgStar = (new OnlyOffR_F()).refreshAvgStar(homeID);
            ViewBag.avgStar = avgStar;

          
            var 路線標籤 = (from h in db.Homes
                        join tag in db.hashtags on h.id equals tag.official_route_id                       
                        where tag.hashtag_name == hashtag
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,

                            //mia
                            hashtag_name = tag.hashtag_name,
                            private_route_id = tag.private_route_id
                        }).ToList();
            vm.offcialAllRoute = 路線標籤;
            return View(vm);
        }
    }
}