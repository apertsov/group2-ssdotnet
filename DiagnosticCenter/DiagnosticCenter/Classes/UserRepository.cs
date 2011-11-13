using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenter.Models;
using System.Web.Security;
using System.Security.Cryptography;
namespace DiagnosticCenter.Classes
{
    public class UserRepository
    {
        //--Membership provider--//
        private DiagnosticsDBEntities context = new DiagnosticsDBEntities();

        public void ChangePassword(string username, string newPassword)
        {
            User user = context.Users.Where(u => u.UserName == username).First();
            User new_user = user;
            new_user.Password = newPassword;
            context.ApplyCurrentValues(user.EntityKey.EntitySetName, new_user);
            context.SaveChanges();
        }
        
        public MembershipUser CreateUser(string username, string password, string email)
        {
            User user = new User();

            user.UserName = username;
            user.Email = email;
            user.Password = password;
            user.PasswordSalt = CreateSalt();
            user.Comments = "";
            user.CreatedDate = DateTime.Now;
            user.LastLoginDate = DateTime.Now;
            user.IsActivated = true;
            user.IsLockedOut = false;
            user.LastLockedOutDate = DateTime.Now;
           
            context.AddToUsers(user);
            context.SaveChanges();
            return GetUser(username);
        }
        
        public bool DeleteUser(string username)
        {
            User user = context.Users.Where(u => u.UserName == username).First();
            try
            {
                context.Users.DeleteObject(user);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public MembershipUserCollection FindUsersByEmail(string emailToMatch)
        {
           MembershipUserCollection lst = new MembershipUserCollection();  
           IEnumerable<User> _user = context.Users.Where(u => u.Email == emailToMatch);
           foreach(User u in _user)
                lst.Add(GetUser(u.UserName));
           return lst;
        }

        public MembershipUserCollection FindUsersByName(string usernameToMatch)
        {
           MembershipUserCollection lst = new MembershipUserCollection();  
           IEnumerable<User> _user = context.Users.Where(u => u.UserName == usernameToMatch);
           foreach(User u in _user)
                lst.Add(GetUser(u.UserName));
           return lst;
        }

        public MembershipUserCollection GetAllUsers()
        {
            MembershipUserCollection lst = new MembershipUserCollection();
            IEnumerable<User> _user = context.Users;
            foreach(User u in _user)
                lst.Add(GetUser(u.UserName));
            return lst;
        }
        
        public MembershipUser GetUser(string username)
         {
             try
             {
                 User _user = context.Users.Where(u => u.UserName == username).First();
                    string _username = _user.UserName;
                    int _providerUserKey = _user.UserID;
                    string _email = _user.Email;
                    string _passwordQuestion = "";
                    string _comment = _user.Comments;
                    bool _isApproved = _user.IsActivated;
                    bool _isLockedOut = _user.IsLockedOut;
                    DateTime _creationDate = _user.CreatedDate;
                    DateTime _lastLoginDate = _user.LastLoginDate;
                    DateTime _lastActivityDate = DateTime.Now;
                    DateTime _lastPasswordChangedDate = DateTime.Now;
                    DateTime _lastLockedOutDate = (DateTime)_user.LastLockedOutDate;

                    MembershipUser user = new MembershipUser("CustomMembershipProvider", _username, _providerUserKey, _email,
                                                            _passwordQuestion,_comment,_isApproved,_isLockedOut,
                                                            _creationDate,_lastLoginDate,_lastActivityDate,_lastPasswordChangedDate,
                                                            _lastLockedOutDate);

                    return user;
             }
               catch(Exception) { return null; }
         }

        public string GetPassword(string username)
        {
            User user = context.Users.Where(u => u.UserName == username).FirstOrDefault();
            return user.Password;
        }

        public string GetUserNameByEmail(string email) 
        {
            User user = context.Users.Where(u => u.Email == email).First();
            return user.UserName;
        }

        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                    saltAndPwd, "sha1");
            return hashedPwd;
        }

        //--Role provider--//

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var user in usernames)
            {
                User _user = context.Users.Include("Roles").Where(u => u.UserName == user).First();
                foreach (var role in roleNames)
                {
                    Role _role = context.Roles.Where(r => r.RoleName == role).First();
                    _user.Roles.Add(_role);
                }
            }
            context.SaveChanges();
        }

        public void CreateRole(string roleName)
        {
            Role role = new Role();
            role.RoleName = roleName;
            context.AddToRoles(role);
            context.SaveChanges();
        }

        public bool DeleteRole(string roleName)
        {
            try
            {
                Role _role = context.Roles.Include("Users").Where(r => r.RoleName == roleName).First();
                _role.Users.Clear();
                context.Roles.DeleteObject(_role);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public string[] GetAllRoles()
        {
            string roles = "";
            IEnumerable<Role> _role = context.Roles;
            foreach (var r in _role)
            {
                roles += r.RoleName + ",";
            }
            if (roles.Length > 0)
            {
                roles = roles.Substring(0, roles.Length - 1);
                return roles.Split(',');
            }
            return new string[0];
        }

        public string[] GetRolesForUser(string username)
        {
            string role = "";
            IEnumerable<Role> roles = context.Roles;
            foreach (var r in roles)
            {
                if (r.Users.Where(u => u.UserName == username).Count() != 0)
                    role += r.RoleName + ",";
            }
            if (role.Length > 0) { role = role.Substring(0, role.Length - 1); return role.Split(','); }
            return new string[0];
        }

        public string[] GetUsersInRole(string roleName)
        {
            string users = "";
            IEnumerable<User> _users = context.Users;
            foreach (var u in _users)
            {
                if (u.Roles.Where(r => r.RoleName == roleName).Count() != 0)
                    users += u.UserName + ",";
            }
            if (users.Length > 0)
            {
                users = users.Substring(0, users.Length - 1);
                return users.Split(',');
            }
            return new string[0];
        }

        public bool IsUserInRole(string username, string roleName)
        {
            User user = context.Users.Include("Roles").Where(u => u.UserName == username).First();
            try
            {
                Role role = user.Roles.Where(r => r.RoleName == roleName).First();
                return true;
            }
            catch (Exception) { return false; }
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var user in usernames)
            {
                User _user = context.Users.Include("Roles").Where(u => u.UserName == user).First();
                foreach (var role in roleNames)
                {
                    Role _role = context.Roles.Where(r => r.RoleName == role).First();
                    _user.Roles.Remove(_role);
                }
            }
            context.SaveChanges();
        }

        public bool RoleExists(string roleName)
        {
            try
            {
                Role role = context.Roles.Where(r => r.RoleName == roleName).First();
                return true;
            }
            catch (Exception) { return false; }
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string users = "";
            Role role = context.Roles.Where(r => r.RoleName == roleName).First();
            List<User> user = role.Users.Where(u => u.UserName.Contains(usernameToMatch)).ToList();
            foreach (var u in user)
                users += u.UserName + ",";
            if (users.Length > 0)
            {
                users = users.Substring(0, users.Length - 1);
                return users.Split(',');
            }
            return new string[0];
        }
    } 
}