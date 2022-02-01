using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Services
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}
