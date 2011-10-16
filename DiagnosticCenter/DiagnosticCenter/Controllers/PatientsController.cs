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
        //контекст моделі
        private DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
       
        //-------Всі пацієнти-------//
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName desc" : "";
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Surname desc" : "";
            ViewBag.BirthDateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            IQueryable<Patient> patients = context.Patients;
            
            if (!String.IsNullOrEmpty(searchString))
                patients = patients.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()));
            switch (sortOrder)
            {
                case "Surname desc":
                    patients = patients.OrderByDescending(s => s.Surname);
                    break;
                case "Date":
                    patients = patients.OrderBy(s => s.BirthDate);
                    break;
                case "Date desc":
                    patients = patients.OrderByDescending(s => s.BirthDate);
                    break;
                case "FirstName desc":
                    patients = patients.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    patients = patients.OrderBy(s => s.FirstName);
                    break;
            } 
            int pageIndex = (page ?? 1); 
            return View(patients.ToPagedList(pageIndex,5));
        }
       
        //-------Створення пацієнта-------//
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient new_Patient)
        {
            Classes.MailSender sender = new Classes.MailSender();
            if (!ModelState.IsValid) 
                return View();
            new_Patient.Password = sender.GeneratePassword();
            context.AddToPatients(new_Patient);
            context.SaveChanges();
            if(new_Patient.Email != "")
            sender.SendPassword(new_Patient.Email);
            return RedirectToAction("Index");
        }
        
        //-------Видалення пацієнта-------//
        public ActionResult Delete(int id)
        {
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            context.Patients.DeleteObject(patient);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //-------Детальна інформація-------//
        public ActionResult Details(int id)
        {
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            return View(patient);
        }

        //-------Редагування інформації-------//
        public ActionResult Edit(int id)
        {
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            return View(patient);
        }
        
        [HttpPost]
        public ActionResult Edit(Patient edit_patient)
        {
            Patient original_patient = context.Patients.Where(p => p.ID_Patient == edit_patient.ID_Patient).First(); 
            if (!ModelState.IsValid)
                return View(original_patient);
            context.ApplyCurrentValues(original_patient.EntityKey.EntitySetName, edit_patient);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //------Пошук пацієнта-------//
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(Patient requestedPatient)
        {
            IQueryable<Patient> patient = context.Patients;
                      
            if (Request.Form["firstname"].ToString().Trim() != "")
                patient = patient.Where(p => p.FirstName.Contains(requestedPatient.FirstName));
            if (Request.Form["surname"].ToString().Trim() != "")
                patient = patient.Where(p => p.Surname.Contains(requestedPatient.Surname));
            if (Request.Form["birthdate"].ToString().Trim() != "")
                patient = patient.Where(p => p.BirthDate == requestedPatient.BirthDate);
            if (Request.Form["city"].ToString().Trim() != "")
                patient = patient.Where(p => p.City.Contains(requestedPatient.City));
            if (Request.Form["address"].ToString().Trim() != "")
                patient = patient.Where(p => p.Address.Contains(requestedPatient.Address));
            if (Request.Form["phone"].ToString().Trim() != "")
                patient = patient.Where(p => p.Phone.Contains(requestedPatient.Phone));
            if (Request.Form["email"].ToString().Trim() != "")
                patient = patient.Where(p => p.Email.Contains(requestedPatient.Email));
            if (Request.Form["optional"] != null)
            {
                patient = patient.Where(p => p.Workplace == requestedPatient.Workplace);
                patient = patient.Where(p => p.Civil_Servant == requestedPatient.Civil_Servant);
            }
            patient = patient.OrderBy(p => p.FirstName);
            return View("Index",patient.ToPagedList(1,5));
        }
        
        public ActionResult SetReferral(int id)
        {
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            ViewBag.PatName = patient.FirstName;
            ViewBag.PatSurname = patient.Surname;
            return View();
        }

        [HttpPost]
        public ActionResult SetReferral(Referral new_ref)
        {
               
            context.AddToReferrals(new_ref);
            context.SaveChanges();
           return View("~/Views/Shared/Print.chtml");
        }
    }
}
