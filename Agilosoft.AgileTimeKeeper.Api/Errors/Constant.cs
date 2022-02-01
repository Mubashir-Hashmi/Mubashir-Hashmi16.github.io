using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Errors
{
    public class Constant
    {
        public enum StatusCode
        {
            OK=200,
            BadRequest=400,
            NotFound=404,
            NoContent=204,
            Created=201,
            Unauhtorized=401,
            Forbidden=403
        }

    }
}
