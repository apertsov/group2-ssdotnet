using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using PagedList;

namespace DiagnosticCenter.Controllers
{ 
    public class CabinetsController : Controller
    {
        private DiagnosticsDBModelContainer Cabinets_db = new DiagnosticsDBModelContainer();

        public ViewResult Index(int? page)
        {

            var pat = from p in Cabinets_db.Cabinets
                      select p;
            pat = pat.OrderBy(item => item.Number);
            int pageIndex = (page ?? 1);
            return View(pat.ToPagedList(pageIndex, 5));
        }

        public ViewResult Details(int id)
        {
            Cabinet cabinet = Cabinets_db.Cabinets.Single(c => c.ID_Cabinet == id);
            return View(cabinet);
        }

        public ActionResult Create()
        {
            List<Department> dept = Cabinets_db.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            ViewBag.Dept = _dept;
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                cabinet.ID_Dept = Convert.ToInt32(Request.Form["Dept"]);
                Cabinets_db.Cabinets.AddObject(cabinet);
                Cabinets_db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(cabinet);
        }
 
        public ActionResult Edit(int id)
        {
            Cabinet cabinet = Cabinets_db.Cabinets.Single(c => c.ID_Cabinet == id);
            return View(cabinet);
        }

        [HttpPost]
        public ActionResult Edit(Cabinet cabinet)
        {
            if (ModelState.IsValid)
            {
                Cabinets_db.Cabinets.Attach(cabinet);
                Cabinets_db.ObjectStateManager.ChangeObjectState(cabinet, EntityState.Modified);
                Cabinets_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cabinet);
        }

        public ActionResult Delete(int id)
        {
            IQueryable<Cabinet> query = from cabinet in Cabinets_db.Cabinets
                                        where cabinet.ID_Cabinet == id
                                        select cabinet;

            foreach (var pat in query)
            {
                Cabinets_db.Cabinets.DeleteObject(pat);
            }
            Cabinets_db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Cabinets_db.Dispose();
            base.Dispose(disposing);
        }
    }
}