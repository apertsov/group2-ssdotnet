using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using DiagnosticCenter.Classes;

public class CMembershipProvider : MembershipProvider
{
    #region Globals
    private int newPasswordLength = 8;
    private string eventSource = "DCMembershipProvider";
    private string eventLog = "Application";
    private string exceptionMessage = "An exception occurred. Please check the Event Log.";
    private string connectionString;
    private MachineKeySection machineKey;
    
    private bool pWriteExceptionsToEventLog;
    public bool WriteExceptionsToEventLog
    {
        get { return pWriteExceptionsToEventLog; }
        set { pWriteExceptionsToEventLog = value; }
    }
    #endregion

    public override void Initialize(string name, NameValueCollection config)
    {
        if (config == null)
            throw new ArgumentNullException("config");
        if (name == null || name.Length == 0)
            name = "DCMembershipProvider";
        if (String.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Sample DC Membership provider");
        }
        base.Initialize(name, config);
        pApplicationName = GetConfigValue(config["applicationName"],
                                        System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
        pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
        pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
        pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));
        pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
        pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
        pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
        pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
        pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

        string temp_format = config["passwordFormat"];
        if (temp_format == null)
        {
            temp_format = "Hashed";
        }
        switch (temp_format)
        {
            case "Hashed":
                pPasswordFormat = MembershipPasswordFormat.Hashed;
                break;
            case "Encrypted":
                pPasswordFormat = MembershipPasswordFormat.Encrypted;
                break;
            case "Clear":
                pPasswordFormat = MembershipPasswordFormat.Clear;
                break;
            default:
                throw new ProviderException("Password format not supported.");
        }
        
        Configuration cfg =
          WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

        if (machineKey.ValidationKey.Contains("AutoGenerate"))
            if (PasswordFormat != MembershipPasswordFormat.Clear)
                throw new ProviderException("Hashed or Encrypted passwords " +
                                            "are not supported with auto-generated keys.");
    }
    private string GetConfigValue(string configValue, string defaultValue)
    {
        if (String.IsNullOrEmpty(configValue))
            return defaultValue;
        return configValue;
    }

    #region Properties
    private string pApplicationName;
    private bool pEnablePasswordReset;
    private bool pEnablePasswordRetrieval;
    private bool pRequiresQuestionAndAnswer;
    private bool pRequiresUniqueEmail;
    private int pMaxInvalidPasswordAttempts;
    private int pPasswordAttemptWindow;
    private MembershipPasswordFormat pPasswordFormat;

    public override string ApplicationName
    {
        get { return pApplicationName; }
        set { pApplicationName = value; }
    }

    public override bool EnablePasswordReset
    {
        get { return pEnablePasswordReset; }
    }


    public override bool EnablePasswordRetrieval
    {
        get { return pEnablePasswordRetrieval; }
    }


    public override bool RequiresQuestionAndAnswer
    {
        get { return pRequiresQuestionAndAnswer; }
    }


    public override bool RequiresUniqueEmail
    {
        get { return pRequiresUniqueEmail; }
    }


    public override int MaxInvalidPasswordAttempts
    {
        get { return pMaxInvalidPasswordAttempts; }
    }


    public override int PasswordAttemptWindow
    {
        get { return pPasswordAttemptWindow; }
    }


    public override MembershipPasswordFormat PasswordFormat
    {
        get { return pPasswordFormat; }
    }

    private int pMinRequiredNonAlphanumericCharacters;
    public override int MinRequiredNonAlphanumericCharacters
    {
        get { return pMinRequiredNonAlphanumericCharacters; }
    }
    private int pMinRequiredPasswordLength;
    public override int MinRequiredPasswordLength
    {
        get { return pMinRequiredPasswordLength; }
    }
    private string pPasswordStrengthRegularExpression;
    public override string PasswordStrengthRegularExpression
    {
        get { return pPasswordStrengthRegularExpression; }
    }
    
    #endregion

    #region Methods

   public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
       if (!ValidateUser(username, oldPassword))
                return false;
            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPassword, true);
            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
       UserRepository user = new UserRepository();
       string encoded = EncodePassword(newPassword); 
       user.ChangePassword(username, encoded);
       return true;
    }

    public override MembershipUser CreateUser(string username,
                                              string password,
                                              string email,string passwordQuestion,
                                              string passwordAnswer,
                                              bool isApproved,
                                              object providerUserKey,
                                              out MembershipCreateStatus status)
    {
        ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username,password,true);
        OnValidatingPassword(args);
        if (args.Cancel)
        {
           status = MembershipCreateStatus.InvalidPassword;
           return null;
        }
        if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
        {
           status = MembershipCreateStatus.DuplicateEmail;
           return null;
        }
        MembershipUser u = GetUser(username, false);
        if (u == null)
        {
           UserRepository _user = new UserRepository();
           string pwd = EncodePassword(password);
            _user.CreateUser(username, pwd, email);
           status = MembershipCreateStatus.Success;
           return GetUser(username, false);
        }
        else
        {
           status = MembershipCreateStatus.DuplicateUserName;
        }
        return null;
}

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        UserRepository _user = new UserRepository();
        return _user.DeleteUser(username);
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {       
        UserRepository _user = new UserRepository();
        MembershipUserCollection users = _user.FindUsersByEmail(emailToMatch);
        totalRecords = users.Count;
        return users;
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        UserRepository _user = new UserRepository();
        MembershipUserCollection users = _user.FindUsersByName(usernameToMatch);
        totalRecords = users.Count;
        return users;
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        UserRepository _user = new UserRepository();
        MembershipUserCollection users = _user.GetAllUsers();
        totalRecords = users.Count;
        return users;
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
       if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }
            UserRepository _user = new UserRepository();
            string password = _user.GetPassword(username);
              
            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = UnEncodePassword(password);
            }

            return password;
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        UserRepository _user = new UserRepository();
        return _user.GetUser(username);
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        return null;
    }

    public override string GetUserNameByEmail(string email)
    {
        UserRepository _user = new UserRepository();
        return _user.GetUserNameByEmail(email);
    }
  
    public override bool ValidateUser(string username, string password)
    {
       
       UserRepository user = new UserRepository();
       MembershipUser u = user.GetUser(username);
       if( u == null ) return false;
       string pwd = user.GetPassword(username);
       if(!CheckPassword(password,pwd)) return false;
       return true;
    }

    private bool CheckPassword(string password, string dbpassword)
    {
        string pass1 = password;
        string pass2 = dbpassword;

        switch (PasswordFormat)
        {
            case MembershipPasswordFormat.Encrypted:
                pass2 = UnEncodePassword(dbpassword);
                break;
            case MembershipPasswordFormat.Hashed:
                pass1 = EncodePassword(password);
                break;
            default:
                break;
        }
        if (pass1 == pass2)
        {
            return true;
        }
        return false;
    }

    private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                      Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword =
                      Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

    private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

    private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    // Not implemented
    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        return false;
    }
    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }
    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }
    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }
    #endregion
}
