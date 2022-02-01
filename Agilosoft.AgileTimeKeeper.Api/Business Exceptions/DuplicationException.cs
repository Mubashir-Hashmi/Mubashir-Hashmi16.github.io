using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Business_Exceptions
{
    [Serializable]
    public class DuplicationException : Exception
    {
        public DuplicationException() { }
        public DuplicationException(string message) :
            base(message)
        { }
    }
}
