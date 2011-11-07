using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenter.Models;
namespace DiagnosticCenter.Models
{
    /// <summary>
    /// Клас описує модель представлення працівника
    /// </summary>
    /// <param name="id">Id працівника</param>
    /// <param name="firstName">Ім'я працівника</param>
    /// <param name="surname">Прізвище працівника</param>
    /// <param name="patronymic">По батькові працівника</param>
    /// <param name="category">Категорія працівника</param>
    /// <param name="position">Посада працівника</param>
    /// <param name="specialty">Спеціальність працівника</param>
    /// <param name="rate">Ставка пацієнта</param>
    /// <param name="department">Назва відділення</param>
    /// <param name="cabinet">Номер кабінету</param>
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
        
        /// <summary>
        /// Заповнення моделі даними
        /// </summary>
        /// <param name="empl">Екземпляр Employee</param>
        public void SetModel(Employee empl)
        {
            this.id = empl.ID_Employee;
            this.firstName = empl.FirstName;
            this.surname = empl.Surname;
            this.patronymic = empl.Patronymic;
            this.category = empl.Category;
            this.position = empl.Position;
            this.specialty = empl.Specialty;
            this.rate = empl.Rate;
            this.department = empl.Department.Name;
            this.cabinet = empl.Cabinet.Number;
        }
    }

    /// <summary>
    /// Клас для створення колекції моделі представлення EmployeeVM
    /// </summary>
    /// <param name="lst">Список моделей представлення</param>
    public class EmployeesVM
    {
       public List<EmployeeVM> lst { get; set; }

       /// <summary>
       /// Конструкор по замовчуванню
       /// </summary>
       public EmployeesVM()
       {
           this.lst = new List<EmployeeVM>();
       }

       /// <summary>
       /// Метод для заповнення моделі даними
       /// </summary>
       /// <param name="query">Результат запиту з таблиці Employee</param>
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