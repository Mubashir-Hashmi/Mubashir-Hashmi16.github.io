using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private ExecutorController _executor;
        private IHubContext<NotificationHub> _hub;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ReportController));

        public ReportController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }

        [Route("GetAllRecordOfTimeEntry")]
        public JsonResult GetAllRecordOfTimeEntry(TimeEntry TimeEntry)
        {
            if(TimeEntry.FilterApplied == 0)
            {
                TimeEntry.UserFilter = null;
                TimeEntry.TaskFilter = null;
                TimeEntry.DetailFilter = null;
                TimeEntry.ProjectFilter = null;
            }
            else
            {
                TimeEntry.UserFilter = TimeEntry.UserFilter == "" ? null : TimeEntry.UserFilter;
                TimeEntry.TaskFilter = TimeEntry.TaskFilter == "" ? null : TimeEntry.TaskFilter;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.DetailFilter;
                TimeEntry.ProjectFilter = TimeEntry.ProjectFilter == "" ? null : TimeEntry.ProjectFilter;
            }
            Object[] parameterArray =
            {
                "@iPageNumber", TimeEntry.PageNumber,
                "@iPageSize",TimeEntry.PageSize,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@vcUserFilter",TimeEntry.UserFilter,
                "@vcProjectFilter",TimeEntry.ProjectFilter,
                "@vcTaskFilter",TimeEntry.TaskFilter,
                "@vcDetailFilter",TimeEntry.DetailFilter,
                "@dtStartDate", TimeEntry.StartDate,
                "@dtEndDate", TimeEntry.EndDate,
                "@iOrderBy", TimeEntry.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllTimeEntry", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllTimeEntry")]
        public JsonResult GetCountOfAllTimeEntry(TimeEntry TimeEntry)
        {
            if (TimeEntry.FilterApplied == 0)
            {
                TimeEntry.UserFilter = null;
                TimeEntry.TaskFilter = null;
                TimeEntry.DetailFilter = null;
                TimeEntry.ProjectFilter = null;
            }
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@vcUserFilter",TimeEntry.UserFilter,
                "@vcProjectFilter",TimeEntry.ProjectFilter,
                "@vcTaskFilter",TimeEntry.TaskFilter,
                "@dtStartDate", TimeEntry.StartDate,
                "@dtEndDate", TimeEntry.EndDate,
                "@vcDetailFilter",TimeEntry.DetailFilter,
                "@bDetailFilter", TimeEntry.FilterApplied
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllTimeEntryRecord", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetAllRecordOfTimeEntryUpdateLog")]
        public JsonResult GetAllRecordOfTimeEntryUpdateLog(TimeEntry TimeEntry)
        {
            if (TimeEntry.FilterApplied == 0)
            {
                TimeEntry.UserFilter = null;
                TimeEntry.TaskFilter = null;
                TimeEntry.DetailFilter = null;
                TimeEntry.ProjectFilter = null;
            }
            Object[] parameterArray =
            {
                "@iPageNumber", TimeEntry.PageNumber,
                "@iPageSize",TimeEntry.PageSize,
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iUpdateFieldFilter",TimeEntry.UpdateField,
                "@vcUserFilter",TimeEntry.UserFilter,
                "@vcProjectFilter",TimeEntry.ProjectFilter,
                "@vcTaskFilter",TimeEntry.TaskFilter,
                "@vcDetailFilter",TimeEntry.DetailFilter,
                "@dtStartDate", TimeEntry.StartDate,
                "@dtEndDate", TimeEntry.EndDate,
                "@iOrderBy", TimeEntry.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_PaginationRecord_AllTimeEntryUpdateLog", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetCountOfAllTimeEntryUpdateLog")]
        public JsonResult GetCountOfAllTimeEntryUpdateLog(TimeEntry TimeEntry)
        {
            if (TimeEntry.FilterApplied == 0)
            {
                TimeEntry.UserFilter = null;
                TimeEntry.TaskFilter = null;
                TimeEntry.DetailFilter = null;
                TimeEntry.ProjectFilter = null;
            }
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iUpdateFieldFilter",TimeEntry.UpdateField,
                "@vcUserFilter",TimeEntry.UserFilter,
                "@vcProjectFilter",TimeEntry.ProjectFilter,
                "@vcTaskFilter",TimeEntry.TaskFilter,
                "@dtStartDate", TimeEntry.StartDate,
                "@dtEndDate", TimeEntry.EndDate,
                "@vcDetailFilter",TimeEntry.DetailFilter,
                "@bDetailFilter", TimeEntry.FilterApplied
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllTimeEntryUpdateLog", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetFilterList")]
        public JsonResult GetFilterList(TimeEntry TimeEntry)
        {
            if (TimeEntry.FilterApplied == 0)
            {
                TimeEntry.UserFilter = null;
                TimeEntry.TaskFilter = null;
                TimeEntry.DetailFilter = null;
                TimeEntry.ProjectFilter = null;
            }
            else
            {
                TimeEntry.UserFilter = TimeEntry.UserFilter == "" ? null : TimeEntry.UserFilter;
                TimeEntry.TaskFilter = TimeEntry.TaskFilter == "" ? null : TimeEntry.UserFilter;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.UserFilter;
                TimeEntry.ProjectFilter = TimeEntry.ProjectFilter == "" ? null : TimeEntry.UserFilter;
            }
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iFlag",TimeEntry.Flag,
                "@iUserId", TimeEntry.fkTimeEntryUserId,
                "@vcUserFilter",TimeEntry.UserFilter,
                "@vcProjectFilter",TimeEntry.ProjectFilter,
                "@vcTaskFilter",TimeEntry.TaskFilter
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_ReportFilterValues", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetSummaryList")]
        public JsonResult GetSummaryList(TimeEntry TimeEntry)
        {
            if(TimeEntry.Flag == 1)
            {
                TimeEntry.UserFilter = TimeEntry.UserFilter == "" ? null : TimeEntry.UserFilter;
                TimeEntry.TaskFilter = TimeEntry.TaskFilter == "" ? null : TimeEntry.TaskFilter;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.DetailFilter;
                TimeEntry.ProjectFilter = TimeEntry.ProjectFilter == "" ? null : TimeEntry.ProjectFilter;
            }
            else if (TimeEntry.Flag == 2)
            {
                TimeEntry.UserFilter = TimeEntry.UserEmail;
                TimeEntry.TaskFilter = TimeEntry.TaskFilter == "" ? null : TimeEntry.TaskFilter;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.DetailFilter;
                TimeEntry.ProjectFilter = TimeEntry.ProjectFilter == "" ? null : TimeEntry.ProjectFilter;
            }
            else if (TimeEntry.Flag == 3)
            {
                TimeEntry.UserFilter = TimeEntry.UserEmail;
                TimeEntry.ProjectFilter = TimeEntry.ProjectName;
                TimeEntry.TaskFilter = TimeEntry.TaskFilter == "" ? null : TimeEntry.TaskFilter;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.DetailFilter;
            }
            else if (TimeEntry.Flag == 4)
            {
                TimeEntry.UserFilter = TimeEntry.UserEmail;
                TimeEntry.ProjectFilter = TimeEntry.ProjectName;
                TimeEntry.TaskFilter = TimeEntry.TaskName;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.DetailFilter;
            }

            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@iFlag",TimeEntry.Flag,
                "@dtStartDate", TimeEntry.StartDate.ToString("MM/dd/yyyy"),
                "@dtEndDate", TimeEntry.EndDate.ToString("MM/dd/yyyy"),
                "@vcUserFilter",TimeEntry.UserFilter,
                "@vcProjectFilter",TimeEntry.ProjectFilter,
                "@vcTaskFilter",TimeEntry.TaskFilter,
                "@vcDetailFilter",TimeEntry.DetailFilter,
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_SummaryList", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("GetTotalTimeWRTDateWRTFilter")]
        public JsonResult GetTotalTimeWRTDateWRTFilter(TimeEntry TimeEntry)
        {
            if (TimeEntry.FilterApplied == 0)
            {
                TimeEntry.UserFilter = null;
                TimeEntry.TaskFilter = null;
                TimeEntry.DetailFilter = null;
                TimeEntry.ProjectFilter = null;
            }
            else
            {
                TimeEntry.UserFilter = TimeEntry.UserFilter == "" ? null : TimeEntry.UserFilter;
                TimeEntry.TaskFilter = TimeEntry.TaskFilter == "" ? null : TimeEntry.UserFilter;
                TimeEntry.DetailFilter = TimeEntry.DetailFilterApplied == 0 ? null : TimeEntry.UserFilter;
                TimeEntry.ProjectFilter = TimeEntry.ProjectFilter == "" ? null : TimeEntry.UserFilter;
            }
            Object[] parameterArray =
            {
                "@iWorkspaceId", TimeEntry.WorkspaceId,
                "@vcUserEmail",TimeEntry.UserEmail,
                "@vcTimeEntryDetail",TimeEntry.TimeEntryDetail,
                "@vcTaskName",TimeEntry.TaskName,
                "@dtStartDate", TimeEntry.StartDate,
                "@dtEndDate", TimeEntry.EndDate,
                "@vcProjectName",TimeEntry.ProjectName,
                "@bDetailFilter", TimeEntry.FilterApplied
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_TotalTimeAccordingToReportFilterValues", parameterArray, log));

            return new JsonResult(result);
        }

    }
}
