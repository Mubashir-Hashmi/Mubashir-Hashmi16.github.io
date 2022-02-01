using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private ExecutorController _executor;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(DesignationController));
        private IHubContext<NotificationHub> _hub;
        public DesignationController(ExecutorController executorController, IHubContext<NotificationHub> hub)
        {
            _executor = executorController;
            _hub = hub;
        }
        [Route("GetAllDesignations")]
        public JsonResult GetAllDesignations()
        {
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllDesignations", log));
            return new JsonResult(result);
        }
    }
    
}
