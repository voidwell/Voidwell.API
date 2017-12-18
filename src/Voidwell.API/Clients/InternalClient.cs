﻿using System;
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
            var baseAddress = new Uri(Constants.Endpoints.Voidwell);

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

        public async Task<JToken> UpdateBlogPost(JToken requestContent)
        {
            var content = JsonContent.FromObject(requestContent);
            var result = await _delegatedHttpClient.PutAsync("blog", content);
            return await result.GetContentAsync<JToken>();
        }

        public Task DeleteBlogPost(string blogPostId)
        {
            return _delegatedHttpClient.DeleteAsync($"blog/{blogPostId}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}