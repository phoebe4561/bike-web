using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace bike_web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            KSBikeEntities db = new KSBikeEntities();
          
            int userID = Convert.ToInt32(Session["id"]);           
            var 我的最愛 = (from h in db.Homes
                        join fav in db.user_favorite on h.id equals fav.official_route_id
                        //from fav in db.user_favorite
                       where fav.user_fav_id== userID && fav.official_route_id>1
                       select new userFav {
                           id=fav.id,
                       official_route_id= fav.official_route_id
                       }).OrderBy(e => e.id).ToList();
            
            var 熱門路線 = (from h in db.Homes
                        where h.id > 1
                        let oc = db.official_route_comment.Where (cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        select new popularRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_allStar = (double?)oc.all_star_summary

                        }).OrderByDescending(e => e.oc_allStar).Take(3).ToList();


            List<popularRoute> 所有官方路線 = new List<popularRoute>();

            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
                所有官方路線 = (from h in db.Homes
                          where h.id >1
                          let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()  
                          select new popularRoute
                          {
                              h_ID = h.id,
                              h_description = h.hdescription,
                              h_name = h.hname,
                              h_distance = (double?)h.hdistance,
                              h_rank = h.hrank,
                              h_img = h.himg,
                              oc_artitleTitleID = oc.article_title_id,
                              oc_allStar = (double?)oc.all_star_summary,
                          }).OrderByDescending(e => e.oc_allStar).ToList(); /*.OrderBy(e => e.h_ID).ToList();*/
            else
                所有官方路線 = (from h in db.Homes
                          where h.id >1
                          let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                          where h.hname.Contains(keyword)
                          select new popularRoute
                          {
                              h_ID = h.id,
                              h_description = h.hdescription,
                              h_name = h.hname,
                              h_distance = (double?)h.hdistance,
                              h_rank = h.hrank,
                              h_img = h.himg,
                              oc_artitleTitleID = oc.article_title_id,
                              oc_allStar = (double?)oc.all_star_summary,
                          }).OrderByDescending(e => e.oc_allStar).ToList(); /*.OrderBy(e => e.h_ID).ToList();*/

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
                        }).OrderByDescending(e => e.p_userID).Take(2).ToList();






            //var result = from id in aryIDs
            //             group id by id into g
            //             orderby g.Count() descending
            //             select new { Id = g.Key, Count = g.Count() };


            //from b in db.Buildings
            //join u in db.BuildingUsers on b.ID equals u.ID_BUILDING into g
            //orderby g.Count() descending, b.Name descending
            //select new
            //{
            //    Id = b.ID,
            //    Name = b.NAME,
            //    Total = g.Count()
            //}


            //objPersons.Where(p => p.Person > 0).GroupBy(p => p.Name).Select(p => new { Name = p.Key, Number = p.Count() }).OrderByDescending(p => p.Number);
            var hashtagranking = db.hashtags.GroupBy(p =>p.hashtag_name).Select(p => new { Name = p.Key, Number = p.Count() }).OrderByDescending(p => p.Number).ToList().Take(10).ToList();
            List<hashtagNamandSum> hashtagNamandSumtheList = new List<hashtagNamandSum>();
            
            foreach (var item in hashtagranking)
            {
                hashtagNamandSum x = new hashtagNamandSum();


                x.Name = item.Name;
                x.Number = item.Number;

                hashtagNamandSumtheList.Add(x);


            }



          



            HomeViewModel vm = new HomeViewModel()
            {
                offcialPopularRoute = 熱門路線,
                offcialAllRoute = 所有官方路線,
                privatePopularArticle = 熱門文章,
                //mia
                checkFav = 我的最愛,
                //德威
                hashtagNamandSum = hashtagNamandSumtheList
            };

            return View(vm);

        }


        public ActionResult officialRoute()
        {
            return RedirectToAction("List", "offRoute");
        }

        //public ActionResult privateRoute()
        //{
        //    return View();
        //}

        public ActionResult memberPage()
        {
            //KSBikeEntities db = new KSBikeEntities();
            //var id = Convert.ToInt32(Session["id"]);
            //var mem = db.users
            //    .Where(m => m.id == id)
            //    .FirstOrDefault();
            if (Session["id"] == null)
            {
               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        
        public ActionResult chatroom()
        {

            return View();
        }


        //mia
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
                SqlDateTime myDateTime = DateTime.Now;
                myfav.official_route_id = 1;
                myfav.datetime = (DateTime)myDateTime;
                //db.user_favorite.Remove(myfav);
                db.SaveChanges();
                message = "delete";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        //德威的
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

        int pageSize = 9;

        public ActionResult privateRoute(int page = 1)
        {



            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                int currentPage = page < 1 ? 1 : page;



                //捕捉登入者id
                var Sid = Convert.ToInt32(Session["ID"]);


                //需要呈現的資料
                KSBikeEntities db = new KSBikeEntities();
                var 熱門文章 = (from p in db.private_route
                            join u in db.users on p.user_id equals u.id
                            select new PrivateRouteData
                            {
                                u_userID = u.id,
                                u_username = u.username,
                                p_ID = p.id,
                                p_artitle_title = p.article_title,
                                p_artitle_img_info = p.article_img_info,
                                p_datetime = (DateTime)p.datetime,
                                p_seen_num = p.seem_num,
                                p_star_num_sum = p.star_num_sum,
                                p_sum_people_give_star_num = p.sum_people_give_star,
                                p_content = p.article_context,
                                //p_star = star_point( p.star_num_sum, p.sum_people_give_star)





                            }).OrderByDescending(e => e.p_ID).ToList();


                //分頁判斷的東西不用管這行
                var result = 熱門文章.ToPagedList(currentPage, pageSize);


                PrivateRouteViewModel prv = new PrivateRouteViewModel
                {
                    熱門文章資訊 = 熱門文章
                };


                int[] fav = db.user_favorite.Where(d => d.user_fav_id == Sid).Select(a => a.private_route_id).ToArray();

                //判斷有沒有加到我的最愛
                foreach (PrivateRouteData item in result)
                {
                    item.hashtag = db.hashtags.Where(e => e.private_route_id == item.p_ID).Select(f => f.hashtag_name).ToList();



                    if (fav.Contains(item.p_ID))
                    {
                        item.p_fav = 1;
                    }
                    else
                    {
                        item.p_fav = 0;
                    }

                }






                return View(result);


            }

        }

    }
}