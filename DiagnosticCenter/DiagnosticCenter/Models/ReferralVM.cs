using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace DiagnosticCenter.Models
{
    /// <summary>
    /// Клас призначений для створення моделі представлення направлення
    /// </summary>
    /// <param name="patient">Ім'я і прізвище пацієнта</param>
    /// <param name="idp">Id пацієнта</param>
    /// <param name="department">Список відділень</param>
    /// <param name="cabinet">Список кабінетів</param>
    /// <param name="employee">Список працівників</param>
    /// <param name="visitDate">Дата відвідування</param>
    /// <param name="todayDate">Дата створення</param>
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

        /// <summary>
        /// Метод для заповнення моделі даними
        /// </summary>
        /// <param name="pat">Id пацієнта</param>
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

        /// <summary>
        /// Створення списку кабінетів
        /// </summary>
        /// <param name="department">Назва відділення</param>
        /// <returns>Список кабінетів</returns>
        public List<SelectListItem> GetCabinet(string department)
        {
            DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
            List<Cabinet> cab = context.Cabinets.Include("Department").ToList();
            IEnumerable<SelectListItem> _cab = cab.Where(e => e.Department.Name == department)
                                                  .Select(e => new SelectListItem { Value = e.Number.ToString(), Text = e.Number.ToString() });
            List<SelectListItem> c = _cab.ToList();
            SelectListItem i = new SelectListItem();
            i.Text = ReferralRes.ReferralStrings.ChooseCab;
            i.Value = "0";
            c.Insert(0, i);
            return c;
        }

        /// <summary>
        /// Створення списку працівників
        /// </summary>
        /// <param name="number">Номер кабінету</param>
        /// <returns>Список кабінетів</returns>
        public List<SelectListItem> GetEmployee(string number)
        {
            DiagnosticsDBModelContainer context = new DiagnosticsDBModelContainer();
            List<Employee> empl = context.Employees.Include("Cabinet").ToList();
            IEnumerable<SelectListItem> _empl = empl.Where(e => e.Cabinet.Number.ToString() == number)
                                                    .Select(e => new SelectListItem { Value = e.ID_Employee.ToString(), Text = e.FirstName + " " + e.Surname });
            List<SelectListItem> c = _empl.ToList();
            SelectListItem i = new SelectListItem();
            i.Text = ReferralRes.ReferralStrings.ChooseDoctor;
            i.Value = "0";
            c.Insert(0, i);
            return  c;
        }
    }
}