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
            return View(model);
        }
        [HttpPost]
        public ViewResult Index(ReferralVM r)
        {

            ViewBag.department = Request.Form[1];
            ViewBag.cabinet = Request.Form[2];
            ViewBag.employee = Request.Form[3];
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
            
            result.Data = _cab.ToList();
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
                Value = e.FirstName + " " + e.Surname
            });

            result.Data = _empl.ToList();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
     
    }
}
