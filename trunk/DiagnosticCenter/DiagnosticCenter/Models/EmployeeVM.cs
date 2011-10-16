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
}