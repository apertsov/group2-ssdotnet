﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using DiagnosticCenter.Classes;

namespace DiagnosticCenter
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

       /** protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //Create culture info object
            CultureInfo ci = new CultureInfo("ru");

            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }*/

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //Очень важно проверять готовность объекта сессии
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Вначале проверяем, что в сессии нет значения
                //и устанавливаем значение по умолчанию
                //это происходит при первом запросе пользователя
                if (ci == null)
                {
                    string langName = UserSettings.GetUserProperty("Culture");

                    if (String.IsNullOrEmpty(langName))
                    {
                        DiagnosticCenter.Models.DiagnosticsDBEntities db = new DiagnosticCenter.Models.DiagnosticsDBEntities();
                        var settings = db.Settings.ToList();
                        if (settings.Count > 0)
                        {
                            langName = settings.First().LanguageId.Trim();                            
                        }

                        if (!String.IsNullOrEmpty(langName))
                        {
                            UserSettings.SetUserProperty("Culture", langName);
                        }
                    }

                    langName = String.IsNullOrEmpty(langName) || langName == "empty" ? "uk" : langName;


                    ci = new CultureInfo(langName);
                    if (User.Identity.IsAuthenticated)
                    {
                        this.Session["Culture"] = ci;
                    }
                }
                //Устанавливаем культуру для каждого запроса
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }

        /*protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //Очень важно проверять готовность объекта сессии
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Вначале проверяем, что в сессии нет значения
                //и устанавливаем значение по умолчанию
                //это происходит при первом запросе пользователя
                if (ci == null)
                {
                    //Устанавливает значение по умолчанию - базовый украинский
                    string langName = "uk";
                    //Пытаемся получить значения с HTTP заголовка
                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        //Получаем список 
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                //Устанавливаем культуру для каждого запроса
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }*/
    }
    
}