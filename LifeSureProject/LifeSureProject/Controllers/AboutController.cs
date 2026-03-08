using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;

namespace LifeSureProject.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        LifeSureDbEntities db = new LifeSureDbEntities(); 
        public ActionResult AboutList()
        {
            var values = db.TblAbout.ToList();
            if (!values.Any())
            {
                ViewBag.Test = "VERİ YOK";
            }
            else
            {
                ViewBag.Test = "VERİ VAR";
            }

            return View(values);
        }
        [HttpGet]
        public ActionResult CreateAbout()
        {
           return View();
        }
        [HttpPost]
        public ActionResult CreateAbout(TblAbout about)
        {
            db.TblAbout.Add(about);
            db.SaveChanges();
            return RedirectToAction("AboutList");
        }
        public ActionResult DeleteAbout(int id)
        {
            var value=db.TblAbout.Find(id);
            db.TblAbout.Remove(value);
            db.SaveChanges();
            return RedirectToAction("AboutList");
        }
        [HttpGet]
        public ActionResult UpdateAbout(int id)
        {
            var value=db.TblAbout.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateAbout(TblAbout About)
        {
            var value=db.TblAbout.Find(About.AboutId);
            value.AboutTitle=About.AboutTitle;
            value.AboutDescription=About.AboutDescription;
            value.AboutImageUrl=About.AboutImageUrl;
            db.SaveChanges();
            return RedirectToAction("AboutList");

        }
    }
}