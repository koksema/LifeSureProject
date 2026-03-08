using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;

namespace LifeSureProject.Controllers
{
    public class OtherFeatureController : Controller
    {
        // GET: OtherFeature
        LifeSureDbEntities db = new LifeSureDbEntities();
        public ActionResult OtherFeatureList()
        {
            var values = db.TblOtherFeature.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddOtherFeature()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOtherFeature(TblOtherFeature OtherFeature)
        {
            db.TblOtherFeature.Add(OtherFeature);
            db.SaveChanges();
            return RedirectToAction("OtherFeatureList");
        }
        public ActionResult DeleteOtherFeature(int id)
        {
            var value = db.TblOtherFeature.Find(id);
            db.TblOtherFeature.Remove(value);
            db.SaveChanges();
            return RedirectToAction("OtherFeatureList");
        }
        [HttpGet]
        public ActionResult UpdateOtherFeature(int id)
        {
            var values = db.TblOtherFeature.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateOtherFeature(TblOtherFeature OtherFeature)
        {
            var value = db.TblOtherFeature.Find(OtherFeature.OtherFeatureId);
            value.OtherFeatureTitle = OtherFeature.OtherFeatureTitle;
            value.OtherFeatureDescription = OtherFeature.OtherFeatureDescription;
            value.OtherFeatureIcon = OtherFeature.OtherFeatureIcon;
            db.SaveChanges();
            return RedirectToAction("OtherFeatureList");
        }
    }
}