using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using System.Web.Security;
using System.Web.Profile;
using System.Web.Helpers;



namespace DiagnosticCenter.Controllers
{
    public class StatisticsController : Controller
    {
        DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
        
        public ActionResult Index()
        {
            MembershipUser current = Membership.GetUser(User.Identity.Name);
            EmployeeVM model = new EmployeeVM();
            Guid c =  (Guid)current.ProviderUserKey;
            Employee empl = context.Employees.Include("Department").Include("Cabinet").Where(i => i.ID_User == c).First();
            model.SetModel(empl);

            ViewBag.PatientsToday = context.Referrals.Where(i => i.CreationDate.Day == DateTime.Now.Day).Count();
            ViewBag.PatientsMonth = context.Referrals.Where(i => i.CreationDate.Month == DateTime.Now.Month).Count();
            ViewBag.PatientsTotal = context.Referrals.Count();
            return View(model);
        }

        public ActionResult PieChart()
        {
            String dept = context.Departments.ToString();
            var key = new Chart(width: 500, height: 200, theme: ChartTheme.Blue).AddTitle("Кількість пацієнтів у відділах за останній місяць")
                .AddLegend("Відділення")
                .AddSeries(
                   chartType: "pie",
                   xValue: new[] {"відділ 1","відділ 2","відділ 3","відділ 4"},
                   yValues: new[] {"20","70","35","60"} )
               .Write();

            return null;
        }

        public ActionResult ColumnChart()
        {
            String dept = context.Departments.ToString();
            var key = new Chart(width: 500, height: 200, theme: ChartTheme.Blue).AddTitle("Кількість пацієнтів по місяцях")
                .AddSeries(
                   chartType: "column",
                   xValue: new[] { "Січ", "Лют", "Бер", "Кві", "Тра","Чер", "Лип", "Сер", "Вер", "Жов", "Лис", "Гру" },
                   yValues: new[] { "100", "270", "35", "160", "100", "270", "35", "160", "100", "270", "35", "160"})
               .Write();

            return null;
        }
    }
}
