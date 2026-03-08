using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;

namespace LifeSureProject.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        LifeSureDbEntities db = new LifeSureDbEntities();
        public ActionResult EmployeeList()
        {
            var values = db.TblEmployee.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(TblEmployee employee)
        {
            db.TblEmployee.Add(employee);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        public ActionResult DeleteEmployee(int id)
        {
            var value = db.TblEmployee.Find(id);
            db.TblEmployee.Remove(value);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            var values = db.TblEmployee.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(TblEmployee employee)
        {
            var value = db.TblEmployee.Find(employee.EmployeeId);
            value.EmployeeName = employee.EmployeeName;
            value.EmployeePhoto = employee.EmployeePhoto;
            value.EmployeeTitle = employee.EmployeeTitle;
            value.EmployeeInstagram = employee.EmployeeInstagram;
            value.EmployeeLinkedIn = employee.EmployeeLinkedIn;
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
    }
}