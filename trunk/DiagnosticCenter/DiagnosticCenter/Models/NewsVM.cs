using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenter.Models;
namespace DiagnosticCenter.Models
{
    public class NewsVM
    {
        public int id { get; set; }
        public int id_dept { get; set; }
        public string text { get; set; }
        public int type { get; set; }
        public int id_employee { get; set; }
        public string topic { get; set; }
    }

    public class AllNewsVM
    {
        public List<NewsVM> lst { get; set; }

        public AllNewsVM()
        {
            this.lst = new List<NewsVM>();
        }
        public void FillModel(IQueryable<News> query)
        {
            lst.Clear();
            foreach (var n in query)
            {
                NewsVM nb = new NewsVM();
                nb.id = n.ID_News;
                nb.id_dept = n.ID_Dept;
                nb.text = n.Text;
                nb.type = n.Type;
                nb.id_employee = n.ID_Employee;
                nb.topic = n.Topic;
                lst.Add(nb);
            }
    

            }
        }
    }
