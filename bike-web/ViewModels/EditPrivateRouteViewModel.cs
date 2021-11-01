using bike_web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class EditPrivateRouteViewModel
    {
        public string article_title { get; set; }
        public string article_context { get; set; }
        public DateTime datetime { get; set; }
        public byte[] article_img_info { get; set; }

    }
}