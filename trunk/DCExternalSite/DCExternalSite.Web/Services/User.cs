using System;
using System.Collections.Generic;
using DCExternalSite.Web.Models;
using DCExternalSite;
using System.Linq;
using System.Web;
using System.Text;

namespace DCExternalSite.BLL
{
    public partial class User
    {
        public static bool Authenticate(string userName, string password)
        {
            using (var ctx = new DiagnosticsDBEntities())
            {
                var user = ctx.Patients.Where(p => p.Email == userName).FirstOrDefault();
                if (user != null && (password == user.Password))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsUserExist(string userName)
        {
            using (var ctx = new DiagnosticsDBEntities())
            {
                var user = ctx.Patients.Where(p => p.Email == userName).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetUserName(string userName)
        {
            using (var ctx = new DiagnosticsDBEntities())
            {
                var user = ctx.Patients.Where(p => p.Email == userName).FirstOrDefault();
                if (user != null)
                {
                    return user.Email;
                }
            }
            return null;
        }
        public static DCExternalSite.Web.User GetUserByLogin(string userName)
        {
            
            using (var ctx = new DiagnosticsDBEntities())
            {
                DCExternalSite.Web.User user = new DCExternalSite.Web.User();
                var patient = ctx.Patients.Where(p => p.Email == userName).FirstOrDefault();
                if (patient != null)
                {
                    user.ID = patient.ID_Patient;
                    user.Name = patient.Email;
                    user.FriendlyName = patient.FirstName + " " + patient.Surname;
                    
                    return user;
                }
            }
            return null;
        }
        
    }

}
