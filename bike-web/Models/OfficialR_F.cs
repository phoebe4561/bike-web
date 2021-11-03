using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bike_web.Models
{
    public class OfficialR_F
    {
        KSBikeEntities db = new KSBikeEntities();

        public List<routeInfo> QrouteInfo(int? articleID)
        {
            if (articleID == null) { articleID = 1; }
            var 路線資訊 = (from info in db.official_route_data
                        where info.home_id == articleID
                        select new routeInfo
                        {
                            id = info.id,
                            home_id = info.home_id,
                            official_data_catalog = info.official_data_catalog,
                            official_data_img = info.official_data_img,
                            official_data_img_info = info.official_data_img_info,
                            official_data_content = info.official_data_content


                        }).OrderBy(by => by.id).ToList();

            return 路線資訊;
        }

        public List<routeComment> QrouteComment(int articleID)
        {
            var 路線評論 = (from info in db.official_route_comment
                        join cmt in db.users on info.comment_user_id equals cmt.id
                        where info.article_title_id == articleID
                        select new routeComment
                        {
                            id = info.id,
                            article_title_id = info.article_title_id,
                            comment_user_id = info.comment_user_id,
                            all_star_summary = info.all_star_summary,
                            user_give_star_num = info.user_give_star_num,
                            comment = info.comment,
                            datetime = info.datetime,
                            userName = cmt.username

                        }).OrderByDescending(by => by.datetime).ToList();

            return 路線評論;
        }

        public List<routeHome> QrouteHome(int articleID)
        {
            var 路線 = (from info in db.Homes
                      where info.id == articleID
                      select new routeHome
                      {
                          id = info.id,
                          hname = info.hname,
                          hdescription = info.hdescription,
                          hrank = info.hrank,
                          hdistance = (decimal)info.hdistance,
                          himg = info.himg

                      }).OrderBy(by => by.id).ToList();

            return 路線;
        }

    }
}