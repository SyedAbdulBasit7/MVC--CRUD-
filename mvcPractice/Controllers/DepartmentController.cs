using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcPractice.Models;
using System.Data.Entity;

namespace mvcPractice.Controllers
{
   
    public class DepartmentController : Controller
    {
        SignupLoginEntities db = new SignupLoginEntities();
        // GET: Department
        public ActionResult Index()
        {
            if (ModelState.IsValid == true)
            {
                var department= db.Departments.ToList();
                return View(department);
            }
            
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department dep)
        {
            if (ModelState.IsValid == true)
            {
                db.Departments.Add(dep);
                var depData = db.SaveChanges();
                if (depData > 0)
                {
                    TempData["message"] = "<script>alert('Department Add Successfully')</script>";
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    TempData["message"] = "<script>alert('Department Add Failed!')</script>";
                }

            }
            return View();
        }
        public ActionResult Edit(int id)
        {
                var selectDep=db.Departments.Where(model => model.depId == id).FirstOrDefault();
            return View(selectDep);
        }
        [HttpPost]
        public ActionResult Edit(Department dep)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(dep).State = EntityState.Modified;
                var a=db.SaveChanges();
                if (a > 0)
                {
                    TempData["editMessage"] = "<script>alert('Successfully Edit!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["editMessage"] = "<script>alert('Failed Edit!')</script>";
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var selectVar = db.Departments.Where(model => model.depId == id).FirstOrDefault();
            db.Entry(selectVar).State = EntityState.Deleted;
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["deleteMessage"] = "<script>alert('Successfully Edit!')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["deleteMessage"] = "<script>alert('Successfully Edit!')</script>";
            }

            return View();
        }
        //[HttpPost]
        //public ActionResult Delete(Department dep)
        //{
           
               
            
        //    return View();
        //}

    }
}