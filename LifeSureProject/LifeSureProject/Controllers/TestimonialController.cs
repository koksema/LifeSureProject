using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;

namespace LifeSureProject.Controllers
{
    public class TestimonialController : Controller
    {
        // GET: Testimonial
        LifeSureDbEntities db = new LifeSureDbEntities();
        public ActionResult TestimonialList()
        {
            var values = db.TblTestimonial.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddTestimonial()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTestimonial(TblTestimonial Testimonial)
        {
            db.TblTestimonial.Add(Testimonial);
            db.SaveChanges();
            return RedirectToAction("TestimonialList");
        }
        public ActionResult DeleteTestimonial(int id)
        {
            var value = db.TblTestimonial.Find(id);
            db.TblTestimonial.Remove(value);
            db.SaveChanges();
            return RedirectToAction("TestimonialList");
        }
        [HttpGet]
        public ActionResult UpdateTestimonial(int id)
        {
            var values = db.TblTestimonial.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateTestimonial(TblTestimonial Testimonial)
        {
            var value = db.TblTestimonial.Find(Testimonial.TestimonialId);
            value.TestimonialName = Testimonial.TestimonialName;
            value.TestimonialTitle = Testimonial.TestimonialTitle;
            value.TestimonialDescription = Testimonial.TestimonialDescription;
            value.TestimonialImgUrl = Testimonial.TestimonialImgUrl;
            db.SaveChanges();
            return RedirectToAction("TestimonialList");
        }
    }
}