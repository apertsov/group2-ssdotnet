using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenter.Models;
namespace DiagnosticCenter.Models
{
    public class EmployeeVM
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string category { get; set; }
        public string position { get; set; }
        public string specialty { get; set; }
        public int rate { get; set; }
        public string department { get; set; }
        public int cabinet { get; set; }
    }

    public class EmployeesVM
    {
       public List<EmployeeVM> lst { get; set; }

       public EmployeesVM()
       {
           this.lst = new List<EmployeeVM>();
       }
        public void FillModel(IQueryable<Employee> query)
       {
           lst.Clear();
           foreach (Employee e in query)
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
               lst.Add(m);
           }
       }
    }
}