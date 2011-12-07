using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;

namespace DiagnosticCenter.Controllers
{
    public class UserSettingsController : Controller
    {
        //
        // GET: /Menu/

        public JsonResult SetUserSetting(string key, string value)
        {
            JsonResult result = new JsonResult();
            
            if (User.Identity.IsAuthenticated)
            {                
                DiagnosticCenter.Classes.UserSettings.SetUserProperty(key, value);
                result.Data = "ok";
            }
            else
                result.Data = "bad";
            
            result.Data = "ok";
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

    }
}
