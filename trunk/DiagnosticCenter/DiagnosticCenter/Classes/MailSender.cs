using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
namespace DiagnosticCenter.Classes
{
    public class MailSender
    {
        private MailMessage message = new MailMessage();
        private SmtpClient smtp = new SmtpClient();
        private Random r = new Random();
        private string password;

        internal string GeneratePassword()
        {   
            password =  Membership.GeneratePassword(8, 3);
            return password;
        }
        
        internal void SendPassword(string sendTo)
        {
            
            message.From = new MailAddress("ss.aspnet.team2@gmail.com");
            message.To.Add(new MailAddress(sendTo));
            
            message.Subject = " Діагностичний центр.";
            message.Body = "Доброго дня! Вам надіслано пароль від вашого акаунту на сайті.\n" +
                            "Логін: " + sendTo + "\n" + "Пароль: " + password;
            message.Priority = MailPriority.Normal;
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}