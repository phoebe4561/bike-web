using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class AddCartViewModel
    {
        public int AddId { get; set; }
        public int AddCount { get; set; }
        public string AddGoDate { get; set; }

        public string AddProName { get; set; }
        public int OrderId { get; set; }
    }
}