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
        public ActionResult Login()
        {

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Register(string Name,string account,string password,DateTime date)
        {
            KSBikeEntities db = new KSBikeEntities();
            user a = new user();
            var date1 = String.Format("{0:yyyy/MM/dd}",date);
            date = Convert.ToDateTime(date1);
            a.username = Name;
            a.email = account;
            a.password = password;
            a.birthday = date;
            db.users.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}