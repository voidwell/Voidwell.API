using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Voidwell.API.HttpDelegatedClient;

namespace Voidwell.API.Clients
{
    public class AuthClient : IAuthClient, IDisposable
    {
        private readonly HttpClient _httpClient;

        public AuthClient(IDelegatedHttpClientFactory delegatedHttpClientFactory)
        {
            _httpClient = delegatedHttpClientFactory.GetHttpClient("voidwell-auth-admin");
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.VoidwellAuth);
        }

        public async Task<JToken> GetAllClients()
        {
            var response = await _httpClient.GetAsync("admin/client");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetClientById(string clientId)
        {
            var response = await _httpClient.GetAsync($"admin/client/{clientId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateClient(JToken client)
        {
            var content = JsonContent.FromObject(client);
            var response = await _httpClient.PostAsync("admin/client", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> UpdateClient(string clientId, JToken client)
        {
            var content = JsonContent.FromObject(client);
            var response = await _httpClient.PutAsync($"admin/client/{clientId}", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateClientSecret(string clientId, JToken secret)
        {
            var content = JsonContent.FromObject(secret);
            var response = await _httpClient.PostAsync($"admin/client/{clientId}/secret", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> DeleteClientSecret(string clientId, string requestQuery)
        {
            var response = await _httpClient.DeleteAsync($"admin/client/{clientId}/secret{requestQuery}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllApiResources()
        {
            var response = await _httpClient.GetAsync("admin/resource");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetApiResourceById(string apiResourceId)
        {
            var response = await _httpClient.GetAsync($"admin/resource/{apiResourceId}");
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateApiResource(JToken apiResource)
        {
            var content = JsonContent.FromObject(apiResource);
            var response = await _httpClient.PostAsync("admin/resource", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> UpdateApiResource(string apiResourceId, JToken apiResource)
        {
            var content = JsonContent.FromObject(apiResource);
            var response = await _httpClient.PutAsync($"admin/resource/{apiResourceId}", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateApiResourceSecret(string apiResourceId, JToken secret)
        {
            var content = JsonContent.FromObject(secret);
            var response = await _httpClient.PostAsync($"admin/resource/{apiResourceId}/secret", content);
            return await response.GetContentAsync<JToken>();
        }

        public async Task<JToken> DeleteApiResourceSecret(string apiResourceId, string queryString)
        {
            var response = await _httpClient.DeleteAsync($"admin/resource/{apiResourceId}/secret{queryString}");
            return await response.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
