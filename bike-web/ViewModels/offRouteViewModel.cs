using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static bike_web.ViewModels.offRouteViewModel;

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


        //mia
        public List<routeComment> RouteComment { get; set; }
        public List<userFav> checkFav { get; set; }

        public List<routeHashtag> RouteHashtag { get; set; }
        public List<string> hashtagName { get; set; }
    }

//mia
public class routeComment
        {
            public int id { get; set; }
            public int article_title_id { get; set; }
            public int comment_user_id { get; set; }
            public Nullable<decimal> all_star_summary { get; set; }
            public int user_give_star_num { get; set; }
            public string comment { get; set; }
            public Nullable<System.DateTime> datetime { get; set; }

            public string userName { get; set; }

        }


    public class routeHashtag
    {
        public int id { get; set; }
        public int official_route_id { get; set; }
        public int private_route_id { get; set; }
        public string hashtag_name { get; set; }


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


        //mia : hasgtag

        public int official_route_id { get; set; }
        public int private_route_id { get; set; }
        public string hashtag_name { get; set; }

    }





}