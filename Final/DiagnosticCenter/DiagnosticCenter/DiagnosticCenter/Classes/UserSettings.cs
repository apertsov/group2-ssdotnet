using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Classes
{
    public struct AllMenuSettings
    {
        public string menusettings;
        public string menunews;
        public string menudeps;
        public string menuempl;
        public string menupatients;
        public string menucabinets;
        public string menuexamtypes;
        public string menuexamtemplates;
        public string menuplan;
    }

    public static class UserSettings
    {
        public static void SetUserProperty(string key, string value)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                DiagnosticsDBEntities db = new DiagnosticsDBEntities();
                MembershipUser currUser = Membership.GetUser();

                DiagnosticCenter.Models.UserSettings prop = null;

                var temp = db.UserSettingsSet.Where(s => (s.UserId == (int)currUser.ProviderUserKey && s.PropName == key));
                if (temp.Count() > 0) prop = temp.First();

                if (prop != null)
                {
                    prop.PropValue = value;
                    db.SaveChanges();
                }
                else
                {
                    prop = new Models.UserSettings();
                    prop.UserId = (int)currUser.ProviderUserKey;
                    prop.PropName = key;
                    prop.PropValue = value;
                    db.UserSettingsSet.AddObject(prop);
                    db.SaveChanges();
                }
            }
        }

        public static string GetUserProperty(string key)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                DiagnosticsDBEntities db = new DiagnosticsDBEntities();
                MembershipUser currUser = Membership.GetUser();

                DiagnosticCenter.Models.UserSettings prop = null;

                var temp = db.UserSettingsSet.Where(s => (s.UserId == (int)currUser.ProviderUserKey && s.PropName == key));
                if (temp.Count() > 0) prop = temp.First();

                if (prop != null)
                {
                    return prop.PropValue;
                }
                else
                {
                    string str;
                    if (key == "Culture") return "";
                    str = (key == "mfold") ? "unfolded" : "o";
                    return str;
                }
            }            
            
            return "empty";
        }

        public static AllMenuSettings GetMenuSettings()
        {
            AllMenuSettings settings;

            DiagnosticsDBEntities db = new DiagnosticsDBEntities();
            MembershipUser currUser = Membership.GetUser();

            var setList = db.UserSettingsSet.Where(s => s.UserId == (int)currUser.ProviderUserKey).ToList();
            if (setList.Count > 0)
            {
                string tempStr = null;
                
                var tempset = setList.Find(s => s.PropName == "menusettings");                
                if (tempset != null)
                    tempStr = tempset.PropValue;
                settings.menusettings = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menunews");
                if (tempset != null)
                    tempStr = tempset.PropValue;                
                settings.menunews = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menudeps");
                if (tempset != null)
                    tempStr = tempset.PropValue;                
                settings.menudeps = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menuempl");
                if (tempset != null)
                    tempStr = tempset.PropValue;                
                settings.menuempl = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menupatients");
                if (tempset != null)
                    tempStr = tempset.PropValue;                
                settings.menupatients = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";
                
                tempset = setList.Find(s => s.PropName == "menucabinets");
                if (tempset != null)
                    tempStr = tempset.PropValue;                
                settings.menucabinets = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menuexamtypes");
                if (tempset != null)
                    tempStr = tempset.PropValue;
                settings.menuexamtypes = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menuexamtemplates");
                if (tempset != null)
                    tempStr = tempset.PropValue;
                settings.menuexamtemplates = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";

                tempset = setList.Find(s => s.PropName == "menuplan");
                if (tempset != null)
                    tempStr = tempset.PropValue;
                settings.menuplan = !String.IsNullOrEmpty(tempStr) ? tempStr : "o";
            }
            else
            {
                settings.menusettings = "o";
                settings.menunews = "o";
                settings.menudeps = "o";
                settings.menuempl = "o";
                settings.menupatients = "o";
                settings.menucabinets = "o";
                settings.menuexamtypes = "o";
                settings.menuexamtemplates = "o";
                settings.menuplan = "o";
            }

            return settings;
        }

        public static string getTheme()
        {
            string theme = (string)HttpContext.Current.Session["Theme"];
            if (!String.IsNullOrEmpty(theme))
                return theme;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                DiagnosticsDBEntities db = new DiagnosticsDBEntities();
                MembershipUser currUser = Membership.GetUser();
                var thset = db.UserSettingsSet.Where(s => (s.UserId == (int)currUser.ProviderUserKey && s.PropName == "theme")).ToList();
                if (thset.Count > 0) theme = thset.First().PropValue;
                
                if (!String.IsNullOrEmpty(theme))
                {
                    HttpContext.Current.Session["Theme"] = theme;
                    return theme;
                }
                else
                {                    
                    var settings = db.Settings.ToList();
                    if (settings.Count > 0)
                    {
                        theme = settings.First().ThemeId;

                        HttpContext.Current.Session["Theme"] = theme;
                        SetUserProperty("theme", theme);
                        return theme;
                    }
                }
            }            
            
            return "Gray";
        }
    }
}