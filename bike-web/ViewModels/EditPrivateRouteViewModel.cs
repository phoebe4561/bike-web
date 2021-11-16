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
       
        public string p_artitle_title { get; set; }
       
       
        

       
        public string p_content { get; set; }
        



        public string p_artitle_img_info { get; set; }
        
        public HttpPostedFileBase ImageFile { get; set; }
        
        public string HashTag { get; set; }

        public List<string> hashtags { get; set; }

    }
}