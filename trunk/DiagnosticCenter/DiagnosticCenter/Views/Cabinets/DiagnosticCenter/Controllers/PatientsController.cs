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
            if (new_Patient.Workplace == null) new_Patient.Workplace = "ні";
            else new_Patient.Workplace = "так";
            if (new_Patient.Civil_Servant == null) new_Patient.Civil_Servant = "ні";
            else new_Patient.Civil_Servant = "так";
            _patients.AddToPatients(new_Patient);
            _patients.SaveChanges();
            return RedirectToAction("Index");
        }

       public ActionResult Index()
        {
            return View(_patients.Patients.ToList());
        }
        //
        // GET: /Patient/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

                     
        //
        // GET: /Patient/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Patient/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
 
         

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                  Patient del_patient = _patients.Patients.FirstOrDefault(p => p.ID_Patient == id);
                  _patients.Patients.DeleteObject(del_patient);

                
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
