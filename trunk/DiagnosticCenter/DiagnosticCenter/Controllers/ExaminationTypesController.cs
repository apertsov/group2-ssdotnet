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
    public class ExaminationTypesController : Controller
    {
        private DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();

        //
        // GET: /ExaminationTypes/

        public ViewResult Index()
        {
            return View(db.ExaminationTypes.ToList());
        }

        //
        // GET: /ExaminationTypes/Details/5

        public ViewResult Details(int id)
        {
            ExaminationType examinationtype = db.ExaminationTypes.Single(e => e.ID_ExmType == id);
            return View(examinationtype);
        }

        //
        // GET: /ExaminationTypes/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ExaminationTypes/Create

        [HttpPost]
        public ActionResult Create(ExaminationType examinationtype)
        {
            if (ModelState.IsValid)
            {
                db.ExaminationTypes.AddObject(examinationtype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(examinationtype);
        }
        
        //
        // GET: /ExaminationTypes/Edit/5
 
        public ActionResult Edit(int id)
        {
            ExaminationType examinationtype = db.ExaminationTypes.Single(e => e.ID_ExmType == id);
            return View(examinationtype);
        }

        //
        // POST: /ExaminationTypes/Edit/5

        [HttpPost]
        public ActionResult Edit(ExaminationType examinationtype)
        {
            if (ModelState.IsValid)
            {
                db.ExaminationTypes.Attach(examinationtype);
                db.ObjectStateManager.ChangeObjectState(examinationtype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examinationtype);
        }

        //
        // GET: /ExaminationTypes/Delete/5
 
        public ActionResult Delete(int id)
        {
            ExaminationType examinationtype = db.ExaminationTypes.Single(e => e.ID_ExmType == id);
            return View(examinationtype);
        }

        //
        // POST: /ExaminationTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ExaminationType examinationtype = db.ExaminationTypes.Single(e => e.ID_ExmType == id);
            db.ExaminationTypes.DeleteObject(examinationtype);
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