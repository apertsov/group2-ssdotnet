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
    /// <summary>
    /// Контроллер <c>EmployeesController</c> описує функціональну частину
    /// для керування записами про працівників діагностичного центру
    /// </summary>
    /// <param name="context">Екземпляр моделі <c>DiagnosticsDBModel</c></param>
    /// <param name="model">Екземпляр класу <c>EmployeesVM</c></param>
    /// <param name="role">Масив стрічок з назвою ролей користувачів</param>
    public class EmployeesController : Controller
    {
        DiagnosticsDBEntities context = new DiagnosticsDBEntities();
        EmployeesVM model = new EmployeesVM();
        string[] role = { "Doctor", "HeadDoctor", "Nurse", "HeadNurse", "DepartmentChiefDoctor", "MedicalRegistrar" };  

        /// <summary>
        /// Action для сторінки з відображенням списку працівників
        /// </summary>
        /// <param name="sortOrder">Порядок сортування</param>
        /// <param name="searchString">Стрічка з даними для пошуку</param>
        /// <param name="page">Індикатор сторінки для Paging</param>
        /// <returns>View зі списком працівників</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.Title = TitleRes.TitleStrings.IndexTitleEmpl;
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

        /// <summary>
        /// Action для сторінки з відображенням детальної інформації про працівника
        /// </summary>
        /// <param name="id">Id працівника</param>
        /// <returns>View із детальною інформацією</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ActionResult Details(int id)
        {
            ViewBag.Title = TitleRes.TitleStrings.DetailsTitle;
            IQueryable<Employee> employee = context.Employees.Include("Department").Include("Cabinet").Where(i => i.ID_Employee == id);
            model.FillModel(employee);
            return View(model.lst.ToList());
        }

        /// <summary>
        /// Action для сторінки створення нового працівника
        /// </summary>
        /// <returns>View з формою для створення</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Title = TitleRes.TitleStrings.AddTitleEmpl;
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            List<SelectListItem> d = _dept.ToList();
            SelectListItem item = new SelectListItem();
            item.Text = EmployeesRes.EmployeesStrings.ChooseDept;
            item.Value = "0";
            d.Insert(0, item);
            List<SelectListItem> c = new List<SelectListItem>();
            item = new SelectListItem();
            item.Text = ReferralRes.ReferralStrings.ChooseCab;
            item.Value = "0";
            c.Insert(0, item);
            ViewBag.Dept = d;
            ViewBag.Cab = c;
            return View();
        }

        /// <summary>
        /// Action обробки вхідних даних з форми створення працівника
        /// </summary>
        /// <remarks>Метод заповнює основні дані в таблицю бази даних,
        /// а також відправляє електронний лист на пошту працівника
        /// із даними для авторизації
        /// </remarks>
        /// <param name="new_empl">Новий екземпляр моделі<c>Employee</c></param>
        /// <returns>Перехід на<c>Index</c>Action або View для повторного вводу даних</returns>
        [HttpPost]
        public ActionResult Create(Employee new_empl)
        {
            if (!ModelState.IsValid) 
                return View();
            Classes.MailSender sender = new Classes.MailSender();
            string newUser = new_empl.Username;
            string pass = sender.GeneratePassword();
            Membership.CreateUser(newUser, pass, new_empl.Email);
            string r = "";
            switch (Request.Form["position"])
            {
                case "лікар": r = role[0]; break;
                case "головний лікар": r = role[1]; break;
                case "медсестра": r = role[2]; break;
                case "головна медсестра": r = role[3]; break;
                case "зав. відділом": r = role[4]; break;
                case "реєстратура": r = role[5]; break;
            }
            Roles.AddUserToRole(newUser, r);

            MembershipUser curr_user = Membership.GetUser(newUser);
            new_empl.ID_User = (int)curr_user.ProviderUserKey;
            new_empl.ID_Dept = System.Convert.ToInt32(Request.Form["Dept"]);
            new_empl.ID_Cabinet = System.Convert.ToInt32(Request.Form["Cab"]);

            context.AddToEmployees(new_empl);
            context.SaveChanges();
            sender.SendPassword(new_empl.Email, newUser);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action для сторінки редагування інформації працівника
        /// </summary>
        /// <param name="id">Id працівника</param>
        /// <returns>View з формою редагування інформації</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = TitleRes.TitleStrings.EditTitle;
            Employee empl = context.Employees.Include("Department").Include("Cabinet").Where(e => e.ID_Employee == id).First();
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => 
                                                              new SelectListItem { Value = e.ID_Dept.ToString(), 
                                                                                   Text = e.Name, 
                                                                                   Selected = e.ID_Dept == empl.ID_Dept });
            List<Cabinet> cab = context.Cabinets.Where(e => e.ID_Dept == empl.ID_Dept).ToList();
            IEnumerable<SelectListItem> _cab = cab.Select(e => 
                                                            new SelectListItem { Value = e.ID_Cabinet.ToString(), 
                                                                                 Text = e.Number.ToString(), 
                                                                                 Selected = e.ID_Cabinet == empl.ID_Cabinet });
            MembershipUser curr_user = Membership.GetUser(empl.ID_User);
            List<SelectListItem> c = _cab.ToList();
            List<Position> pos = context.Positions.ToList();
            IEnumerable <SelectListItem> _pos = pos.Select(e => 
                                                            new SelectListItem { Value = e.Name, 
                                                                                 Text = e.Name, 
                                                                                 Selected = e.Name == empl.Position });
            ViewBag.Pos = _pos;
            ViewBag.Dept = _dept;
            ViewBag.Cab = c;
            ViewBag.Email = curr_user.Email;
            return View(empl);
        }

        /// <summary>
        /// Action обробки введеної інформації з форми для редагування
        /// </summary>
        /// <param name="edit_empl">Екземпляр Employee для редагування</param>
        /// <returns>Перехід на Index Action</returns>
        [HttpPost]
        public ActionResult Edit(Employee edit_empl)
        {
           Employee original_empl = context.Employees.Include("Department")
                                                     .Include("Cabinet")
                                                     .Where(i =>i.ID_Employee == edit_empl.ID_Employee)
                                                     .First();
            edit_empl.Email = original_empl.Email;
            edit_empl.Username = original_empl.Username;
            string r = "";
            switch (Request.Form["Pos"])
            {
                case "лікар": r = role[0]; break;
                case "головний лікар": r = role[1]; break;
                case "медсестра": r = role[2]; break;
                case "головна медсестра": r = role[3]; break;
                case "зав. відділом": r = role[4]; break;
                case "реєстратура": r = role[5]; break;
            }
            MembershipUser curr_user = Membership.GetUser(edit_empl.ID_User);
            foreach (string item in role)
                if (Roles.IsUserInRole(curr_user.UserName, item))
                {
                    Roles.RemoveUserFromRole(curr_user.UserName, item);
                    break;
                }
            Roles.AddUserToRole(curr_user.UserName, r);
            curr_user.Email = edit_empl.Email;
            edit_empl.Position = Request.Form["Pos"];
            edit_empl.ID_Dept = System.Convert.ToInt32(Request.Form["Dept"]);
            edit_empl.ID_Cabinet = System.Convert.ToInt32(Request.Form["Cab"]);
            context.ApplyCurrentValues(original_empl.EntityKey.EntitySetName, edit_empl);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action для видалення працівника
        /// </summary>
        /// <param name="id">Id працівника</param>
        /// <returns>Перехід на Index Action</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor")]
        public ActionResult Delete(int id)
        {
            Employee employee = context.Employees.Where(e => e.ID_Employee == id).First();
            MembershipUser curr_user = Membership.GetUser(employee.ID_User);
            context.Employees.DeleteObject(employee);
            Membership.DeleteUser(curr_user.UserName);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Пошук працівника
        /// </summary>
        /// <returns>Delete View</returns>
        [Authorize(Roles = "Administrator,DepartmentChiefDoctor,Doctor,HeadNurse,MedicalRegistrar,Nurse")]
        public ActionResult Search()
        {
            ViewBag.Title = TitleRes.TitleStrings.SearchTitle;
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.ID_Dept.ToString(), Text = e.Name });
            List<Cabinet> cab = context.Cabinets.ToList();
            IEnumerable<SelectListItem> _cab = cab.Select(e => new SelectListItem { Value = e.ID_Cabinet.ToString(), Text = e.Number.ToString() });
            ViewBag.Dept = _dept;
            ViewBag.Cab = _cab;
            return View();
        }

        /// <summary>
        /// Обробка інформації для пошуку
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View зі списком результату пошуку</returns>
        [HttpPost]
        public ActionResult Search(int? id)
        {
            IQueryable<Employee> employee = context.Employees.Include("Department").Include("Cabinet");

            if (Request.Form["FirstName"].ToString().Trim() != "")
                employee = employee.Where(p => p.FirstName.Contains(Request.Form["FirstName"].ToString()));
            if (Request.Form["Surname"].ToString().Trim() != "")
                employee = employee.Where(p => p.Surname.Contains(Request.Form["Surname"].ToString()));
            if (Request.Form["Category"].ToString().Trim() != "")
                employee = employee.Where(p => p.Category.Contains(Request.Form["Category"].ToString()));
            if (Request.Form["Specialty"].ToString().Trim() != "")
                employee = employee.Where(p => p.Category.Contains(Request.Form["Specialty"].ToString()));
            if (Request.Form["position"].ToString().Trim() != "")
                employee = employee.Where(p => p.Position == Request.Form["position"].ToString());
            if (Request.Form["Cab"].ToString() != "")
            {
                int cabinet = Convert.ToInt32(Request.Form["Cab"]);
                employee = employee.Where(p => p.ID_Cabinet == cabinet);
            }
            if (Request.Form["Dept"].ToString() != "")
            {
                int dept = Convert.ToInt32(Request.Form["Dept"]);
                employee = employee.Where(p => p.ID_Dept == dept);
            }
            employee = employee.OrderBy(p => p.FirstName);
            model.FillModel(employee);
            return View("Index", model.lst.ToPagedList(1, 5));
        }

        /// <summary>
        /// Перевірка існування Username користувача
        /// </summary>
        /// <param name="username">Username користувача</param>
        /// <returns>JSON з результатом перевірки</returns>
        public JsonResult UserExist(string username)
        {
            var users = System.Web.Security.Membership.FindUsersByName(username);
            if (username == null || username == string.Empty)
            {
                return Json("Обов'язкове поле", JsonRequestBehavior.AllowGet);
            }
            if (users.Count > 0)
            {
                return Json("Користувач вже присутній у системі", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Перевірка існування електронної адреси користувача
        /// </summary>
        /// <param name="username">Email користувача</param>
        /// <returns>JSON з результатом перевірки</returns>
        public JsonResult UserExists(string email)
        {
            var users = System.Web.Security.Membership.FindUsersByEmail(email);
            if (email == null || email == string.Empty)
            {
                return Json("Обов'язкове поле", JsonRequestBehavior.AllowGet);
            }
            if (users.Count > 0)
            {
                return Json("Користувач вже присутній у системі", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Створення списку кабінетів
        /// </summary>
        /// <param name="deptName">Назва відділу для визначення кабінетів</param>
        /// <returns>Список кабінетів</returns>
        public JsonResult Cabinet(string deptName)
        {
            JsonResult result = new JsonResult();
            List<Cabinet> cab = context.Cabinets.Include("Department").ToList();
            IEnumerable<SelectListItem> _cab = cab.Where(e => e.Department.ID_Dept == Convert.ToInt32(deptName)).Select(e => new SelectListItem
            {
                Text = e.Number.ToString(),
                Value = e.ID_Cabinet.ToString()
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
        /// Створення списку кабінетів для Edit View
        /// </summary>
        /// <param name="deptName">Назва відділу для визначення кабінетів</param>
        /// <returns>Список кабінетів</returns>
        public JsonResult EditCabinet(string deptName)
        {
            JsonResult result = new JsonResult();
            List<Cabinet> cab = context.Cabinets.Include("Department").ToList();
            IEnumerable<SelectListItem> _cab = cab.Where(e => e.Department.ID_Dept == Convert.ToInt32(deptName)).Select(e => new SelectListItem
            {
                Text = e.Number.ToString(),
                Value = e.ID_Cabinet.ToString()
            });
            List<SelectListItem> c = _cab.ToList();
            result.Data = c;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}