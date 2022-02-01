using Agilosoft.AgileTimeKeeper.Api.Business_Exceptions;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : Controller
    {
        private ExecutorController _executor;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(WorkspaceController));
        private IHubContext<NotificationHub> _hub;
        public WorkspaceController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }

        [Route("DeleteWorkspaceUserRelationship")]
        public JsonResult DeleteWorkspaceUserRelationship(Workspace workspace)
        {
            Object[] parameterArray =
            {
                "@iWorkspaceUserRelationshipId", workspace.WorkspaceUserRelationshipId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_WorkspaceUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }
        [Route("GetWorkspaceWRTUser")]
        public JsonResult GetWorkspaceWRTUser(User user)
        {
            Object[] parameterArray =
            {
                "@vcUserEmail",user.UserEmail
            };
            return new JsonResult(_executor.ExecuteQuery("usp_Get_WorkspaceWRTUser", parameterArray, log));
        }

        [Route("GetAllRecordOfWorkspaceWRTUser")]
        public JsonResult GetAllRecordOfWorkspaceWRTUser(User user)
        {
            Object[] parameterArray =
            {
                "@vcUserEmail", user.UserEmail
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_WorkspaceListWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetWorkspaceUserRelationshipId")]
        public JsonResult GetWorkspaceUserRelationshipId(Workspace workspace)
        {
            Object[] parameterArray =
            {
                "@iUserId", workspace.UserId,
                "@iWorkspaceId", workspace.pkWorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_WorkspaceUserRelationshipId", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertWorkspaceUserRelationship")]
        public JsonResult InsertWorkspaceUserRelationship(Workspace workspace)
        {
            Object[] parameterArray =
            {
                "@iUserId", workspace.UserId,
                "@iWorkspaceId", workspace.pkWorkspaceId,
                "@iUserRoleId", workspace.UserRoleId,
                "@iWorkspaceActiveStatus", workspace.WorkspaceActiveStatus,
                "@iUserActiveStatus", workspace.UserActiveStatus
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_WorkspaceUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertWorkspace")]
        public JsonResult InsertWorkspace(Workspace workspace)
        {
            Object[] parameterArray =
            {
                "@vcWorkspaceName", workspace.WorkspaceName,
                "@iOrganizationId", workspace.fkWorkspaceOrganizationId,
                "@dTotalTime", workspace.WorkspaceTotalTime,
                "@iWorkspaceTimeEntryLockTime", workspace.WorkspaceTimeEntryLockTime,
                "@iUserId", workspace.UserId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_Workspace", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateWorkspace")]
        public JsonResult UpdateWorkspace(Workspace workspace)
        {
            Object[] parameterArray =
            {
                "@vcWorkspaceName", workspace.WorkspaceName,
                "@iOrganizationId", workspace.fkWorkspaceOrganizationId,
                "@dTotalTime", workspace.WorkspaceTotalTime,
                "@iWorkspaceId", workspace.pkWorkspaceId,
                "@iWorkspaceTimeEntryLockTime", workspace.WorkspaceTimeEntryLockTime,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_Workspace", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateActiveStatus")]
        public JsonResult UpdateActiveStatus(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iFlag", workspace.Flag,
                "@iRelationshipId", workspace.RelationshipId,
                "@iActiveStatus", workspace.UserActiveStatus
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_ActiveStatus", paramenterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateUserRole")]
        public JsonResult UpdateUserRole(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iRoleId", workspace.UserRoleId,
                "@iRelationshipId", workspace.RelationshipId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_UserRole", paramenterArray, log));

            return new JsonResult(result);
        }

        [Route("LastestActivityOfTeamMembers")]
        public JsonResult GetLastestActivityOfTeamMembers(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iWorkSpaceId", workspace.pkWorkspaceId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TeamMembersActivities", paramenterArray, log));
            return new JsonResult(result);
        }

        [Route("TotalTimeOfTeamMembers")]
        public JsonResult GetTotalTimeOfTeamMembersWRTProject(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iWorkSpaceId", workspace.pkWorkspaceId,
                "@dStartdate", workspace.Startdate,
                "@dEnddate", workspace.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TotalTimeOfTeamMembersWRTProject", paramenterArray, log));
            return new JsonResult(result);
        }

        [Route("TotalTimeOfTeamMembersBillable")]
        public JsonResult GetTotalTimeOfTeamMembersWRTBillable(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iWorkSpaceId", workspace.pkWorkspaceId,
                "@dStartdate", workspace.Startdate,
                "@dEnddate", workspace.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TotalTimeOfTeamMembersWRTBillability", paramenterArray, log));
            return new JsonResult(result);
        }


        [Route("TeamReportFilters")]
        public JsonResult GetTeamReportFilters(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iUserId", workspace.UserId,
                "@iWorkSpaceId", workspace.pkWorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TeamMembersForReport", paramenterArray, log));
            //DataTable projects = (_executor.ExecuteQuery("usp_Get_ProjectsForReport", paramenterArray, log));
            //DataTable task = (_executor.ExecuteQuery("usp_Get_TasksForReport", paramenterArray, log));
            //return new JsonResult( (new JsonResult(team), new JsonResult(projects), new JsonResult(task) ));
            return new JsonResult(result);

        }

        [Route("ProjectReportFilters")]
        public JsonResult GetProjectReportFilters(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iUserId", workspace.UserId,
                "@iWorkSpaceId", workspace.pkWorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_ProjectsForReport", paramenterArray, log));
            return new JsonResult(result);

        }

        [Route("TaskReportFilters")]
        public JsonResult GetTaskReportFilters(Workspace workspace)
        {
            Object[] paramenterArray =
            {
                "@iUserId", workspace.UserId,
                "@iWorkSpaceId", workspace.pkWorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TasksForReport", paramenterArray, log));
            return new JsonResult(result);

        }

    }
}
