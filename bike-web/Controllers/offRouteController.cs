using bike_web.Models;
using bike_web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bike_web.Controllers
{
    public class offRouteController : Controller
    {
        // GET: offRoute
        public ActionResult List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 所有路線 = (from h in db.Homes
                        where h.id >1
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();

            var 初級路線 = (from h in db.Homes
                        where h.id > 1
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        where h.hrank == "初級"
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();

            var 中級路線 = (from h in db.Homes
                        where h.id > 1
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        where h.hrank == "中級"
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();

            var 高級路線 = (from h in db.Homes
                        where h.id > 1
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        where h.hrank == "高級"
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();

            //mia
            int userID = Convert.ToInt32(Session["id"]);
            var 我的最愛 = (from fav in db.user_favorite
                        where fav.user_fav_id == userID && fav.official_route_id >1
                        select new userFav
                        {
                            id = fav.id,
                            official_route_id = fav.official_route_id
                        }).OrderBy(e => e.id).ToList();

           


            offRouteViewModel vm = new offRouteViewModel()
            {
                offcialAllRoute = 所有路線,
                offcialLowRoute = 初級路線,
                offcialMiddleRoute = 中級路線,
                offcialHighRoute = 高級路線,
                //mia
                checkFav = 我的最愛
            };

            return View(vm);
        }

        public ActionResult 初級List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 初級路線 = (from h in db.Homes 
                        where h.hrank == "初級"
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()                        
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();
            //mia
            int userID = Convert.ToInt32(Session["id"]);
            var 我的最愛 = (from fav in db.user_favorite
                        where fav.user_fav_id == userID && fav.official_route_id >1
                        select new userFav
                        {
                            id = fav.id,
                            official_route_id = fav.official_route_id
                        }).OrderBy(e => e.id).ToList();

            offRouteViewModel orvm = new offRouteViewModel()
            {
                offcialAllRoute = 初級路線,
                 //mia
                checkFav = 我的最愛
            };

            return View(orvm);
        }

        [HttpPost]
        public ActionResult 初級List(offRouteViewModel vm)
        {
            KSBikeEntities db = new KSBikeEntities();
            offRouteViewModel search = new offRouteViewModel();
            search.searchKeyword = vm.searchKeyword;
            search.searchRank = vm.searchRank;
            search.searchDistance = vm.searchDistance;
            if (search.searchRank == "初級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "初級" && h.hdistance >1
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "初級" && h.hdistance < 10 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "初級" && h.hdistance >= 10 && h.hdistance <= 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            {
                                var 路線key = (from h in db.Homes
                                             let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                             where h.hrank == "初級" && h.hdistance >= 10 && h.hdistance <= 20 && h.hname.Contains(search.searchKeyword)
                                             select new offRoute
                                             {
                                                 h_ID = h.id,
                                                 h_description = h.hdescription,
                                                 h_name = h.hname,
                                                 h_distance = (double?)h.hdistance,
                                                 h_rank = h.hrank,
                                                 h_img = h.himg,
                                                 oc_artitleTitleID = oc.article_title_id,
                                                 oc_allStar = (double?)oc.all_star_summary,
                                             }).ToList();
                                ofvm.offcialAllRoute = 路線key;
                                return View(ofvm);
                            }
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else
                {
                    return RedirectToAction("noResult", "offRoute");

                }
            }
            else if (search.searchRank == "中級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "中級" && h.hdistance < 10
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "中級" && h.hdistance < 10 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "中級" && h.hdistance >= 10 && h.hdistance <= 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "中級" && h.hdistance >= 10 && h.hdistance <= 20 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else
                {
                    return RedirectToAction("noResult", "offRoute");

                }
            }
            else if (search.searchRank == "高級")
            {
                if (search.searchDistance == 2)
                {
                    return RedirectToAction("noResult", "offRoute");
                }
                else if (search.searchDistance == 3)
                {
                    return RedirectToAction("noResult", "offRoute");
                }
                else
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "高級" && h.hdistance > 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            {
                                var 路線key = (from h in db.Homes
                                             let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                             where h.hrank == "高級" && h.hdistance > 20 && h.hname.Contains(search.searchKeyword)
                                             select new offRoute
                                             {
                                                 h_ID = h.id,
                                                 h_description = h.hdescription,
                                                 h_name = h.hname,
                                                 h_distance = (double?)h.hdistance,
                                                 h_rank = h.hrank,
                                                 h_img = h.himg,
                                                 oc_artitleTitleID = oc.article_title_id,
                                                 oc_allStar = (double?)oc.all_star_summary,
                                             }).ToList();
                                ofvm.offcialAllRoute = 路線key;
                                return View(ofvm);
                            }
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
            }
            else
            {
                var 路線 = (from h in db.Homes
                          let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                          where h.hname.Contains(search.searchKeyword)
                          select new offRoute
                          {
                              h_ID = h.id,
                              h_description = h.hdescription,
                              h_name = h.hname,
                              h_distance = (double?)h.hdistance,
                              h_rank = h.hrank,
                              h_img = h.himg,
                              oc_artitleTitleID = oc.article_title_id,
                              oc_allStar = (double?)oc.all_star_summary,
                          }).ToList();
                offRouteViewModel ofvm = new offRouteViewModel()
                {
                    offcialAllRoute = 路線,
                };

                return View(ofvm);
            }
        }

        public ActionResult 中級List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 中級路線 = (from h in db.Homes
                        where h.id > 1
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        where h.hrank == "中級"
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();

            //mia
            int userID = Convert.ToInt32(Session["id"]);
            var 我的最愛 = (from fav in db.user_favorite
                        where fav.user_fav_id == userID && fav.official_route_id >1
                        select new userFav
                        {
                            id = fav.id,
                            official_route_id = fav.official_route_id
                        }).OrderBy(e => e.id).ToList();

            offRouteViewModel orvm = new offRouteViewModel()
            {
                offcialAllRoute = 中級路線,
                 //mia
                checkFav = 我的最愛
            };

            return View(orvm);
        }

        [HttpPost]
        public ActionResult 中級List(offRouteViewModel vm)
        {
            KSBikeEntities db = new KSBikeEntities();
            offRouteViewModel search = new offRouteViewModel();
            search.searchKeyword = vm.searchKeyword;
            search.searchRank = vm.searchRank;
            search.searchDistance = vm.searchDistance;
            if (search.searchRank == "初級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "初級" && h.hdistance < 10
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "初級" && h.hdistance < 10 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "初級" && h.hdistance >= 10 && h.hdistance <= 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            {
                                var 路線key = (from h in db.Homes
                                             let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                             where h.hrank == "初級" && h.hdistance >= 10 && h.hdistance <= 20 && h.hname.Contains(search.searchKeyword)
                                             select new offRoute
                                             {
                                                 h_ID = h.id,
                                                 h_description = h.hdescription,
                                                 h_name = h.hname,
                                                 h_distance = (double?)h.hdistance,
                                                 h_rank = h.hrank,
                                                 h_img = h.himg,
                                                 oc_artitleTitleID = oc.article_title_id,
                                                 oc_allStar = (double?)oc.all_star_summary,
                                             }).ToList();
                                ofvm.offcialAllRoute = 路線key;
                                return View(ofvm);
                            }
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else
                {
                    return RedirectToAction("noResult", "offRoute");

                }
            }
            else if (search.searchRank == "中級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "中級" && h.hdistance < 10
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "中級" && h.hdistance < 10 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "中級" && h.hdistance >= 10 && h.hdistance <= 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "中級" && h.hdistance >= 10 && h.hdistance <= 20 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else
                {
                    return RedirectToAction("noResult", "offRoute");

                }
            }
            else if (search.searchRank == "高級")
            {
                if (search.searchDistance == 2)
                {
                    return RedirectToAction("noResult", "offRoute");
                }
                else if (search.searchDistance == 3)
                {
                    return RedirectToAction("noResult", "offRoute");
                }
                else
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "高級" && h.hdistance > 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            {
                                var 路線key = (from h in db.Homes
                                             let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                             where h.hrank == "高級" && h.hdistance > 20 && h.hname.Contains(search.searchKeyword)
                                             select new offRoute
                                             {
                                                 h_ID = h.id,
                                                 h_description = h.hdescription,
                                                 h_name = h.hname,
                                                 h_distance = (double?)h.hdistance,
                                                 h_rank = h.hrank,
                                                 h_img = h.himg,
                                                 oc_artitleTitleID = oc.article_title_id,
                                                 oc_allStar = (double?)oc.all_star_summary,
                                             }).ToList();
                                ofvm.offcialAllRoute = 路線key;
                                return View(ofvm);
                            }
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
            }
            else
            {
                var 路線 = (from h in db.Homes
                          let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                          where h.hname.Contains(search.searchKeyword)
                          select new offRoute
                          {
                              h_ID = h.id,
                              h_description = h.hdescription,
                              h_name = h.hname,
                              h_distance = (double?)h.hdistance,
                              h_rank = h.hrank,
                              h_img = h.himg,
                              oc_artitleTitleID = oc.article_title_id,
                              oc_allStar = (double?)oc.all_star_summary,
                          }).ToList();
                offRouteViewModel ofvm = new offRouteViewModel()
                {
                    offcialAllRoute = 路線,
                };

                return View(ofvm);
            }
        }

        public ActionResult 高級List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 高級路線 = (from h in db.Homes
                        where h.id > 1
                        //join oc in db.official_route_comment on h.id equals oc.article_title_id
                        let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                        where h.hrank == "高級"
                        select new offRoute
                        {
                            h_ID = h.id,
                            h_description = h.hdescription,
                            h_name = h.hname,
                            h_distance = (double?)h.hdistance,
                            h_rank = h.hrank,
                            h_img = h.himg,
                            oc_artitleTitleID = oc.article_title_id,
                            oc_allStar = (double?)oc.all_star_summary,
                        }).ToList();

            //mia
            int userID = Convert.ToInt32(Session["id"]);
            var 我的最愛 = (from fav in db.user_favorite
                        where fav.user_fav_id == userID && fav.official_route_id >1
                        select new userFav
                        {
                            id = fav.id,
                            official_route_id = fav.official_route_id
                        }).OrderBy(e => e.id).ToList();

            offRouteViewModel orvm = new offRouteViewModel()
            {
                offcialAllRoute = 高級路線,
                //mia
                checkFav = 我的最愛
            };

            return View(orvm);
        }

        [HttpPost]
        public ActionResult 高級List(offRouteViewModel vm)
        {
            KSBikeEntities db = new KSBikeEntities();
            offRouteViewModel search = new offRouteViewModel();
            search.searchKeyword = vm.searchKeyword;
            search.searchRank = vm.searchRank;
            search.searchDistance = vm.searchDistance;
            if (search.searchRank == "初級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "初級" && h.hdistance < 10
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "初級" && h.hdistance < 10 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "初級" && h.hdistance >= 10 && h.hdistance <= 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            {
                                var 路線key = (from h in db.Homes
                                             let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                             where h.hrank == "初級" && h.hdistance >= 10 && h.hdistance <= 20 && h.hname.Contains(search.searchKeyword)
                                             select new offRoute
                                             {
                                                 h_ID = h.id,
                                                 h_description = h.hdescription,
                                                 h_name = h.hname,
                                                 h_distance = (double?)h.hdistance,
                                                 h_rank = h.hrank,
                                                 h_img = h.himg,
                                                 oc_artitleTitleID = oc.article_title_id,
                                                 oc_allStar = (double?)oc.all_star_summary,
                                             }).ToList();
                                ofvm.offcialAllRoute = 路線key;
                                return View(ofvm);
                            }
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else
                {
                    return RedirectToAction("noResult", "offRoute");

                }
            }
            else if (search.searchRank == "中級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "中級" && h.hdistance < 10
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "中級" && h.hdistance < 10 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "中級" && h.hdistance >= 10 && h.hdistance <= 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            var 路線key = (from h in db.Homes
                                         let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                         where h.hrank == "中級" && h.hdistance >= 10 && h.hdistance <= 20 && h.hname.Contains(search.searchKeyword)
                                         select new offRoute
                                         {
                                             h_ID = h.id,
                                             h_description = h.hdescription,
                                             h_name = h.hname,
                                             h_distance = (double?)h.hdistance,
                                             h_rank = h.hrank,
                                             h_img = h.himg,
                                             oc_artitleTitleID = oc.article_title_id,
                                             oc_allStar = (double?)oc.all_star_summary,
                                         }).ToList();
                            ofvm.offcialAllRoute = 路線key;
                            return View(ofvm);
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
                else
                {
                    return RedirectToAction("noResult", "offRoute");

                }
            }
            else if (search.searchRank == "高級")
            {
                if (search.searchDistance == 2)
                {
                    return RedirectToAction("noResult", "offRoute");
                }
                else if (search.searchDistance == 3)
                {
                    return RedirectToAction("noResult", "offRoute");
                }
                else
                {
                    var 路線 = (from h in db.Homes
                              let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                              where h.hrank == "高級" && h.hdistance > 20
                              select new offRoute
                              {
                                  h_ID = h.id,
                                  h_description = h.hdescription,
                                  h_name = h.hname,
                                  h_distance = (double?)h.hdistance,
                                  h_rank = h.hrank,
                                  h_img = h.himg,
                                  oc_artitleTitleID = oc.article_title_id,
                                  oc_allStar = (double?)oc.all_star_summary,
                              }).ToList();
                    offRouteViewModel ofvm = new offRouteViewModel();
                    foreach (var item in 路線)
                    {
                        if (search.searchKeyword == null)
                        {
                            search.searchKeyword = "";
                        }
                        if (item.h_name.Contains(search.searchKeyword))
                        {
                            {
                                var 路線key = (from h in db.Homes
                                             let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                                             where h.hrank == "高級" && h.hdistance > 20 && h.hname.Contains(search.searchKeyword)
                                             select new offRoute
                                             {
                                                 h_ID = h.id,
                                                 h_description = h.hdescription,
                                                 h_name = h.hname,
                                                 h_distance = (double?)h.hdistance,
                                                 h_rank = h.hrank,
                                                 h_img = h.himg,
                                                 oc_artitleTitleID = oc.article_title_id,
                                                 oc_allStar = (double?)oc.all_star_summary,
                                             }).ToList();
                                ofvm.offcialAllRoute = 路線key;
                                return View(ofvm);
                            }
                        }
                        else
                        {
                            return RedirectToAction("noResult", "offRoute");
                        }
                    }
                    return View(ofvm);
                }
            }
            else
            {
                var 路線 = (from h in db.Homes
                          let oc = db.official_route_comment.Where(cmt => h.id == cmt.article_title_id).FirstOrDefault()
                          where h.hname.Contains(search.searchKeyword)
                          select new offRoute
                          {
                              h_ID = h.id,
                              h_description = h.hdescription,
                              h_name = h.hname,
                              h_distance = (double?)h.hdistance,
                              h_rank = h.hrank,
                              h_img = h.himg,
                              oc_artitleTitleID = oc.article_title_id,
                              oc_allStar = (double?)oc.all_star_summary,
                          }).ToList();
                offRouteViewModel ofvm = new offRouteViewModel()
                {
                    offcialAllRoute = 路線,
                };

                return View(ofvm);
            }
        }
        
        public ActionResult noResult()
        {
            return View();
        }
    }
}

