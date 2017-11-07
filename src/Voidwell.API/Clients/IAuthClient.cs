using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Voidwell.API.Clients
{
    public interface IAuthClient
    {
        Task<JToken> Register(JToken registrationForm);
        Task<JToken> Login(JToken loginForm);
        Task<JToken> GetSecurityQuestions();
    }
}
