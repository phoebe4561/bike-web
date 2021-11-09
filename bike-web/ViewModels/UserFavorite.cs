using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class UserFavorite
    {
        public int fav_id { get; set; }
        public int user_fav_id { get; set; }
        public int official_route_id { get; set; }
        public DateTime datetime { get; set; }
        public string official_By_Home_title { get; set; }
        public int private_route_id { get; set; }
        public string article_title { get; set; }

        
    }
}