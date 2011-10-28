using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{
    public class ErrorPageController : Controller
    {
        public ViewResult Index(string errTitle, string errDescription, string errGoBackAction, string errGoBackController)
        {
            ErrorPageVM err = new ErrorPageVM();

            err.errTitle = errTitle;
            err.errDescription = errDescription;
            err.errGoBackAction = errGoBackAction;
            err.errGoBackController = errGoBackController;

            return View(err);
        }   
    }
}
