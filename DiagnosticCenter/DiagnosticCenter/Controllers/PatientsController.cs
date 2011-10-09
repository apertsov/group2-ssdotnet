using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{
    public class PatientsController : Controller
    {
        private DiagnosticsDBModelContainer _patients = new DiagnosticsDBModelContainer();
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient new_Patient)
        {
            _patients.AddToPatients(new_Patient);
            _patients.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            return View(_patients.Patients.ToList());
        }
    }
}
