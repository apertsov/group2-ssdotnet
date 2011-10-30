using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;
using System.Web.Security;
using System.Web.Profile;

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

    }
}
