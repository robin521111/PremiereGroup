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


        public string ReturnContentForSpeadNews(int ID)
        {
            var content = from d in DB.BrandSpreadMapNewstbl
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
                       select new { ID = d.ID, BrandName = d.BrandName, Content=d.Content }).ToList()
                       .Select(x => new  { ID = x.ID, BrandName = x.BrandName, Content = x.Content});

            foreach (var item in instances)
            {

                if (instances.Select(x=>x.BrandName).Contains(item.BrandName))
                {
                  
                   JObject oTempt = JObject.FromObject( new {
                    data=   from b in instances
                           where b.BrandName == item.BrandName && b.ID != item.ID
                           select  new {content = b.Content, ID = b.ID }
                   });

                   var contents = oTempt["data"].Values<JToken>().ToArray();
                   foreach (var c in contents)
                   {
                       
                   }
                   string rss = oTempt["data"][0]["content"].ToString();
                   JObject o1 = JObject.Parse(rss);

                   JObject o2 = JObject.Parse(item.Content.ToString());
                    o2.Merge(o1,new JsonMergeSettings {
                    MergeArrayHandling = MergeArrayHandling.Union
                    });
                }

            }

            JObject o = JObject.FromObject(new
            {
                chart = (from p in instances
                        select new
                        {
                            ID = p.ID,
                            BrandName = p.BrandName,
                            Content = p.Content
                        })
                        
            });
            
            return Json(o.ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DateChangedForSpreadNews(int fromDate, int toDate)
        {
            
            var instances = (from d in DB.BrandSpreadMapNewstbl
                             where (d.Period >= fromDate && d.Period <= toDate)
                             select new { ID = d.ID, BrandName = d.BrandName, Content = d.Content }).ToList()
                       .Select(x => new { ID = x.ID, BrandName = x.BrandName, Content = x.Content });

            foreach (var item in instances)
            {
                if (instances.Select(x => x.BrandName).Contains(item.BrandName))
                {
                    JObject oTempt = JObject.FromObject(new
                    {
                        data = from b in instances
                               where b.BrandName == item.BrandName
                               select new { content = b.Content }
                    });

                    var contents = oTempt["data"].Values<JToken>().ToArray();
                    foreach (var c in contents)
                    {
                       JObject o1 = JObject.Parse(c.ToString());
                       IEnumerable<JToken> obj1 =  o1["content"]["data"]["wordgraph"][0]["features"].Values<JToken>().ToArray();
                       foreach (var i in obj1)
                       {

                       }
                       JObject o2 = JObject.Parse(item.Content.ToString());
                       o2.Merge(o1, new JsonMergeSettings
                       {
                           MergeArrayHandling = MergeArrayHandling.Merge
                       });

                    }



                }


            }

            JObject o = JObject.FromObject(new
            {
                chart = (from p in instances
                         select new
                         {
                             
                             BrandName = p.BrandName,
                             ID = p.ID,
                             Content = p.Content
                         })

            });

            return Json(o.ToString(), JsonRequestBehavior.AllowGet);
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
