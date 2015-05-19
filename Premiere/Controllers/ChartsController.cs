using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Premiere.Models;
using Newtonsoft.Json.Linq;
using System.IO;

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
        /// return back javascript code , but not used anymore
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public ActionResult BrandDataAdding(BrandExposureLine model)
        //{
        //    return JavaScript("$('#button').click(function () { var chart = $('#container').highcharts();if (chart.series.length===1) { chart.addSeries({data: [194.1, 95.6, 54.4, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4]});}});});");
        //}

        public string ReturnContentForSpead(int ID)
        {
            var content = from d in DB.BrandSpreadMapBlogtbl
                       where d.ID == ID
                       select (string)d.Content;
            return content.FirstOrDefault().ToString();
        }

        public JsonResult DataChanged(int fromDate, int toDate)
        {
            var instances = (from d in DB.BrandSpreadMapBlogtbl
                       where (d.Period >= fromDate && d.Period <= toDate)
                       select new { ID = d.ID, BrandName = d.BrandName, Content=d.Content }).ToList()
                       .Select(x => new  { ID = x.ID, BrandName = x.BrandName, Content = x.Content});
            JObject o = JObject.FromObject(new
            {
                chart = from p in instances
                        select new
                        {
                            ID = p.ID,
                            BrandName = p.BrandName,
                            Content = p.Content
                        }
            });
            //foreach (var item in instances)
            //{
            //    JObject res = new JObject(
            //                    new JProperty("chart",
            //                        new JObject(
            //                            new JProperty("ID", item.ID),
            //                            new JProperty("BrandName", item.BrandName),
            //                            new JProperty("Content", item.Content))));
            //}
            //foreach (var item in ids)
            //{
            //    TempData.Add(item.ID.ToString(),item.BrandName);
            //}


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

        public ActionResult BrandImage(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandImagetbl.ToList();
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
