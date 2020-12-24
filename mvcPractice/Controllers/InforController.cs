using mvcPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace mvcPractice.Controllers
{
    public class InforController : Controller
    {
        SignupLoginEntities db = new SignupLoginEntities();
        // GET: Infor
        public ActionResult Index()
        {
            if (ModelState.IsValid == true) {
                var employeeData=db.Employees.ToList();
                ViewBag.myMessage = ("Data found");
                return View(employeeData);
            }
            else
            {
                ViewBag.myMessage = ("Data Not found");
                return View();
            }

           
        }

        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid == true)
            {
                db.Employees.Add(emp);
                int a=db.SaveChanges();         
                if (a > 0) {
                    TempData["Insertmessage"] = "<script>alert('Added Successfully!')</script>";
                    return RedirectToAction("Index", "Infor");
                }
                else
                {
                    TempData["Insertmessage"] = "<script>alert('Try Again Failed!')</script>";
                }
                
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var empSelect = db.Employees.Where(model => model.empId == id).FirstOrDefault();
            return View(empSelect);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if(ModelState.IsValid==true)
            {
                db.Entry(emp).State = EntityState.Modified;
                var empEdit = db.SaveChanges();
                if (empEdit > 0)
                {
                    TempData["editMessage"] = "<script>alert('Edit Successfully!')</script>";
                    return RedirectToAction("Index", "Infor");
                }
                else
                {
                    TempData["editMessage"] = "<script>alert('Edit Failed!')</script>";
                }
            }
            
            return View();
        }

        public ActionResult Delete(int id)
        {
            var selectData = db.Employees.Where(model => model.empId == id).FirstOrDefault();
            if (selectData != null)
            {
                db.Entry(selectData).State = EntityState.Deleted;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["DeleteMessage"] = "<script>alert('Deleted Successfully!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["DeleteMessage"] = "<script>alert('Deleted Failed!')</script>";
                }
            }
         
            return View();
        }
        //[HttpPost]
        //public ActionResult Delete(Employee emp)
        //{
               
        //        return RedirectToAction("Index");
            
        //}


    }
}