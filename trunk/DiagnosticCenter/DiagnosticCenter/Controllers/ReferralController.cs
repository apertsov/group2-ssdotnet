using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
namespace DiagnosticCenter.Controllers
{
    public class ReferralController : Controller
    {

        DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
        ReferralVM model = new ReferralVM();
        
        public ActionResult Index()
        {
            
            
            model.SetModel(Convert.ToInt32(TempData["id"]));
            ViewData["IDP"] = TempData["id"];
            return View(model);
        }
        [HttpPost]
        public ViewResult Index(ReferralVM r)
        {

            if (!ModelState.IsValid)
            {
                model.SetModel(Convert.ToInt32(Request.Form[6]));
                return View(model);
            }
            Referral new_ref = new Referral();
            
            new_ref.CreationDate = DateTime.Parse(Request.Form[5]);
            new_ref.VisitDate = DateTime.Parse(Request.Form[4]);
            new_ref.ID_Employee = Convert.ToInt32(Request.Form[3]);
            new_ref.ID_Patient = Convert.ToInt32(Request.Form[6]);
            context.AddToReferrals(new_ref);
            context.SaveChanges();

            Employee _empl = context.Employees.Where(e => e.ID_Employee == new_ref.ID_Employee).First();
            ViewBag.patient = Request.Form[0];
            ViewBag.department = Request.Form[1];
            ViewBag.cabinet = Request.Form[2];
            ViewBag.employee = _empl.FirstName + " " + _empl.Surname;
             return View("Print", r);
        }
       
        public JsonResult Cabinet(string deptName)
        {
            JsonResult result = new JsonResult();
            List<Cabinet> cab = context.Cabinets.Include("Department").ToList();
            IEnumerable<SelectListItem> _cab = cab.Where(e => e.Department.Name == deptName).Select(e => new SelectListItem
                             {
                                 Text = e.Number.ToString(),
                                 Value = e.Number.ToString()
                             });

            List<SelectListItem> c = _cab.ToList();
            SelectListItem i = new SelectListItem();
            i.Text = "--Кабінет--";
            i.Value = "0";
            c.Insert(0, i);
            result.Data = c;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public JsonResult Employee(string cabName)
        {
            JsonResult result = new JsonResult();
            List<Employee> empl = context.Employees.Include("Cabinet").ToList();
            IEnumerable<SelectListItem> _empl = empl.Where(e => e.Cabinet.Number == Convert.ToInt32(cabName)).Select(e => new SelectListItem
            {
                Text = e.FirstName + " " + e.Surname,
                Value = e.ID_Employee.ToString()
            });

            List<SelectListItem> c = _empl.ToList();
            SelectListItem i = new SelectListItem();
            i.Text = "--Лікар--";
            i.Value = "0";
            c.Insert(0, i);
            result.Data = c;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
     
    }
}
