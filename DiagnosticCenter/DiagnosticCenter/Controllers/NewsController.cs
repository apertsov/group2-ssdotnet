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
        private DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();
        DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();    //контекст моделі бд
        AllNewsVM model = new AllNewsVM();     //ViewModel для відображення новин
        string[] r = { "Doctor", "HeadDoctor", "Nurse", "HeadNurse", "DepartmentChiefDoctor", "MedicalRegistrar" }; // масив ролей

        //-------Всі новини-------//
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            IQueryable<News> news = context.News;

            ViewBag.TopicSortParm = String.IsNullOrEmpty(sortOrder) ? "Topic desc" : "";
            ViewBag.TextSortParm = String.IsNullOrEmpty(sortOrder) ? "Text desc" : "";
            ViewBag.ID_NewsSortParm = String.IsNullOrEmpty(sortOrder) ? "ID_New desc" : "";
            ViewBag.ID_DeptSortParm = String.IsNullOrEmpty(sortOrder) ? "ID_Dept desc" : "";
          
            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.Topic.ToUpper().Contains(searchString.ToUpper()));
            }
           
            switch (sortOrder)
            {
                case "Topic desc":
                    news = news.OrderByDescending(s => s.Topic);
                    break;
                case "Text desc":
                    news = news.OrderByDescending(s => s.Text);
                    break;
                case "ID_Employee desc":
                    news = news.OrderByDescending(s => s.ID_Employee);
                    break;
                case "ID_Dept desc":
                    news = news.OrderByDescending(s => s.ID_Dept);
                    break;
                default:
                    news = news.OrderByDescending(s => s.Topic);
                    break;
            }

            /*foreach(News _buf in news)
            {
                News a = new News();
                a = _buf;
            }*/
            
            model.FillModel(news);
  
            int pageIndex = (page ?? 1);
            return View(model.lst.ToPagedList(pageIndex, 5));
        }

        /*----- Детальна інформація про новини -----*/
        public ActionResult Details(int id)
        {
            IQueryable<News> news = context.News.Include("Employee").Where(i => i.ID_News == id);
            model.FillModel(news);
            return View(model.lst.ToList());
        }
 

        /*----- Створення новин -----*/
        public ActionResult Create()
        {
            List<Employee> empl = context.Employees.ToList();
            IEnumerable<SelectListItem> _empl = empl.Select(n => new SelectListItem { Value = n.ID_Employee.ToString(), Text = n.Position.ToString() });
            
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(n => new SelectListItem { Value = n.ID_Dept.ToString(), Text = n.Name });

            List<News> ntype = context.News.ToList();
            IEnumerable<SelectListItem> _ntype = ntype.Select(n => new SelectListItem { Value = n.Type.ToString(), Text = n.Type.ToString() });

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
        public ActionResult Edit(int id)
        {
            News news = context.News.Include("Employee").Where(n => n.ID_News == id).First();
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(n => new SelectListItem { Value = n.ID_Dept.ToString(), Text = n.Name, Selected = n.ID_Dept == id });
            List<Employee> empl = context.Employees.ToList();
            IEnumerable<SelectListItem> _empl = empl.Select(n => new SelectListItem { Value = n.ID_Employee.ToString(), Text = n.ID_Employee.ToString(), Selected = n.ID_Employee == id });
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
            //News news = context.News.Where(n => n.ID_News == id).First();
            //context.News.DeleteObject(news);
            //context.SaveChanges();
            //return RedirectToAction("Index");
            News news = db.News.Single(n => n.ID_News == id);
            return View(news);
        
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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