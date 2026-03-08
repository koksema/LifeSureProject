using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;

namespace LifeSureProject.Controllers
{
    public class FeatureController : Controller
    {
        // GET: Feature
        LifeSureDbEntities db = new LifeSureDbEntities();
        public ActionResult FeatureList()
        {
            var values = db.TblFeature.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddFeature()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFeature(TblFeature Feature)
        {
            db.TblFeature.Add(Feature);
            db.SaveChanges();
            return RedirectToAction("FeatureList");
        }
        public ActionResult DeleteFeature(int id)
        {
            var value = db.TblFeature.Find(id);
            db.TblFeature.Remove(value);
            db.SaveChanges();
            return RedirectToAction("FeatureList");
        }
    }
}