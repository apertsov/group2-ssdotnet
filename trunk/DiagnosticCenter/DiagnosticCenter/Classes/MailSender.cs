using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

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
            password = "";
            string digit = "0123456789";
            string letter = "abcdefghijklmnopqrstuvwxyz";
            int arr_index = 0;
            
            while (password.Length != 8)
            {

                arr_index = r.Next(1, 3);
                if (arr_index == 1) password += digit[r.Next(0, 10)];
                else password += letter[r.Next(0, 26)];
            }
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