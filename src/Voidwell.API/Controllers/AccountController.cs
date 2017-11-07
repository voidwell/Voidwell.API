using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAuthClient _authClient;

        public AccountController(IAuthClient authClient)
        {
            _authClient = authClient;
        }

        [HttpPost("register")]
        public Task<JToken> PostRegister([FromBody]JToken registerForm)
        {
            return _authClient.Register(registerForm);
        }

        [HttpPost("login")]
        public Task<JToken> PostLogin([FromBody]JToken loginForm)
        {
            return _authClient.Login(loginForm);
        }

        [HttpGet("questions")]
        public Task<JToken> GetSecurityQuestions()
        {
            return _authClient.GetSecurityQuestions();
        }
    }
}
