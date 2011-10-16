using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DiagnosticCenter.Models;
using PagedList;


namespace DiagnosticCenter.Controllers
{
    public class EmployeesController : Controller
    {
        DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer(); //контекст моделі бд
        EmployeesVM model = new EmployeesVM(); //  ViewModel для відображення інформації працівників
        string[] r = { "Doctor", "HeadDoctor", "Nurse", "HeadNurse", "DepartmentChiefDoctor", "MedicalRegistrar" }; // масив ролей  
        
        //-------Всі працівники-------//
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            IQueryable<Employee> employee = context.Employees.Include("Department").Include("Cabinet");
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName desc" : "";
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Surname desc" : "";
            ViewBag.DepartmentSortParm = String.IsNullOrEmpty(sortOrder) ? "Department desc" : "";
            if (!String.IsNullOrEmpty(searchString))
            {
                employee = employee.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Surname desc":
                    employee = employee.OrderByDescending(s => s.Surname);
                    break;
                case "FirstName desc":
                    employee = employee.OrderByDescending(s => s.FirstName);
                    break;
                case "Department desc":
                    employee = employee.OrderByDescending(s => s.Department.Name);
                    break;
                default:
                    employee = employee.OrderBy(s => s.FirstName);
                    break;
            }
            model.FillModel(employee);
            int pageIndex = (page ?? 1);
            return View(model.lst.ToPagedList(pageIndex, 5));
        }

       //-------Детальна інформація-------//
        public ActionResult Details(int id)
        {
            IQueryable<Employee> employee = context.Employees.Include("Department").Where(i => i.ID_Employee == id);
            model.FillModel(employee);
            return View(model.lst.ToList());
        }

        //-------Створення працівників-------//
        public ActionResult Create()
        {   
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            List<Cabinet> cab = context.Cabinets.ToList(); 
            IEnumerable<SelectListItem> _cab = cab.Select(e => new SelectListItem { Value = e.ID_Cabinet.ToString(), Text = e.Number.ToString() });
            ViewBag.Depts = _dept;        
            ViewBag.Cabinets = _cab;                        
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Employee new_empl)
        {
            if (!ModelState.IsValid) return View();
            Classes.MailSender sender = new Classes.MailSender();
            
            string newUser = Request.Form["username"];
            string pass = sender.GeneratePassword();
           
            Membership.CreateUser(newUser,sender.GeneratePassword(), Request.Form["e-mail"]);
            string role = "";
            switch(Request.Form["position"])
            {
                case "лікар": role = r[0]; break;
                case "головний лікар": role = r[1]; break;
                case "медсестра": role = r[2]; break;
                case "головна медсестра": role = r[3]; break;
                case "зав. відділом": role = r[4]; break;
                case "реєстратура": role = r[5]; break;
            }
            Roles.AddUserToRole(newUser, role);
            
            MembershipUser curr_user = Membership.GetUser(newUser);
            new_empl.ID_User = (Guid)curr_user.ProviderUserKey;
            new_empl.ID_Dept = System.Convert.ToInt32(Request.Form["Depts"]);
            new_empl.ID_Cabinet = System.Convert.ToInt32(Request.Form["Cabinets"]);
            
            context.AddToEmployees(new_empl);
            context.SaveChanges();
            sender.SendPassword(curr_user.Email);
            return RedirectToAction("Index");
        }
        
        //-------Редагування інформації------//
        public ActionResult Edit(int id)
        {
            Employee empl = context.Employees.Include("Department").Where(i => i.ID_Employee == id).First();
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            List<Cabinet> cab = context.Cabinets.ToList();
            IEnumerable<SelectListItem> _cab = cab.Select(e => new SelectListItem { Value = e.ID_Cabinet.ToString(), Text = e.Number.ToString() });
            ViewBag.Depts = _dept;
            ViewBag.Cabinets = _cab;          
            return View(empl);
        }

        [HttpPost]
        public ActionResult Edit(Employee edit_empl)
        {
            Employee original_empl = context.Employees.Include("Department").Where(i => i.ID_Employee == edit_empl.ID_Employee).First();
                   
            if (!ModelState.IsValid)
                return View(original_empl);

            string role = "";
            switch (Request.Form["position"])
            {
                case "лікар": role = r[0]; break;
                case "головний лікар": role = r[1]; break;
                case "медсестра": role = r[2]; break;
                case "головна медсестра": role = r[3]; break;
                case "зав. відділом": role = r[4]; break;
                case "реєстратура": role = r[5]; break;
            }
            MembershipUser curr_user = Membership.GetUser(edit_empl.ID_User);
            foreach( string item in r)
                if(Roles.IsUserInRole(curr_user.UserName,item))
                {
                    Roles.RemoveUserFromRole(curr_user.UserName, item);
                    break;
                }
            Roles.AddUserToRole(curr_user.UserName, role);
            curr_user.Email = Request.Form["email"];
            edit_empl.ID_Dept = System.Convert.ToInt32(Request.Form["Depts"]);
            edit_empl.ID_Cabinet = System.Convert.ToInt32(Request.Form["Cabinets"]);
            context.ApplyCurrentValues(original_empl.EntityKey.EntitySetName, edit_empl);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //------Видалення працівників-------//
        public ActionResult Delete(int id)
        {
            Employee employee = context.Employees.Where(e => e.ID_Employee == id).First();
            MembershipUser curr_user = Membership.GetUser(employee.ID_User);
            context.Employees.DeleteObject(employee);
            Membership.DeleteUser(curr_user.UserName);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //-------Пошук працівників-------//
        public ActionResult Search()
        {
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            List<Cabinet> cab = context.Cabinets.ToList();
            IEnumerable<SelectListItem> _cab = cab.Select(e => new SelectListItem { Value = e.ID_Cabinet.ToString(), Text = e.Number.ToString() });
            ViewBag.Depts = _dept;
            ViewBag.Cabinets = _cab;          
            return View();
        }

        [HttpPost]
        public ActionResult Search(Employee requestedEmployee)
        {
            IQueryable<Employee> employee = context.Employees;
            int cabinet = Convert.ToInt32(Request.Form["Cabinets"]);
            if (Request.Form["FirstName"].ToString().Trim() != "")
                employee = employee.Where(p => p.FirstName.Contains(requestedEmployee.FirstName));
            if (Request.Form["Surname"].ToString().Trim() != "")
                employee = employee.Where(p => p.Surname.Contains(requestedEmployee.Surname));
            if (Request.Form["Category"].ToString().Trim() != "")
                employee = employee.Where(p => p.Category.Contains(requestedEmployee.Category));
            if (Request.Form["Specialty"].ToString().Trim() != "")
                employee = employee.Where(p => p.Category.Contains(requestedEmployee.Specialty));
            if (Request.Form["position"].ToString().Trim() != "")
                employee = employee.Where(p => p.Position.Contains(requestedEmployee.Position));
           // if (Request.Form["Cabinets"] != null)
           //     employee = employee.Where(p => p.ID_Cabinet == );
           // if (Request.Form["Depts"] != null)
           //     employee = employee.Where(p => p.ID_Dept == requestedEmployee.ID_Dept);
            employee = employee.OrderBy(p => p.FirstName);
            model.FillModel(employee);
            return View("Index", model.lst.ToPagedList(1,5));
        }

    }
}
