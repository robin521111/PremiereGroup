using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrustructure;
using System.Configuration;

namespace LeduInfo.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/
        LeduInfo.Models.PremiereDB db = new Models.PremiereDB();
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult UploadPage()
        //{
        //    var JasonModel = db.JsonModeltbl.ToList();
        //    return View(JasonModel);
        //}

        //public ActionResult HeaderImg()
        //{
        //    var headerImgPath=db.FileResourcetbl.Find(1).Path;
        //    image.MakeThumbnail(headerImgPath);
        //    return base.File(headerImgPath, "image/jpeg");
        //}

        //public ActionResult HeaderImagPath()
        //{
        //    Image image = new Image();
            
        //    var images = from f in db.FileResourcetbl
        //                 select f.Path;
        //    return View(images.ToList());
        //}

    }
}
