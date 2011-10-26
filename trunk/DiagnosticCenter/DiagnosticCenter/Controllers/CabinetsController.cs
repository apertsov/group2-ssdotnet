using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using PagedList;

namespace DiagnosticCenter.Controllers
{ 
    public class CabinetsController : Controller
    {
        private DiagnosticsDBModelContainer Cabinets_db = new DiagnosticsDBModelContainer();

        public ActionResult Index(string searchString, int? page)
        {
            int itemsOnPage = 5; //TODO Read from Parameters table
            
            int cabNo = 0;
            IOrderedQueryable<Cabinet> cab = Cabinets_db.Cabinets.OrderBy(c => c.Number); ;
            if (int.TryParse(searchString, out cabNo))
                cab = Cabinets_db.Cabinets.Where(c => c.Number == cabNo).OrderBy(c => c.Number);
 
            int pageIndex = (page ?? 1);
            return View(cab.ToPagedList(pageIndex, itemsOnPage));
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

            foreach (var cab in query)
            {
                Cabinets_db.Cabinets.DeleteObject(cab);
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