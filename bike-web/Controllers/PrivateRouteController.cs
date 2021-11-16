using bike_web.Models;
using bike_web.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class PrivateRouteController : Controller
    {
        // GET: PrivateRoute
        // GET: PrivateRoute
        //public ActionResult PrivateRoute()
        //{
        //    return View();
        //}

        public ActionResult privateRoutePage(int? articletitleid)
        {
            var Sid = Convert.ToInt32(Session["ID"]);
            List<string> hashtags = new List<string>();

            if (articletitleid != null)
            {
                var db = new KSBikeEntities();
                var y = db.private_route.Where(k => k.id == articletitleid).FirstOrDefault();
                if (y == null)
                {
                    return RedirectToAction("privateRoutePage", new { articletitleid = 1 });
                }
                Session["articletitleid"] = articletitleid;
                y.seem_num += 1;
                db.SaveChanges();


                //db.Database.CommandTimeout = 3 * 60;
                List<private_route_comment> PrivateRouteComments = db.private_route_comment.Where(k => k.article_title_id == articletitleid).ToList();
                //
                List<commentWithName> 留言資訊 = new List<commentWithName>();
                foreach (var item in PrivateRouteComments)
                {
                    commentWithName z = new commentWithName();
                    z.id = item.id;
                    z.comment_user_name = db.users.Where(x => x.id == item.comment_user_id).FirstOrDefault().username;
                    z.user_give_star_num = item.user_give_star_num;
                    z.comment = item.comment;
                    z.datetime = (DateTime)item.datetime;
                    留言資訊.Add(z);



                }










                thePrivateRouteData 熱門文章 = (from p in db.private_route
                                            join u in db.users on p.user_id equals u.id
                                            where p.id == (int)articletitleid
                                            select new thePrivateRouteData
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

                                            }).FirstOrDefault();
                //判斷有沒有在我的最愛


                int[] fav = db.user_favorite.Where(d => d.user_fav_id == Sid).Select(a => a.private_route_id).ToArray();

                if (fav.Contains(熱門文章.p_ID))
                {
                    熱門文章.p_fav = 1;
                }
                else
                {
                    熱門文章.p_fav = 0;
                }


                //捕捉每篇文章hashtag
                var hahtags = db.hashtags.Where(d => d.private_route_id == 熱門文章.p_ID).ToArray();

                if (hahtags == null)
                {
                    PrivateRouteandCommentViewModel vm = new PrivateRouteandCommentViewModel()
                    {
                        熱門文章 = 熱門文章,
                        留言資訊 = 留言資訊,

                    };


                    //
                    return View(vm);

                }
                else
                {
                    foreach (var item in hahtags)
                    {
                        hashtags.Add(item.hashtag_name);

                    }
                    PrivateRouteandCommentViewModel vm1 = new PrivateRouteandCommentViewModel()
                    {
                        熱門文章 = 熱門文章,
                        留言資訊 = 留言資訊,
                        HashTags = hashtags
                    };



                    return View(vm1);


                }





            }
            return View();


        }

        //public ActionResult AddMessage()
        //{

        //    int AddMessageNum = (int)TempData["TDMessage"];
        //    AddMessageNum += 1;
        //    TempData["TDMessage"] = AddMessageNum.ToString();

        //    return RedirectToAction("PrivateRoute", new { articletitleid = 1 });


        //}


        [HttpPost]
        public ActionResult saveMessage()
        {
            int starnum = 0;
            string starnumtext = Request.Form["txtNum"].Trim();
            int articleid = Convert.ToInt32(Session["articletitleid"]);
            if (starnumtext == "1")
            {
                starnum = 1;
            }
            else if (starnumtext == "2")
            {
                starnum = 2;
            }
            else if (starnumtext == "3")
            {
                starnum = 3;
            }
            else if (starnumtext == "4")
            {
                starnum = 4;
            }
            else if (starnumtext == "5")
            {
                starnum = 5;
            }
            else
            {
                starnum = 0;
            }

            SqlDateTime myDateTime = DateTime.Now;
            //DateTime nowdateTime = DateTime.Now;
            //string sqlFormattedDate = nowdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            KSBikeEntities db = new KSBikeEntities();
            private_route_comment x = new private_route_comment();


            x.article_title_id = articleid;
            x.comment_user_id = Convert.ToInt32(Session["ID"]);


            x.user_give_star_num = starnum;
            x.comment = Request.Form["txtComment"];
            x.datetime = (DateTime)myDateTime;
            db.private_route_comment.Add(x);
            db.SaveChanges();

            var y = db.private_route.Where(k => k.id == articleid).FirstOrDefault();
            y.star_num_sum += starnum;
            y.sum_people_give_star += 1;
            db.SaveChanges();

            return RedirectToAction("privateRoutePage", new { articletitleid = articleid });
        }

        [HttpPost]
        public ActionResult addToMyFav(int articleid)
        {
            string message = "";
            int Sid = Convert.ToInt32(Session["id"]);
            KSBikeEntities db = new KSBikeEntities();

            var myfav = db.user_favorite.Where(m => m.user_fav_id == Sid && m.private_route_id == articleid).FirstOrDefault();


            if (myfav == null)
            {
                user_favorite x = new user_favorite();
                SqlDateTime myDateTime = DateTime.Now;
                x.datetime = (DateTime)myDateTime;
                x.user_fav_id = Sid;
                x.private_route_id = articleid;
                x.official_route_id = 0;
                db.user_favorite.Add(x);
                db.SaveChanges();
                message = "add";
            }
            else
            {
                db.user_favorite.Remove(myfav);
                db.SaveChanges();
                message = "delete";
            }

            return Json(message, JsonRequestBehavior.AllowGet);



        }



        int pageSize = 9;

        //這裡開始
        public ActionResult privateRoute(string hashtag, int page =1)
        {



            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {


                int currentPage = page < 1 ? 1 : page;


                var Sid = Convert.ToInt32(Session["ID"]);



                KSBikeEntities db = new KSBikeEntities();

                //先拿到PR的id
                var privateRouteIds = db.hashtags.Where(x => x.hashtag_name == hashtag).OrderByDescending(e => e.id).ToList(); ;


                List<PrivateRouteData> 熱門文章資訊 = new List<PrivateRouteData>();

                foreach (var item in privateRouteIds)
                {

                    private_route y = db.private_route.Where(z => z.id == item.private_route_id).FirstOrDefault();
                    PrivateRouteData x = new PrivateRouteData();
                    x.u_userID = y.user_id;
                    x.u_username = db.users.Where(a => a.id == y.user_id).FirstOrDefault().username;
                    x.p_ID = y.id;
                    x.p_artitle_title = y.article_title;
                    x.p_artitle_img_info = y.article_img_info;
                    x.p_datetime = (DateTime)y.datetime;
                    x.p_seen_num = y.seem_num;
                    x.p_star_num_sum = y.star_num_sum;
                    x.p_sum_people_give_star_num = y.sum_people_give_star;
                    x.p_content = y.article_context;
                    熱門文章資訊.Add(x);

                }










                var result = 熱門文章資訊.ToPagedList(currentPage, pageSize);


                //PrivateRouteViewModel prv = new PrivateRouteViewModel
                //{
                //    熱門文章資訊 = 熱門文章資訊
                //};


                int[] fav = db.user_favorite.Where(d => d.user_fav_id == Sid).Select(a => a.private_route_id).ToArray();
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


                Session["hashtag"] = hashtag;

                return View(result);


            }

        }

        [HttpPost]
        public ActionResult saveComment(int articleid, string starnum, string comment)
        {
            int starnumnum = 0;
            starnum = starnum.Trim();

            if (starnum == "1")
            {
                starnumnum = 1;
            }
            else if (starnum == "2")
            {
                starnumnum = 2;
            }
            else if (starnum == "3")
            {
                starnumnum = 3;
            }
            else if (starnum == "4")
            {
                starnumnum = 4;
            }
            else if (starnum == "5")
            {
                starnumnum = 5;
            }
            else
            {
                starnumnum = 0;
            }

            //儲存comment
            SqlDateTime myDateTime = DateTime.Now;

            KSBikeEntities db = new KSBikeEntities();
            private_route_comment x = new private_route_comment();


            x.article_title_id = articleid;
            x.comment_user_id = Convert.ToInt32(Session["ID"]);


            x.user_give_star_num = starnumnum;
            x.comment = comment;
            x.datetime = (DateTime)myDateTime;
            db.private_route_comment.Add(x);
            db.SaveChanges();

            //更改privateroute 星星數跟評分人數
            if (starnumnum != 0)
            {
                var y = db.private_route.Where(k => k.id == articleid).FirstOrDefault();
                y.star_num_sum += starnumnum;
                y.sum_people_give_star += 1;
                db.SaveChanges();
            }

            //送出前五則評論資訊(取出的時序要顛倒)，總頁數(除以五去算)
            List<private_route_comment> PrivateRouteComments = db.private_route_comment.Where(k => k.article_title_id == articleid).ToList();


            int pagenum = 0;
            if (PrivateRouteComments.Count() % 5 > 0)
            {
                pagenum = PrivateRouteComments.Count() / 5 + 1;
            }
            else if (PrivateRouteComments.Count() / 5 > 0)
            {
                pagenum = PrivateRouteComments.Count() / 5;
            }


            //逆轉
            PrivateRouteComments.Reverse();
            if (PrivateRouteComments.Count() >= 5)
            {
                PrivateRouteComments = PrivateRouteComments.Take(5).ToList();
            }
            //選前5個





            List<commentWithName> 留言資訊 = new List<commentWithName>();
            foreach (var item in PrivateRouteComments)
            {
                commentWithName z = new commentWithName();
                z.id = item.id;
                z.comment_user_name = db.users.Where(c => c.id == item.comment_user_id).FirstOrDefault().username;
                z.user_give_star_num = item.user_give_star_num;
                z.comment = item.comment;
                z.datetime = (DateTime)item.datetime;
                留言資訊.Add(z);



            }

            //送出評分人數與總評分

            var privateArticle = db.private_route.Where(s => s.id == articleid).FirstOrDefault();
            double star = 0;
            if ((int)privateArticle.sum_people_give_star != 0)
            {
                star = (double)Math.Round((decimal)(int)privateArticle.star_num_sum / (int)privateArticle.sum_people_give_star, 2);
            }




            //創一個viewmodel一起傳回去
            PrivateComment vm = new PrivateComment()
            {
                pagenum = pagenum,
                留言資訊 = 留言資訊,
                star = star
            };

            return Json(vm, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult changePage(int articleId, int page)
        {
            KSBikeEntities db = new KSBikeEntities();
            List<private_route_comment> PrivateRouteComments = db.private_route_comment.Where(k => k.article_title_id == articleId).ToList();

            int pagenum = 0;
            if (PrivateRouteComments.Count() % 5 > 0)
            {
                pagenum = PrivateRouteComments.Count() / 5 + 1;
            }
            else if (PrivateRouteComments.Count() / 5 > 0)
            {
                pagenum = PrivateRouteComments.Count() / 5;
            }

            PrivateRouteComments.Reverse();
            if (PrivateRouteComments.Count() >= page * 5)
            {
                PrivateRouteComments = PrivateRouteComments.GetRange((page - 1) * 5, 5);
            }
            else
            {
                int thelastcomment = PrivateRouteComments.Count() - ((page - 1) * 5);
                PrivateRouteComments = PrivateRouteComments.GetRange((page - 1) * 5, thelastcomment);
            }
            List<commentWithName> 留言資訊 = new List<commentWithName>();
            foreach (var item in PrivateRouteComments)
            {
                commentWithName z = new commentWithName();
                z.id = item.id;
                z.comment_user_name = db.users.Where(c => c.id == item.comment_user_id).FirstOrDefault().username;
                z.user_give_star_num = item.user_give_star_num;
                z.comment = item.comment;
                z.datetime = (DateTime)item.datetime;
                留言資訊.Add(z);



            }

            PrivateComment vm = new PrivateComment()
            {
                pagenum = pagenum,
                留言資訊 = 留言資訊,
                nowpage = page
            };


            return Json(vm, JsonRequestBehavior.AllowGet);


        }

       






    }
}