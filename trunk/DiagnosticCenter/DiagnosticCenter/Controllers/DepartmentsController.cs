using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{
    public class DepartmentsController : Controller
    {
        //
        // GET: /Departments/

        public ActionResult Index()
        {
            return View( Departments.Instance.getList() );
        }
        public ActionResult Details(int id)
        {
            return View( Departments.Instance.getByID(id) );
        }
        /*----- CREATE -----*/
        public ActionResult Create()
        {
            return View( Department.CreateDepartment(0,"",""));
        }
        [HttpPost]
        public ActionResult Create( Department d )
        {
            if (!TryUpdateModel(d))
            {
                return View(d);
            }
            Departments.Instance.add( d );
            return View("Details", d);
        }
        /*----- DELETE -----*/
        public ActionResult Delete(int id)
        {
            return View( Departments.Instance.getByID(id) );
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection fc)
        {
            Departments.Instance.deleteByID(id);
            return RedirectToAction("Index");
        }
    }
}
