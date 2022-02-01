using Agilosoft.AgileTimeKeeper.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Agilosoft.AgileTimeKeeper.Api.Controllers;
using log4net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Agilosoft.AgileTimeKeeper.Api.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(AuthenticateService));
        private readonly AppSettings _appSettings;
        private byte[] _hashPassword;
        private ExecutorController _executor;
        private IRefreshTokenGenerator _refreshTokenGenerator;
        public AuthenticateService(IOptions<AppSettings> appsettings, ExecutorController executor,IRefreshTokenGenerator refreshTokenGenerator)
        {
            _executor = executor;
            _appSettings = appsettings.Value;
            _refreshTokenGenerator = refreshTokenGenerator;
        }
        public Dictionary<string,string> Authenticate(User user)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (user.LoginType == 1) 
            { 
                DataTable result = VerifiyUser(user);
                if (result.Rows.Count<=0)
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_appSettings.key);
                string role = _executor.GetRole(result.Rows[0].ItemArray[3].ToString());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, result.Rows[0].ItemArray[3].ToString()),
                        new Claim(ClaimTypes.Name, result.Rows[0].ItemArray[4].ToString()),
                        new Claim(ClaimTypes.Surname, result.Rows[0].ItemArray[5].ToString()),
                        new Claim(ClaimTypes.Role, role),
                        new Claim(ClaimTypes.NameIdentifier, result.Rows[0].ItemArray[2].ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.RefreshToken = _refreshTokenGenerator.GenerateToken();
                user.Token = tokenHandler.WriteToken(token);

                user.UserPassword = null;
                dict.Add("UserId", result.Rows[0].ItemArray[2].ToString());
                dict.Add("UserEmail", result.Rows[0].ItemArray[3].ToString());
                dict.Add("FirstName", result.Rows[0].ItemArray[4].ToString());
                dict.Add("LastName", result.Rows[0].ItemArray[5].ToString());
                dict.Add("Role", role);
                dict.Add("LoginType", user.LoginType.ToString());
                dict.Add("Token", user.Token);
                dict.Add("RefreshToken", user.RefreshToken);
                return dict;
            }
            else
            {
                return dict;
            }
        }
        public Dictionary<string, string> RefreshToken(User User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.key);
            SecurityToken validatedToken;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var principal = tokenHandler.ValidateToken(User.Token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false 
                },out validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;
            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new Exception("Invalid Token passed!");
            }
            var username = principal.Identity.Name;
            return dict;
        }
        public DataTable VerifiyUser(User user)
        {
            if (user.LoginType == 1)
            {
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    UTF8Encoding utf = new UTF8Encoding();
                    _hashPassword = md5.ComputeHash(utf.GetBytes(user.UserPassword));
                    user.UserPassword = Convert.ToBase64String(_hashPassword);
                }
            }
            Object[] parameterArray =
            {
                "@vcUserEmail", user.UserEmail,
                "@vcUserPassword", user.UserPassword,
                "@iLoginType", user.LoginType
            };
            DataTable result=(_executor.ExecuteQuery("usp_Validate_User", parameterArray, log));
            return result;
        }
    }
}
