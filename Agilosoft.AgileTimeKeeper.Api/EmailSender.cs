using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Agilosoft.AgileTimeKeeper.Api.Model;
using EmailSender.Service;

namespace Agilosoft.AgileTimeKeeper.Api
{
    public class EmailSender
    {
        
        public void SendEmailToUser(Email email)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            string emailSender = email.EmailFrom;
            string emailSenderPassword = email.Password;
            string emailSenderHost = email.SmtpServer;
            int emailSenderPort = Convert.ToInt16(email.Port);
            Boolean emailIsSSL = Convert.ToBoolean(email.emailIsSSL);
            string mailText = email.EmailBody;
            string subject = email.EmailSubject;
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailSender);
                mail.To.Add(email.EmailTo);
                mail.Subject = subject;
                mail.Body = mailText;
                mail.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient(emailSenderHost, emailSenderPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailSender, emailSenderPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

        }
    }
}
