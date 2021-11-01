using bike_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class MemberPageViewModel
    {
        public user User { get; set; }

        public List<private_route> private_Routes { get; set; }  
        public List<Home> official_Route_BY_Home{get; set;}
        public List<UserFavorite> userFavoritesByOF { get; set; }
        public List<UserFavorite> userFavoritesByPR { get; set; }
        
    }
}