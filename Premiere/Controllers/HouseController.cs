using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeduInfo.Models;
using System.IO;

namespace LeduInfo.Controllers
{
    public class HouseController : Controller
    {
        private leduInfoDB db = new leduInfoDB();

        //
        // GET: /House/

        //public ActionResult Index()
        //{
        //    return View(db.ImgPathstbl.ToList());
        //}

        //public ActionResult HouseDetails(int id = 0)
        //{
        //    HouseInfo houseinfo = db.HouseInfotbl.Find(id);
        //    if (houseinfo == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View("HouseDetails", houseinfo);
        //}
        //
        // GET: /House/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    HouseInfo houseinfo = db.HouseInfotbl.Find(id);
        //    if (houseinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(houseinfo);
        //}

        //public ActionResult HouseList()
        //{
        //    return View(db.HouseInfotbl.ToList());
        //}
        public ActionResult GetImageList(int id=0)
        {
            //string mp=@"C:\LeduHouse\ResouseImg\1.jpg";
            ImgPath imgpath = db.ImgPathstbl.Find(id);
            string mp = @imgpath.Path.ToString();
            //string mp = "@" + from u in db.ImgPaths
            //                  where u.pathID == id
            //                  select u;
            return File(mp, "img");
        }

        //public ActionResult ShowHouseInfo(int id=0)
        //{
        //    //BulkManager.BulkFromAdventure();

        //    var houseinfo = db.HouseInfotbl.Find(id);
        //    return View(houseinfo);
        //}

        
        //
        // GET: /House/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /House/Create

        //[HttpPost]
        //public ActionResult Create(HouseInfo houseinfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HouseInfotbl.Add(houseinfo);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(houseinfo);
        //}

        //
        // GET: /House/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    HouseInfo houseinfo = db.HouseInfotbl.Find(id);
        //    if (houseinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(houseinfo);
        //}

        //
        // POST: /House/Edit/5

        [HttpPost]
        public ActionResult Edit(HouseInfo houseinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseinfo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(houseinfo);
        }

        //
        // GET: /House/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    HouseInfo houseinfo = db.HouseInfotbl.Find(id);
        //    if (houseinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(houseinfo);
        //}

        public ActionResult SaveComments(string param1="")
        {
            db.VoteCommenttbl.Add(new VoteComments { Comments = param1});
            db.SaveChanges();
           // var msg = "<div id=\"vote-info\" class=\"alert alert-info\"><strong>headsup!</strong> your content has been saved!</div>";
            return View();
        }
        //
        // POST: /House/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    HouseInfo houseinfo = db.HouseInfotbl.Find(id);
        //    db.HouseInfotbl.Remove(houseinfo);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

  

        
    }
}