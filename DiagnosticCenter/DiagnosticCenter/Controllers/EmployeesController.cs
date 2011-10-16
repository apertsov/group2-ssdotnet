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
        DiagnosticsDBModelContainer _empl = new DiagnosticsDBModelContainer();
        

        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            List<EmployeeVM> model = new List<EmployeeVM>();
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName desc" : "";
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Surname desc" : "";
            ViewBag.DepartmentSortParm = String.IsNullOrEmpty(sortOrder) ? "Department desc" : "";
            IQueryable<Employee> empl = _empl.Employees.Include("Department");
            if (!String.IsNullOrEmpty(searchString))
            {
                empl = empl.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Surname desc":
                    empl = empl.OrderByDescending(s => s.Surname);
                    break;
                case "FirstName desc":
                    empl = empl.OrderByDescending(s => s.FirstName);
                    break;
                case "Department desc":
                    empl = empl.OrderByDescending(s => s.Department.Name);
                    break;
                default:
                    empl = empl.OrderBy(s => s.FirstName);
                    break;
            }
            
            foreach (Employee e in empl)
            {
               
                EmployeeVM m = new EmployeeVM();
                m.id = e.ID_Employee;
                m.surname = e.Surname;
                m.firstName = e.FirstName;
                m.patronymic = e.Patronymic;
                m.specialty = e.Specialty;
                m.position = e.Position;
                m.rate = e.Rate;
                m.category = e.Category;
                m.department = e.Department.Name;
                m.cabinet = e.Cabinet.Number;
                model.Add(m);
            }
            int pageIndex = (page ?? 1);
            return View(model.ToPagedList(pageIndex, 5));
        }

       
        public ActionResult Details(int id)
        {
            IEnumerable<Employee> empl = _empl.Employees.Include("Department").Where(i => i.ID_Employee == id);
            
            List<EmployeeVM> model = new List<EmployeeVM>();
            foreach( var e in empl){
                EmployeeVM m = new EmployeeVM();
                m.id =  e.ID_Employee;
                m.surname = e.Surname;
                m.firstName = e.FirstName;
                m.patronymic = e.Patronymic;
                m.specialty = e.Specialty;
                m.position = e.Position;
                m.rate = e.Rate;
                m.category = e.Category;
                m.department = e.Department.Name;
                m.cabinet = e.Cabinet.Number;
                model.Add(m);
            }
            return View(model.ToList());
        }

       

        public ActionResult Create()
        {   
            List<Department> dept = _empl.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            List<Cabinet> cab = _empl.Cabinets.ToList(); 
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
           
            Membership.CreateUser(Request.Form["username"],sender.GeneratePassword(), Request.Form["e-mail"]);
            Roles.AddUserToRole(Request.Form["username"], Request.Form["position"]);
            MembershipUser curr_user = Membership.GetUser(Request.Form["username"]);
            new_empl.ID_User = (Guid)curr_user.ProviderUserKey;
            
            new_empl.ID_Dept = System.Convert.ToInt32(Request.Form["Depts"]);
            new_empl.ID_Cabinet = System.Convert.ToInt32(Request.Form["Cabinets"]);
            _empl.AddToEmployees(new_empl);
            _empl.SaveChanges();
                       
            sender.SendPassword(curr_user.Email);

            return RedirectToAction("Index");
        }
        
        
        public ActionResult Edit(int id)
        {
            Employee empl = _empl.Employees.Include("Department").Where(i => i.ID_Employee == id).First();
            List<Department> dept = _empl.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name});
            
            List<Cabinet> cab = _empl.Cabinets.ToList();
            IEnumerable<SelectListItem> _cab = cab.Select(e => new SelectListItem { Value = e.ID_Cabinet.ToString(), Text = e.Number.ToString() });
            ViewBag.Depts = _dept;
            ViewBag.Cabinets = _cab;          
            return View(empl);
        }

        

        [HttpPost]
        public ActionResult Edit(Employee edit_empl)
        {
            var original_empl = _empl.Employees.Include("Department").Where(i => i.ID_Employee == edit_empl.ID_Employee).First();
                                  
            if (!ModelState.IsValid)
                return View(original_empl);
           edit_empl.ID_Dept = System.Convert.ToInt32(Request.Form["Depts"]);
           edit_empl.ID_Cabinet = System.Convert.ToInt32(Request.Form["Cabinets"]);
            _empl.ApplyCurrentValues(original_empl.EntityKey.EntitySetName, edit_empl);
            _empl.SaveChanges();
            return RedirectToAction("Index");
        }

       
        public ActionResult Delete(int id)
        {
            IQueryable<Employee> query = from empl in _empl.Employees
                                        where empl.ID_Employee == id
                                        select empl;

            foreach (var pat in query)
            {
                _empl.Employees.DeleteObject(pat);
            }
            _empl.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Search()
        {
            
            List<Department> dept = _empl.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });

            List<Cabinet> cab = _empl.Cabinets.ToList();
            IEnumerable<SelectListItem> _cab = cab.Select(e => new SelectListItem { Value = e.ID_Cabinet.ToString(), Text = e.Number.ToString() });
            ViewBag.Depts = _dept;
            ViewBag.Cabinets = _cab;          
            return View();
        }

        
        [HttpPost]
        public ActionResult Search(int? id)
        {
            string name = ViewData["Name"].ToString();
            return RedirectToAction("SearchResult", name);
        }

    }
}
