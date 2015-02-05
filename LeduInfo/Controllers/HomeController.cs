using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeduInfo.Models;
using Infrustructure;
using System.Security;

namespace LeduInfo.Controllers
{
    public class HomeController : Controller
    {
        PremiereDB DB = new PremiereDB();
        Infrustructure.Blog.MicroBlog microblog = new Infrustructure.Blog.MicroBlog();

        public ActionResult Index(string userName = "")
        {
            ViewBag.Message = userName;

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult GraphicData()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        public ActionResult LeduIndex()
        {
            ViewBag.Message = "Your Info system for renting.";
            return View();
        }

        //public ActionResult InfoPage()
        //{
        //    ViewBag.Message = "This is the info page for user to search!";
        //    return View();
        //}
        //public ActionResult Charts()
        //{
        //    ViewBag.Charts = " This is a Chart for demo ";
        //    return View();
        //}

        //public ActionResult Vote()
        //{
        //    ViewBag.Message = "Vote for your topic!";
        //    return View();
        //}

        //public ActionResult Redirect()
        //{
        //    Response.RedirectToRoute("Default");
        //    return View();
        //}

        public ActionResult AboutMe()
        {
            return View();
        }

        
        //public ActionResult Blog()
        //{
        //    return View();
        //}

        //public ActionResult BlogPost()
        //{
        //    ViewBag.popularWord = DB.Tagstbl.ToList();
        //    var bloglist = DB.Blogtbl.ToList();
        //    return View(bloglist);
        //}

        public ActionResult MakeThumbnail()
        {
            return View();
        }

        public ActionResult UserProfilo()
        {
            return View();
        }
    }
}
