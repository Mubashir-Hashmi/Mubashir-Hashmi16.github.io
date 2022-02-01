using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Security.Cryptography;
using Agilosoft.AgileTimeKeeper.Api.Services;
using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Newtonsoft.Json.Linq;
using Agilosoft.AgileTimeKeeper.Api.Business_Exceptions;
using Agilosoft.AgileTimeKeeper.Api.Errors;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private byte[] _hashPassword;
        private ExecutorController _executor;
        private IRandomPasswordGenerator _randomPassword;
        static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(UserController));
        private IHubContext<NotificationHub> _hub;
        private static JObject _responseConstant;

        public UserController(ExecutorController executorController, IHubContext<NotificationHub> hub, IRandomPasswordGenerator randomPassword)
        { 
            _randomPassword = randomPassword;
            _executor = executorController;
            _hub = hub;
            _responseConstant=JObject.Parse(System.IO.File.ReadAllText(@"Errors/Constant.json"));
        }
        //[Route("AuthenticateUser")]
        //public JsonResult AuthenticateUser(User user)
        //{
        //    if (user.LoginType == 1)
        //    {
        //        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        //        {
        //            UTF8Encoding utf = new UTF8Encoding();
        //            _hashPassword = md5.ComputeHash(utf.GetBytes(user.UserPassword));
        //            user.UserPassword = Convert.ToBase64String(_hashPassword);
        //        }
        //    }
        //    Object[] parameterArray =
        //    {
        //        "@vcUserEmail", user.UserEmail,
        //        "@vcUserPassword", user.UserPassword,
        //        "@iLoginType", user.LoginType
        //    };
        //    DataTable result = (_executor.ExecuteQuery("usp_Validate_User", parameterArray, log));
        //    return new JsonResult(result);
        //}
        [Authorize]
        [Route("GetUserById")]
        public JsonResult GetUserById(User user)
        {
            ResponseJson responseJson = new ResponseJson();
            Object[] parameterArray =
            {
                "@vcUserEmail",user.UserEmail
            };
            responseJson.Data = _executor.ExecuteQuery("usp_Get_UserById", parameterArray, log);
            if (responseJson.Data.Rows.Count != 0)
            {
                responseJson.StatusCode = (int)Constant.StatusCode.OK;
                var data=(JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            else
            {
                responseJson.StatusCode = (int)Constant.StatusCode.NotFound;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            return new JsonResult(responseJson);
        }
        [Authorize]
        [Route("GetUserInformation")]
        public JsonResult GetUserInformation(User user)
        {
            Object[] parameterArray =
            {
                "@vcUserEmail", user.UserEmail,
                "@iWorkspaceId", user.WorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_UserInformation", parameterArray, log));

            return new JsonResult(result);
        }

        [Route("InsertUser")]
        public JsonResult InsertUser(User user)
        {
            ResponseJson responseJson = new ResponseJson();
            if (user.UserPassword == null)
            {
                user.UserPassword = "";
            }
            string systemPassword = _randomPassword.PasswordGenerate();
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf = new UTF8Encoding();
                _hashPassword = md5.ComputeHash(utf.GetBytes(user.UserPassword));
                user.UserPassword = Convert.ToBase64String(_hashPassword);
            }
            Object[] parameterArray =
            {
                "@vcUserFirstName", user.UserFirstName,
                "@vcUserLastName", user.UserLastName,
                "@vcUserEmail", user.UserEmail,
                "@vcSystemPassword",systemPassword,
                "@vcUserPassword",user.UserPassword,
                "@ifkUserOrganizationId", user.fkUserOrganizationId,
                "@ifkDesignationId",user.fkDesignationId,
                "@iUserRoleId",user.UserRoleId,
                "@vcUserAddress",user.UserAddress,
                "@vcUserCity", user.UserCity,
                "@vcUserState",user.UserState,
                "@vcUserCountry", user.UserCountry,
                "@vcUserMobileNo", user.UserMobileNo,
                "@iUserTimeEntryLockTime",user.UserTimeEntryLockTime,
                "@ifkWorkspaceId", user.WorkspaceId
            };
            responseJson.Data = (_executor.ExecuteQuery("usp_Ins_User", parameterArray, log));
            if (responseJson.Data.Rows.Count != 0)
            {
                responseJson.StatusCode = (int)Constant.StatusCode.Created;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            else
            {
                responseJson.StatusCode = (int)Constant.StatusCode.BadRequest;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            return new JsonResult(responseJson);
        }
        [Authorize]
        [Route("UpdateUser")]
        public JsonResult UpdateUser(User user)
        {
            ResponseJson responseJson = new ResponseJson();
            Object[] parameterArray =
            {
                "@vcUserEmail",user.UserEmail,
                "@vcUserFirstName", user.UserFirstName,
                "@vcUserLastName", user.UserLastName,
                "@vcUserAddress",user.UserAddress,
                "@vcUserCity", user.UserCity,
                "@vcUserState",user.UserState,
                "@vcUserCountry", user.UserCountry,
                "@vcUserMobileNo", user.UserMobileNo,
            };
            responseJson.Data = (_executor.ExecuteQuery("usp_Upd_User", parameterArray, log));
            if (responseJson.Data.Rows.Count != 0)
            {
                responseJson.StatusCode = (int)Constant.StatusCode.Created;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            else
            {
                responseJson.StatusCode = (int)Constant.StatusCode.BadRequest;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            return new JsonResult(responseJson);
        }
        [Route("DeleteUSer")]
        public JsonResult DeleteUser(User user)
        {
            ResponseJson responseJson = new ResponseJson();
            Object[] parameterArray =
            {
                "@vcUserEmail",user.UserEmail
            };
            responseJson.Data = (_executor.ExecuteQuery("usp_Del_User", parameterArray, log));
            if (responseJson.Data.Rows.Count != 0)
            {
                responseJson.StatusCode = (int)Constant.StatusCode.NoContent;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            else
            {
                responseJson.StatusCode = (int)Constant.StatusCode.BadRequest;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            return new JsonResult(responseJson);
        }
        [Authorize]
        [Route("GetCountOfAllUserWRTWorkspace")]
        public JsonResult GetCountOfAllUserWRTWorkspace(User user)
        {
            Object[] parameterArray =
            {
                "@iWorkspaceId", user.WorkspaceId
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_Count_AllUserWRTWorkspace", parameterArray, log));

            return new JsonResult(result);
        }
        [Authorize]
        [Route("GetAllRecordOfUserWRTWorkspace")]
        public JsonResult GetAllRecordOfUserWRTWorkspace(User user)
        {
            ResponseJson responseJson = new ResponseJson();
            Object[] parameterArray =
            {
                "@iWorkspaceId", user.WorkspaceId,
                "@iPageNumber", user.PageNumber,
                "@iRecordType", user.RecordType,
                "@iPageSize", user.PageSize,
                "@iOrderBy", user.OrderBy
            };
            DataTable result = (_executor.ExecuteQuery("usp_Get_AllUserWRTWorkspace", parameterArray, log));
            foreach(DataRow dr in result.Rows)
            {
                DataTable dt = GetUserProjectRoleWRTWorkspace(dr["Email"].ToString(),user.WorkspaceId);
                if (dt.Rows.Count != 0) { 
                    foreach(DataRow dr2 in dt.Rows)
                    {
                        dr["Role"]+= ", "+dr2["ProjectRoleName"].ToString();
                    }
                }
            }
            responseJson.Data = result;
            if (responseJson.Data.Rows.Count != 0)
            {
                responseJson.StatusCode = (int)Constant.StatusCode.OK;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            else
            {
                responseJson.StatusCode = (int)Constant.StatusCode.NotFound;
                var data = (JObject)_responseConstant.GetValue(responseJson.StatusCode.ToString());
                responseJson.Message = (string)data.GetValue("Message");
            }
            return new JsonResult(responseJson);
        }
        [Authorize]
        [Route("CheckIfUserExist")]
        public JsonResult CheckIfUserExist(User user)
        {
            Object[] parameterArray =
            {
                "@vcUserEmail", user.UserEmail
            };
            DataTable result = (_executor.ExecuteQuery("usp_Chk_IfUserExist", parameterArray, log));
            
            return new JsonResult(result);
        }
        [Authorize]
        [Route("GetRole")]
        public JsonResult GetRole(User user)
        {
            Object[] parameterArray =
            {
                "@vcUserEmail", user.UserEmail
            };
            DataTable result = (_executor.ExecuteQuery("usp_CHK_UserRole", log));

            return new JsonResult(result);
        }
        public DataTable GetUserProjectRoleWRTWorkspace(string email,int workspaceID )
        {
            Object[] parameterArray =
            {
                "@vcUserEmail",email,
                "@iWorkspaceId",workspaceID
            };
            return  _executor.ExecuteQuery("usp_Get_UserRolesWRTProject", parameterArray, log);
        }

    }
}
