using System;
using bike_web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class CShoppingCartViewModel
    {
        public product Product { get; set; }
        public int id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string product_name { get; set; }
        public string product_img { get; set; }
        public Nullable<int> product_price { get; set; }
        public Nullable<int> order_num { get; set; }
        public Nullable<int> order_price_total { get { return Convert.ToInt32(this.product_price * this.order_num); } }
        public string order_date { get; set; }
        public string go_date { get; set; }
       

       
    }
}