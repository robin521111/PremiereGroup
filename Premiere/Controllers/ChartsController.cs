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

        public string ReturnContent(int ID)
        {
            var content = from d in DB.BrandSpreadMaptbl
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

        public ActionResult BrandExposure_map(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandSpreadMaptbl.ToList();
            return View(model);
        }

        public ActionResult GraphicData(string title)
        {
            ViewBag.Title = title;
            var model = DB.BrandSpreadMaptbl.ToList();
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
