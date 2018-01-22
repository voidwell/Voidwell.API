using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("authadmin")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public class AuthAdminController : Controller
    {
        private readonly IUserManagementClient _userManagementClient;
        private readonly ILogger<AccountController> _logger;

        public AuthAdminController(IUserManagementClient userManagementClient, ILogger<AccountController> logger)
        {
            _userManagementClient = userManagementClient;
            _logger = logger;
        }

        [HttpGet("users")]
        public Task<JToken> GetUsers()
        {
            return _userManagementClient.GetAllUsers();
        }

        [HttpDelete("user/{userId:guid}")]
        public Task<JToken> DeleteUser(Guid userId)
        {
            return _userManagementClient.DeleteUser(userId);
        }

        [HttpGet("user/{userId:guid}")]
        public Task<JToken> GetUser(Guid userId)
        {
            return _userManagementClient.GetUser(userId);
        }

        [HttpPut("user/{userId:guid}/roles")]
        public Task<JToken> UpdateUserRoles(Guid userId, [FromBody]JToken userRoles)
        {
            return _userManagementClient.UpdateUserRoles(userId, userRoles);
        }

        [HttpGet("roles")]
        public Task<JToken> GetRoles()
        {
            return _userManagementClient.GetAllRoles();
        }

        [HttpPost("role")]
        public Task<JToken> PostRole([FromBody]JToken roleRequest)
        {
            return _userManagementClient.CreateRole(roleRequest);
        }

        [HttpDelete("role/{roleId:guid}")]
        public Task<JToken> DeleteRole(Guid roleId)
        {
            return _userManagementClient.DeleteRole(roleId);
        }
    }
}
