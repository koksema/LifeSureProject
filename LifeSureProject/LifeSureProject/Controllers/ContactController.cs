using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using LifeSureProject.Models.DataModels;

namespace LifeSureProject.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        LifeSureDbEntities db = new LifeSureDbEntities();
        public ActionResult ContactList()
        {
            var values = db.TblContact.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddContact(TblContact contact)
        {
            db.TblContact.Add(contact);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }
        public ActionResult DeleteContact(int id)
        {
            var contact = db.TblContact.Find(id);
            db.TblContact.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }
        [HttpGet]
        public ActionResult UpdateContact(int id)
        {
            var contact = db.TblContact.Find(id);
            return View(contact);
        }
        [HttpPost]
        public ActionResult UpdateContact(TblContact contact)
        {
            var value=db.TblContact.Find(contact.ContactId);
            value.ContactAddress = contact.ContactAddress;
            value.ContactEmail = contact.ContactEmail;
            value.ContactPhone = contact.ContactPhone;
            db.SaveChanges();
            return RedirectToAction("Contact");
        }
    }
}