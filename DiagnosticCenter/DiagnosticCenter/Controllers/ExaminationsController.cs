using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using System.Web.Security;

namespace DiagnosticCenter.Controllers
{ 
    public class ExaminationsController : Controller
    {
        private DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();

        //
        // GET: /Examinations/

        public ViewResult Index(int? id)
        {
            int index = (id ?? -1);
            Patient patient = db.Patients.Where(p => p.ID_Patient == index).First();
            ViewBag.Patient = patient;

            var examinations = db.Examinations.Include("Employee").Include("Patient").Include("ExaminationType").Where(e => e.Patient.ID_Patient == index);
            return View(examinations.ToList());
        }

        //
        // GET: /Examinations/Details/5

        public ViewResult Details(int id)
        {
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            return View(examination);
        }

        //
        // GET: /Examinations/Create

        public ActionResult Create(int? id)
        {
            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Surname");
            //ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName");
            ViewBag.ID_ExmType = new SelectList(db.ExaminationTypes, "ID_ExmType", "Name");

            MembershipUser currUser = Membership.GetUser();
            Employee currEmployee = db.Employees.Include("Department").Where(e => e.ID_User == (Guid)currUser.ProviderUserKey).First();
            
            //temporary section start
            //if (employee == null)
            //    employee = db.Employees.First();
            if (currEmployee == null)
                throw new Exception("З юзером щось не добре!!!!");
            //temporary section end
            ViewBag.Employee = currEmployee;

            int pId = (id ?? -1);
            Patient patient = db.Patients.Where(p => p.ID_Patient == id).First();
            if (patient == null)
                throw new Exception("З пацієнтом щось не добре!!!!");
            ViewBag.Patient = patient;
            
            // всі паблік шаблони для відділу і власні шаблони
            List<ExaminationTemplate> _templ = db.ExaminationTemplates.Include("Employee").
                        Where(e => (e.Employee.ID_Dept == currEmployee.Department.ID_Dept && e.IsPrivate == false)
                                || (e.Employee.ID_Employee == currEmployee.ID_Employee && e.IsPrivate == true)).ToList();
            IEnumerable<SelectListItem> templates = _templ.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name + (e.IsPrivate ? " [yours private]" : "")});
            ViewBag.Templ = templates;
            return View();
        } 

        //
        // POST: /Examinations/Create

        [HttpPost]
        public ActionResult Create(Examination examination)
        {
            if (ModelState.IsValid)
            {
                int patientId = Int32.Parse(Request.Form.Get("patientID"));
                Patient patient = db.Patients.Where(p => p.ID_Patient == patientId).First();
                examination.Patient = patient;

                MembershipUser currUser = Membership.GetUser();
                Employee employee = db.Employees.Where(e => e.ID_User == (Guid)currUser.ProviderUserKey).First();
                examination.Employee = employee;

                ExaminationType examType = db.ExaminationTypes.Where(e => e.ID_ExmType == examination.ID_ExmType).First();
                examination.ExaminationType = examType;

                db.Examinations.AddObject(examination);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = patientId });  
            }

            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Surname", examination.ID_Employee);
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName", examination.ID_Patient);
            ViewBag.ID_ExmType = new SelectList(db.ExaminationTypes, "ID_ExmType", "Name");
            return View(examination);
        }
        
        //
        // GET: /Examinations/Edit/5
 
        public ActionResult Edit(int id)
        {
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Category", examination.ID_Employee);
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName", examination.ID_Patient);
            return View(examination);
        }

        //
        // POST: /Examinations/Edit/5

        [HttpPost]
        public ActionResult Edit(Examination examination)
        {
            if (ModelState.IsValid)
            {
                db.Examinations.Attach(examination);
                db.ObjectStateManager.ChangeObjectState(examination, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Employee = new SelectList(db.Employees, "ID_Employee", "Category", examination.ID_Employee);
            ViewBag.ID_Patient = new SelectList(db.Patients, "ID_Patient", "FirstName", examination.ID_Patient);
            return View(examination);
        }

        //
        // GET: /Examinations/Delete/5
 
        public ActionResult Delete(int id)
        {
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            return View(examination);
        }

        //
        // POST: /Examinations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Examination examination = db.Examinations.Single(e => e.ID_Examination == id);
            db.Examinations.DeleteObject(examination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}