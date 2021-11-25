using bike_web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class popularRoute
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

        //mia
        public user_favorite checkFav { get; set; }

    }

    public class popularArticle
    {
        public int p_userID { get; set; }
        public string p_artitle_title { get; set; }
        [DisplayName("文章名稱")]
        public string p_artitle_img_info { get; set; }
        [DisplayName("文章照片")]
        public DateTime p_datetime { get; set; }
        [DisplayName("po文日期")]
        public double? p_seen_num { get; set; }
        [DisplayName("觀看次數")]
        public double? p_star_num { get; set; }
        [DisplayName("星星評分")]
        public string u_username { get; set; }
        [DisplayName("文章投稿人")]
        public int u_userID { get; set; }
    }

    //mia
    public class userFav {
        public int id { get; set; }
        public int user_fav_id { get; set; }
        public int private_route_id { get; set; }
        public int official_route_id { get; set; }
        public System.DateTime datetime { get; set; }

    }


    public class hashtagNamandSum
    {
        public string Name { get; set; }
        public int Number { get; set; }


    }


    public class HomeViewModel
    {
        public List<popularRoute> offcialPopularRoute { get; set; }
        public List<popularRoute> offcialAllRoute { get; set; }
        public List<popularArticle> privatePopularArticle { get; set; }


        //mia
        public List<userFav> checkFav { get; set; }


        //德威
        public List<hashtagNamandSum> hashtagNamandSum { get; set; }

    }
}