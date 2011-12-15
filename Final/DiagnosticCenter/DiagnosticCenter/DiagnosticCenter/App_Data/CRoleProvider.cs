using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using DiagnosticCenter.Classes;

public class CRoleProvider: RoleProvider
{
    #region Globals
    private string eventSource = "CRoleProvider";
    private string eventLog = "Application";
    private string exceptionMessage = "An exception occurred. Please check the Event Log.";
    private bool pWriteExceptionsToEventLog = false;

    public bool WriteExceptionsToEventLog
    {
        get { return pWriteExceptionsToEventLog; }
        set { pWriteExceptionsToEventLog = value; }
    }
    #endregion

    public override void Initialize(string name, NameValueCollection config)
    {

        //
        // Initialize values from web.config.
        //

        if (config == null)
            throw new ArgumentNullException("config");

        if (name == null || name.Length == 0)
            name = "CRoleProvider";

        if (String.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Sample Role provider");
        }

        // Initialize the abstract base class.
        base.Initialize(name, config);


        if (config["applicationName"] == null || config["applicationName"].Trim() == "")
        {
            pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
        }
        else
        {
            pApplicationName = config["applicationName"];
        }


        if (config["writeExceptionsToEventLog"] != null)
        {
            if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
            {
                pWriteExceptionsToEventLog = true;
            }
        }

    }

    private string pApplicationName;
    public override string ApplicationName
    {
        get { return pApplicationName; }
        set { pApplicationName = value; }
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
        foreach (string rolename in roleNames)
            if (!RoleExists(rolename)) throw new ProviderException("Role name not found.");
        foreach (string username in usernames)
        {
            if (username.Contains(",")) throw new ArgumentException("User names cannot contain commas.");
            foreach (string rolename in roleNames)
                if (IsUserInRole(username, rolename)) throw new ProviderException("User is already in role.");

        }
        UserRepository user = new UserRepository();
        user.AddUsersToRoles(usernames, roleNames);
    }
 
    public override void CreateRole(string roleName)
    {
        if (roleName.Contains(","))
        {
            throw new ArgumentException("Role names cannot contain commas.");
        }

        if (RoleExists(roleName))
        {
            throw new ProviderException("Role name already exists.");
        }

        UserRepository user = new UserRepository();
        user.CreateRole(roleName);
        
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
        if (!RoleExists(roleName))
        {
            throw new ProviderException("Role does not exist.");
        }

        if (throwOnPopulatedRole && GetUsersInRole(roleName).Length > 0)
        {
            throw new ProviderException("Cannot delete a populated role.");
        }
        UserRepository _user = new UserRepository();
        return _user.DeleteRole(roleName);
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
        UserRepository _user = new UserRepository();
        return _user.FindUsersInRole(roleName, usernameToMatch);
    }

    public override string[] GetAllRoles()
    {
        UserRepository _user = new UserRepository();
        return _user.GetAllRoles();
    }

    public override string[] GetRolesForUser(string username)
    {
        UserRepository _user = new UserRepository();
        return _user.GetRolesForUser(username);
    }

    public override string[] GetUsersInRole(string roleName)
    {
        UserRepository _user = new UserRepository();
        return _user.GetUsersInRole(roleName);
    }

    public override bool IsUserInRole(string username, string roleName)
    {
        UserRepository _user = new UserRepository();
        return _user.IsUserInRole(username,roleName);
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
        foreach (string rolename in roleNames)
      {
        if (!RoleExists(rolename))
        {
          throw new ProviderException("Role name not found.");
        }
      }

      foreach (string username in usernames)
      {
        foreach (string rolename in roleNames)
        {
          if (!IsUserInRole(username, rolename))
          {
            throw new ProviderException("User is not in role.");
          }
        }
      }
    UserRepository _user = new UserRepository();
        _user.RemoveUsersFromRoles(usernames,roleNames);
    }

    public override bool RoleExists(string roleName)
    {
        UserRepository _user = new UserRepository();
        return _user.RoleExists(roleName);
    }
}
