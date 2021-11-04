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
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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

            offRouteViewModel vm = new offRouteViewModel()
            {
                offcialAllRoute = 所有路線,
                offcialLowRoute = 初級路線,
                offcialMiddleRoute = 中級路線,
                offcialHighRoute = 高級路線,
            };

            return View(vm);
        }

        public ActionResult 初級List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 初級路線 = (from h in db.Homes
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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
            offRouteViewModel orvm = new offRouteViewModel()
            {
                offcialAllRoute = 初級路線
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
            decimal v1 = 10.0M;
            decimal v2 = 20.0M;
            if (search.searchRank == "初級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
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
                    offRouteViewModel ofvm = new offRouteViewModel()
                    {
                        offcialAllRoute = 路線,
                    };
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "初級" && h.hdistance > 10 && h.hdistance < 20 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "初級" && h.hdistance > 20 && h.hname.Contains(search.searchKeyword)
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
            else if (search.searchRank == "中級")
            {
                if (search.searchDistance == 1)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance < v1 && h.hname.Contains(search.searchKeyword)
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
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance > v1 && h.hdistance < v2 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance > v2 && h.hname.Contains(search.searchKeyword)
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
            else if (search.searchRank == "高級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance < v1 && h.hname.Contains(search.searchKeyword)
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
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance > v1 && h.hdistance < v2 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance > v2 && h.hname.Contains(search.searchKeyword)
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
            else
            {
                //var 路線 = (from h in db.Homes
                //          join oc in db.official_route_comment on h.id equals oc.article_title_id
                //          select new offRoute
                //          {
                //              h_ID = h.id,
                //              h_description = h.hdescription,
                //              h_name = h.hname,
                //              h_distance = (double?)h.hdistance,
                //              h_rank = h.hrank,
                //              h_img = h.himg,
                //              oc_artitleTitleID = oc.article_title_id,
                //              oc_allStar = (double?)oc.all_star_summary,
                //          }).ToList();
                //offRouteViewModel ofvm = new offRouteViewModel()
                //{
                //    offcialAllRoute = 路線,
                //};
                //return View(ofvm);

                return RedirectToAction("初級List", "offRoute");
            }
        }

        public ActionResult 中級List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 中級路線 = (from h in db.Homes
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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
            offRouteViewModel orvm = new offRouteViewModel()
            {
                offcialAllRoute = 中級路線
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
            decimal v1 = 10.0M;
            decimal v2 = 20.0M;
            if (search.searchRank == "初級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
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
                    offRouteViewModel ofvm = new offRouteViewModel()
                    {
                        offcialAllRoute = 路線,
                    };
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "初級" && h.hdistance > 10 && h.hdistance < 20 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "初級" && h.hdistance > 20 && h.hname.Contains(search.searchKeyword)
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
            else if (search.searchRank == "中級")
            {
                if (search.searchDistance == 1)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance < v1 && h.hname.Contains(search.searchKeyword)
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
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance > v1 && h.hdistance < v2 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance > v2 && h.hname.Contains(search.searchKeyword)
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
            else if (search.searchRank == "高級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance < v1 && h.hname.Contains(search.searchKeyword)
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
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance > v1 && h.hdistance < v2 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance > v2 && h.hname.Contains(search.searchKeyword)
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
            else
            {
                //var 路線 = (from h in db.Homes
                //          join oc in db.official_route_comment on h.id equals oc.article_title_id
                //          select new offRoute
                //          {
                //              h_ID = h.id,
                //              h_description = h.hdescription,
                //              h_name = h.hname,
                //              h_distance = (double?)h.hdistance,
                //              h_rank = h.hrank,
                //              h_img = h.himg,
                //              oc_artitleTitleID = oc.article_title_id,
                //              oc_allStar = (double?)oc.all_star_summary,
                //          }).ToList();
                //offRouteViewModel ofvm = new offRouteViewModel()
                //{
                //    offcialAllRoute = 路線,
                //};
                //return View(ofvm);

                return RedirectToAction("初級List", "offRoute");
            }
        }

        public ActionResult 高級List()
        {
            KSBikeEntities db = new KSBikeEntities();
            var 高級路線 = (from h in db.Homes
                        join oc in db.official_route_comment on h.id equals oc.article_title_id
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
            offRouteViewModel orvm = new offRouteViewModel()
            {
                offcialAllRoute = 高級路線
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
            decimal v1 = 10.0M;
            decimal v2 = 20.0M;
            if (search.searchRank == "初級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
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
                    offRouteViewModel ofvm = new offRouteViewModel()
                    {
                        offcialAllRoute = 路線,
                    };
                    return View(ofvm);
                }
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "初級" && h.hdistance > 10 && h.hdistance < 20 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "初級" && h.hdistance > 20 && h.hname.Contains(search.searchKeyword)
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
            else if (search.searchRank == "中級")
            {
                if (search.searchDistance == 1)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance < v1 && h.hname.Contains(search.searchKeyword)
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
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance > v1 && h.hdistance < v2 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "中級" && h.hdistance > v2 && h.hname.Contains(search.searchKeyword)
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
            else if (search.searchRank == "高級")
            {
                if (search.searchDistance == 2)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance < v1 && h.hname.Contains(search.searchKeyword)
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
                else if (search.searchDistance == 3)
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance > v1 && h.hdistance < v2 && h.hname.Contains(search.searchKeyword)
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
                else
                {
                    var 路線 = (from h in db.Homes
                              join oc in db.official_route_comment on h.id equals oc.article_title_id
                              where h.hrank == "高級" && h.hdistance > v2 && h.hname.Contains(search.searchKeyword)
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
            else
            {
                //var 路線 = (from h in db.Homes
                //          join oc in db.official_route_comment on h.id equals oc.article_title_id
                //          select new offRoute
                //          {
                //              h_ID = h.id,
                //              h_description = h.hdescription,
                //              h_name = h.hname,
                //              h_distance = (double?)h.hdistance,
                //              h_rank = h.hrank,
                //              h_img = h.himg,
                //              oc_artitleTitleID = oc.article_title_id,
                //              oc_allStar = (double?)oc.all_star_summary,
                //          }).ToList();
                //offRouteViewModel ofvm = new offRouteViewModel()
                //{
                //    offcialAllRoute = 路線,
                //};
                //return View(ofvm);

                return RedirectToAction("初級List", "offRoute");
            }
        }
    }
}

