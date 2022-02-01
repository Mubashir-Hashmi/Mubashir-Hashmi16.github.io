using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Security.Cryptography;

using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Microsoft.AspNetCore.SignalR;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Agilosoft.AgileTimeKeeper.Api.Business_Exceptions;
using Newtonsoft.Json;
using Agilosoft.AgileTimeKeeper.Api.DataProcessing;
using System.Collections.Generic;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ExecutorController _executor;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ProjectController));
        private IHubContext<NotificationHub> _hub;

        public ProjectController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }

        [Route("CheckIfProjectExist")]
        public JsonResult CheckIfProjectExist(Project project)
        {
            Object[] parameterArray =
            {
                "@vcProjectName", project.ProjectName,
                "@iWorkspaceId", project.fkProjectWorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Chk_IfProjectExist", parameterArray, log));

            return new JsonResult(result);
        }


        [Route("CheckProjectUserRelationship")]
        public JsonResult CheckProjectUserRelationship(Project project)
        {
            Object[] parameterArray =
            {
                "@iUserId", project.UserId,
                "@iProjectId", project.pkProjectId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Chk_ProjectUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("DeleteProject")]
        public JsonResult DeleteProject(Project project)
        {
            Object[] parameterArray =
            {
                "@iProjectId", project.pkProjectId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_Project", parameterArray, log));

            return new JsonResult(result);
        }
        
        [Route("DeleteProjectUserRelationship")]
        public JsonResult DeleteProjectUserRelationship(Project project)
        {
            Object[] parameterArray =
            {
                "@iProjectUserRelationshipId", project.ProjectUserRelationshipId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_ProjectUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllProject")]
        public JsonResult GetCountOfAllProject(Project project)
        {
            Object[] parameterArray =
            {
                "@iWorkspaceId", project.fkProjectWorkspaceId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllProjectWRTWorkspace", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllProjectWRTUser")]
        public JsonResult GetCountOfAllProjectWRTUser(Project project)
        {
            Object[] parameterArray =
            {
                "@iUserId", project.UserId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllProjectWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllUserWRTProject")]
        public JsonResult GetAllUserCountWRTProject(Project project)
        {
            Object[] parameterArray =
            {
                "@iProjectId", project.pkProjectId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllUserWRTProject", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfProjectWRTWorkspace")]
        public JsonResult GetProjectListWRTWorkspace(Project project)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", project.PageNumber,
                "@iPageSize", project.PageSize,
                "@iWorkspaceId", project.fkProjectWorkspaceId,
                "@iOrderBy", project.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_PaginationRecord_AllProjectWRTWorkspace", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllProjects")]
        public JsonResult GetProjectListWRTUser(Project project)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", project.PageNumber,
                "@iPageSize", project.PageSize,
                "@iWorkspaceId", project.fkProjectWorkspaceId,
                "@iUserId", project.UserId,
                "@iOrderBy", project.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllProject", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfUserWRTProject")]
        public JsonResult GetAllRecordOfUserWRTProject(Project project)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", project.PageNumber,
                "@iPageSize", project.PageSize,
                "@iProjectId", project.pkProjectId,
                "@iOrderBy", project.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllUserWRTProject", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertProjectUserRelationship")]
        public JsonResult InsertProjectUserRelationship(Project project)
        {
            Object[] parameterArray =
            {
                "@iUserId", project.UserId,
                "@iProjectId",project.pkProjectId,
                "@iProjectRoleId", project.ProjectRoleId,
                "@iUserActiveStatus", project.IsActive
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_ProjectUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertProject")]
        public JsonResult InsertProject(Project project)
        {
            Object[] parameterArray =
            {
                "@vcProjectName", project.ProjectName,
                "@iWorkspaceId", project.fkProjectWorkspaceId,
                "@dTotalTime", project.ProjectTotalTime,
                "@iActiveStatus", project.IsActive,
                "@iProjectBillableType", project.IsProjectBillable,
                "@iProjectTimeEntryLockTime", project.ProjectTimeEntryUpdateLockTime,
                "@iUserId", project.UserId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_Project", parameterArray, log));
            
            return new JsonResult(result);
        }
        [Route("GetAllProjectMembers")]
        public JsonResult GetAllProjectMembers(Project project)
        {
            Object[] parameterArary =
            {
                "@ifkProjectId"
            };
            return new JsonResult(Ok());
        }

        [Route("UpdateProjectUserRelationship")]
        public JsonResult UpdateProjectUserRelationship(Project project)
        {
            Object[] parameterArray =
            {
                "@iRelationshipId", project.ProjectUserRelationshipId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_ProjectUserRelationship", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateProject")]
        public JsonResult UpdateProject(Project project)
        {
            Object[] parameterArray =
            {
                "@vcProjectName", project.ProjectName,
                "@iWorkspaceId", project.fkProjectWorkspaceId,
                "@dTotalTime", project.ProjectTotalTime,
                "@iProjectId", project.pkProjectId,
                "@iActiveStatus", project.IsActive,
                "@iProjectBillableType", project.IsProjectBillable,
                "@iProjectTimeEntryUpdateLockTime", project.ProjectTimeEntryUpdateLockTime
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_Project", parameterArray, log));

            return new JsonResult(result);
        }


        [Route("UserTotalTimeWRTProjects")]
        public JsonResult GetUserTotalTimeWRTProjects(Project project)
        {
            Object[] parameterArray =
             {
                "@iUserId", project.UserId,
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserTotalTimeWRTProjects_Modified", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UserTotalTimeWRTBillability")]
        public JsonResult GetUserTotalTimeWRTBillability(Project project)
        {
            Object[] parameterArray =
             {
                "@iUserId", project.UserId,
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserTotalTimeWRTBillability", parameterArray, log));

            return new JsonResult(result);
        }


        [Route("UserTopProject")]
        public JsonResult GetUserTopProject(Project project)
        {
            Object[] parameterArray =
             {
                "@iUserId", project.UserId,
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserTopProject", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("TopProject")]
        public JsonResult TopProject(Project project)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TopProject", parameterArray, log));

            return new JsonResult(result);
        }


        //usp_Get_TotalTimeofProjectsWRTWorkspace
        [Route("TotalTimeofProjectsWRTWorkspace")]
        public JsonResult GetTotalTimeofProjectsWRTWorkspace(Project project)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TotalTimeofProjectsWRTWorkspace", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("TotalTimeofProjectsWRTBillability")]
        public JsonResult GetTotalTimeofProjectsWRTBillability(Project project)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TotalTimeWRTBillability", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("LegendWRTBillability")]
        public JsonResult LegendWRTBillability(Project project)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_LegendDataWRTBillability", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("LegendWRTProjects")]
        public JsonResult LegendWRTProjects(Project project)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_LegendDataWRTProjects", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UserLegendWRTBillability")]
        public JsonResult UserLegendWRTBillability(Project project)
        {
            Object[] parameterArray =
             {
                "@iUserId", project.UserId,
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserLegendDataWRTBillability", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UserLegendWRTProjects")]
        public JsonResult UserLegendWRTProjects(Project project)
        {
            Object[] parameterArray =
             {
                "@iUserId", project.UserId,
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserLegendDataWRTProjects", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetProjects")]
        public JsonResult GetProjects(Project project)
        {
            Object[] parameterArray =
             {
                
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Projects", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetUserProjects")]
        public JsonResult GetUserProjects(Project project)
        {
            Object[] parameterArray =
             {
                "@iUserId", project.UserId,
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserProjects", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("TotalTimeWRTReportFilter")]
        public JsonResult TotalTimeWRTReportFilter(Project project)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@iTeamIds", project.teamIds,
                "@iProjectIds", project.projectIds,
                "@iTaskIds", project.taskIds,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate,
            };


            DataTable result = (_executor.ExecuteQuery("usp_Get_TotalTimeofProjectsWRTReportTeamFilter", parameterArray, log));
            //JsonResult jsonObj = new JsonResult(result);
            
            //String obj = jsonObj.Value.ToString();
            //jsonObj.Value("");
            return new JsonResult(result);
            //String j = JsonConvert.SerializeObject(result);
            //return j;
        }

        [Route("ReportListWRTReportFilter")]
        public JsonResult ReportListWRTReportFilter(Project project)
        {
            Object[] parameterArray =
                {

                    "@iWorkSpaceId", project.fkProjectWorkspaceId,
                    "@iTeamIds", project.teamIds,
                    "@iProjectIds", project.projectIds,
                    "@iTaskIds", project.taskIds,
                    "@vcColumns", String.Join(",",project.order),
                    "@dStartdate", project.Startdate,
                    "@dEnddate", project.Enddate,
                };


            DataTable result = (_executor.ExecuteQuery("usp_Get_ReportListWRTReportFilter", parameterArray, log));

            List<Parent> list = Report.generateSummaryList(result, project.order);

            return new JsonResult(list);
            
        }

        [Route("PieWRTReportFilter")]
        public JsonResult PieWRTReportFilter(Project project)
        {
            Object[] parameterArray =
            {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@iTeamIds", project.teamIds,
                "@iProjectIds", project.projectIds,
                "@iTaskIds", project.taskIds,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate,
            };


            DataTable result = (_executor.ExecuteQuery("usp_Get_PieDataWRTReportFilters", parameterArray, log));

            return new JsonResult(result);

        }

        [Route("DetailedWRTReportFilter")]
        public JsonResult DetailedWRTReportFilter(Project project)
        {
            Object[] parameterArray =
            {
                "@iWorkSpaceId", project.fkProjectWorkspaceId,
                "@iTeamIds", project.teamIds,
                "@iProjectIds", project.projectIds,
                "@iTaskIds", project.taskIds,
                "@dStartdate", project.Startdate,
                "@dEnddate", project.Enddate,
            };

            DataTable result = (_executor.ExecuteQuery("usp_Get_ReportDetailedListWRTReportFilter", parameterArray, log));

            return new JsonResult(result);

        }



    }


}
