﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            //int depID = 2;
            //var context = new DiagnosticsDBModelContainer();
            //var employees = context.Employees.Where(e => e.ID_Dept == depID).ToList();

            //employees[i].Cabinet.Number


            return View();
        }
    }
}