using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Agilosoft.AgileTimeKeeper.Api.Model;
using Agilosoft.AgileTimeKeeper.Api.Services;
using log4net;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly ILog _log = LogManager.GetLogger(Startup.repository.Name, typeof(AuthenticateController));
        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [Route("AuthenticateUser")]
        public IActionResult AuthenticateUser(User user)
        {
            Dictionary<string, string> dict = _authenticateService.Authenticate(user);
            if (user.Token == null)
            {
                return BadRequest(new { message = "Username or Password Incorrect" });
            }
            else
            {

                return new JsonResult(dict);
            }
        }
        [Route("RefreshToken")]
        public IActionResult RefreshToken(User user)
        {
            return Ok();
        }
    }
}
