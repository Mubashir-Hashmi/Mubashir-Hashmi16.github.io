using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private ExecutorController _executor;
        private IHubContext<NotificationHub> _hub;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ReportController));

        public OrganizationController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }
        [Route("GetAllOrganizations")]
        public JsonResult GetAllOrganizations()
        {
            DataTable result = _executor.ExecuteQuery("usp_Get_AllOrganizations", log);
            return new JsonResult(result);
        }
        [Route("CheckLoginType")]
        public JsonResult CheckLoginType(Organization organization)
        {
            Object[] parameterArray =
            {
                "@OrganizationName", organization.OrganizationName,
            };
            DataTable result = (_executor.ExecuteQuery("usp_CheckOrganizationLoginType", parameterArray, log));

            return new JsonResult(result);
        }

    }
}
