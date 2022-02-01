using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Email
    {
        public string EmailFrom
        {
            get;
            set;
        }
        public string SmtpServer 
        {
            get; 
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public bool emailIsSSL
        {
            get;
            set;
        }
        public string EmailTo
        {
            get;
            set;
        }
        public string EmailSubject
        {
            get;
            set;
        }
        public string EmailBody
        { 
            get;
            set;
        }
        public string EmailFromName
        {
            get;
            set;
        }
        public string EmailToName
        {
            get;
            set;
        }
    }
}
