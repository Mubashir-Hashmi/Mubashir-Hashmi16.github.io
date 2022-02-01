using Agilosoft.AgileTimeKeeper.Api.Constants;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Data;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private ExecutorController _executor;
        private static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(CalendarController));
        private IHubContext<NotificationHub> _hub;

        public CalendarController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }

        
        [Route("GetUserAllTimeEnties/{WorkspaceId:int}/{fkTimeEntryUserId:int}")]
        public IActionResult GetUserAllTimeEnties(int WorkspaceId,int fkTimeEntryUserId)
        {
            Object[] parameterArray =
            {
                TimeEntryParameters.WORKSPACE_ID, WorkspaceId,
                TimeEntryParameters.USER_ID, fkTimeEntryUserId
            };
            DataTable result = (_executor.ExecuteQuery(CalendarStoreProcedures.GET_CALENDAR_USER_ALL_TIME_ENTRIES, parameterArray, log));

            if (result.Rows.Count == 0)
            {
                return NotFound(new { errorMessage = CalendarTimeEntryErrorReponses.NOT_FOUND });
            }

            return Ok(result);
        }

       
        [Route("GetUserTimeEntry/{pkTimeEntryId:int}")]
        public IActionResult GetUserTimeEntry(int pkTimeEntryId)
        {
           
            Object[] parameterArray =
            {
                TimeEntryParameters.TIME_ENTRY_ID, pkTimeEntryId,
            };
            DataTable result = (_executor.ExecuteQuery(CalendarStoreProcedures.GET_CALENDAR_TIME_ENTRY, parameterArray, log));

            
            if (result.Rows.Count == 0)
            {
                return NotFound(new { errorMessage = CalendarTimeEntryErrorReponses.NOT_FOUND });
            }

            return Ok(result);
        }

        [Route("InsertUserTimeEntry")]
        public IActionResult InsertUserTimeEntry(TimeEntry TimeEntry)
        {

         
            Object[] parameterArray =
            {
                TimeEntryParameters.TIME_ENTRY_START_TIME , TimeEntry.TimeEntryStartTime,
                TimeEntryParameters.TIME_ENTRY_END_TIME , TimeEntry.TimeEntryEndTime,
                TimeEntryParameters.TIME_ENTRY_DETAIL , TimeEntry.TimeEntryDetail,
                TimeEntryParameters.USER_ID ,TimeEntry.fkTimeEntryUserId,
                TimeEntryParameters.TASK_ID ,TimeEntry.fkTimeEntryTaskId,
                TimeEntryParameters.PROJECT_ID, TimeEntry.fkTimeEntryProjectId,
                TimeEntryParameters.TIME_ENTRY_BILLABLE_TYPE , TimeEntry.BillableType,
                TimeEntryParameters.TIME_ENTRY_ACTIVE_STATUS , TimeEntry.ActiveStatus,
                TimeEntryParameters.WORKSPACE_ID, TimeEntry.WorkspaceId,
            };
            try
            {
                DataTable result = (_executor.ExecuteQuery(CalendarStoreProcedures.INSERT_CALENDAR_TIME_ENTRY, parameterArray, log));
                return NoContent();
            }
            catch
            {
                return BadRequest(new { errorMessage = CalendarTimeEntryErrorReponses.UNABLE_TO_INSERT });
            }
          
        }

        [Route("UpdateUserTimeEntry")]
        public IActionResult UpdateUserTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                TimeEntryParameters.TIME_ENTRY_ID , TimeEntry.pkTimeEntryId,
                TimeEntryParameters.TIME_ENTRY_START_TIME , TimeEntry.TimeEntryStartTime,
                TimeEntryParameters.TIME_ENTRY_END_TIME , TimeEntry.TimeEntryEndTime,
                TimeEntryParameters.TIME_ENTRY_DETAIL , TimeEntry.TimeEntryDetail,
                TimeEntryParameters.USER_ID , TimeEntry.fkTimeEntryUserId,
                TimeEntryParameters.TASK_ID , TimeEntry.fkTimeEntryTaskId,
                TimeEntryParameters.PROJECT_ID , TimeEntry.fkTimeEntryProjectId,
                TimeEntryParameters.TIME_ENTRY_BILLABLE_TYPE , TimeEntry.BillableType,
                TimeEntryParameters.WORKSPACE_ID , TimeEntry.WorkspaceId,
            };
            try
            {
                DataTable result = (_executor.ExecuteQuery(CalendarStoreProcedures.UPDATE_CALENDAR_TIME_ENTRY, parameterArray, log));
                return NoContent();
            }
            catch
            {
                return BadRequest(new { errorMessage = CalendarTimeEntryErrorReponses.UNABLE_TO_UPDATE });
            }
        
        }

        [Route("DeleteUserTimeEntry")]
        public IActionResult DeleteUserTimeEntry(TimeEntry TimeEntry)
        {
            Object[] parameterArray =
            {
                TimeEntryParameters.TIME_ENTRY_ID , TimeEntry.pkTimeEntryId,
                TimeEntryParameters.WORKSPACE_ID , TimeEntry.WorkspaceId,
                TimeEntryParameters.USER_ID , TimeEntry.fkTimeEntryUserId,
                TimeEntryParameters.PROJECT_ID , TimeEntry.fkTimeEntryProjectId,
                TimeEntryParameters.TASK_ID , TimeEntry.fkTimeEntryTaskId,
                TimeEntryParameters.TOTAL_TIME , TimeEntry.TimeEntryTotalTime
            };
            try
            {
                DataTable result = (_executor.ExecuteQuery(CalendarStoreProcedures.DELETE_CALENDAR_TIME_ENTRY, parameterArray, log));
                return NoContent();
            }
            catch
            {

                return BadRequest(new { errorMessage = CalendarTimeEntryErrorReponses.UNABLE_TO_DELETE });
            }
    
        }
    }
}