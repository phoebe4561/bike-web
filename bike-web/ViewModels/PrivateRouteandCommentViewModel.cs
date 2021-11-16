using bike_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class thePrivateRouteData
    {
        public int p_ID { get; set; }
        public string p_artitle_title { get; set; }
        //[DisplayName("文章名稱")]
        public string p_artitle_img_info { get; set; }
        //[DisplayName("文章照片")]
        public DateTime p_datetime { get; set; }
        //[DisplayName("po文日期")]
        public double? p_seen_num { get; set; }
        //[DisplayName("觀看次數")]
        public double? p_star_num_sum { get; set; }
        //[DisplayName("星星總數")]

        public double? p_sum_people_give_star_num { get; set; }
        //[DisplayName("星星總評分人數")]
        public string p_content { get; set; }
        //[DisplayName("文章內容")]
        //public double? p_star { get; set; }
        //[DisplayName("星星評分")]
        public string u_username { get; set; }
        //[DisplayName("文章投稿人")]
        public int u_userID { get; set; }
        public int p_fav { get; set; }

    }

    public class commentWithName
    {
        public int id { get; set; }
        public string comment_user_name{ get; set; }
        public int user_give_star_num { get; set; }
        public string comment { get; set; }

        public DateTime datetime { get; set; }
    }

    public class PrivateRouteandCommentViewModel
    {
        public thePrivateRouteData 熱門文章 { get; set; }
        public List<commentWithName> 留言資訊 { get; set; }

        public List<string> HashTags { get; set; }
    }

    
}