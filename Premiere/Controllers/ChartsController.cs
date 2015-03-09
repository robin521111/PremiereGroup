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
        
        public ActionResult BrandExposure_line()
        {
            //var lineData = from uploaded in DB.UploadHandlertbl
            //               where uploaded.Content !=null
            //               select uploaded.Content;
            return View();
        }

        public ActionResult BrandExposure_bubble()
        {
            return View();
        }

        public ActionResult BrandExposure_map()
        {
            return View();
        }

        public ActionResult GraphicData()
        {
            return View();
        }
    }
}
