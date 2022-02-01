using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Agilosoft.AgileTimeKeeper.Api.Services;
using log4net;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectRoleController : ControllerBase
    {
        private ExecutorController _executor;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ProjectRoleController));
        public ProjectRoleController(ExecutorController executor)
        {
            _executor = executor;
        }
        [Route("GetAllProjectRole")]
        public JsonResult GetAllProjectRole()
        {
            DataTable result = _executor.ExecuteQuery("usp_Get_AllProjectRole", log);
            return new JsonResult(result);
        }
    }
}
