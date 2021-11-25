using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class MemberController : Controller
    {

        // GET: Member
        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            string message = "";
            KSBikeEntities db = new KSBikeEntities();
            var login = db.users
                .Where(m => m.email == email && m.password == password)
                .FirstOrDefault();
            if (login == null)
            {
                message = "fail";
            }
            else
            {
                Session["username"] = login.username;
                Session["id"] = login.id;
                Session["email"] = login.email;
                message = "pass";
            }
            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult Register(string name, string email, string password, DateTime birthday)
        {
            string message = "";

            KSBikeEntities db = new KSBikeEntities();
            var regis = db.users
                .Where(m => m.email == email)
                .FirstOrDefault();
            user a = new user();
            if (regis == null)
            {
                var date1 = String.Format("{0:yyyy/MM/dd}", birthday);
                birthday = Convert.ToDateTime(date1);
                a.username = name;
                a.email = email;
                a.created_at = DateTime.Now;
                a.password = password;
                a.birthday = birthday;
                db.users.Add(a);
                db.SaveChanges();
                message = "ok";
            }
            else
            {
                message = "emaildobble";
            }
            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult MemberPage()
        {
            //KSBikeEntities db = new KSBikeEntities();
            MemberPageViewModel MPVM = new MemberPageViewModel();
            KSBikeEntities db = new KSBikeEntities();
            var userid = Convert.ToInt32(Session["id"]);
            MPVM.User = db.users.Where(e => e.id == userid).FirstOrDefault();
            MPVM.userFavoritesByOF = (from o in db.Homes
                                      where o.id > 1
                                      //let f = db.user_favorite.Where(fav => o.id == fav.official_route_id).FirstOrDefault()
                                      join f in db.user_favorite on o.id equals f.official_route_id
                                      where f.user_fav_id == userid
                                      select new UserFavorite
                                      {
                                          user_fav_id = f.user_fav_id,//哪一位使用者點了喜歡
                                          fav_id = f.id,//我的最愛的ID
                                          official_route_id = o.id,//網站路線的ID
                                          official_By_Home_title = o.hname//網站路線的標題

                                      })./*Take(5).*/ToList();


            MPVM.private_Routes = db.private_route.Where(p => p.user_id == userid).ToList();    
            MPVM.userFavoritesByPR = (from p in db.private_route
                                      join f in db.user_favorite on p.id equals f.private_route_id
                                      select new UserFavorite
                                      {
                                          user_fav_id = f.user_fav_id,//哪一位使用者點了喜歡
                                          fav_id = f.id,//我的最愛的ID
                                          private_route_id = f.private_route_id,//私人路線的ID
                                          article_title = p.article_title,//私人路線的標題
                                      }).Take(5).ToList();


            //var mem = db.users
            //    .Where(m => m.id == id)
            //    .FirstOrDefault();


            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(MPVM);
            }

        }
        public ActionResult routeEditPage()
        {
            if (Session["id"] != null)
            {

                KSBikeEntities db = new KSBikeEntities();
                EditPrivateRouteViewModel x = new EditPrivateRouteViewModel();
                x.hashtags = db.hashtags.Select(b => b.hashtag_name).Distinct().ToList();

                return View(x);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult routeEditPage(EditPrivateRouteViewModel editPrivateRouteViewModel)
        {
            //處理圖片
            string fileName = Path.GetFileNameWithoutExtension(editPrivateRouteViewModel.ImageFile.FileName);
            string extension = Path.GetExtension(editPrivateRouteViewModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            editPrivateRouteViewModel.p_artitle_img_info = "~/postedImage/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/postedImage/"), fileName);
            editPrivateRouteViewModel.ImageFile.SaveAs(fileName);


            //處理編輯器




            SqlDateTime myDateTime = DateTime.Now;
            int privateRouteId = 0;
            KSBikeEntities db = new KSBikeEntities();
            private_route x = new private_route();


            try
            {
                x.fav_num = 0;
                x.seem_num = 0;
                x.star_num_sum = 0;
                x.sum_people_give_star = 0;
                x.article_img_id = 0;//最好取消
                //試試看
                x.private_route_comment = null;



                x.user_id = Convert.ToInt32(Session["ID"]);
                x.article_title = editPrivateRouteViewModel.p_artitle_title;
                System.Diagnostics.Debug.WriteLine(x.article_title);


                x.datetime = (DateTime)myDateTime;
                System.Diagnostics.Debug.WriteLine(x.datetime);
                x.article_context = editPrivateRouteViewModel.p_content;
                System.Diagnostics.Debug.WriteLine(x.article_context);
                x.article_img_info = editPrivateRouteViewModel.p_artitle_img_info;
                System.Diagnostics.Debug.WriteLine(x.article_img_info);



                db.private_route.Add(x);
                db.SaveChanges();



            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }



            //x.fav_num = 0;
            //x.seem_num = 0;
            //x.star_num_sum = 0;
            //x.sum_people_give_star = 0;
            //x.article_img_id = 0;//最好取消
            ////試試看
            //x.private_route_comment = null;



            //x.user_id = Convert.ToInt32(Session["ID"]);
            //x.article_title = editPrivateRouteViewModel.p_artitle_title;
            //x.datetime = (DateTime)myDateTime;
            //x.article_context = editPrivateRouteViewModel.p_content;
            //x.article_img_info = editPrivateRouteViewModel.p_artitle_img_info;




            //db.private_route.Add(x);
            //db.SaveChanges();

            privateRouteId = x.id;


            if (editPrivateRouteViewModel.HashTag != null)
            {
                string hashTag = editPrivateRouteViewModel.HashTag.Trim();
                string[] hashtagList = hashTag.Split(' ');
                foreach (string item in hashtagList)
                {



                    try
                    {
                        hashtag y = new hashtag();
                        y.official_route_id = 0;
                        y.private_route_id = privateRouteId;
                        y.hashtag_name = item;
                        db.hashtags.Add(y);
                        db.SaveChanges();


                    }
                    catch
                    {
                        throw;
                    }





                }






            }






            ModelState.Clear();










            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public JsonResult UploadFiles(HttpPostedFileBase uploadFiles)
        {
            string returnImagePath = string.Empty;
            string filename, extension,/* imageName,*/ imageSavePath;
            System.Diagnostics.Debug.WriteLine(uploadFiles);
            if (uploadFiles.ContentLength > 0)
            {
                filename = Path.GetFileNameWithoutExtension(uploadFiles.FileName);
                extension = Path.GetExtension(uploadFiles.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                //editPrivateRouteViewModel.p_artitle_img_info = "~/postedImage/" + filename;
                imageSavePath = Path.Combine(Server.MapPath("~/postedImage/"), filename);
                uploadFiles.SaveAs(imageSavePath);
                returnImagePath = "/postedImage/" + filename;




                //imageName = filename + DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                //imageSavePath = Server.MapPath("/postedImage/") + imageName + extension;
                //uploadFiles.SaveAs(imageSavePath);


                //returnImagePath = "/postedImage/" + imageName + extension;
            }
            return Json(Convert.ToString(returnImagePath), JsonRequestBehavior.AllowGet);





        }


        ////[HttpGet]
        ////public ActionResult SearchHint(string key)
        ////{
        ////    KSBikeEntities db = new KSBikeEntities();
        ////    List<string> hints = db.hashtags.Where(s => s.hashtag_name.Contains(key)).Distinct().Take(5).Select(x =>x.hashtag_name).ToList();
        ////    return Json(hints, JsonRequestBehavior.AllowGet);

        ////}


        //[HttpPost]
        //public JsonResult FileUpload( HttpPostedFileBase ImageFile)
        //{

        //    String path = "";
        //    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
        //    string extension = Path.GetExtension(ImageFile.FileName);
        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //    //editPrivateRouteViewModel.p_artitle_img_info = "~/postedImage/" + fileName;
        //    fileName = Path.Combine(Server.MapPath("~/postedImage/"), fileName);
        //    ImageFile.SaveAs(fileName);
        //    path += "~/postedImage/" + fileName;

        //    return Json(new { path }, JsonRequestBehavior.AllowGet);



        //}
        [HttpPost]
        public ActionResult DeleteByoffical_fav(int articleID)
        {

            string message = "";
            int userID = Convert.ToInt32(Session["id"]);
            KSBikeEntities db = new KSBikeEntities();

            var myfav = db.user_favorite.Where(m => m.user_fav_id == userID && m.official_route_id == articleID).FirstOrDefault();

            user_favorite x = new user_favorite();
            SqlDateTime myDateTime = DateTime.Now;
            x.datetime = (DateTime)myDateTime;
            x.official_route_id = 0;
            db.SaveChanges();
            message = "delete";

            return Json(message, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult Delete(int? id) 
        {

            int Sid = Convert.ToInt32(Session["id"]);
            KSBikeEntities db = new KSBikeEntities();

            var myfav = db.user_favorite.Where(m => m.user_fav_id == Sid && m.private_route_id == id).FirstOrDefault();



            db.user_favorite.Remove(myfav);
            db.SaveChanges();




            return RedirectToAction("Index", "Home");

        }

        

    }

}