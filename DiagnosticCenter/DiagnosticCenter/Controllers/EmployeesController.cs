using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
namespace DiagnosticCenter.Controllers
{
    public class EmployeesController : Controller
    {
        DiagnosticsDBModelContainer _empl = new DiagnosticsDBModelContainer();

        public ActionResult Index()
        {
            return View(_empl.Employees.ToList());
        }

       
        public ActionResult Details(int id)
        {
            IQueryable<Employee> query = from empl in _empl.Employees
                                        where empl.ID_Employee == id
                                        select empl;

            return View(query.ToList());
        }

       

        public ActionResult Create()
        {
            return View();
        } 

        

        [HttpPost]
        public ActionResult Create(Employee new_empl)
        {
            if (!ModelState.IsValid) return View();


            _empl.AddToEmployees(new_empl);
            _empl.SaveChanges();

            return RedirectToAction("Index");
        }
        
        
        public ActionResult Edit(int id)
        {
            var edit_empl = (from p in _empl.Employees
                                where p.ID_Employee == id
                                select p).First();



            return View(edit_empl);
        }

        //
        // POST: /Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee edit_empl)
        {
            var original_empl = (from p in _empl.Employees
                                    where p.ID_Employee == edit_empl.ID_Employee
                                    select p).First();
            if (!ModelState.IsValid)
                return View(original_empl);

            _empl.ApplyCurrentValues(original_empl.EntityKey.EntitySetName, edit_empl);
            _empl.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Employees/Delete/5
 
        public ActionResult Delete(int id)
        {
            IQueryable<Employee> query = from empl in _empl.Employees
                                        where empl.ID_Employee == id
                                        select empl;

            foreach (var pat in query)
            {
                _empl.Employees.DeleteObject(pat);
            }
            _empl.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(int? id)
        {
            string name = ViewData["Name"].ToString();
            return RedirectToAction("SearchResult", name);
        }


        public ActionResult SearchResult(string name)
        {

            IQueryable<Employee> query = from p in _empl.Employees
                                        where p.Position.Contains(name)
                                        select p;
            List<Employee> pat = new List<Employee>();
            foreach (Employee p in query)
                pat.Add(p);
            return View(pat);
        }
    }
}
