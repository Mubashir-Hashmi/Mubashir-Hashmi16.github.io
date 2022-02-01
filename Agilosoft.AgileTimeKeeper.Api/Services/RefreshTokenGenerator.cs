using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Services
{
    public class RefreshTokenGenerator: IRefreshTokenGenerator
    {
        public string GenerateToken()
        {
            var randomnumber = new byte[32];
            using(var randomNumberGenerator= RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomnumber);
                return Convert.ToBase64String(randomnumber);
            }
        }
    }
}
