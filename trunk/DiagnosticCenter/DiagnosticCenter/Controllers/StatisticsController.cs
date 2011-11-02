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

            
            return View(model);
        }

        public ActionResult PieChart()
        {
            List<Department> dept = context.Departments.ToList();
            List<int> c_dept = new List<int>();
            foreach (Department d in dept)
            {
                int sum = 0;
                List<Employee> empl = context.Employees.Where(e => e.ID_Dept == d.ID_Dept).ToList();
                foreach (Employee e in empl)
                {
                    sum += e.Referral.Count();
                }
                c_dept.Add(sum);
            }
            List<string> str_dept = new List<string>();
            foreach( Department d in dept)
                str_dept.Add(d.Name);

            var key = new Chart(width: 500, height: 200, theme: ChartTheme.Blue).AddTitle("Кількість пацієнтів у відділах за останній місяць")
                .AddLegend("Відділення")
                .AddSeries(
                   chartType: "pie",
                   xValue: str_dept,
                   yValues: c_dept )
               .Write();

            return null;
        }

        public ActionResult ColumnChart()
        {
            
            List<int> c_reff = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                DateTime mnth = new DateTime(2011, i, 10);
                int reff = context.Referrals.Where(item => item.CreationDate.Month == mnth.Month).Count();
                c_reff.Add(reff);
            }
            var key = new Chart(width: 500, height: 200, theme: ChartTheme.Blue).AddTitle("Кількість пацієнтів по місяцях")
                .AddSeries(
                   chartType: "column",
                   xValue: new[] { "Січ", "Лют", "Бер", "Кві", "Тра","Чер", "Лип", "Сер", "Вер", "Жов", "Лис", "Гру" },
                   yValues: c_reff)
               .Write();
            return null;
        }

        public ActionResult StatPage()
        {
            ViewBag.PatientsToday = context.Referrals.Where(i => i.CreationDate.Day == DateTime.Now.Day).Count();
            ViewBag.PatientsMonth = context.Referrals.Where(i => i.CreationDate.Month == DateTime.Now.Month).Count();
            ViewBag.PatientsTotal = context.Referrals.Count();
            return View();
        }
    }

}
