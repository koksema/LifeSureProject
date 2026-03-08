using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;
using LifeSureProject.Service;

namespace LifeSureProject.Controllers
{
    public class HomeController : Controller
    {
        LifeSureDbEntities db = new LifeSureDbEntities();

        public ActionResult Index()
        {
            var linkedInUser = new LifeSureProject.ViewModels.LinkedinViewModel
            {
                Username = "muratyucedag",
                PublicIdentifier = "muratyucedag",
                FollowersCount = 95583
            };

            return View(linkedInUser);
        }
        public PartialViewResult Feature()
        {
            var features = db.TblFeature.ToList();
            return PartialView(features);
        }

        public PartialViewResult OtherFeature()
        {
            var values = db.TblOtherFeature
                .OrderByDescending(x => x.OtherFeatureId)
                .Take(4)
                .ToList();
            return PartialView(values);
        }

        public PartialViewResult About()
        {
            var about = db.TblAbout.ToList();
            return PartialView(about);
        }


        public PartialViewResult Service()
        {
            var values = db.TblService
                .OrderByDescending(x => x.ServiceId)
                .Take(4)
                .ToList();
            return PartialView(values);
        }
        public PartialViewResult Employee()
        {
            var employees = db.TblEmployee.ToList();
            return PartialView(employees);
        }

        public PartialViewResult Testimonial()
        {
            var testimonials = db.TblTestimonial.ToList();
            return PartialView(testimonials);
        }
        public PartialViewResult Contact()
        {
            var contacts = db.TblContact.ToList();
            return PartialView(contacts);
        }
        public PartialViewResult Spinner()
        {
            return PartialView();
        }
        public PartialViewResult Navbar()
        {
            return PartialView();
        }
        public PartialViewResult Scripts()
        {
            return PartialView();
        }

        public PartialViewResult FAQ()
        {
            var faq = db.TblQuestion.OrderByDescending(x => x.QuestionId)
                .Take(4)
                .ToList();
            return PartialView(faq);
        }
    }
}