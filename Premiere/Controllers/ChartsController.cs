using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Premiere.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using Infrustructure;
namespace Premiere.Controllers
{
    public class ChartsController : Controller
    {
        //
        // GET: /Charts/
        PremiereDB DB = new PremiereDB();
        
        public ActionResult BrandExposure_line(string title)
        {
            ViewBag.Title = title;
            //List<BrandExposureLine> list = new List<BrandExposureLine>();
            IEnumerable<BrandExposureLine> model = DB.BrandExposureLinetbl.ToList();
            return View(DB.BrandExposureLinetbl.ToList());
        }




        /// <summary>
        /// 返回图谱(blog)的数据
        /// </summary>
        /// <param name="ID">导入数据的ID</param>
        /// <returns></returns>
        public string ReturnContentForSpeadBlog(int ID)
        {
            var content = from d in DB.BrandSpreadMapBlogtbl
                       where d.ID == ID
                       select (string)d.Content;
            return content.FirstOrDefault().ToString();
        }

        /// <summary>
        /// return content for Spread via ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string ReturnContentForSpeadNews(int ID)
        {
            var content = from d in DB.BrandSpreadMapNewstbl
                          where d.ID == ID
                          select (string)d.Content;
            return content.FirstOrDefault().ToString();
        }

        /// <summary>
        /// return content for Graphic via ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string ReturnContentForMediaData(int ID)
        {
            var content = from d in DB.MediaFocusMaptbl
                          where d.ID == ID
                          select (string)d.Content;
            return content.FirstOrDefault().ToString();
        }

        /// <summary>
        /// return back date from time slot
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public JsonResult DateChangedForSpreadBlog(int fromDate, int toDate)
        {
            var instances = (from d in DB.BrandSpreadMapBlogtbl
                             where (d.Period >= fromDate && d.Period <= toDate)
                             select new { ID = d.ID, BrandName = d.BrandName, Content = d.Content, Period = d.Period }).ToList()
                                  .Select(x => new { ID = x.ID, BrandName = x.BrandName, Content = x.Content, Period = x.Period });

            //List<Premiere.Models.BrandSpreadMapNews> listNews = new List<BrandSpreadMapNews>();


            //var charts = from p in instances select new { brandName = p.BrandName, ID = p.ID, Content = p.Content };

            //var charts_data = o["data"].Values<JToken>().ToArray();

            //var instance_list = instances.ToList();

            var obj = instances.GroupBy(x => x.BrandName).Select(x => x.First());
            JObject o = null;
            JObject o3 = null;

            foreach (var item in instances)
            {
                if (instances.Select(x => x.BrandName).Contains(item.BrandName))
                {
                    JObject oTempt = JObject.FromObject(new
                    {
                        data = from b in instances
                               where b.BrandName == item.BrandName && item.Period != b.Period
                               select new { content = b.Content }
                    });

                    var contents = oTempt["data"].Values<JToken>().ToArray();

                    JObject o2 = JObject.Parse(item.Content.ToString());
                    JObject compare_str2 = JObject.Parse(o2["data"]["wordgraph"][0].ToString());
                    foreach (var c in contents)
                    {
                        string rss = (string)c["content"];
                        JObject o1 = JObject.Parse(rss);
                        JObject compare_str = JObject.Parse(o1["data"]["wordgraph"][0].ToString());

                        //string rss1 = item.Content.ToString();
                        //JObject t = (JObject)t2["content"];

                        compare_str2.Merge(compare_str, new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Concat
                        });

                        o3 = o2;
                        o3["data"]["wordgraph"][0] = compare_str2;


                        //foreach (JToken child in o3.Children())
                        //{
                        //    foreach (JToken grandchild in child)
                        //    {
                        //        foreach (JToken grandGrandchild in grandchild)
                        //        {
                        //            var property = grandGrandchild as JProperty;
                        //            if (property.ToString()== item.BrandName)
                        //            {

                        //            }
                        //        }
                        //    }
                        //}
                        //charts.ToList();
                        //var distinct_chart = charts.GroupBy(x => x.brandName).Select(x => x);
                        //foreach (var d in obj.ToList())
                        //{
                        //    if (d.BrandName == item.BrandName)
                        //    {

                        //    }
                        //}


                    }

                    Dictionary<string, string> distinctCharts = new Dictionary<string, string>();
                    var distinctName = instances.GroupBy(x => x.BrandName).Select(x => x.First());





                    //if (contents.First() != null)
                    //{
                    //    foreach (var brand in distinctName.ToList())
                    //    {
                    //        if (item.BrandName == brand.BrandName)
                    //        {


                    //        }
                    //    }

                    //}





                    //foreach (var l in instance_list)
                    //{
                    //    if (l.BrandName == item.BrandName)
                    //    {
                    //        instance_list.Remove(l);
                    //    }
                    //}

                    //instance_list.Add(new { ID = item.ID, BrandName = item.BrandName, Content = item.Content, Period = item.Period });

                }



            }

