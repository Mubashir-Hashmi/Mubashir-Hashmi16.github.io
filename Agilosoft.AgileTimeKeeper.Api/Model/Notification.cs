using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Notification
    {
        public int pkNotificationTypeId
        {
            get;
            set;
        }
        public string NotificationType
        {
            get;
            set;
        }
        public int IsEmailSendingEnabled
        {
            get;
            set;
        }
        public int pkNotificationId
        {
            get;
            set;
        }
        public int NotificationToUser
        {
            get;
            set;
        }
        public int NotificationFromUser
        {
            get;
            set;
        }
        public DateTime NotificationDate
        {
            get;
            set;
        }
        public string NotificaionDescription
        {
            get;
            set;
        }
        public int WorkspaceId
        {
            get;
            set;
        }
        public int PageNumber
        {
            get;
            set;
        }
        public int PageSize
        {
            get;
            set;
        }
        public int OrderBy
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
        public string EmailToName
        {
            get;
            set;
        }
    }
}
