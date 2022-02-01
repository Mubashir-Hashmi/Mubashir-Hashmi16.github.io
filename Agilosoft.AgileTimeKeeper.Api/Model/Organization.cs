using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Organization
    {
        public string OrganizationName
        {
            get;
            set;
        }
        public int OrganizationId
        {
            get;
            set;
        }
        public string LoginType
        {
            get;
            set;
        }
        public int LoginTypeId
        {
            get;
            set;
        }
    }
}
