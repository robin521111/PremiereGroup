using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Premiere.Controllers
{
    public class ChartsController : Controller
    {
        //
        // GET: /Charts/

        public ActionResult BrandExposure_line()
        {
            return View();
        }

        public ActionResult BrandExposure_bubble()
        {
            return View();
        }

    }
}
