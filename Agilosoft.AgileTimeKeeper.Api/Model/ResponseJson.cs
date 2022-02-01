using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class ResponseJson
    {
        public int StatusCode
        {
            get; 
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public DataTable Data
        {
            get;
            set;
        }
    }
}
