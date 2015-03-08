using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeduInfo.Models;

namespace LeduInfo.Controllers
{
    public class VoteController : Controller
    {
        //
        // GET: /Vote/

        private leduInfoDB db = new leduInfoDB();
        //public ActionResult VotePage()
        //{
        //    return View(db.ImgPathstbl.ToList());
        //}

        public ActionResult VoteResult(int id = 0)
        {
            var result = id;
            return View();
        }



        //public ActionResult Vote()
        //{
        //    var voteimg = new Vote_Algorithm();
        //    return View(voteimg);
        //}

        //public ActionResult Vote()
        //{
        //    leduInfoDB db = new leduInfoDB();
        //    var imglist = db.ImgPathstbl.ToList();
        //    return View(imglist);
        //}

    }
}
