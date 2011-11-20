namespace DCExternalSite.Web
{    
    using System.Security.Authentication;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
    using System.Threading;
    using System.Security.Principal;
    using HLUser = DCExternalSite.BLL.User;
    using DCExternalSite.BLL;    
    using DCExternalSite.Web.Services;

    /// <summary>
    /// RIA Services DomainService responsible for authenticating users when
    /// they try to log on to the application.
    ///
    /// Most of the functionality is already provided by the base class
    /// AuthenticationBase
    /// </summary>
    [EnableClientAccess]
    public class AuthenticationService : AuthenticationBase<User>
    {
        protected override bool ValidateUser(string username, string password)
        {
            return HLUser.Authenticate(username, password);
        }

        protected override User GetAuthenticatedUser(IPrincipal pricipal)
        {
            User user = null;
            if (HLUser.IsUserExist(pricipal.Identity.Name))
            {
                user = new User();
                var dbUser = HLUser.GetUserByLogin(pricipal.Identity.Name);
                if (dbUser != null)
                {
                    user.ID = dbUser.ID;                    
                    user.Name = dbUser.Name;
                    user.FriendlyName = dbUser.FriendlyName;
                    user.ID = dbUser.ID;
                }
            }
            return user;
        }

       
    }


}
