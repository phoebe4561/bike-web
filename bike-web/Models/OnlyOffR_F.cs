using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bike_web.Models
{
    public class OnlyOffR_F
    {

        public string refreshAvgStar(int articleID) {
            string result = "0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=KSBike;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand($"select AVG(Cast(user_give_star_num as Float)) as 'avgStar' from official_route_comment where article_title_id = {articleID}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            { result = reader["avgStar"].ToString(); }
            con.Close();
            if (result.Length > 3) { result = result.Substring(0, 3); }
            return result;
        }

        public void updateAvgStar(int articleID,string avgStar)
        {            
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=KSBike;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand($"update official_route_comment set all_star_summary=${avgStar} where article_title_id = {articleID}", con);
            cmd.ExecuteNonQuery();
            con.Close();          
        }

        public List<routeComment> QrouteComment(int articleID)
    {
        KSBikeEntities db = new KSBikeEntities();
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


    }






}