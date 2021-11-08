using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{

        public class routeInfo
        {
            //public official_route_data route { get; set; }

            //        public OfficialVM() { this.route = new official_route_data() ;   }

            //        public int id { get { return this.route.id; } set {this.route.id=value; } }
            public int id { get; set; }

         
            public int home_id { get; set; }


           
            public string official_data_catalog { get; set; }


           
            public string official_data_img { get; set; }

           
            public string official_data_img_info { get; set; }


          
            public string official_data_content { get; set; }

        }

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

        public class routeHome
        {
            public int id { get; set; }

            public string hname { get; set; }

            public string hdescription { get; set; }
            public string hrank { get; set; }

            public decimal hdistance { get; set; }

            public string himg { get; set; }

        }

  
        

        public class OfficialVM
        {
            public List<routeInfo> RouteInfo { get; set; }

            public List<routeComment> RouteComment { get; set; }

            public List<routeHome> RouteHome { get; set; }           

        }
    
}