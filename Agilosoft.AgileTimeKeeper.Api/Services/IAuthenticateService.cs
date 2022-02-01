using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agilosoft.AgileTimeKeeper.Api.Model;

namespace Agilosoft.AgileTimeKeeper.Api.Services
{
    public interface IAuthenticateService
    {
        Dictionary<string,string> Authenticate(User user);
        Dictionary<string, string> RefreshToken(User user);
    }
}
