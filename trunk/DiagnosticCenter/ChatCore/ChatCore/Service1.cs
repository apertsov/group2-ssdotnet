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
        private ChatUser _user;
        
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

            Console.WriteLine("+" + name);

            return res;
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
            Console.WriteLine("-" + _user.Name);
            _user = null;
        }
    }
}
