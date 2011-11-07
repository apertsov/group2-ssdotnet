using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using PagedList;

namespace DiagnosticCenter.Controllers
{
    /// <summary>
    ///  Контроллер описує функціональну частину системи для
    ///  керування записами про пацієнтів діагностичного центру  
    /// </summary>
    /// <param name="context">Екземпляр моделі бази даних</param>
    public class PatientsController : Controller
    {
        private DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();

        /// <summary>
        /// Action для сторінки з відображенням списку пацієнтів
        /// </summary>
        /// <param name="sortOrder">Порядок сортування</param>
        /// <param name="searchString">Стрічка з даними для пошуку</param>
        /// <param name="page">Індикатор сторінки для Paging</param>
        /// <returns>View зі списком пацієнтів</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.Title = TitleRes.TitleStrings.IndexTitlePat;
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
            return View(patients.ToPagedList(pageIndex, 5));
        }

        /// <summary>
        /// Action для сторінки створення нового пацієнта
        /// </summary>
        /// <returns>View з формою для створення</returns>
        [Authorize(Roles = "MedicalRegistrar")]
        public ActionResult Create()
        {
            ViewBag.Title = TitleRes.TitleStrings.AddTitlePat;
            return View();
        }

        /// <summary>
        /// Action обробки вхідних даних з форми створення пацієнта
        /// </summary>
        /// <remarks>Метод заповнює основні дані в таблицю бази даних,
        /// а також відправляє електронний лист на пошту пацієнта
        /// із даними для авторизації
        /// </remarks>
        /// <param name="new_Patient">Новий екземпляр моделі<c>Patient</c></param>
        /// <returns>Перехід на<c>Index</c>Action або View для повторного вводу даних</returns>
        [HttpPost]
        public ActionResult Create(Patient new_Patient)
        {
            Classes.MailSender sender = new Classes.MailSender();
            if (!ModelState.IsValid)
                return View();
            new_Patient.Password = sender.GeneratePassword();
            context.AddToPatients(new_Patient);
            context.SaveChanges();
            if (new_Patient.Email != "")
                sender.SendPassword(new_Patient.Email, null);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action для видалення пацієнта
        /// </summary>
        /// <param name="id">Id пацієнта</param>
        /// <returns>Перехід на Index Action</returns>
        [Authorize(Roles = "DepartmentChiefDoctor,Doctor")]
        public ActionResult Delete(int id)
        {
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            context.Patients.DeleteObject(patient);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action для сторінки з відображенням детальної інформації про пацієнта
        /// </summary>
        /// <param name="id">Id пацієнта</param>
        /// <returns>View із детальною інформацією</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ActionResult Details(int id)
        {
            ViewBag.Title = TitleRes.TitleStrings.DetailsTitle;
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            return View(patient);
        }

        /// <summary>
        /// Action для сторінки редагування інформації пацієнта
        /// </summary>
        /// <param name="id">Id пацієнта</param>
        /// <returns>View з формою редагування інформації</returns>
        [Authorize(Roles = "DepartmentChiefDoctor,Doctor,MedicalRegistrar")]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = TitleRes.TitleStrings.EditTitle;
            Patient patient = context.Patients.Where(p => p.ID_Patient == id).First();
            return View(patient);
        }

        /// <summary>
        /// Action обробки введеної інформації з форми для редагування
        /// </summary>
        /// <param name="edit_patient">Екземпляр Patient для редагування</param>
        /// <returns>Перехід на Index Action</returns>
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

        /// <summary>
        /// Action для сторінки пошуку пацієнта
        /// </summary>
        /// <returns>Delete View</returns>

        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ActionResult Search()
        {
            ViewBag.Title = TitleRes.TitleStrings.SearchTitle;
            return View();
        }

        /// <summary>
        /// Обробка інформації для пошуку
        /// </summary>
        /// <param name="id">Id пацієнта</param>
        /// <returns>View із результатом пошуку</returns>
        [HttpPost]
        public ActionResult Search(int? id)
        {
            IQueryable<Patient> patient = context.Patients;
            string temp = "";
            if (Request.Form["firstname"].ToString().Trim() != "")
            {
                temp = Request.Form["firstname"].ToString();
                patient = patient.Where(p => p.FirstName.Contains(temp));
            }
            if (Request.Form["surname"].ToString().Trim() != "")
            {
                temp = Request.Form["surname"].ToString();
                patient = patient.Where(p => p.Surname.Contains(temp));
            }
            if (Request.Form["city"].ToString().Trim() != "")
            {
                temp = Request.Form["city"].ToString();
                patient = patient.Where(p => p.City.Contains(temp));
            }
            if (Request.Form["address"].ToString().Trim() != "")
            {
                temp = Request.Form["address"].ToString();
                patient = patient.Where(p => p.Address.Contains(temp));
            }
            if (Request.Form["phone"].ToString().Trim() != "")
            {
                temp = Request.Form["phone"].ToString();
                patient = patient.Where(p => p.Phone.Contains(temp));
            }
            if (Request.Form["email"].ToString().Trim() != "")
            {
                temp = Request.Form["email"].ToString();
                patient = patient.Where(p => p.Email.Contains(temp));
            }
            if (Request.Form["optional"] != null)
            {
                if (Request.Form["worker"] != null)
                    patient = patient.Where(p => p.Workplace == true);
                else
                    patient = patient.Where(p => p.Workplace == false);
                if (Request.Form["civil"] != null)
                    patient = patient.Where(p => p.Civil_Servant == true);
                else
                    patient = patient.Where(p => p.Civil_Servant == false);
            }
            patient = patient.OrderBy(p => p.FirstName);
            return View("Index", patient.ToPagedList(1, 5));
        }

        /// <summary>
        /// Action для переходу на сторінку виписування направлення
        /// </summary>
        /// <param name="id">Id пацієнта</param>
        /// <returns>Перехід на Referral/Index Action</returns>
        public ActionResult GetReferral(int id)
        {
            TempData["id"] = id.ToString();
            return RedirectToAction("Index", "Referral");
        }
    }
}
