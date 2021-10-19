using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
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
                Session["id"] = login.id;
                Session["email"] = login.email;
                Session["password"] = login.password;
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
                .Where(m => m.email == email && m.password == password)
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
    }
}