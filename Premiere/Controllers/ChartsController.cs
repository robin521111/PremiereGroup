using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Premiere.Models;

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
            List<BrandList> list = new List<BrandList>();
            IEnumerable<BrandList> brandlist = from b in DB.BrandExposureLinetbl
                                               where b.BrandName != null
                                               select new BrandList { brandlist = b.BrandName };


            foreach (var item in brandlist)
            {
                list.Add(item);
            }
            return View(list.ToList());
        }

        public ActionResult BrandExposure_bubble(string title)
        {
            ViewBag.Title = title;
            return View();
        }

        public ActionResult BrandExposure_map(string title)
        {
            ViewBag.Title = title;
            return View();
        }

        public ActionResult GraphicData(string title)
        {
            ViewBag.Title = title;
            return View();
        }

        public ActionResult BrandImage(string title)
        {
            ViewBag.Title = title;
            return View();
        }

        public ActionResult DesignSense(string title)
        {
            ViewBag.Title = title;
            return View();
        }
        public ActionResult SexRatio(string title)
        {
            ViewBag.Title = title;
            return View();
        }
        public ActionResult BrandFocus(string title)
        {
            ViewBag.Title = title;
            return View();
        }

    }
}
