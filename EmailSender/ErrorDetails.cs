using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSender
{
    public class ErrorDetails
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
        public string Result
        {
            get;
            set;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this) ;
        }
    }
}
