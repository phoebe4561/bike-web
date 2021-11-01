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
        public ActionResult Login(string email,string password)
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
        public ActionResult Register(string name,string email,string password,DateTime birthday)
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
            KSBikeEntities db = new KSBikeEntities();
            var id = Convert.ToInt32(Session["id"]);
            var mem = db.users
                .Where(m => m.id == id)
                .FirstOrDefault();
            if (mem == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(mem);
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
        public ActionResult routeEditPage(string article_title,string article_context,DateTime datetime)
        {
            var userId = Convert.ToInt32(Session["id"]);
            var editDate = String.Format("{0:yyyy/MM/dd}", datetime);
            string message = "";
            KSBikeEntities db = new KSBikeEntities();
            private_route PR = new private_route();
            var article_title_exist=db.private_route
                .Where(ed => ed.article_title == article_title)
                .FirstOrDefault();

            if (Session["id"] != null)
            {
                string photoName = "";
                if(Request.Files.Count!=0)
                {
                    for(int i = 0; i < Request.Files.Count; i++)
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
    }
}