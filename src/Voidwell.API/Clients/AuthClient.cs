using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Voidwell.API.Clients
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.VoidwellAuth);
        }

        public async Task<JToken> Register(JToken registrationForm)
        {
            var content = JsonContent.FromObject(registrationForm);
            var response = await _httpClient.PostAsync("account/register", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> Login(JToken loginForm)
        {
            var content = JsonContent.FromObject(loginForm);
            var response = await _httpClient.PostAsync("account/login", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetSecurityQuestions()
        {
            var response = await _httpClient.GetAsync("account/questions");
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
