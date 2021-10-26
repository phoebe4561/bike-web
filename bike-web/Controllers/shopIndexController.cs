using System;
using System.Collections.Generic;
using bike_web.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bike_web.Models;
using System.Web.Security;

namespace bike_web.Controllers
{
    public class shopIndexController : Controller
    {
        KSBikeEntities db = new KSBikeEntities();
        // GET: shopIndex
        public ActionResult shopIndex()
        {
            var Products = db.products.OrderByDescending(m => m.id).ToList();

            return View(Products);
        }

        public ActionResult shoppingCar()
        {
            var Sid = Convert.ToInt32(Session["ID"]);
            var myWish = db.orders.Where(m => m.user_id == Sid && m.order_pay == "未付款").ToList();
            return View(myWish);
        }

        public ActionResult myorder()
        {
            var Oid = Convert.ToInt32(Session["ID"]);
            var orderDetail = db.orders.Where(m => m.user_id == Oid && m.order_pay == "已付款").ToList();
            return View(orderDetail);
        }
    }
}