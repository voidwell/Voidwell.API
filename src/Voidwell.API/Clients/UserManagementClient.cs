using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Voidwell.API.HttpAuthenticatedClient;
using Voidwell.API.HttpDelegatedClient;
using System.Collections.Generic;

namespace Voidwell.API.Clients
{
    public class UserManagementClient : IUserManagementClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _delegatedHttpClient;

        public UserManagementClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory,
            IDelegatedHttpClientFactory delegatedHttpClientFactory)
        {
            var baseAddress = new Uri("http://voidwellusermanagement:5000");

            _httpClient = authenticatedHttpClientFactory.GetHttpClient();
            _httpClient.BaseAddress = baseAddress;

            _delegatedHttpClient = delegatedHttpClientFactory.GetHttpClient("voidwell-usermanagement");
            _delegatedHttpClient.BaseAddress = baseAddress;
        }

        public async Task Register(JToken registrationForm)
        {
            var content = JsonContent.FromObject(registrationForm);
            var response = await _httpClient.PostAsync("register", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            var response = await _delegatedHttpClient.GetAsync("roles");
            return await response.GetContentAsync<IEnumerable<string>>();
        }

        public async Task<JToken> GetSecurityQuestions()
        {
            var response = await _httpClient.GetAsync("questions");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetUser(Guid userId)
        {
            var response = await _httpClient.GetAsync($"user/{userId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("user/all");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> DeleteUser(Guid userId)
        {
            var response = await _httpClient.DeleteAsync($"user/{userId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllRoles()
        {
            var response = await _httpClient.GetAsync("roles/all");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateRole(JToken roleRequest)
        {
            var content = JsonContent.FromObject(roleRequest);
            var response = await _httpClient.PostAsync("roles", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> DeleteRole(Guid roleId)
        {
            var response = await _httpClient.DeleteAsync($"roles/{roleId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> ChangePassword(JToken passwordChange)
        {
            var content = JsonContent.FromObject(passwordChange);
            var response = await _delegatedHttpClient.PostAsync("password", content);
            var res = await response.Content.ReadAsStringAsync();
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> ResetPasswordStart(JToken passwordResetStart)
        {
            var content = JsonContent.FromObject(passwordResetStart);
            var response = await _httpClient.PostAsync("resetpassword/start", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> ResetPasswordQuestions(JToken passwordResetQuestions)
        {
            var content = JsonContent.FromObject(passwordResetQuestions);
            var response = await _httpClient.PostAsync("resetpassword/questions", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task ResetPassword(JToken passwordResetRequest)
        {
            var content = JsonContent.FromObject(passwordResetRequest);
            var response = await _httpClient.PostAsync("resetpassword", content);
            await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> UpdateUserRoles(Guid userId, JToken userRoles)
        {
            var content = JsonContent.FromObject(userRoles);
            var response = await _httpClient.PutAsync($"user/{userId}/roles", content);
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
