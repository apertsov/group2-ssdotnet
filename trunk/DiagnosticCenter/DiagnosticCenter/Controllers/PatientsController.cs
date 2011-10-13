using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using PagedList;

namespace DiagnosticCenter.Controllers
{
    public class PatientsController : Controller
    {
        
        private DiagnosticsDBModelContainer _patients = new DiagnosticsDBModelContainer();
       
        
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName desc" : "";
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Surname desc" : "";
            ViewBag.BirthDateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            var pat = from p in _patients.Patients
                      select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                pat = pat.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Surname desc":
                    pat = pat.OrderByDescending(s => s.Surname);
                    break;
                case "Date":
                    pat = pat.OrderBy(s => s.BirthDate);
                    break;
                case "Date desc":
                    pat = pat.OrderByDescending(s => s.BirthDate);
                    break;
                case "FirstName desc":
                    pat = pat.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    pat = pat.OrderBy(s => s.FirstName);
                    break;
            } 
            
            int pageIndex = (page ?? 1); 
    
            return View(pat.ToPagedList(pageIndex,5));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient new_Patient)
        {
            Classes.MailSender sender = new Classes.MailSender();
           
 
            if (!ModelState.IsValid) return View();
                  
            new_Patient.Password = sender.GeneratePassword();
            _patients.AddToPatients(new_Patient);
            _patients.SaveChanges();
            sender.SendPassword(new_Patient.Email);
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
        public ActionResult Search(Patient s_pat)
        {
            var pat = from p in _patients.Patients
                      select p;
           
            if (Request.Form["FirstName"].ToString().Trim() != "")
                pat = pat.Where(p => p.FirstName.Contains(s_pat.FirstName));
            if (Request.Form["Surname"].ToString().Trim() != "")
                pat = pat.Where(p => p.Surname.Contains(s_pat.Surname));
            if (Request.Form["BirthDate"].ToString().Trim() != "")
                pat = pat.Where(p => p.BirthDate == s_pat.BirthDate);
            pat = pat.Where(p => p.Sex == s_pat.Sex);
            if (Request.Form["City"].ToString().Trim() != "")
                pat = pat.Where(p => p.City.Contains(s_pat.City));
            if (Request.Form["Address"].ToString().Trim() != "")
                pat = pat.Where(p => p.Address.Contains(s_pat.Address));
            if (Request.Form["Phone"].ToString().Trim() != "")
                pat = pat.Where(p => p.Phone.Contains(s_pat.Phone));
            if (Request.Form["Email"].ToString().Trim() != "")
                pat = pat.Where(p => p.Email.Contains(s_pat.Email));
            
            pat = pat.OrderBy(p => p.FirstName);
           
            return View("Index",pat.ToPagedList(1,5));
        }

      
    }
}
