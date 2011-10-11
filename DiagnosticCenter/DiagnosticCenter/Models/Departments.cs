using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenter.Models
{
    public class Departments
    {
        public static Departments Instance = new Departments();
        public List<Department> list = new List<Department>();
        public Departments()
        {
            list.Add
            (
                Department.CreateDepartment(1, "Кардіологічне", "Кардіологічне відділення")
            );
            list.Add
            (
                Department.CreateDepartment(2, "Неврологічне", "Неврологічне відділення")
            );
            list.Add
            (
                Department.CreateDepartment(3, "Ендокринологічне", "Ендокринологічне відділення")
            );
        }
        public List<Department> getList()
        {
            return list;
        }
        public Department getByID(int id)
        {
            return list.Find( d => d.ID_Dept == id);
        }
        public Department add( Department d )
        {
            int max = list.Max(x => x.ID_Dept);
            d.ID_Dept = max + 1;
            list.Add(d);
            return d;
        }
        public void deleteByID(int id)
        {
            list.Remove( list.Find( d => d.ID_Dept == id) );
        }
    }
}