using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class shopIndexController : Controller
    {
        //產品選單頁面
        KSBikeEntities db = new KSBikeEntities();
        // GET: shopIndex
        public ActionResult shopIndex()
        {
            var Products = db.products.OrderByDescending(m => m.id).ToList();
            List<CProductViewModel> list = new List<CProductViewModel>();

            foreach (product p in Products)
                list.Add(new CProductViewModel() { Product = p });
            return View(list);
        }

        //加入購物車頁面
        public ActionResult AddToCart(int id)
        {
                product prod = db.products.FirstOrDefault(p => p.id == id);
                if (prod == null)
                    return RedirectToAction("shopIndex");
                return View(prod);
          
        }
        [HttpPost]
        public ActionResult AddToCart(AddCartViewModel vModel)
        {
            var Sid = Convert.ToInt32(Session["ID"]);
            var Sname = Session["username"].ToString();
            
                product prod = db.products.FirstOrDefault(p => p.id == vModel.AddId);

                var totalPrice = (int)(prod.product_price * vModel.AddCount);
                if (prod != null)
                {
                    order cart = new order();

                    cart.user_id = Sid;
                    cart.user_name = Sname;
                    cart.product_name = prod.product_name;
                    cart.product_img = prod.product_img;
                    cart.product_price = prod.product_price;
                    cart.order_num = vModel.AddCount;
                    cart.order_price_total = totalPrice;
                    cart.order_date = DateTime.Now.ToString("yyyy-MM-dd");
                    cart.go_date = vModel.AddGoDate.ToString();
                    cart.order_pay = "未付款";
                    db.orders.Add(cart);
                    db.SaveChanges();
                                    
                }
            return RedirectToAction("shoppingCar");
           
        }

        //購物車內容
        public ActionResult shoppingCar()
        {
            var Sid = Convert.ToInt32(Session["ID"]);
            var myWish = db.orders.Where(m => m.user_id == Sid && m.order_pay == "未付款").ToList();
            return View(myWish);
        }

        //訂單內容
        public ActionResult myorder()
        {
            var Oid = Convert.ToInt32(Session["ID"]);
            var orderDetail = db.orders.Where(m => m.user_id == Oid && m.order_pay == "已信用卡付款").ToList();
            return View(orderDetail);
        }

        //刪除訂單
        public ActionResult DeletCart(int id)
        {
            order Ord = db.orders.FirstOrDefault(p => p.id == id);
            if (Ord != null)
            {
                db.orders.Remove(Ord);
                db.SaveChanges();
            }
            return RedirectToAction("shoppingCar");


        }

        public ActionResult DeletOrder(int id)
        {
            order Ord = db.orders.FirstOrDefault(p => p.id == id);
            if (Ord != null)
            {
                db.orders.Remove(Ord);
                db.SaveChanges();
            }
            return RedirectToAction("myorder");


        }


        //結帳頁面
        public ActionResult GoBuy(int id)
        {
            var Sid = Convert.ToInt32(Session["ID"]);
            order Ord = db.orders.FirstOrDefault(p => p.id == id);
            if (Ord == null)
                return RedirectToAction("shopIndex");
            return View(Ord);

        }
        [HttpPost]
      
        public ActionResult GoBuy(order editOrder)
        {
            string message = "";
            order Ord = db.orders.FirstOrDefault(p => p.id == editOrder.id);

            if (Ord != null && Ord.order_pay == "未付款")
            {
               
                SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=KSBike;Integrated Security=True";
            con.Open();
            string strSQL = "update orders set order_pay=@K_order_pay,order_num=@Num, order_price_total=@SumPrice, go_date=@Date where id=@Id ;";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            string pay = "已信用卡付款";
            int totalPrice = (int)(Ord.product_price * editOrder.AddCount);

            cmd.Parameters.AddWithValue("@Date", editOrder.AddGoDate.ToString());
            cmd.Parameters.AddWithValue("@Num", editOrder.AddCount);
            cmd.Parameters.AddWithValue("@SumPrice", totalPrice);
            cmd.Parameters.AddWithValue("@K_order_pay", pay);
            cmd.Parameters.AddWithValue("@Id", Ord.id);
           
            int rows = cmd.ExecuteNonQuery();
             message = "ok";
            con.Close();
           
            }
            return RedirectToAction("myorder");

           
        }

       
    }
}

