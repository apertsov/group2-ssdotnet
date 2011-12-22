using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DiagnosticCenter.Models;
using PagedList;
using System.Collections.ObjectModel;


namespace DiagnosticCenter.Controllers
{
    public class NewsController : Controller
    {
        private DiagnosticsDBEntities db = new DiagnosticsDBEntities();
        DiagnosticsDBEntities context = new DiagnosticsDBEntities();    //контекст моделі бд
        AllNewsVM model = new AllNewsVM();     //ViewModel для відображення новин
        string[] r = { "Doctor", "HeadDoctor", "Nurse", "HeadNurse", "DepartmentChiefDoctor", "MedicalRegistrar" }; // масив ролей

        //-------Всі новини-------//
        [Authorize(Roles = "Administrator, DepartmentChiefDoctor, Doctor, HeadDoctor, HeadNurse, Nurse, MedicalRegistrar")]
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            IQueryable<News> news = context.News;
          
            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.Topic.ToUpper().Contains(searchString.ToUpper()));
            }
            
            int idUser = (int)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            if (context.Employees.Where(e => e.ID_User == idUser).Count() == 0)
                return RedirectToAction("Index", "ErrorPage", new
                {
                    errTitle = ViewRes.PlanStrings.Error1Text,
                    errDescription = ViewRes.PlanStrings.Error1Recomendation,
                    errGoBackAction = "Index",
                    errGoBackController = "News"
                });

            int emplId = context.Employees.Where(e => e.ID_User == idUser).FirstOrDefault().ID_Employee;
            ViewBag.emplId = emplId;
            
            ViewBag.RoleName = Roles.GetRolesForUser(User.Identity.Name).ToString();
            model.FillModel(news);
            int pageIndex = (page ?? 1);
            return View(model.lst.ToPagedList(pageIndex, 10));
        }

        /*----- Детальна інформація про новини -----*/
        public ActionResult Details(int id)
        {
            IQueryable<News> news = context.News.Include("Employee").Where(i => i.ID_News == id);
            model.FillModel(news);
            return View(model.lst.ToList());
        }
 

        /*----- Створення новин -----*/
        [Authorize(Roles = "Administrator, DepartmentChiefDoctor, Doctor, HeadDoctor, HeadNurse")]
        public ActionResult Create()
        {
            int idUser = (int)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            
            int emplId = context.Employees.Where(e => e.ID_User == idUser).FirstOrDefault().ID_Employee;
            List<Employee> empl = context.Employees.ToList();
            IEnumerable<SelectListItem> _empl = empl.Where(e=>e.ID_Employee == emplId).Select(n => new SelectListItem { Value = n.ID_Employee.ToString(), Text = n.FirstName.ToString() + " " + n.Surname.ToString() });

            int depId = context.Employees.Where(e => e.ID_User == idUser).FirstOrDefault().ID_Dept;
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Where(d => d.ID_Dept == depId).Select(n => new SelectListItem { Value = n.ID_Dept.ToString(), Text = n.Name });

            List<SelectListItem> _ntype = new List<SelectListItem>();
            _ntype.Add(new SelectListItem(){Value = "1", Text = "Внутрішні новини"});
            _ntype.Add(new SelectListItem(){Value = "2", Text = "Зовнішні новини" });

            ViewBag.Empls = _empl;
            ViewBag.Depts = _dept;
            ViewBag.Ntypes = _ntype;
            return View();
        }

        [HttpPost]
        public ActionResult Create(News new_news)
    {
            if (!ModelState.IsValid) return View();
              
            new_news.ID_Employee = System.Convert.ToInt32(Request.Form["Empls"]);
            new_news.ID_Dept = System.Convert.ToInt32(Request.Form["Depts"]);
            new_news.Type = System.Convert.ToByte(Request.Form["Ntypes"]);
                        
            context.AddToNews(new_news);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //-------Редагування новин------//
        [Authorize(Roles = "Administrator, DepartmentChiefDoctor, Doctor, HeadDoctor, HeadNurse")]
        public ActionResult Edit(int id)
        {
            News news = context.News.Include("Employee").Where(n => n.ID_News == id).First();

            int idUser = (int)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            int depId = context.Employees.Where(e => e.ID_User == idUser).FirstOrDefault().ID_Dept;
            
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Where(d => d.ID_Dept == depId).
                                                     Select(n => new SelectListItem{
                                                        Value = n.ID_Dept.ToString(), 
                                                        Text = n.Name, 
                                                        Selected = n.ID_Dept == news.ID_Dept 
                                                                                    });


            int emplId = context.Employees.Where(e => e.ID_User == idUser).FirstOrDefault().ID_Employee;
            List<Employee> empl = context.Employees.ToList();
            IEnumerable<SelectListItem> _empl = empl.Where(e => e.ID_Employee == emplId).
                                                     Select(n => new SelectListItem{
                                                        Value = n.ID_Employee.ToString(), 
                                                        Text = n.FirstName.ToString() + " " + n.Surname.ToString(), 
                                                        Selected = n.ID_Employee == news.ID_Employee 
                                                                                    });
            ViewBag.Depts = _dept;
            ViewBag.Employees = _empl;
            return View(news);

        }

        [HttpPost]
        public ActionResult Edit(News edit_news)
        {

            //News original_news = context.News.Include("Department").Include("Employee").Where(n => n.ID_News == edit_news.ID_News).First();
            News original_news = context.News.Where(n => n.ID_News == edit_news.ID_News).First();
            //edit_news.Topic = original_news.Topic;
            //edit_news.Text = original_news.Text;

            edit_news.ID_Dept = System.Convert.ToInt32(Request.Form["Depts"]);
            edit_news.ID_Employee = System.Convert.ToInt32(Request.Form["Employees"]);
            
            
            context.ApplyCurrentValues(original_news.EntityKey.EntitySetName, edit_news);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //------Видалення новин-------//
                public ActionResult Delete(int id)
        {
            News news = db.News.Single(n => n.ID_News == id);
            db.News.DeleteObject(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //-------Пошук новин-------//
        public ActionResult Search()
        {
            List<News> topic = context.News.ToList();
            IEnumerable<SelectListItem> _topic = topic.Select(n => new SelectListItem { Value = n.Topic.ToString(), Text = n.Topic.ToString() });
            List<News> text = context.News.ToList();
            IEnumerable<SelectListItem> _text = text.Select(n => new SelectListItem { Value = n.Text.ToString(), Text = n.Text.ToString() });
            ViewBag.Depts = _topic;
            ViewBag.Cabinets = _text;
            return View();
        }

        [HttpPost]
        public ActionResult Search(News requestedNews)
        {
            IQueryable<News> news = context.News.Include("Department").Include("Employee");

            if (Request.Form["Topic"].Trim() != "")
                news = news.Where(p => p.Topic.Contains(requestedNews.Topic));
            if (Request.Form["Text"].Trim() != "")
                news = news.Where(p => p.Text.Contains(requestedNews.Text));
            if (Request.Form["Topic"] != "")
            {
                string topic = Request.Form["Topic"];
                news = news.Where(p => p.Topic == topic);
            }
            if (Request.Form["Text"] != "")
            {
                string text = Request.Form["Text"];
                news = news.Where(p => p.Text == text);
            }
            news = news.OrderBy(p => p.Topic);
            model.FillModel(news);
            return View("Index", model.lst.ToPagedList(1, 5));
        }
    }
}