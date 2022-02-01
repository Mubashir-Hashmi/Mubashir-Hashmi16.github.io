using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Tracker
    {
        public Int64 pkTrackerId
        {
            get;
            set;
        }
        public int fkTrackerUserId
        {
            get;
            set;
        }
        public int fkTrackerTaskId
        {
            get;
            set;
        }
        public int fkTrackerProjectId
        {
            get;
            set;
        }
        public DateTime TrackerStartTime
        {
            get;
            set;
        }
        public DateTime TrackerEndTime
        {
            get;
            set;
        }
        public double TrackerTotalTime
        {
            get;
            set;
        }
        public DateTime TrackerPostTime
        {
            get;
            set;
        }
        public DateTime TrackerUpdateTime
        {
            get;
            set;
        }
        public string TrackerDetail
        {
            get;
            set;
        }
        public int WorkspaceId
        {
            get;
            set;
        }
        public string UserEmail
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string TaskName
        {
            get;
            set;
        }
        public int Flag
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
        public string UserFilter
        {
            get;
            set;
        }
        public string ProjectFilter
        {
            get;
            set;
        }
        public string TaskFilter
        {
            get;
            set;
        }
        public string DetailFilter
        {
            get;
            set;
        }
        public int TrackerMonth
        {
            get;
            set;
        }
        public int TrackerType
        {
            get;
            set;
        }
        public string PreviousTrackerValue
        {
            get;
            set;
        }
        public string UpdatedTrackerValue
        {
            get;
            set;
        }
        public int UpdateField
        {
            get;
            set;
        }
        public int TrackerLockTime
        {
            get;
            set;
        }
    }
}
