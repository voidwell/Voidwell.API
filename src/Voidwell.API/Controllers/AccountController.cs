using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserManagementClient _userManagementClient;

        public AccountController(IUserManagementClient userManagementClient)
        {
            _userManagementClient = userManagementClient;
        }

        [HttpGet("questions")]
        public Task<JToken> GetSecurityQuestions()
        {
            return _userManagementClient.GetSecurityQuestions();
        }

        [HttpPost("register")]
        public Task PostRegister([FromBody]JToken registerForm)
        {
            return _userManagementClient.Register(registerForm);
        }

        [HttpGet("roles")]
        public Task<IEnumerable<string>> GetUserRoles()
        {
            return _userManagementClient.GetRoles();
        }

        [HttpPost("resetpasswordstart")]
        public Task<JToken> PostResetPasswordStart([FromBody]JToken passwordResetStart)
        {
            return _userManagementClient.ResetPasswordStart(passwordResetStart);
        }

        [HttpPost("resetpasswordquestions")]
        public Task<JToken> PostResetPasswordQuestions([FromBody]JToken passwordResetQuestions)
        {
            return _userManagementClient.ResetPasswordQuestions(passwordResetQuestions);
        }

        [HttpPost("resetpassword")]
        public Task PostResetPassword([FromBody]JToken passwordResetRequest)
        {
            return _userManagementClient.ResetPassword(passwordResetRequest);
        }

        [HttpPost("changepassword")]
        [Authorize(Roles = "User")]
        public Task<JToken> PostChangePassword([FromBody]JToken passwordChangeForm)
        {
            return _userManagementClient.ChangePassword(passwordChangeForm);
        }
    }
}
