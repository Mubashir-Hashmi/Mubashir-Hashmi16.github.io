using Agilosoft.AgileTimeKeeper.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender.Service;
using log4net;
using Microsoft.AspNetCore.SignalR;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using System.Data;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private ExecutorController _executor;
        private readonly Email _emailConfiguration;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(NotificationController));
        private IHubContext<NotificationHub> _hub;


        public NotificationController(ExecutorController executorController, Email emailConfiguration, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _emailConfiguration = emailConfiguration;
            _hub = hub;
        }

        [Route("SendNotification")]
        public JsonResult SendNotification(Notification notification)
        {
            Object[] parameterArray =
            {
                "@iNotificationFromUserId", notification.NotificationFromUser,
                "@iNotificationToUserId", notification.NotificationToUser,
                "@iNotificationFromWorkspaceId", notification.WorkspaceId,
                "@iNotificationType", notification.pkNotificationTypeId,
                "@dtNotificationDate", notification.NotificationDate,
                "@vcDescription", notification.NotificaionDescription
            };
            if(notification.IsEmailSendingEnabled == 1)
            {
                _executor.SendEmailToUser(_emailConfiguration.SmtpServer, Convert.ToInt16(_emailConfiguration.Port),
                    Convert.ToBoolean(_emailConfiguration.emailIsSSL), _emailConfiguration.EmailFrom, _emailConfiguration.EmailFromName,
                    _emailConfiguration.Password, notification.EmailTo, notification.EmailToName, notification.EmailBody, notification.EmailSubject, log);
            }


            DataTable result = (_executor.ExecuteQuery("usp_Ins_Notification", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetNotification")]
        public JsonResult GetNotification(Notification notification)
        {

            Object[] parameterArray =
            {
                "@iUserId", notification.NotificationToUser
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Notification", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetLastTimeEntryPostTime")]
        public JsonResult GetLastTimeEntryPostTime(Notification notification)
        {
            Object[] parameterArray =
            {
                "@iUserId", notification.NotificationFromUser,
                "@iWorkspaceId", notification.WorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_LastTimeEntryPostTime", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("CheckRunningTimeEntry")]
        public JsonResult CheckRunningTimeEntry(Notification notification)
        {
            Object[] parameterArray =
            {
                "@iUserId", notification.NotificationFromUser,
                "@iWorkspaceId", notification.WorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Chk_RunningTimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllNotificationWRTUser")]
        public JsonResult GetAllNotificationWRTUser(Notification notification)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", notification.PageNumber,
                "@iPageSize", notification.PageSize,
                "@iUserId", notification.NotificationToUser,
                "@iOrderBy", notification.OrderBy,
                "@iNotificationType", notification.pkNotificationTypeId,
                "@iNotificationFrom", notification.NotificationFromUser
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_PaginationRecord_AllNotificaitonWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllNotificationWRTUser")]
        public JsonResult GetCountOfAllNotificationWRTUser(Notification notification)
        {
            Object[] parameterArray =
            {
                "@iUserId", notification.NotificationToUser,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllNotificationWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllNotificationType")]
        public JsonResult GetAllNotificationType(Notification notification)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", notification.PageNumber,
                "@iPageSize", notification.PageSize,
                "@iOrderBy", notification.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_PaginationRecord_AllNotificaitonType", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllNotificationType")]
        public JsonResult GetCountOfAllNotificationType()
        {
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllNotificationType",log));

            return new JsonResult(result);
        }


    }
}
