using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Voidwell.API.HttpDelegatedClient;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class InternalClient : IInternalClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _delegatedHttpClient;

        public InternalClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory,
            IDelegatedHttpClientFactory delegatedHttpClientFactory)
        {
            var baseAddress = new Uri(Constants.Endpoints.VoidwellBlog);

            _httpClient = authenticatedHttpClientFactory.GetHttpClient();
            _httpClient.BaseAddress = baseAddress;

            _delegatedHttpClient = delegatedHttpClientFactory.GetHttpClient("voidwell-internal");
            _delegatedHttpClient.BaseAddress = baseAddress;
        }

        public async Task<JToken> GetAllBlogPosts()
        {
            var result = await _httpClient.GetAsync("blog");
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetBlogPost(string blogPostId)
        {
            var result = await _httpClient.GetAsync($"blog/{blogPostId}");
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateBlogPost(JToken requestContent)
        {
            var content = JsonContent.FromObject(requestContent);
            var result = await _delegatedHttpClient.PostAsync("blog", content);
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> UpdateBlogPost(string blogPostId, JToken requestContent)
        {
            var content = JsonContent.FromObject(requestContent);
            var result = await _delegatedHttpClient.PutAsync($"blog/{blogPostId}", content);
            return await result.GetContentAsync<JToken>();
        }

        public Task DeleteBlogPost(string blogPostId)
        {
            return _delegatedHttpClient.DeleteAsync($"blog/{blogPostId}");
        }

        public async Task<JToken> GetAllEvents()
        {
            var result = await _httpClient.GetAsync("gameevent");
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetAllEventsByGame(string gameId)
        {
            var result = await _httpClient.GetAsync($"gameevent/game/{gameId}");
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> GetEvent(string eventId)
        {
            var result = await _httpClient.GetAsync($"gameevent/{eventId}");
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> UpdateEvent(string eventId, JToken requestContent)
        {
            var content = JsonContent.FromObject(requestContent);
            var result = await _delegatedHttpClient.PutAsync($"gameevent/{eventId}", content);
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> CreateEvent(JToken requestContent)
        {
            var content = JsonContent.FromObject(requestContent);
            var result = await _delegatedHttpClient.PostAsync($"gameevent", content);
            return await result.GetContentAsync<JToken>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
