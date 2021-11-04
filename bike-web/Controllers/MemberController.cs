using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class MemberController : Controller
    {

        // GET: Member
        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            string message = "";
            KSBikeEntities db = new KSBikeEntities();
            var login = db.users
                .Where(m => m.email == email && m.password == password)
                .FirstOrDefault();
            if (login == null)
            {
                message = "fail";
            }
            else
            {
                Session["username"] = login.username;
                Session["id"] = login.id;
                Session["email"] = login.email;
                message = "pass";
            }
            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult Register(string name, string email, string password, DateTime birthday)
        {
            string message = "";

            KSBikeEntities db = new KSBikeEntities();
            var regis = db.users
                .Where(m => m.email == email)
                .FirstOrDefault();
            user a = new user();
            if (regis == null)
            {
                var date1 = String.Format("{0:yyyy/MM/dd}", birthday);
                birthday = Convert.ToDateTime(date1);
                a.username = name;
                a.email = email;
                a.created_at = DateTime.Now;
                a.password = password;
                a.birthday = birthday;
                db.users.Add(a);
                db.SaveChanges();
                message = "ok";
            }
            else
            {
                message = "emaildobble";
            }
            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult MemberPage()
        {
            //KSBikeEntities db = new KSBikeEntities();
            MemberPageViewModel MPVM = new MemberPageViewModel();
            KSBikeEntities db = new KSBikeEntities();
            var userid = Convert.ToInt32(Session["id"]);
            MPVM.User = db.users.Where(e => e.id == userid).FirstOrDefault();
            MPVM.private_Routes = db.private_route.Where(p => p.user_id == userid).ToList();
            //MPVM.private_Routes_For_Ones = db.private_route.Where(p => p.user_id == userid).ToList();
            MPVM.userFavoritesByOF = (from o in db.Homes
                                      join f in db.user_favorite on o.id equals f.official_route_id
                                      select new UserFavorite
                                      {
                                          user_fav_id = f.user_fav_id,//哪一位使用者點了喜歡
                                          fav_id = f.id,//我的最愛的ID
                                          official_route_id = o.id,//網站路線的ID
                                          official_By_Home_title = o.hname//網站路線的標題
                                      }).Take(5).ToList();
            MPVM.userFavoritesByPR = (from p in db.private_route
                                      join f in db.user_favorite on p.id equals f.private_route_id
                                      select new UserFavorite
                                      {
                                          user_fav_id = f.user_fav_id,//哪一位使用者點了喜歡
                                          fav_id = f.id,//我的最愛的ID
                                          private_route_id = f.private_route_id,//私人路線的ID
                                          article_title = p.article_title,//私人路線的標題
                                      }).Take(5).ToList();


            //var mem = db.users
            //    .Where(m => m.id == id)
            //    .FirstOrDefault();


            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(MPVM);
            }

        }
        public ActionResult routeEditPage() {
            if (Session["id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult routeEditPage(string article_title, string article_context, DateTime datetime)
        {
            var userId = Convert.ToInt32(Session["id"]);
            var editDate = String.Format("{0:yyyy/MM/dd}", datetime);
            string message = "";
            KSBikeEntities db = new KSBikeEntities();
            private_route PR = new private_route();
            var article_title_exist = db.private_route
                .Where(ed => ed.article_title == article_title)
                .FirstOrDefault();

            if (Session["id"] != null)
            {
                string photoName = "";
                if (Request.Files.Count != 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        photoName = Guid.NewGuid().ToString() + ".jpg";
                        var path = Path.Combine(Server.MapPath("../../imageForPrivateRoute/"), photoName);
                    }
                }//這裡要改
                if (article_title_exist != null)
                {
                    message = "titleAndUserdouble";
                }
                else
                {
                    PR.article_title = article_title;
                    PR.user_id = userId;
                    PR.datetime = Convert.ToDateTime(editDate);
                    PR.article_img_info = photoName;
                    PR.article_context = article_context;
                    message = "ok";
                    db.private_route.Add(PR);
                    db.SaveChanges();
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }
        public ActionResult EditbyPrivateroute(int? id)
        {
            if(Session["id"]!=null)
            {
                KSBikeEntities db = new KSBikeEntities();
                var get = db.private_route
                    .Where(g => g.id == id)
                    .FirstOrDefault();
                return View(get);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditbyPrivateroute(private_route pr)
        {

            KSBikeEntities db = new KSBikeEntities();
            var get = db.private_route
                .Where(g => g.id == pr.id)
                .FirstOrDefault();
            var getdouble = db.private_route
                .Where(g => g.article_title == pr.article_title)
                .FirstOrDefault();
            if(get !=null)
            {
                if (getdouble == null)
                {
                    if (!ModelState.IsValidField("article_title"))
                    {
                        var emptyValue = new ValueProviderResult(
                            string.Empty,
                            string.Empty,
                            System.Globalization.CultureInfo.CurrentCulture);
                        ModelState.SetModelValue(
                            "article_title",
                            emptyValue);
                    }
                    get.article_title = pr.article_title;
                    get.article_context = pr.article_context;
                    db.SaveChanges();
                }
                else
                {
                    
                    ModelState.AddModelError("article_title", "您輸入的標題已存在，請重新輸入");
                    return View();
                }
            }
            return RedirectToAction("MemberPage","Member");
        }

        //public ActionResult editByPrivateRoute(int? id)
        //{
        //    KSBikeEntities db = new KSBikeEntities();
        //    var get = db.private_route
        //        .Where(g => g.id == id)
        //        .FirstOrDefault();
        //    return View(get);
        //}


        public ActionResult DeleteByPrivate(int id)
        {
            KSBikeEntities db = new KSBikeEntities();
            var Delete = db.private_route.FirstOrDefault(d => d.id == id);
            if(Delete!=null)
            {
                db.private_route.Remove(Delete);
                db.SaveChanges();
            }
            
            return RedirectToAction("MemberPage", "Member");
        }
        public ActionResult DeleteByoffical_fav(int id)
        {
            KSBikeEntities db = new KSBikeEntities();
            var Delete = db.user_favorite.FirstOrDefault(d => d.official_route_id == id);
            if (Delete != null)
            {
                db.user_favorite.Remove(Delete);
                db.SaveChanges();
            }

            return RedirectToAction("MemberPage", "Member");
        }
        public ActionResult DeleteByPrivate_fav(int id)
        {
            KSBikeEntities db = new KSBikeEntities();
            var Delete = db.user_favorite.FirstOrDefault(d => d.private_route_id == id);
            if (Delete != null)
            {
                db.user_favorite.Remove(Delete);
                db.SaveChanges();
            }

            return RedirectToAction("MemberPage", "Member");
        }

    }
}