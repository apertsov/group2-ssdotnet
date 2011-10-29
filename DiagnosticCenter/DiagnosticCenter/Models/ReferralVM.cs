using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace DiagnosticCenter.Models
{
    public class ReferralVM
    {
        public string patient { get; set; }
        public int idp { get; set; }
        public List<SelectListItem> department { get; set; }
        public List<SelectListItem> cabinet { get; set; }
        public List<SelectListItem> employee { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [DataType(DataType.DateTime,ErrorMessage="Невірний формат")]
        public DateTime visitDate { get; set; }
        
        public DateTime todayDate { get; set; }

        public void SetModel(int pat)
        {
            DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
            List<Department> dept = context.Departments.ToList();
            IEnumerable<SelectListItem> _dept = dept.Select(e => new SelectListItem { Value = e.Name, Text = e.Name });
            Patient _pat = context.Patients.Where(p => p.ID_Patient == pat).First();
            this.department = _dept.ToList();
            this.patient = _pat.FirstName + " " + _pat.Surname;
            this.todayDate = DateTime.Now;
            this.cabinet = GetCabinet(dept[0].Name);
            this.employee = GetEmployee(this.cabinet.First().Text);
            this.idp = pat;
            
        }
        public List<SelectListItem> GetCabinet(string department)
        {
            DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
            List<Cabinet> cab = context.Cabinets.Include("Department").ToList();
            IEnumerable<SelectListItem> _cab = cab.Where(e => e.Department.Name == department)
                                                  .Select(e => new SelectListItem { Value = e.Number.ToString(), Text = e.Number.ToString() });
            List<SelectListItem> c = _cab.ToList();
            SelectListItem i = new SelectListItem();
            i.Text = "--Кабінет--";
            i.Value = "0";
            c.Insert(0, i);
            return c;
        }
        public List<SelectListItem> GetEmployee(string number)
        {
            DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
            List<Employee> empl = context.Employees.Include("Cabinet").ToList();
            IEnumerable<SelectListItem> _empl = empl.Where(e => e.Cabinet.Number.ToString() == number)
                                                    .Select(e => new SelectListItem { Value = e.ID_Employee.ToString(), Text = e.FirstName + " " + e.Surname });
            List<SelectListItem> c = _empl.ToList();
            SelectListItem i = new SelectListItem();
            i.Text = "--Лікар--";
            i.Value = "0";
            c.Insert(0, i);
            return  c;
        }
    }
}