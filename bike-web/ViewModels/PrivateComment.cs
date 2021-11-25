using bike_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class PrivateComment
    {
       

        public int pagenum { get; set; }
        //捕捉總頁數
        public List<commentWithName> 留言資訊 { get; set; }
        //捕捉前5頁資訊

        public double star { get; set; }
        //捕捉新的星星數
        public int nowpage { get; set; }
        //現在的頁數
    }
}