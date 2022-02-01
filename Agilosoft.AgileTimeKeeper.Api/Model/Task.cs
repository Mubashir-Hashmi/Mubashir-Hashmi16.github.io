using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Task
    {
        public int pkTaskId
        {
            get;
            set;
        }
        public string TaskName
        {
            get;
            set;
        }
        public int fkTaskProjectId
        {
            get;
            set;
        }
        public int fkProjectWorkspaceId
        {
            get;
            set;
        }
        public double TaskTotalTime
        {
            get;
            set;
        }
        public int IsActive
        {
            get;
            set;
        }
        public int TaskUserRelationshipId
        {
            get;
            set;
        }
        public int UserId
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
    }
}
