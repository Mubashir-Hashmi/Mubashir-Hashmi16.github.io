using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Workspace
    {
        public int pkWorkspaceId 
        {
            get; 
            set; 
        }
        public string WorkspaceName 
        { 
            get; 
            set; 
        }
        public int fkWorkspaceOrganizationId 
        { 
            get; 
            set; 
        }
        public double WorkspaceTotalTime 
        { 
            get; 
            set; 
        }
        public int UserId 
        { 
            get; 
            set; 
        }
        public int WorkspaceUserRelationshipId 
        { 
            get; 
            set; 
        }
        public int WorkspaceTimeEntryLockTime 
        { 
            get; 
            set; 
        }
        public int UserRoleId
        {
            get;
            set;
        }
        public int UserActiveStatus
        {
            get;
            set;
        }
        public int WorkspaceActiveStatus
        {
            get;
            set;
        }
        public int Flag
        {
            get;
            set;
        }
        public int RelationshipId
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
