using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class offRouteViewModel
    {
        public List<offRoute> offcialAllRoute { get; set; }
        public List<offRoute> offcialLowRoute { get; set; }
        public List<offRoute> offcialMiddleRoute { get; set; }
        public List<offRoute> offcialHighRoute { get; set; }
        public string searchKeyword {get; set;}
        public string searchRank {get; set;}
        public double searchDistance { get; set; }

    }

    public class offRoute
    {
        public int h_ID { get; set; }
        public string h_name { get; set; }
        [DisplayName("路線名稱")]
        public string h_description { get; set; }
        [DisplayName("路線介紹")]
        public string h_rank { get; set; }
        [DisplayName("路線分級")]
        public double? h_distance { get; set; }
        [DisplayName("路線距離")]
        public string h_img { get; set; }
        [DisplayName("路線照片")]
        public double? oc_allStar { get; set; }
        [DisplayName("星星評分")]
        public int oc_artitleTitleID { get; set; }
        public string od_official_data_catalog { get; set; }
        public string od_official_data_img { get; set; }
        public string od_official_data_img_info { get; set; }
        public string od_official_data_img_content { get; set; }
        public int od_homeID { get; set; }
        public int od_offRouteID { get; set; }

    }
}