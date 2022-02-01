using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using log4net;

namespace EmailSender.Service
{
    public class SendEmail
    {
        public SendEmail()
        {

        }
        public static JsonResult SendEmailToUser(string Server, Int16 Port, Boolean IsSSL, string EmailFrom,string MessageFromName, string Password, string EmailTo, string MessageToName, string EmailBody, string EmailSubject, ILog log)
        {

            var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Suhail", "fyp.ubit92@gmail.com"));
            //message.From.Add(new MailboxAddress("Suhail","suhail.rajani@yahoo.com"));
            message.From.Add(new MailboxAddress("Suhail", EmailFrom));
            message.To.Add(new MailboxAddress("Suhail", EmailTo));
            message.Subject = EmailSubject;
            message.Body = new TextPart("plain")
            {
                Text = EmailBody
            };
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(Server, Port, IsSSL);
                    client.Authenticate(EmailFrom, Password);
                    //client.Authenticate("suhail.rajani@yahoo.com", "okpjqojrmjguorrj");
                    //client.Authenticate("fyp.ubit92@gmail.com", "suhail1234");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return new JsonResult("Email Sent");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
                object result = new ErrorDetails { StatusCode = 500, Message = ex.Message.ToString(), Result = "Error" };
                return new JsonResult(result);
            }
            
        }
    }
}
