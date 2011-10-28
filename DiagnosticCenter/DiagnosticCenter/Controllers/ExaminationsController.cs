using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{ 
    public class ExaminationsController : Controller
    {
        private DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();

        //
        // GET: /Examinations/

        public ViewResult Index(int? id)
        {
            int index = (id ?? -1);
            var examinations = db.Examinations.Include("Employee").Include("Patient").Include("ExaminationType").Where(e => e.Patient.ID_Patient == index);
            return View(examinations.ToList());
        }

        //
        // GET: /Examinations/Details/5

        public ViewResult Details(int id)
        {
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            return View(examination);
        }

        //
        // GET: /Examinations/Create

        public ActionResult Create()
        {
            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Category");
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName");
            return View();
        } 

        //
        // POST: /Examinations/Create

        [HttpPost]
        public ActionResult Create(Examination examination)
        {
            if (ModelState.IsValid)
            {
                db.Examinations.AddObject(examination);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Category", examination.ID_Employee);
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName", examination.ID_Patient);
            return View(examination);
        }
        
        //
        // GET: /Examinations/Edit/5
 
        public ActionResult Edit(int id)
        {
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Category", examination.ID_Employee);
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName", examination.ID_Patient);
            return View(examination);
        }

        //
        // POST: /Examinations/Edit/5

        [HttpPost]
        public ActionResult Edit(Examination examination)
        {
            if (ModelState.IsValid)
            {
                db.Examinations.Attach(examination);
                db.ObjectStateManager.ChangeObjectState(examination, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Category", examination.ID_Employee);
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName", examination.ID_Patient);
            return View(examination);
        }

        //
        // GET: /Examinations/Delete/5
 
        public ActionResult Delete(int id)
        {
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            return View(examination);
        }

        //
        // POST: /Examinations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            db.Examinations.DeleteObject(examination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}