using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

using System.Data.SqlClient;

class ChatUser
{
    public string Name;
    public ChatCore.IChatCallback Callback;
}

class Account
{
    public string Login,Password;
    public static bool operator==( Account a, Account b )
    {
        return a.Login == b.Login && a.Password == b.Password;
    }
    public static bool operator!=(Account a, Account b)
    {
        return a.Login != b.Login || a.Password != b.Password;
    }
}

namespace ChatCore
{
    [
        ServiceBehavior
        (
            InstanceContextMode = InstanceContextMode.PerSession,
            ConcurrencyMode = ConcurrencyMode.Multiple
        )
    ]
    public class ChatService : IChatService
    {
        static List<ChatUser> users = new List<ChatUser>();
        static List<Account> accounts = new List<Account>();
        private ChatUser _user;

        static void SaveAccounts()
        {
            StreamWriter sw = File.CreateText( "registers.txt" );
            sw.WriteLine( accounts.Count );
            foreach (Account a in accounts)
            {
                sw.WriteLine(a.Login);
                sw.WriteLine(a.Password);
            }
            sw.Close();
        }

        public static void AcInfo()
        {
            Console.WriteLine("-----------------");
            foreach (Account a in accounts)
            {
                Console.WriteLine(a.Login);
            }
        }

        static ChatService()
        {
            // ////////////
            Console.WriteLine("--------");

            StreamReader fs = File.OpenText( "registers.txt" );
            int n = int.Parse( fs.ReadLine() );
            for(int i=0;i<n;i++)
                accounts.Add( new Account(){ Login = fs.ReadLine(), Password = fs.ReadLine()});
            fs.Close();
        }
/***************************************/
        public static bool ExistsEmploye(string userName)
        {
            using (SqlConnection connection = new SqlConnection("server=.\\SQLEXPRESS;Trusted_connection=yes;database=DiagnosticsDB;connection timeout=30"))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Username FROM Employees;";
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                        if (userName == reader[0].ToString())
                            return true;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
        public static bool Authenticate(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection("server=.\\SQLEXPRESS;Trusted_connection=yes;database=DiagnosticsDB;connection timeout=30"))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Username,Password FROM Users;";
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                        if (userName == reader[0].ToString() && password == reader[1].ToString())
                            return true;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
/***************************************/
        public List<string> Join(string name,string password)
        {
            if (password != null && !Authenticate( name, password ) )
                        return null;

            if (password == null && ExistsEmploye(name)) return null;

            IChatCallback callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();


            List<string> res = new List<string>();
            foreach (ChatUser x in users)
                res.Add(x.Name);


            foreach (ChatUser x in users)
                x.Callback.UserEnter(name);

            _user = new ChatUser() { Name = name, Callback = callback };
            users.Add(_user);

            Console.WriteLine(name);
            Console.WriteLine(callback);

            return res;
        }

        private bool ExistsEmpoye(string name)
        {
            throw new NotImplementedException();
        }

        public void Send(string msg)
        {
            foreach (ChatUser x in users)
                x.Callback.Receive(_user.Name, msg);
            
            Console.WriteLine(msg);
        }

        public void SendPrivate(string name, string msg)
        {
            users.Find(x => x.Name == name).Callback.ReceivePrivate( _user.Name, msg );
        }

        public void Leave()
        {
            users.Remove(_user);
            foreach (ChatUser x in users)
                x.Callback.UserLeave(_user.Name);
            _user = null;
        }

        public List<string> Register(string name, string password)
        {
            if (accounts.Exists(a => a.Login == name && a.Password == password))
                return null;
            else
            {
                accounts.Add(new Account { Login = name, Password = password });
                SaveAccounts();    
                return Join(name, password);
            }
        }

        public void ChangePassword(string password)
        {
            Account x = accounts.Find(a => a.Login == _user.Name);
            if (password == null)
            {
                accounts.Remove(x);
                SaveAccounts();
            }
            else
                x.Password = password;
        }
    }
}
