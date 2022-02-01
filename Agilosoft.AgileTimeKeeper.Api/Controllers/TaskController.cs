using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Security.Cryptography;

using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Agilosoft.AgileTimeKeeper.Api.Business_Exceptions;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ExecutorController _executor;
        private IHubContext<NotificationHub> _hub;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(TaskController));

        public TaskController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }


        [Route("CheckIfTaskExist")]
        public JsonResult CheckIfTaskExist(Task task)
        {
            Object[] parameterArray =
            {
                "@vcTaskName", task.TaskName,
                "@iProjectId", task.fkTaskProjectId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Chk_IfTaskExist", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("DeleteTaskUserRelationship")]
        public JsonResult DeleteTaskUserRelationship(Task task)
        {
            Object[] parameterArray =
            {
                "@iTaskUserRelationshipId", task.TaskUserRelationshipId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_TaskUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("DeleteTask")]
        public JsonResult DeleteTask(Task task)
        {
            Object[] parameterArray =
            {
                "@iTaskId", task.pkTaskId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_Task", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllTaskWRTProject")]
        public JsonResult GetCountOfAllTaskWRTProject(Task task)
        {
            Object[] parameterArray =
            {
                "@iProjectId", task.fkTaskProjectId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllTaskWRTProject", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfTaskWRTProject")]
        public JsonResult GetAllRecordOfTaskWRTProject(Task task)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", task.PageNumber,
                "@iPageSize", task.PageSize,
                "@iProjectId", task.fkTaskProjectId,
                "@iOrderBy", task.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllTask", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllUserWRTTask")]
        public JsonResult GetCountOfAllUserWRTTask(Task task)
        {
            Object[] parameterArray =
            {
                "@iTaskId", task.pkTaskId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllUserWRTTask", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfUserWRTTask")]
        public JsonResult GetAllRecordOfUserWRTTask(Task task)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", task.PageNumber,
                "@iPageSize", task.PageSize,
                "@iTaskId", task.pkTaskId,
                "@iOrderBy", task.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_PaginationRecord_AllUserWRTTask", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertTaskUserRelationship")]
        public JsonResult InsertTaskUserRelationship(Task task)
        {
            Object[] parameterArray =
            {
                "@iUserId", task.UserId,
                "@iTaskId", task.pkTaskId,
                "@iUserActiveStatus", task.IsActive
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_TaskUserRelationship", parameterArray, log));
            if (result.Rows[0]["Result"].ToString() == "Error")
            {
                throw new DuplicationException(result.Rows[0]["Message"].ToString());
            }

            return new JsonResult(result);
        }

        [Route("InsertTask")]
        public JsonResult InsertTask(Task task)
        {
            Object[] parameterArray =
            {
                "@vcTaskName", task.TaskName,
                "@iProjectId", task.fkTaskProjectId,
                "@dTotalTime", task.TaskTotalTime,
                "@iActiveStatus", task.IsActive,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_Task", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateTask")]
        public JsonResult UpdateTask(Task task)
        {
            Object[] parameterArray =
            {
                "@vcTaskName", task.TaskName,
                "@iProjectId", task.fkTaskProjectId,
                "@dTotalTime", task.TaskTotalTime,
                "@iTaskId", task.pkTaskId,
                "@iActiveStatus", task.IsActive
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_task", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetTaskWRTUser")]
        public JsonResult GetTaskWRTUser(Task task)
        {
            Object[] parameterArray =
            {
                "@iUserId", task.UserId,
                "@iWorkSpaceId", task.fkProjectWorkspaceId,
                "@dStartdate", task.Startdate,
                "@dEnddate", task.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllTaskWRTUserWithTotalTime", parameterArray, log));

            return new JsonResult(result);
        }

    }
}
