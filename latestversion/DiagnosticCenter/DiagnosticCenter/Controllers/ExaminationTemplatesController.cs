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
    public class ExaminationTemplatesController : Controller
    {
        private DiagnosticsDBEntities db = new DiagnosticsDBEntities();

        //
        // GET: /ExaminationTemplates/

        public ViewResult Index()
        {
            var examinationtemplates = db.ExaminationTemplates.Include("ExaminationType").Include("Employee");
            return View(examinationtemplates.ToList());
        }

        //
        // GET: /ExaminationTemplates/Details/5

        public ViewResult Details(int id)
        {
            ExaminationTemplate examinationtemplate = db.ExaminationTemplates.Single(e => e.Id == id);
            return View(examinationtemplate);
        }

        //
        // GET: /ExaminationTemplates/Create

        public ActionResult Create()
        {
            ViewBag.ExaminationTypeID_ExmType = new SelectList(db.ExaminationTypes, "ID_ExmType", "Name");
            ViewBag.EmployeeID_Employee = new SelectList(db.Employees, "ID_Employee", "Category");
            return View();
        } 

        //
        // POST: /ExaminationTemplates/Create

        [HttpPost]
        public ActionResult Create(ExaminationTemplate examinationtemplate)
        {
            if (ModelState.IsValid)
            {
                MembershipUser currUser = Membership.GetUser();
                Employee employee = db.Employees.Where(e => e.ID_User == (int)currUser.ProviderUserKey).First();
                
                //temporary section start
                //if (employee == null)
                //    employee = db.Employees.First();
                if (employee == null)
                    throw new Exception("З юзером щось не добре!!!!");
                //temporary section end


                examinationtemplate.Employee = employee;

                db.ExaminationTemplates.AddObject(examinationtemplate);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ExaminationTypeID_ExmType = new SelectList(db.ExaminationTypes, "ID_ExmType", "Name", examinationtemplate.ExaminationTypeID_ExmType);
                                   
            ViewBag.EmployeeID_Employee = new SelectList(db.Employees, "ID_Employee", "Category", examinationtemplate.EmployeeID_Employee);
            return View(examinationtemplate);
        }

        public JsonResult GetTemplate(int id)
        {
            JsonResult result = new JsonResult();

            var template = db.ExaminationTemplates.Where(e => e.Id == id).First();
            string templateText = template.Body;

            result.Data = templateText;           
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        
        //
        // GET: /ExaminationTemplates/Edit/5
 
        public ActionResult Edit(int id)
        {
            ExaminationTemplate examinationtemplate = db.ExaminationTemplates.Single(e => e.Id == id);
            ViewBag.ExaminationTypeID_ExmType = new SelectList(db.ExaminationTypes, "ID_ExmType", "Name", examinationtemplate.ExaminationTypeID_ExmType);
            ViewBag.EmployeeID_Employee = new SelectList(db.Employees, "ID_Employee", "Surname", examinationtemplate.EmployeeID_Employee);
            return View(examinationtemplate);
        }

        //
        // POST: /ExaminationTemplates/Edit/5

        [HttpPost]
        public ActionResult Edit(ExaminationTemplate examinationtemplate)
        {
            if (ModelState.IsValid)
            {
                db.ExaminationTemplates.Attach(examinationtemplate);
                db.ObjectStateManager.ChangeObjectState(examinationtemplate, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExaminationTypeID_ExmType = new SelectList(db.ExaminationTypes, "ID_ExmType", "Name", examinationtemplate.ExaminationTypeID_ExmType);
            ViewBag.EmployeeID_Employee = new SelectList(db.Employees, "ID_Employee", "Surname", examinationtemplate.EmployeeID_Employee);
            return View(examinationtemplate);
        }

        //
        // GET: /ExaminationTemplates/Delete/5
 
        public ActionResult Delete(int id)
        {
            ExaminationTemplate examinationtemplate = db.ExaminationTemplates.Single(e => e.Id == id);
            return View(examinationtemplate);
        }

        //
        // POST: /ExaminationTemplates/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ExaminationTemplate examinationtemplate = db.ExaminationTemplates.Single(e => e.Id == id);
            db.ExaminationTemplates.DeleteObject(examinationtemplate);
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