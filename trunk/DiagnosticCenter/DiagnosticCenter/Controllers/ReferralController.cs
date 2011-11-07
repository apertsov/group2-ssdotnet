using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
namespace DiagnosticCenter.Controllers
{
    /// <summary>
    /// Контроллер для створення направлення пацієнта
    /// </summary>
    /// <param name="context">Екземпляр моделі бази даних</param>
    /// <param name="model">Еземпляр класу <c>ReferralVM</c></param>
    public class ReferralController : Controller
    {
        DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
        ReferralVM model = new ReferralVM();

        /// <summary>
        /// Action для сторінки виписування направлення
        /// </summary>
        /// <param name="id">Id пацієнта</param>
        /// <returns>Index View із формою написання направлення</returns>
        [Authorize(Roles = "DepartmentChiefDoctor,Doctor,MedicalRegistrar")]
        public ActionResult Index(int id)
        {
            ViewBag.Title = TitleRes.TitleStrings.IndexTitleRef;
            model.SetModel(id);
            ViewData["IDP"] = id;
            return View(model);
        }

        /// <summary>
        /// Action обробки введеної інформації та перехід на роздруковування
        /// </summary>
        /// <param name="r">Екземпляр моделі <c>ReferralVM</c></param>
        /// <returns>Index View або Print View</returns>
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
            Employee _empl = context.Employees.Where(e => e.ID_Employee == new_ref.ID_Employee).First();
            Department _d  = context.Departments.Where(d => d.ID_Dept == _empl.ID_Dept).First();
            new_ref.ID_Dept = _d.ID_Dept;
            context.AddToReferrals(new_ref);
            context.SaveChanges();
            ViewBag.patient = Request.Form[0];
            ViewBag.department = Request.Form[1];
            ViewBag.cabinet = Request.Form[2];
            ViewBag.employee = _empl.FirstName + " " + _empl.Surname;
            return View("Print", r);
        }
       
        /// <summary>
        /// Action для створення списку кабінетів
        /// </summary>
        /// <param name="deptName">Назва відділення для списку</param>
        /// <returns>Створений список</returns>
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
            i.Text = ReferralRes.ReferralStrings.ChooseCab;
            i.Value = "0";
            c.Insert(0, i);
            result.Data = c;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        /// <summary>
        /// Action для створення списку працівників 
        /// </summary>
        /// <param name="cabName">Номер кабінету</param>
        /// <returns>Створений список</returns>
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
            i.Text =ReferralRes.ReferralStrings.ChooseDoctor;
            i.Value = "0";
            c.Insert(0, i);
            result.Data = c;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
     
    }
}
