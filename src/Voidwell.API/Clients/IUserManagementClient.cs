using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Voidwell.API.Clients
{
    public interface IUserManagementClient
    {
        Task Register(JToken registrationForm);
        Task<IEnumerable<string>> GetRoles();
        Task<JToken> GetSecurityQuestions();
        Task<JToken> GetAllUsers();
        Task<JToken> GetAllRoles();
        Task<JToken> DeleteUser(Guid userId);
        Task<JToken> GetUser(Guid userId);
        Task<JToken> CreateRole(JToken roleRequest);
        Task<JToken> DeleteRole(Guid roleId);
        Task<JToken> ChangePassword(JToken passwordChange);
        Task<JToken> ResetPasswordStart(JToken passwordResetStart);
        Task<JToken> ResetPasswordQuestions(JToken passwordResetQuestions);
        Task ResetPassword(JToken passwordResetRequest);
    }
}
