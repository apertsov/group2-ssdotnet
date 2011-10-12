using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenter.Models
{
    public class Departments
    {
        public static Departments Instance = new Departments();
        DiagnosticsDBModelContainer DB = new DiagnosticsDBModelContainer();
        public Departments()
        {
        }
        public List<Department> getList()
        {   
            return (from d in DB.Departments select d).ToList();
        }
        public Department getByID(int id)
        {
            return (from d in DB.Departments where d.ID_Dept == id select d).First();
        }
        public Department add( Department d )
        {
            List<Department> list = (from a in DB.Departments select a).ToList();
            int max = list.Count == 0 ? 1 : list.Max(x => x.ID_Dept);
            d.ID_Dept = max + 1;
            DB.Departments.AddObject(d);
            DB.SaveChanges();
            return d;
        }
        public void deleteByID(int id)
        {
            var X = from d in DB.Departments where d.ID_Dept == id select d;
            DB.Departments.DeleteObject(X.First());
            DB.SaveChanges();
        }
    }
}