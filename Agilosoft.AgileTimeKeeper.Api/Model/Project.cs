using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Project
    {
        public int pkProjectId 
        { 
            get; 
            set; 
        }
        public string ProjectName
        {
            get;
            set;
        }
        public int ProjectRoleId 
        {
            get;
            set;
        }

        public int fkProjectWorkspaceId
        {
            get;
            set;
        }
        public double ProjectTotalTime
        {
            get;
            set;
        }
        public int IsActive
        {
            get;
            set;
        }
        public int IsProjectBillable
        {
            get;
            set;
        }
        public int ProjectUserRelationshipId
        {
            get;
            set;
        }
        public int UserId
        {
            get;
            set;
        }
        public int ProjectTimeEntryUpdateLockTime
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
