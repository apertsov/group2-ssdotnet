using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Models
{
    public class Departments
    {
        public static Departments Instance = new Departments();
        DiagnosticsDBEntities DB = new DiagnosticsDBEntities();
        public Departments()
        {
        }
        public List<Department> getList()
        {   
            return (from d in DB.Departments orderby d.Name select d).ToList();
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
        public Department update(int id, Department d)
        {
            var X = from x in DB.Departments where x.ID_Dept == id select x;
            if (X.First() == null) return null;
            d.ID_Dept = X.First().ID_Dept;
            DB.Departments.DeleteObject(X.First());
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