using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class User
    {
        public string UserSalutation
        {
            get;
            set;
        }
        public string UserFirstName
        {
            get;
            set;
        }
        public string UserLastName
        {
            get;
            set;
        }
        public string UserEmail
        {
            get;
            set;
        }
        public string UserPassword
        {
            get;
            set;
        }
        public int fkUserOrganizationId
        {
            get;
            set;
        }
        public int UserRoleId
        {
            get;
            set;
        }
        public int fkDesignationId
        {
            get;
            set;
        }
        public string UserAddress
        {
            get;
            set;
        }
        public string UserCity
        {
            get;
            set;
        }
        public string UserState
        {
            get;
            set;
        }
        public string UserCountry
        {
            get;
            set;
        }
        public string UserPostalCode
        {
            get;
            set;
        }
        public string UserMobileNo
        {
            get;
            set;
        }
        public double UserTotalTime
        {
            get;
            set;
        }
        public int WorkspaceId
        {
            get;
            set;
        }
        public int UserTimeEntryLockTime
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
        public int RecordType
        {
            get;
            set;
        }
        public int LoginType
        {
            get;
            set;
        }
        public string Token
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get; 
            set;
        }
    }
}