            JObject o4 = JObject.FromObject(new
            {
                chart = (from p in obj
                         select new
                         {
                             BrandName = p.BrandName,
                             ID = p.ID,
                             Content = p.Content
                         })

            });


            return Json(o4.ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DateChangedForMedia(int fromDate, int toDate)
        {

            var instances = (from d in DB.MediaFocusMaptbl
                             where (d.Period >= fromDate && d.Period <= toDate)
                             select new { ID = d.ID, BrandName = d.BrandName, Content = d.Content, Period = d.Period }).ToList()
                       .Select(x => new { ID = x.ID, BrandName = x.BrandName, Content = x.Content, Period = x.Period });

            //List<Premiere.Models.BrandSpreadMapNews> listNews = new List<BrandSpreadMapNews>();


            //var charts = from p in instances select new { brandName = p.BrandName, ID = p.ID, Content = p.Content };

            //var charts_data = o["data"].Values<JToken>().ToArray();

            //var instance_list = instances.ToList();

            var obj = instances.GroupBy(x => x.BrandName).Select(x => x.First());
            JObject o = null;
            JObject o3 = null;

            foreach (var item in instances)
            {
                if (instances.Select(x => x.BrandName).Contains(item.BrandName))
                {
                    JObject oTempt = JObject.FromObject(new
                    {
                        data = from b in instances
                               where b.BrandName == item.BrandName && item.Period != b.Period
                               select new { content = b.Content }
                    });

                    var contents = oTempt["data"].Values<JToken>().ToArray();

                    JObject o2 = JObject.Parse(item.Content.ToString());
                    JObject compare_str2 = JObject.Parse(o2["data"]["wordgraph"][0].ToString());
                    foreach (var c in contents)
                    {
                        string rss = (string)c["content"];
                        JObject o1 = JObject.Parse(rss);
                        JObject compare_str = JObject.Parse(o1["data"]["wordgraph"][0].ToString());

                        //string rss1 = item.Content.ToString();
                        //JObject t = (JObject)t2["content"];

                        compare_str2.Merge(compare_str, new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Concat
                        });

                        o3 = o2;
                        o3["data"]["wordgraph"][0] = compare_str2;
                    }

                    Dictionary<string, string> distinctCharts = new Dictionary<string, string>();
                    var distinctName = instances.GroupBy(x => x.BrandName).Select(x => x.First());






                }



            }

            JObject o4 = JObject.FromObject(new
            {
                chart = (from p in obj
                         select new
                         {
                             BrandName = p.BrandName,
                             ID = p.ID,
                             Content = p.Content
                         })

            });


            return Json(o4.ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DateChangedForSpreadNews(int fromDate, int toDate)
        {
            
            var instances = (from d in DB.BrandSpreadMapNewstbl
                             where (d.Period >= fromDate && d.Period <= toDate)
                             select new { ID = d.ID, BrandName = d.BrandName, Content = d.Content, Period = d.Period }).ToList()
                       .Select(x => new { ID = x.ID, BrandName = x.BrandName, Content = x.Content, Period = x.Period });

            //List<Premiere.Models.BrandSpreadMapNews> listNews = new List<BrandSpreadMapNews>();
           
            
            //var charts = from p in instances select new { brandName = p.BrandName, ID = p.ID, Content = p.Content };

            //var charts_data = o["data"].Values<JToken>().ToArray();

            //var instance_list = instances.ToList();

            var obj=instances.GroupBy(x => x.BrandName).Select(x => x.First());
            JObject o = null;
            JObject o3 = null;

            foreach (var item in instances)
            {
                if (instances.Select(x => x.BrandName).Contains(item.BrandName))
                {
                    JObject oTempt = JObject.FromObject(new
                    {
                        data = from b in instances
                               where b.BrandName == item.BrandName && item.Period !=b.Period
                               select new { content = b.Content }
                    });

                    var contents = oTempt["data"].Values<JToken>().ToArray();
                    
                    JObject o2 = JObject.Parse(item.Content.ToString());
                    JObject compare_str2 = JObject.Parse(o2["data"]["wordgraph"][0].ToString());
                    foreach (var c in contents)
                    {
                        string rss = (string)c["content"];
                        JObject o1 = JObject.Parse(rss);
                        JObject compare_str = JObject.Parse(o1["data"]["wordgraph"][0].ToString());
                        
                       //string rss1 = item.Content.ToString();
                       //JObject t = (JObject)t2["content"];
                       
                       compare_str2.Merge(compare_str, new JsonMergeSettings
                       {
                           MergeArrayHandling = MergeArrayHandling.Concat
                       });

                       o3 = o2;
                       o3["data"]["wordgraph"][0] = compare_str2;


                       //foreach (JToken child in o3.Children())
                       //{
                       //    foreach (JToken grandchild in child)
                       //    {
                       //        foreach (JToken grandGrandchild in grandchild)
                       //        {
                       //            var property = grandGrandchild as JProperty;
                       //            if (property.ToString()== item.BrandName)
                       //            {

                       //            }
                       //        }
                       //    }
                       //}
                       //charts.ToList();
                       //var distinct_chart = charts.GroupBy(x => x.brandName).Select(x => x);
                       //foreach (var d in obj.ToList())
                       //{
                       //    if (d.BrandName == item.BrandName)
                       //    {
                                
                       //    }
                       //}


                    }

                    Dictionary<string, string> distinctCharts = new Dictionary<string, string>();
                    var distinctName = instances.GroupBy(x => x.BrandName).Select(x => x.First());
                    


                    

                    //if (contents.First() != null)
                    //{
                    //    foreach (var brand in distinctName.ToList())
                    //    {
                    //        if (item.BrandName == brand.BrandName)
                    //        {


                    //        }
                    //    }

                    //}
                   

                   

                   
                    //foreach (var l in instance_list)
                    //{
                    //    if (l.BrandName == item.BrandName)
                    //    {
                    //        instance_list.Remove(l);
                    //    }
                    //}

                    //instance_list.Add(new { ID = item.ID, BrandName = item.BrandName, Content = item.Content, Period = item.Period });

                }

                

            }

            JObject o4 = JObject.FromObject(new
            {
                chart = (from p in obj
                         select new
                         {
                             BrandName = p.BrandName,
                             ID = p.ID,
                             Content = p.Content
                         })

            });


            return Json(o4.ToString(), JsonRequestBehavior.AllowGet);
        }
        public string ReturnContentForMedia(int ID)
        {
            var content = from d in DB.MediaFocusMaptbl
                          where d.ID == ID
                          select (string)d.Content;
            return content.FirstOrDefault().ToString();
        }

        public ActionResult BrandExposure_bubble(string title)
        {
            ViewBag.Title = title;
            IEnumerable<BrandExposureBubble> model =DB.BrandExposureBubbletbl.ToList();
            return View(model);
        }

        public ActionResult BrandExposure_mapBlog(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandSpreadMapBlogtbl.ToList();
            return View(model);
        }
        public ActionResult BrandExposure_mapNews(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandSpreadMapNewstbl.ToList();
            return View(model);
        }


        public ActionResult GetGraphicData(string brand_name)
        {
            var Content = from c in DB.MediaFocusMaptbl
                          where c.BrandName == brand_name
                          select c.Content;
            return View(Content.ToString());
        }

        public ActionResult GraphicData(string title)
        {
            ViewBag.Title = title;
            var model = DB.MediaFocusMaptbl.ToList();
            return View(model);
        }

        public ActionResult BrandImageBlog(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandImageBlogtbl.ToList();
            return View(model);
        }

        public ActionResult BrandImageNews(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandImageNewstbl.ToList();
            return View(model);
        }
        public ActionResult DesignSense(string title)
        {
            ViewBag.Title = title;
            var model = DB.DesignSensetbl.ToList();
            return View(model);
        }
        public ActionResult SexRatio(string title)
        {
            ViewBag.Title = title;
            var model = DB.SexRatiotbl.ToList();
            return View(model);
        }
        public ActionResult BrandFocus(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandFocustbl.ToList();
            return View(model);
        }

    }
}
