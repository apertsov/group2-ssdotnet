namespace DCExternalSite.Web
{
    using System;
    using System.ComponentModel;
    /*
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace MyApplication.BLL
    {
        public partial class User
        {
            public static bool Authenticate(string userName, string password)
            {
                using (var ctx = new MyApplicationEntities())
                {
                    var user = ctx.Users.Where(p => p.Login == userName).FirstOrDefault();
                    if (user != null && Hasher.VerifyMd5Hash(password, user.Password))
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool IsUserExist(string userName)
            {
                using (var ctx = new MyApplicationEntities())
                {
                    var user = ctx.Users.Where(p => p.Login == userName).FirstOrDefault();
                    if (user != null)
                    {
                        return true;
                    }
                }
                return false;
            }

            public static string GetUserName(string userName)
            {
                using (var ctx = new MyApplicationEntities())
                {
                    var user = ctx.Users.Where(p => p.Login == userName).FirstOrDefault();
                    if (user != null)
                    {
                        return user.Name;
                    }
                }
                return null;
            }
            public static User GetUserByLogin(string userName)
            {
                using (var ctx = new MyApplicationEntities())
                {
                    var user = ctx.Users.Where(p => p.Login == userName).FirstOrDefault();
                    if (user != null)
                    {
                        return user;
                    }
                }
                return null;
            }
            public int GetProjectsCount()
            {
                using (var ctx = new MyApplicationEntities())
                {
                    return ctx.UserProjects.Where(p => p.UserID == ID).Count();
                }
            }
            public int GetManagedProjectsCount()
            {
                using (var ctx = new MyApplicationEntities())
                {
                    return ctx.UserProjects.Include("ProjectRole").Where(p => p.UserID == ID && p.ProjectRole.Name == "Manager").Count();
                }
            }
            public List<int> GetManagedProjects()
            {
                using (var ctx = new MyApplicationEntities())
                {
                    return (from pu in ctx.UserProjects.Include("ProjectRole")
                            where pu.UserID == ID && pu.ProjectRole.Name == "Manager"
                            select pu.ProjectID).ToList();
                }
            }
            public static string GetProjectRole(int userId, int projectId)
            {
                using (var ctx = new MyApplicationEntities())
                {
                    return (from up in ctx.UserProjects.Include("ProjectRole")
                            where up.UserID == userId && up.ProjectID == projectId
                            select up.ProjectRole.Name).FirstOrDefault();
                }

            }

        }

    //}
}*/




    
    /// <summary>
    /// Extensions to the <see cref="User"/> class.
    /// </summary>
    public partial class User
    {
        #region Make DisplayName Bindable

        /// <summary>
        /// Override of the <c>OnPropertyChanged</c> method that generates
        /// property change notifications when <see cref="User.DisplayName"/> changes.
        /// </summary>
        /// <param name="e">The property change event args.</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            base.OnPropertyChanged(e);

            if (e.PropertyName == "Name" || e.PropertyName == "FriendlyName")
            {
                this.RaisePropertyChanged("DisplayName");
            }
        }
        #endregion
    }
}
