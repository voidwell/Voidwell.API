using System;
using System.Net.Http;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.Clients
{
    public class DaybreakGamesPS4USClient : DaybreakGamesClient, IDaybreakGamesPS4USClient
    {
        private readonly HttpClient _httpClient;

        public DaybreakGamesPS4USClient(IAuthenticatedHttpClientFactory authenticatedHttpClientFactory)
            : base(authenticatedHttpClientFactory)
        {
            _httpClient.BaseAddress = new Uri(Constants.Endpoints.DaybreakGamesPs4Us);
        }
    }
}