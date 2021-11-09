using System;
using bike_web.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace bike_web.ViewModels
{
    public class CProductViewModel
    {
        public product Product { get; set; }
        public HttpPostedFileBase image { get; set; }
        public CProductViewModel()
        {
            this.Product = new product();
        }
        public int id
        {
            get { return this.Product.id; }
            set { this.Product.id = value; }
        }

        [Required(ErrorMessage = "品名不可為空白")]
        [DisplayName("品名")]
        public string product_name
        {
            get { return this.Product.product_name; }
            set { this.Product.product_name = value; }
        }

        [DisplayName("優惠價")]
        public int product_price
        {
            get { return this.Product.product_price; }
            set { this.Product.product_price = value; }
        }

        [DisplayName("圖示")]
        public string product_img
        {
            get { return this.Product.product_img; }
            set { this.Product.product_img = value; }
        }

        [DisplayName("名額")]
        public Nullable<int> product_num { get; set; }
    }
}