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
        
        DiagnosticsDBModelContainer _patients = new DiagnosticsDBModelContainer();


        public ViewResult Index()
        {
            return View(_patients.Patients.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient new_Patient)
        {
            if (!ModelState.IsValid) return View();


            _patients.AddToPatients(new_Patient);
            _patients.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            IQueryable<Patient> query = from patient in _patients.Patients
                                        where patient.ID_Patient == id
                                        select patient;

            foreach (var pat in query)
            {
                _patients.Patients.DeleteObject(pat);
            }
            _patients.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            IQueryable<Patient> query = from patient in _patients.Patients
                                        where patient.ID_Patient == id
                                        select patient;

            return View(query.ToList());
        }


        public ActionResult Edit(int id)
        {
            var edit_patient = (from p in _patients.Patients
                                where p.ID_Patient == id
                                select p).First();



            return View(edit_patient);
        }
        [HttpPost]
        public ActionResult Edit(Patient edit_patient)
        {
            var original_patient = (from p in _patients.Patients
                                    where p.ID_Patient == edit_patient.ID_Patient
                                    select p).First();
            if (!ModelState.IsValid)
                return View(original_patient);

            _patients.ApplyCurrentValues(original_patient.EntityKey.EntitySetName, edit_patient);
            _patients.SaveChanges();
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

            IQueryable<Patient> query = from p in _patients.Patients
                                        where p.Name.Contains(name)
                                        select p;
            List<Patient> pat = new List<Patient>();
            foreach (Patient p in query)
                pat.Add(p);
            return View(pat);
        }
    }
}
