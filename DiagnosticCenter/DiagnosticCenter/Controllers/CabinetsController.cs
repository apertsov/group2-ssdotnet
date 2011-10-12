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
    public class CabinetsController : Controller
    {
        private DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();

        //
        // GET: /Cabinets/

        //public ViewResult Index()
        //{
        //    return View(db.Cabinets.ToList());
        //}

        public ActionResult Index(int? page)
        {
            const int pageSize = 10;
            var cabinets = CabinetsDataAccess.GetPagedCabinets((page ?? 0) * pageSize, pageSize);
            ViewBag.HasPrevious = cabinets.HasPrevious;
            ViewBag.HasMore = cabinets.HasNext;
            ViewBag.CurrentPage = (page ?? 0);
            return View(cabinets.Entities);
        }

        //
        // GET: /Cabinets/Details/5

        public ViewResult Details(int id)
        {
            Cabinet cabinet = db.Cabinets.Single(c => c.ID_Cabinet == id);
            return View(cabinet);
        }

        //
        // GET: /Cabinets/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Cabinets/Create

        [HttpPost]
        public ActionResult Create(Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                db.Cabinets.AddObject(cabinet);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(cabinet);
        }
        
        //
        // GET: /Cabinets/Edit/5
 
        public ActionResult Edit(int id)
        {
            Cabinet cabinet = db.Cabinets.Single(c => c.ID_Cabinet == id);
            return View(cabinet);
        }

        //
        // POST: /Cabinets/Edit/5

        [HttpPost]
        public ActionResult Edit(Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                db.Cabinets.Attach(cabinet);
                db.ObjectStateManager.ChangeObjectState(cabinet, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cabinet);
        }

        //
        // GET: /Cabinets/Delete/5
 
        public ActionResult Delete(int id)
        {
            Cabinet cabinet = db.Cabinets.Single(c => c.ID_Cabinet == id);
            return View(cabinet);
        }

        //
        // POST: /Cabinets/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Cabinet cabinet = db.Cabinets.Single(c => c.ID_Cabinet == id);
            db.Cabinets.DeleteObject(cabinet);
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