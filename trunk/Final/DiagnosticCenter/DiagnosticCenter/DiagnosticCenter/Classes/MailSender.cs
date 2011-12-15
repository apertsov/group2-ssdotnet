using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
namespace DiagnosticCenter.Classes
{
    /// <summary>
    /// Клас <c>MailSender</c> використовується для
    /// генерування паролів користувачів та відправки 
    /// електронних листів з даними для авторизації
    /// користувачам на пошту 
    /// </summary>
    /// <param name="message">Повідомлення електронної пошти</param>
    /// <param name="smtp">Екземпляр SMTP протоколу</param>
    /// <param name="r">Генератор випадкового числа</param>
    /// <param name="password">Згенерований пароль</param>
    public class MailSender
    {
        private MailMessage message = new MailMessage(); 
        private SmtpClient smtp = new SmtpClient();
        private Random r = new Random();
        private string password;

        /// <summary>
        /// Метод генерує пароль для користувача
        /// </summary>
        /// <returns>Стрічку з паролем</returns>
        internal string GeneratePassword()
        {
            this.password = Membership.GeneratePassword(8, 0);
            return password;
        }
        
        /// <summary>
        /// Метод відправляє лист на електронну адресу
        /// </summary>
        /// <param name="sendTo">Електронна пошта одержувача</param>
        /// <param name="username">Ім'я користувача</param>
        internal void SendPassword(string sendTo, string username, out string error)
        {
           
            error = "";
            message.From = new MailAddress("ss.aspnet.team2@gmail.com");
            message.To.Add(new MailAddress(sendTo));
            
            message.Subject = " Діагностичний центр.";
            if(username == null)
            message.Body = "Доброго дня! Вам надіслано пароль від вашого акаунту на сайті.\n" +
                            "Логін: " + sendTo + "\n" + "Пароль: " + password;
            else
                message.Body = "Доброго дня! Вам надіслано пароль від вашого акаунту на сайті.\n" +
                           "Логін: " + username + "\n" + "Пароль: " + password;
           
            message.Priority = MailPriority.Normal;
         /*   try
            {
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (SmtpException) { error = "Неможливо надіслати повідомлення на пошту!"; } */
            
            
        }
    }
}