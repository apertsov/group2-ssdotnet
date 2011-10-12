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
    public class PlanController : Controller
    {
        private DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();

        //
        // GET: /Plan/

        public ViewResult Index()
        {
            return View(db.Days.ToList());
        }

        //
        // GET: /Plan/Details/5

        public ViewResult Details(int id)
        {
            Day day = db.Days.Single(d => d.ID_Day == id);
            return View(day);
        }

        //
        // GET: /Plan/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Plan/Create

        [HttpPost]
        public ActionResult Create(Day day)
        {
            if (ModelState.IsValid)
            {
                db.Days.AddObject(day);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(day);
        }
        
        //
        // GET: /Plan/Edit/5
 
        public ActionResult Edit(int id)
        {
            Day day = db.Days.Single(d => d.ID_Day == id);
            return View(day);
        }

        //
        // POST: /Plan/Edit/5

        [HttpPost]
        public ActionResult Edit(Day day)
        {
            if (ModelState.IsValid)
            {
                db.Days.Attach(day);
                db.ObjectStateManager.ChangeObjectState(day, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(day);
        }

        //
        // GET: /Plan/Delete/5
 
        public ActionResult Delete(int id)
        {
            Day day = db.Days.Single(d => d.ID_Day == id);
            return View(day);
        }

        //
        // POST: /Plan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Day day = db.Days.Single(d => d.ID_Day == id);
            db.Days.DeleteObject(day);
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