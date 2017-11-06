using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Voidwell.API.Clients
{
    public class VoidwellClient : IVoidwellClient, IDisposable
    {
        private readonly HttpClient _httpClient;

        public VoidwellClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.Voidwell);
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
            var result = await _httpClient.PostAsync("blog", content);
            return await result.GetContentAsync<JToken>();
        }

        public async Task<JToken> UpdateBlogPost(JToken requestContent)
        {
            var content = JsonContent.FromObject(requestContent);
            var result = await _httpClient.PutAsync("blog", content);
            return await result.GetContentAsync<JToken>();
        }

        public Task DeleteBlogPost(string blogPostId)
        {
            return _httpClient.DeleteAsync($"blog/{blogPostId}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
