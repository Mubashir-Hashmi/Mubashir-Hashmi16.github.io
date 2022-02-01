using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Errors
{
    public class Error
    {
        public Error(int ErrorCode, string ErrorMessage, string result, string ErrorDetails = null)
        {
            errorCode = ErrorCode;
            errorMessage = ErrorMessage;
            Result = result;
            errorDetails = ErrorDetails;
        }

        public int errorCode
        {
            get;
            set;
        }
        public string errorMessage
        {
            get;
            set;
        }
        public string Result
        {
            get;
            set;
        }
        public string errorDetails
        {
            get;
            set;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }

    

    
}
