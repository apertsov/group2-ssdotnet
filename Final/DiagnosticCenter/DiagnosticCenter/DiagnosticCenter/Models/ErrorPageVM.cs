using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenter.Models
{
    public class ErrorPageVM
    {
        public string errTitle { get; set; }
        public string errDescription { get; set; }
        public string errGoBackController { get; set; }
        public string errGoBackAction { get; set; }
    }
}