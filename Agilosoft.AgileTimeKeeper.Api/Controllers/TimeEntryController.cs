using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Agilosoft.AgileTimeKeeper.Api.DataProcessing;

using System.Data;
using System.Collections.Generic;
using Agilosoft.AgileTimeKeeper.Api.Controllers;
using Agilosoft.AgileTimeKeeper.Api;

namespace Agilosoft.AgileTimeK
{ 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private ExecutorController _executor;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(TimeEntryController));
        private IHubContext<NotificationHub> _hub;
        public TimeEntryController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }

        [Route("DeleteTimeEntry")]
        public JsonResult DeleteTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@biTimeEntryId", TimeEntry.pkTimeEntryId,
                "@iUserId",TimeEntry.fkTimeEntryUserId,
                "@iTaskId",TimeEntry.fkTimeEntryTaskId,
                "@iProjectId",TimeEntry.fkTimeEntryProjectId,
                "@iWorkspaceId",TimeEntry.WorkspaceId,
                "@dTotalTime", TimeEntry.TimeEntryTotalTime
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_TimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("DeleteTimeEntryGroup")]
        public JsonResult DeleteTimeEntryGroup(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iUserId",TimeEntry.fkTimeEntryUserId,
                "@iTaskId",TimeEntry.fkTimeEntryTaskId,
                "@vcTimeEntryDetail", TimeEntry.TimeEntryDetail,
                "@iProjectId",TimeEntry.fkTimeEntryProjectId,
                "@dtStartTime", TimeEntry.TimeEntryStartTime,
                "@iWorkspaceId",TimeEntry.WorkspaceId,
                "@dTotalTime", TimeEntry.TimeEntryTotalTime
            };
            DataTable result = (_executor.ExecuteQuery("usp_Del_TimeEntryGroup", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetActiveTimeEntry")]
        public JsonResult GetActiveTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iUserId", TimeEntry.fkTimeEntryUserId,
                "@iWorkspaceId", TimeEntry.WorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_ActiveTimeEntryWRTUserWRTWorkpace", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetDateWRTUser")]
        public JsonResult GetDateWRTUser(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iUserId", TimeEntry.fkTimeEntryUserId,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@dtTimeEntryStartDate", TimeEntry.TimeEntryStartTime,
                "@dtTimeEntryEndDate", TimeEntry.TimeEntryEndTime
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_DateWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfTimeEntryWRTDateWRTUser")]
        public JsonResult GetAllRecordOfTimeEntryWRTDateWRTUser(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iUserId", TimeEntry.fkTimeEntryUserId,
                "@dtTimeEntryDate", TimeEntry.TimeEntryStartTime,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@vcTimeEntryDetail", TimeEntry.TimeEntryDetail,
                "@iTaskId", TimeEntry.fkTimeEntryTaskId,
                "@iPageNumber", TimeEntry.PageNumber,
                "@iPageSize", TimeEntry.PageSize
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllTimeEntryWRTDateWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfTimeEntryWRTUser")]
        public JsonResult GetAllRecordOfTimeEntryWRTUser(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iPageNumber", TimeEntry.PageNumber,
                "@iPageSize", TimeEntry.PageSize,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iUserId", TimeEntry.fkTimeEntryUserId

            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllUserTimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllGroupedTimeEntry")]
        public JsonResult GetCountOfAllGroupedTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iUserId", TimeEntry.fkTimeEntryUserId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllTimeEntryGroupedRecord", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllTimeEntryWRTUser")]
        public JsonResult GetCountOfAllTimeEntryWRTUser(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iUserId", TimeEntry.fkTimeEntryUserId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllTimeEntryWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetProjectAndTaskList")]
        public JsonResult GetProjectAndTaskList(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iUserId", TimeEntry.fkTimeEntryUserId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_ProjectAndTaskListWRTUser", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertTimeEntry")]
        public JsonResult InsertTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@dtTimeEntryStartTime",TimeEntry.TimeEntryStartTime,
                "@dtTimeEntryEndTime",TimeEntry.TimeEntryEndTime,
                "@vcTimeEntryDetail",TimeEntry.TimeEntryDetail,
                "@iUserId",TimeEntry.fkTimeEntryUserId,
                "@iTaskId",TimeEntry.fkTimeEntryTaskId,
                "@iProjectId",TimeEntry.fkTimeEntryProjectId,
                "@iTimeEntryBillableType", TimeEntry.BillableType,
                "@iTimeEntryActiveStatus", TimeEntry.ActiveStatus
            };
            DataTable result = (_executor.ExecuteQuery("usp_Ins_TimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateTimeEntry")]
        public JsonResult UpdateTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@biTimeEntryId", TimeEntry.pkTimeEntryId,
                "@dtTimeEntryStartTime",TimeEntry.TimeEntryStartTime,
                "@dtTimeEntryEndTime",TimeEntry.TimeEntryEndTime,
                "@vcTimeEntryDetail",TimeEntry.TimeEntryDetail,
                "@iUserId",TimeEntry.fkTimeEntryUserId,
                "@iTaskId",TimeEntry.fkTimeEntryTaskId,
                "@iProjectId",TimeEntry.fkTimeEntryProjectId,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@dPreviousTotalTime", TimeEntry.PreviousTotalTime,
                "@dTotalTime", TimeEntry.TimeEntryTotalTime,
                "@vcPreviousValue2", TimeEntry.PreviousTimeEntryValue2,
                "@vcUpdatedValue2", TimeEntry.UpdatedTimeEntryValue2,
                "@iTimeEntryBillableType", TimeEntry.BillableType,
                "@iUpdateField", TimeEntry.UpdateField,
                "@vcPreviousValue", TimeEntry.PreviousTimeEntryValue,
                "@vcUpdatedValue", TimeEntry.UpdatedTimeEntryValue,
                "@iTimeEntryActiveStatus", TimeEntry.ActiveStatus
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_TimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateTimeEntryInsertion")]
        public JsonResult UpdateTimeEntryInsertion(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@biTimeEntryId", TimeEntry.pkTimeEntryId,
                "@dtTimeEntryStartTime",TimeEntry.TimeEntryStartTime,
                "@dtTimeEntryEndTime",TimeEntry.TimeEntryEndTime,
                "@vcTimeEntryDetail",TimeEntry.TimeEntryDetail,
                "@iUserId",TimeEntry.fkTimeEntryUserId,
                "@iTaskId",TimeEntry.fkTimeEntryTaskId,
                "@iProjectId",TimeEntry.fkTimeEntryProjectId,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@dTotalTime", TimeEntry.TimeEntryTotalTime,
                "@iTimeEntryBillableType", TimeEntry.BillableType,
                "@iTimeEntryActiveStatus", TimeEntry.ActiveStatus
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_TimeEntryInsertion", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("UpdateTimeEntryLockTime")]
        public JsonResult UpdateTimeEntryLockTime(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@iUpdateTime", TimeEntry.TimeEntryLockTime,
                "@iUpdateId", TimeEntry.UpdateField,
                "@iFlag", TimeEntry.Flag,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_TimeEntryLockTime", parameterArray, log));

            return new JsonResult(result);
        }
		
		[Route("UpdateRunningTimeEntry")]
        public JsonResult UpdateRunningTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                "@biTimeEntryId", TimeEntry.pkTimeEntryId,
                "@iTaskId", TimeEntry.fkTimeEntryTaskId,
                "@iProjectId", TimeEntry.fkTimeEntryProjectId,
                "@vcTimeEntryDetail", TimeEntry.TimeEntryDetail,
                "@iBillableType", TimeEntry.BillableType,
                "@dtTimeEntryStartTime", TimeEntry.TimeEntryStartTime,
                "@iFlag", TimeEntry.Flag,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Upd_RunningTimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetBillableType")]
        public JsonResult GetBillableType()
        {
            DataTable result = (_executor.ExecuteQuery("usp_GetBillableType", log));

            return new JsonResult(result);
        }

        [Route("GetActiveStatus")]
        public JsonResult GetActiveStatus()
        {
            DataTable result = (_executor.ExecuteQuery("usp_GetActiveStatus", log));

            return new JsonResult(result);
        }

        [Route("TotalTimeWRTReportFilter")]
        public JsonResult TotalTimeWRTReportFilter(TimeEntry timeEntry)
        {
            Object[] parameterArray =
             {
                "@iWorkSpaceId", timeEntry.WorkspaceId,
                "@iTeamIds", timeEntry.teamIds,
                "@iProjectIds", timeEntry.projectIds,
                "@iTaskIds", timeEntry.taskIds,
                "@dStartdate", timeEntry.Startdate,
                "@dEnddate", timeEntry.Enddate,
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
        public JsonResult ReportListWRTReportFilter(TimeEntry timeEntry)
        {
            Object[] parameterArray =
                {

                    "@iWorkSpaceId", timeEntry.WorkspaceId,
                    "@iTeamIds", timeEntry.teamIds,
                    "@iProjectIds", timeEntry.projectIds,
                    "@iTaskIds", timeEntry.taskIds,
                    "@vcColumns", String.Join(",",timeEntry.order),
                    "@dStartdate", timeEntry.Startdate,
                    "@dEnddate", timeEntry.Enddate,
                };


            DataTable result = (_executor.ExecuteQuery("usp_Get_ReportListWRTReportFilter", parameterArray, log));

            List<Parent> list = Report.generateSummaryList(result, timeEntry.order);

            return new JsonResult(list);

        }

        [Route("PieWRTReportFilter")]
        public JsonResult PieWRTReportFilter(TimeEntry timeEntry)
        {
            Object[] parameterArray =
            {
                "@iWorkSpaceId", timeEntry.WorkspaceId,
                "@iTeamIds", timeEntry.teamIds,
                "@iProjectIds", timeEntry.projectIds,
                "@iTaskIds", timeEntry.taskIds,
                "@dStartdate", timeEntry.Startdate,
                "@dEnddate", timeEntry.Enddate,
            };


            DataTable result = (_executor.ExecuteQuery("usp_Get_PieDataWRTReportFilters", parameterArray, log));

            return new JsonResult(result);

        }

        [Route("DetailedWRTReportFilter")]
        public JsonResult DetailedWRTReportFilter(TimeEntry timeEntry)
        {
            Object[] parameterArray =
            {
                "@iWorkSpaceId", timeEntry.WorkspaceId,
                "@iTeamIds", timeEntry.teamIds,
                "@iProjectIds", timeEntry.projectIds,
                "@iTaskIds", timeEntry.taskIds,
                "@dStartdate", timeEntry.Startdate,
                "@dEnddate", timeEntry.Enddate,
            };

            DataTable result = (_executor.ExecuteQuery("usp_Get_ReportDetailedListWRTReportFilter", parameterArray, log));

            return new JsonResult(result);

        }


        [Route("ReportWeeklyListWRTReportFilter")]
        public JsonResult ReportWeeklyListWRTReportFilter(TimeEntry timeEntry)
        {
            Object[] parameterArray =
                {
                    "@iWorkSpaceId", timeEntry.WorkspaceId,
                    "@iTeamIds", timeEntry.teamIds,
                    "@iProjectIds", timeEntry.projectIds,
                    "@iTaskIds", timeEntry.taskIds,
                    "@dStartdate", timeEntry.Startdate,
                    "@dEnddate", timeEntry.Enddate,
                };

            DataTable result = (_executor.ExecuteQuery("usp_Get_ReportWeeklyListWRTReportFilter", parameterArray, log));

            List<Parent> list = Report.generateWeeklyList(result, timeEntry.order, timeEntry.Startdate, timeEntry.Enddate);

            return new JsonResult(list);

        }
    }
}
