using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class TimeEntry
    {
        public Int64 pkTimeEntryId
        {
            get;
            set;
        }
        public int fkTimeEntryUserId
        {
            get;
            set;
        }
        public int fkTimeEntryTaskId
        {
            get;
            set;
        }
        public int fkTimeEntryProjectId
        {
            get;
            set;
        }
        public DateTime TimeEntryStartTime
        {
            get;
            set;
        }
        public DateTime TimeEntryEndTime
        {
            get;
            set;
        }
        public decimal TimeEntryTotalTime
        {
            get;
            set;
        }
        public DateTime TimeEntryPostTime
        {
            get;
            set;
        }
        public DateTime TimeEntryUpdateTime
        {
            get;
            set;
        }
        public string TimeEntryDetail
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
        public int TimeEntryMonth
        {
            get;
            set;
        }
        public int BillableType
        {
            get;
            set;
        }
        public string PreviousTimeEntryValue
        {
            get;
            set;
        }
        public string UpdatedTimeEntryValue
        {
            get;
            set;
        }
        public int UpdateField
        {
            get;
            set;
        }
        public int TimeEntryLockTime
        {
            get;
            set;
        }
        public int RecordNumber
        {
            get;
            set;
        }
        public int FirstRecordNumber
        {
            get;
            set;
        }
        public int LastRecordNumber
        {
            get;
            set;
        }
        public int OrderBy
        {
            get;
            set;
        }
        public int ActiveStatus
        {
            get;
            set;
        }
        public decimal PreviousTotalTime
        {
            get;
            set;
        }
        public string PreviousTimeEntryValue2
        {
            get;
            set;
        }
        public string UpdatedTimeEntryValue2
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime EndDate
        {
            get;
            set;
        }
        public int FilterApplied 
        {
            get;
            set;
        }
        public int DetailFilterApplied
        {
            get;
            set;
        }

        public string Startdate
        {
            get;
            set;
        }

        public string Enddate
        {
            get;
            set;
        }
        public string teamIds
        {
            get;
            set;
        }

        public string projectIds
        {
            get;
            set;
        }

        public string taskIds
        {
            get;
            set;
        }

        public int pFlag
        {
            get;
            set;
        } = 0;

        public int uFlag
        {
            get;
            set;
        } = 0;

        public int tFlag
        {
            get;
            set;
        } = 0;

        public int dFlag
        {
            get;
            set;
        } = 0;

        public int dateFlag
        {
            get;
            set;
        } = 0;

        public int monthFlag
        {
            get;
            set;
        } = 0;

        public String[] order
        {
            get;
            set;
        }
    }
}
