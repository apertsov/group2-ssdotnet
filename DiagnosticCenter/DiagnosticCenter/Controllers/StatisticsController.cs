using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using System.Web.Security;
using System.Web.Profile;
using System.Web.Helpers;
using System.Web.ApplicationServices;



namespace DiagnosticCenter.Controllers
{
    /// <summary>
    /// Контроллер описує функціональну частину 
    /// для підрахування та виводу статистики і профілю користувача
    /// </summary>
    public class StatisticsController : Controller
    {
        DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
        
        /// <summary>
        /// Вивід сторінки профілю користувача
        /// </summary>
        /// <returns>Index View</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ActionResult Index()
        {
            ViewBag.Title = TitleRes.TitleStrings.ProfileTitle;
            MembershipUser current = Membership.GetUser(User.Identity.Name);
            EmployeeVM model = new EmployeeVM();
            Guid c =  (Guid)current.ProviderUserKey;
            Employee empl = context.Employees.Include("Department").Include("Cabinet").Where(i => i.ID_User == c).First();
            model.SetModel(empl);
            return View(model);
        }
        
        /// <summary>
        /// Зміна паролю
        /// </summary>
        /// <param name="id">Id працівника</param>
        /// <returns>View із результатом зміни</returns>
        [HttpPost]
        public ActionResult Index(int? id)
        {
            MembershipUser user = Membership.GetUser();
            string oldPass = Request.Form["oldPass"].ToString();
            string newPass = Request.Form["newPass"].ToString();
            string confirmPass = Request.Form["confirmPass"].ToString();
            string pass = user.GetPassword();
              
            if ((Membership.ValidateUser(user.UserName,oldPass)) && (newPass == confirmPass))
              
                    {
                        user.ChangePassword(oldPass, newPass);
                        ViewBag.Error = ViewRes.StatisticsStrings.Applied;
                        return View();
                    }
                else
                {
                    ViewBag.Error = ViewRes.StatisticsStrings.NotApplied;
                    return View();
                }
            
        }

        /// <summary>
        /// Створення діаграми для статистики по відділеннях
        /// </summary>
        public void PieChart()
        {
            List<Department> dept = context.Departments.ToList();
            List<int> c_dept = new List<int>();
            List<Referral> reff = context.Referrals.ToList();
            foreach (Department d in dept)
            {
                int sum = reff.Where(i => i.CreationDate.Month == DateTime.Now.Month).Where(i => i.ID_Dept == d.ID_Dept).Count();
                c_dept.Add(sum);
            }
            List<string> str_dept = new List<string>();
            foreach( Department d in dept)
                str_dept.Add(d.Name);

            var key = new Chart(width: 500, height: 200, theme: ChartTheme.Blue).AddTitle(ViewRes.StatisticsStrings.TitlePieChart)
                .AddLegend(ViewRes.StatisticsStrings.LegendPieChart)
                .AddSeries(
                   chartType: "pie",
                   xValue: str_dept,
                   yValues: c_dept )
               .Write();
        }
        /// <summary>
        /// Створення діаграми для загальної статистики по пацієнтах
        /// </summary>
        /// <returns></returns>
        public ActionResult ColumnChart()
        {
            
            List<int> c_reff = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                DateTime mnth = new DateTime(2011, i, 10);
                int reff = context.Referrals.Where(item => item.CreationDate.Month == mnth.Month).Count();
                c_reff.Add(reff);
            }
            var key = new Chart(width: 500, height: 200, theme: ChartTheme.Blue).AddTitle(ViewRes.StatisticsStrings.TitleColumnChart)
                .AddSeries(
                   chartType: "column",
                   xValue: new[] { ViewRes.StatisticsStrings.Jan, ViewRes.StatisticsStrings.Feb, ViewRes.StatisticsStrings.Mar, 
                                   ViewRes.StatisticsStrings.Apr, ViewRes.StatisticsStrings.May,ViewRes.StatisticsStrings.Jun, 
                                   ViewRes.StatisticsStrings.Jul, ViewRes.StatisticsStrings.Aug, ViewRes.StatisticsStrings.Sep, 
                                   ViewRes.StatisticsStrings.Oct, ViewRes.StatisticsStrings.Nov, ViewRes.StatisticsStrings.Dec },
                   yValues: c_reff)
               .Write();
            return null;
        }

        /// <summary>
        /// Сторінка статистики
        /// </summary>
        /// <returns>View на сторінку статистики</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ActionResult StatPage()
        {
            ViewBag.Title = TitleRes.TitleStrings.StatTitle;
            ViewBag.PatientsToday = context.Referrals.Where(i => i.CreationDate.Day == DateTime.Now.Day).Count();
            ViewBag.PatientsMonth = context.Referrals.Where(i => i.CreationDate.Month == DateTime.Now.Month).Count();
            ViewBag.PatientsTotal = context.Referrals.Count();
            return View();
        }
    }

}
